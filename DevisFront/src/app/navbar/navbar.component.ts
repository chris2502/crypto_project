import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  showLogout:boolean = false;
  user:string;
  constructor() { }

  ngOnInit() {
    this.checkSession();
    if (localStorage.getItem('currentUser')) {
      this.user = JSON.parse(localStorage.getItem('currentUser')).Email;
      console.log(this.user);
    }
  }

  checkSession():boolean {
    var checkKey = localStorage.getItem('currentUser');
    if(checkKey == null){
      this.showLogout = true; // changed
      console.log('null key: oue');
      return this.showLogout;
    } else {
      this.showLogout = false; // changed
      //this check will only be available once the user is logged in
      console.log('key exist: non ');
      return this.showLogout;
    }
  }

}
