import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginFormComponent } from './feature-autentication/login-form/login-form.component';
import { IdentityComponent } from './identity.component';

const routes: Routes = [{ path: '', component: IdentityComponent , children: [
  {path: 'login', component: LoginFormComponent}
]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IdentityRoutingModule { }
