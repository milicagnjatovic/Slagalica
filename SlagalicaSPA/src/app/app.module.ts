import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationInterceptor } from './shared/interceptors/authentication.interceptor';
import { WhoKnowsComponent } from './games/who-knows/who-knows.component';

@NgModule({
  declarations: [
    AppComponent,
    WhoKnowsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
    ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true} 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
