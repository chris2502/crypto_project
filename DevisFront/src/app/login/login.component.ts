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
        response => {

          console.log(response)

          if (response["Code"] == 200) {
            this.alertService.success('Authentification correcte, vérifiez votre boîte mail', true);
          }
          else{
            this.alertService.error('Utilisateur ou mot de passe incorrect', false);
          }

        },
        error => {
          console.log('oulalala c pas bon');
          this.alertService.error('Problème interne, veuillez contacter l\'administrateur', false);
        });
  }

  logout() {
    let user = localStorage.getItem('currentUser');
    if (user) {
      this.authenticationService.logout(JSON.parse(user).Token).subscribe(
        data => {
          localStorage.removeItem('currentUser');
          this.alertService.success('Utilisateur déconnecté', true);
        },
        error => {
          this.alertService.error(error);
        });

    }


  }

}
