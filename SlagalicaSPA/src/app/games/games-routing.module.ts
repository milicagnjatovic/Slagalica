import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GamesComponent } from './games.component';
import { WhoKnowsComponent } from './who-knows/who-knows.component';

const routes: Routes = [{ path: '', component: GamesComponent ,
  children: [
    { path: 'who-knows', component: WhoKnowsComponent }
  ]}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GamesRoutingModule { }
