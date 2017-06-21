import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './common/auth.guard';

import { UserComponent } from './user/user.component';
import {LoginComponent} from './login/login.component';
import {HomeComponent} from './home/home.component';
import {RegisterComponent} from "./register/register.component";
import {DevisClassicComponent} from "./devis-classic/devis-classic.component";

const appRoutes: Routes = [
  { path: '',  component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'login',  component: LoginComponent },
  { path: 'user',  component: UserComponent , canActivate: [AuthGuard]},
  { path: 'register', component: RegisterComponent },
  { path: 'devisClassic', component: DevisClassicComponent },
  // otherwise redirect to home
  { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
