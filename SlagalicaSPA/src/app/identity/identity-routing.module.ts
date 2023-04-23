import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginFormComponent } from './feature-autentication/login-form/login-form.component';
import { RegisterFormComponent } from './feature-autentication/register-form/register-form.component';
import { IdentityComponent } from './identity.component';
import { UserProfileComponent } from './feature-autentication/user-profile/user-profile.component';
import { LogoutComponent } from './feature-autentication/logout/logout.component';

const routes: Routes = [{ path: '', component: IdentityComponent , children: [{path: 'login', component: LoginFormComponent}]},
  {path: 'register', component: RegisterFormComponent},
  {path: 'profile', component: UserProfileComponent},
  {path: 'logout', component: LogoutComponent}, 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IdentityRoutingModule { }
