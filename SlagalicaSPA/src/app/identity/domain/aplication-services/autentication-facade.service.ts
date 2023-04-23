import { Injectable } from '@angular/core';
import { Observable, map, catchError, of, switchMap, take } from 'rxjs';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';
import { JwtPayloadKeys } from 'src/app/shared/jwt/jwt-payload-keys';
import { JwtService } from 'src/app/shared/jwt/jwt.service';
import { AutenticationService } from '../infrastructure/autentication.service';
import { ILoginRequest } from '../model/login-request';
import { ILoginResponse } from '../model/login-response';
import { IUserDetails } from '../model/user-details';
import { UserFacadeService } from './user-facade.service';
import { AppState, IAppState } from 'src/app/shared/app-state/app-state';
import { ILogoutRequest } from '../model/logout-request';
import { IRefreshTokenRequest } from '../model/refresh-token-request.service';
import { IRefreshTokenResponse } from '../model/refresh-token-response';

@Injectable({
  providedIn: 'root'
})
export class AutenticationFacadeService {

  constructor(private autenticationService: AutenticationService, 
    private appStateService: AppStateService, 
    private jwtService: JwtService,
    private userService: UserFacadeService){ }

  public login(username: string, password: string) : Observable<boolean> {
    const request : ILoginRequest = {username, password};
    return this.autenticationService.login(request).pipe(
      switchMap( (loginResponse: ILoginResponse) => { 
        console.log(loginResponse);
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);

        const payload = this.jwtService.parsePayload(loginResponse.accessToken);
        this.appStateService.setUsername(payload[JwtPayloadKeys.Username]);
        this.appStateService.setEmail(payload[JwtPayloadKeys.Email]);
        this.appStateService.setRole(payload[JwtPayloadKeys.Role]);

        return this.userService.getUserDetails(payload[JwtPayloadKeys.Username]);
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setFirstName(userDetails.firstName);
        this.appStateService.setLastName(userDetails.lastName);
        this.appStateService.setUserId(userDetails.id);
        return true;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();
        return of(false);
      })
    );
  }

  public logout(): Observable<boolean>{
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: ILogoutRequest = {username: appState.username as string, refreshToken: appState.refreshToken as string};
        return request;
      }),
      switchMap((request: ILogoutRequest) => this.autenticationService.logout(request)),
      map(() => {
        this.appStateService.clearAppState();
        return true;
      }),
      catchError((err) => {
        console.log(err);
        return of(false);
      })
    )
  }

  public refreshToken(): Observable<string | null>{
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: IRefreshTokenRequest = { username: appState.username as string, refreshtoken: appState.refreshToken as string};
        return request;
      }),
      switchMap((request: IRefreshTokenRequest) => this.autenticationService.refreshToken(request)),
      map((response: IRefreshTokenResponse) => {
        this.appStateService.setAccessToken(response.accesstoken);
        this.appStateService.setRefreshToken(response.refreshtoken);
        return response.accesstoken;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();
        return of(null);
      })
      );
  }
}
