import { Component, OnInit } from '@angular/core';
import {AlertService} from "../_services/alert.service";
import {AuthenticationService} from "../_services/authentication.service";
import {Router} from "@angular/router";

import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { UserCreate } from '../_interfaces/userCreate.interface';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
  export class RegisterComponent implements OnInit {
    model: any = {};
    loading = true;

    public userCreate: UserCreate;

      ngOnInit() {
        this.userCreate = {
          username: '',
          password: '',
          passwordConfirm: ''
        }
      }

  constructor(private alertService: AlertService, private authenticationService: AuthenticationService,private router: Router) { }



  checkPassword(passwordConfirm:string,password:string){
    if(!(passwordConfirm==password) ){
      console.log('mot de passe non égaux');
      this.loading = true;
      return true;
    }
    else {
      console.log('mot de passe  égaux');
      this.loading = false;
      return false;
    }
  }

  register(username:string,password:string) {
    console.log("Formulaire validé !!!!: "+ username + ' '+ password);

    this.authenticationService.register(username,password)
        .subscribe(
            data => {
                this.alertService.success('Registration successful', true);
                this.router.navigate(['/login']);
            },
            error => {
                this.alertService.error(error);

            });
  }

}
