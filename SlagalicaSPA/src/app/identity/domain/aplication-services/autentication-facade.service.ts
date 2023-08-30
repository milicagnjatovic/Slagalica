import { Injectable } from '@angular/core';
import { Observable, map, catchError, of, switchMap, take, connect } from 'rxjs';
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
import { IRegisterRequest } from '../model/register-request';
import { IUpdateuserRequest } from '../model/update-user-request';

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
    console.log("request ")
    console.log(request)
    return this.autenticationService.login(request).pipe(
      switchMap( (loginResponse: ILoginResponse) => { 
        console.log(loginResponse);
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);

        const payload = this.jwtService.parsePayload(loginResponse.accessToken);
        this.appStateService.setUsername(payload[JwtPayloadKeys.Username]);
        this.appStateService.setEmail(payload[JwtPayloadKeys.Email]);
        this.appStateService.setRole(payload[JwtPayloadKeys.Role]);
        this.appStateService.setNumbersOfGames(payload[JwtPayloadKeys.PlayedGames], payload[JwtPayloadKeys.WonGames]);

        console.log("payload ")
        console.log(payload)
        console.log(payload[JwtPayloadKeys.Username])
        console.log(payload[JwtPayloadKeys.Email])
        console.log(payload[JwtPayloadKeys.Role])
        console.log(payload[JwtPayloadKeys.PlayedGames])
        console.log(payload[JwtPayloadKeys.WonGames])

        return this.userService.getUserDetails(payload[JwtPayloadKeys.Username]);
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setFirstName(userDetails.firstName);
        this.appStateService.setLastName(userDetails.lastName);
        this.appStateService.setUserId(userDetails.id);
        return true;
      }),
      catchError((err) => {
        console.error('AutenticationFacadeService:')
        console.error(err);
        this.appStateService.clearAppState();
        return of(false);
      })
    );
  }


  public register(username: string, password: string, email: string, firstName: string, lastName: string)  : Observable<boolean> {
    const request : IRegisterRequest = {username, password, email, firstName, lastName, playedGames:0, wonGames: 0};
    console.log("request ")
    console.log(request)
    return this.autenticationService.register(request).pipe(

      switchMap( (loginResponse: ILoginResponse) => { 
        console.log(loginResponse);
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);

        const payload = this.jwtService.parsePayload(loginResponse.accessToken);
        this.appStateService.setUsername(payload[JwtPayloadKeys.Username]);
        this.appStateService.setEmail(payload[JwtPayloadKeys.Email]);
        this.appStateService.setRole(payload[JwtPayloadKeys.Role]);
        this.appStateService

        console.log("payload ")
        console.log(payload)
        console.log(payload[JwtPayloadKeys.Username])
        console.log(payload[JwtPayloadKeys.Email])
        console.log(payload[JwtPayloadKeys.Role])
        console.log(payload[JwtPayloadKeys.PlayedGames])
        console.log(payload[JwtPayloadKeys.WonGames])

        return this.userService.getUserDetails(payload[JwtPayloadKeys.Username]);
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setFirstName(userDetails.firstName);
        this.appStateService.setLastName(userDetails.lastName);
        this.appStateService.setUserId(userDetails.id);
        return true;
      }),
      catchError((err) => {
        console.error('AutenticationFacadeService:')
        console.error(err);
        this.appStateService.clearAppState();
        return of(false);
      }
      )
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

  public gameOverUserUpdate(isWin:boolean): Observable<boolean>{
    const request : IUpdateuserRequest = this.appStateService.getUserDto(isWin);
    console.log("request ")
    console.log(request)
    return this.autenticationService.gameOverUserUpdate(request).pipe(
      switchMap( (loginResponse: ILoginResponse) => { 
        console.log(loginResponse);
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);

        const payload = this.jwtService.parsePayload(loginResponse.accessToken);
        this.appStateService.setUsername(payload[JwtPayloadKeys.Username]);
        this.appStateService.setEmail(payload[JwtPayloadKeys.Email]);
        this.appStateService.setRole(payload[JwtPayloadKeys.Role]);
        this.appStateService.setNumbersOfGames(payload[JwtPayloadKeys.PlayedGames], payload[JwtPayloadKeys.WonGames]);

        console.log("payload ")
        console.log(payload)
        console.log(payload[JwtPayloadKeys.Username])
        console.log(payload[JwtPayloadKeys.Email])
        console.log(payload[JwtPayloadKeys.Role])
        console.log(payload[JwtPayloadKeys.PlayedGames])
        console.log(payload[JwtPayloadKeys.WonGames])

        return this.userService.getUserDetails(payload[JwtPayloadKeys.Username]);
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setFirstName(userDetails.firstName);
        this.appStateService.setLastName(userDetails.lastName);
        this.appStateService.setUserId(userDetails.id);
        return true;
      }),
      catchError((err) => {
        console.error('AutenticationFacadeService:')
        console.error(err);
        this.appStateService.clearAppState();
        return of(false);
      })
    );
  }
}
