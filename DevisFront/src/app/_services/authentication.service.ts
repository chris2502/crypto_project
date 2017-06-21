import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { backend } from '../config/backend'

import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable'

@Injectable()
export class AuthenticationService {
  constructor(private http: Http) { }

  login(username: string, password: string) {

    return this.http.get(backend.url + '/Auth/'+username+'/'+password)
      .map((res: Response) => res.json())
     // .do( res => console.log('HTTP response :', res))
      .catch( (error: any) => Observable.throw( error.json().error || 'Server error during login' ) );
    ;
  }

  register(email: string, password: string) {
    return this.http.post(backend.url + '/Auth/Signup/'+email+'/'+password, '')
      .map((response: Response) => response.json())
      .catch( (error: any) => Observable.throw (error.json().error || 'Server error during register'));
  }

}
