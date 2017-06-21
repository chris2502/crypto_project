import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

import 'rxjs/Rx';
import { User } from '../_models/user';
import { Observable } from 'rxjs/Observable'



@Injectable()
export class UserService {
  //private headers = new Headers({'Content-Type': 'application/json'});
  //private userUrl = 'http://www.mocky.io/v2/58ed2e050f0000e801787cb2';
  private userUrl = 'http://127.0.0.1:9000/user/';

  constructor(private http: Http){}

  public getUsers(): Observable<Array<User>> {

    console.log(this.userUrl+this.jwt()+'.json');
    return this.http.get(this.userUrl+this.jwt())
      .map( (res: Response) => res.json())
      .do( res => console.log('HTTP response:', res))
      .catch( (error: any) => Observable.throw( error.json().error || 'Server error' ) );

  }


  private jwt() {
    // create authorization header with jwt token
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser && currentUser.Token) {
      let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
      return new RequestOptions({ headers: headers });
    }
  }





}
