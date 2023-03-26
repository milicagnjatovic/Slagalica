import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILoginRequest } from '../model/login-request';
import { ILoginResponse } from '../model/login-response';

@Injectable({
  providedIn: 'root'
})
export class AutenticationService {

  constructor(private httpClient: HttpClient) { }

  public login(request: ILoginRequest) : Observable<ILoginResponse> {
    return this.httpClient.post<ILoginResponse>("https://localhost:4000", request); 
  }
}

///api/v1/Autentication/Login