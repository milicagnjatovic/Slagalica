import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IdentityRoutingModule } from './identity-routing.module';
import { IdentityComponent } from './identity.component';
import { LoginFormComponent } from './feature-autentication/login-form/login-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterFormComponent } from './feature-autentication/register-form/register-form.component';
import { UserProfileComponent } from './feature-autentication/user-profile/user-profile.component';
import { LogoutComponent } from './feature-autentication/logout/logout.component';


@NgModule({
  declarations: [
    IdentityComponent,
    LoginFormComponent,
    RegisterFormComponent,
    UserProfileComponent,
    LogoutComponent
  ],
  imports: [
    CommonModule,
    IdentityRoutingModule,
    ReactiveFormsModule
  ]
})
export class IdentityModule { }
