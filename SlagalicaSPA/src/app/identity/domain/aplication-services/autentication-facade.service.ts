import { Injectable } from '@angular/core';
import { Observable, map, catchError, of } from 'rxjs';
import { AutenticationService } from '../infrastructure/autentication.service';
import { ILoginRequest } from '../model/login-request';
import { ILoginResponse } from '../model/login-response';

@Injectable({
  providedIn: 'root'
})
export class AutenticationFacadeService {

  constructor(private autenticationService: AutenticationService){ }

  public login(username: string, password: string) : Observable<boolean> {
    const request : ILoginRequest = {username, password};
    return this.autenticationService.login(request).pipe(
      map( (loginResponse: ILoginResponse) => { 
        console.log(loginResponse);
        return true;
      }),
      catchError((err) => {
        console.log(err);
        return of(false);
      })
    );
  }
}
