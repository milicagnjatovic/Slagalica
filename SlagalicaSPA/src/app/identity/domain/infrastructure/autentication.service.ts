import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILoginRequest } from '../model/login-request';
import { ILoginResponse } from '../model/login-response';
import { ILogoutRequest } from '../model/logout-request';
import { IRefreshTokenRequest } from '../model/refresh-token-request.service';
import { IRefreshTokenResponse } from '../model/refresh-token-response';
import { IRegisterRequest } from '../model/register-request';
import { IUpdateuserRequest } from '../model/update-user-request';

@Injectable({
  providedIn: 'root'
})
export class AutenticationService {
  // private readonly url: string = 'https://f9c42a0c-c829-401c-8373-527a2340f523.mock.pstmn.io/login';
  private readonly url: string = 'http://localhost:4000/api/v1/Authentication';

  constructor(private httpClient: HttpClient) { }

  public login(request: ILoginRequest) : Observable<ILoginResponse> {
    return this.httpClient.post<ILoginResponse>(`${this.url}/Login`, request); 
  }

  public register(request: IRegisterRequest) : Observable<ILoginResponse> {
    console.log('Request ')
    console.log(request)
    return this.httpClient.post<ILoginResponse>(`${this.url}/RegisterPlayer`, request); 
  }

  public logout(request: ILogoutRequest) : Observable<Object>{
     return this.httpClient.post(`${this.url}/logout`, request);
  }
  
  public refreshToken(request: IRefreshTokenRequest) : Observable<IRefreshTokenResponse>{
     return this.httpClient.post<IRefreshTokenResponse>(`${this.url}/refresh`, request);
  }

  public gameOverUserUpdate(request: IUpdateuserRequest): Observable<ILoginResponse>{
    return this.httpClient.post<ILoginResponse>(`${this.url}/AddPlayedGames`, request);
  }
}
