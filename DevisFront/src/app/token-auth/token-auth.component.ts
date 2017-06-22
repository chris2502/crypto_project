import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from "@angular/router";
import {AuthenticationService} from "../_services/authentication.service";

@Component({
  selector: 'app-token-auth',
  templateUrl: './token-auth.component.html',
  styleUrls: ['./token-auth.component.css']
})
export class TokenAuthComponent implements OnInit {

  private mail: string;
  private token: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) {
      this.route.params.subscribe(params => {
        this.mail = params["mail"]
        this.token = params["token"]
      })
  }

  ngOnInit() {
    this.authAndRedirect(this.mail, this.token);
  }

  private authAndRedirect(mail: string, token: string) {
    this.authenticationService.authent(mail, token)
      .subscribe( data => {
        if (data && data.Token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(data));
        }

        this.router.navigate(['']);

      })
  }

}
