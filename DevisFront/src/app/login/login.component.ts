import { Component, OnInit } from '@angular/core';
import { Router,ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { AlertService } from '../_services/alert.service';
import { AuthenticationService } from '../_services/authentication.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  returnUrl: string;

  constructor(public router: Router, public http: Http,private route: ActivatedRoute,private alertService: AlertService, private authenticationService: AuthenticationService) {

  }

  ngOnInit() {
    // reset login status
    this.logout();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    console.log(this.returnUrl);
  }


  login(event, username: string, password : string) {
    event.preventDefault();
    this.authenticationService.login(username, password)
      .subscribe(
        data => {
          let user = data;
          if (user && user.Token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            this.alertService.success('Registration successful', true);
            this.router.navigate([this.returnUrl]);
          }
          else {
            this.alertService.error('Utilisateur ou mot de passe incorrect', false);
          }

        },
        error => {
          console.log('oulalala c pas bon');
          this.alertService.error('Problème interne, veuillez contacter l\'administrateur', false);
        });
  }

  logout() {
    // remove user from local storage to log user out
    if (localStorage.getItem('currentUser')) {
      this.alertService.success('Utilisateur déconnecter', true);
    }
    localStorage.removeItem('currentUser');
  }

}
