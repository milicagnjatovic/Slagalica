import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{ path: '', //'identity', 
loadChildren: () => import('./identity/identity.module').then(m => m.IdentityModule) },
{ path: 'play', loadChildren: () => import('./games/games.module').then(m => m.GamesModule) }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
