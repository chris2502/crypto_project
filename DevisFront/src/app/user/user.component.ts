import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params }   from '@angular/router';
import { Location }                 from '@angular/common';


import {User} from '../_models/user';
import {UserService} from '../_services/user.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent  implements OnInit
{

  title = "Liste des utilisateurs";
  users : User[];

  constructor(private userService : UserService,private route: ActivatedRoute,
              private location: Location){}

  ngOnInit():void {
    this.userService.getUsers().subscribe(
      (data: User[]) => this.users = data,
      (error) => console.log(error)
    );
  }



}
