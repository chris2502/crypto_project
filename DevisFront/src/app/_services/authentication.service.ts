import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { backend } from '../config/backend'

import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable'

let TAILLE_BLOC = 4;

@Injectable()
export class AuthenticationService {
  constructor(private http: Http) { }

  randomIntFromInterval(min, max)
  {
    return Math.floor(Math.random()*(max-min+1)+min);
  }

  exclusiveOR(x: Uint8Array, y: Uint8Array): Uint8Array {
    var result = new Uint8Array(x.length);
    for (var i = 0; i < x.length; i++) {
      result[i] = x[i] ^ y[i];
    }
    return result;
  }


  chiffrer(uneCle: Uint8Array, uneSource: Uint8Array, ) {
    var aChiffrer = new Array<Uint8Array>(uneSource.length / TAILLE_BLOC);
    var chiffre /* chiffré */ = Array<Uint8Array>(aChiffrer.length);
    var destination = new Uint8Array(uneSource.length + TAILLE_BLOC);

    if (uneSource.length % TAILLE_BLOC != 0)
    {
      throw "Le tableau source n'a pas une taille valide. Spécifiez la taille des blocs et réessayez.";
    }

    var iv = new Uint8Array(TAILLE_BLOC);

    // Initialisation du vecteur d'initialisation
    for (var i = 0; i < iv.length; i++)
    {
      iv[i] = this.randomIntFromInterval(0, 255);
      destination[uneSource.length + i] = iv[i];
    }

    // Création du tableau de blocs à partir du fichier
    for (var j = 0; j < aChiffrer.length; j++)
    {
      aChiffrer[j] = new Uint8Array(TAILLE_BLOC);
      for (var i = 0; i < TAILLE_BLOC; i++)
      {
        aChiffrer[j][i] = uneSource[j * TAILLE_BLOC + i];
      }
    }

    // Chiffrement
    for (var numBloc = 0; numBloc < aChiffrer.length; numBloc++)
    {
      // 1ère phase
      var result1 = new Uint8Array(TAILLE_BLOC);
      result1 = this.exclusiveOR(iv, aChiffrer[numBloc]);
      // 2ème phase
      var result2 = this.exclusiveOR(result1, uneCle);
      chiffre[numBloc] = new Uint8Array(TAILLE_BLOC);
      for (var i = 0; i < TAILLE_BLOC; i++)
      {
        chiffre[numBloc][i] = result2[i];
        iv[i] = result2[i];
      }
    }

    // Création du fichier de destination à partir du tableau de blocs
    for (var j = 0; j < chiffre.length; j++)
    {
      for (var i = 0; i < TAILLE_BLOC; i++)
      {
        destination[j * TAILLE_BLOC + i] = chiffre[j][i];
      }
    }

    return destination;
  }

  stringToByteArray(source: string) : Uint8Array {
    var resultSize = source.length + (TAILLE_BLOC-source.length%TAILLE_BLOC);
    var result = new Uint8Array(resultSize);
    for(var i = 0 ; i < resultSize ; i++) {
      if(i<source.length) {
        result[i] = source.charCodeAt(i);
      }
      else {
        result[i] = 0;
      }
    }

    return result;
  }

  login(username: string, password: string) {

    var uneCle = new Uint8Array(TAILLE_BLOC);

    uneCle[0] = 55;
    uneCle[1] = 66;
    uneCle[2] = 77;
    uneCle[3] = 88;

    return this.http.get(backend.url + '/Auth/'+username+'/'+ this.chiffrer(uneCle, this.stringToByteArray(password)))
      .map((res: Response) => res.json())
     // .do( res => console.log('HTTP response :', res))
      .catch( (error: any) => Observable.throw( error.json().error || 'Server error during login' ) );
    ;
  }

  register(email: string, password: string) {
    return this.http.post(backend.url + '/Auth/Signup/'+email+'/'+password, '')  // To change
      .map((response: Response) => response.json())
      .catch( (error: any) => Observable.throw (error.json().error || 'Server error during register'));
  }

}
