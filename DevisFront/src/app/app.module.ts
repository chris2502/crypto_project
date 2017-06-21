import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';


import { routing } from './app.routing';
import { AuthGuard } from './common/auth.guard';

import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import {UserService} from './_services/user.service';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AlertComponent } from './_directives/alert.component';
import { AlertService } from './_services/alert.service';
import { AuthenticationService } from './_services/authentication.service';
import { RegisterComponent } from './register/register.component';
import { DevisClassicComponent } from './devis-classic/devis-classic.component';

import { Ng2TableModule } from 'ng2-table/ng2-table';


import { PaginationModule } from 'ngx-bootstrap';
import { TabsModule } from 'ngx-bootstrap';




@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    NavbarComponent,
    LoginComponent,
    HomeComponent,
    AlertComponent,
    RegisterComponent,
    DevisClassicComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    routing,
    Ng2TableModule,
    PaginationModule.forRoot(),
    TabsModule.forRoot()
  ],
  providers: [UserService, AuthGuard , AlertService, AuthenticationService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
