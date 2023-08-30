import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LocalStorageKeys } from '../local-storage/local-storage-keys';
import { LocalStorageService } from '../local-storage/local-storage.service';
import { AppState, IAppState } from './app-state';
import { Role } from './roles';
import { IUpdateuserRequest } from '../../identity/domain/model/update-user-request'

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  private appState: IAppState = new AppState();
  private appStateSubject: BehaviorSubject<IAppState> = new BehaviorSubject<IAppState>(this.appState);
  private appStateObservable: Observable<IAppState> = this.appStateSubject.asObservable();
  
  constructor(private localStorageService: LocalStorageService) {
    this.restoreFromLocalStorage();
  }

  public getAppState(): Observable<IAppState> {
    return this.appStateObservable;
  }

  public setAccessToken(accessToken: string): void{
    this.appState = this.appState.clone();
    this.appState.accessToken = accessToken;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }
  
  public setRefreshToken(refreshToken: string): void{
    this.appState = this.appState.clone();
    this.appState.refreshToken = refreshToken;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }
  
  public setUsername(username: string): void{
    this.appState = this.appState.clone();
    this.appState.username = username;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }
  
  public setEmail(email: string): void{
    this.appState = this.appState.clone();
    this.appState.email = email;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }
  
  public setRole(roles: Role | Role[]): void{
    this.appState = this.appState.clone();
    this.appState.roles = roles;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }


  public setFirstName(name: string): void{
    this.appState = this.appState.clone();
    this.appState.firstName = name;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setLastName(name: string): void{
    this.appState = this.appState.clone();
    this.appState.lastName = name;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setUserId(id: string): void{
    this.appState = this.appState.clone();
    this.appState.userId = id;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setNumbersOfGames(played: number, won: number): void {
    this.appState = this.appState.clone();
    this.appState.playedGames = played;
    this.appState.wonGames = won;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public clearAppState(): void {
    this.localStorageService.clear(LocalStorageKeys.AppState); 
    this.appState = new AppState();
    this.appStateSubject.next(this.appState);
  }

  private restoreFromLocalStorage(): void {
    const appState: IAppState | null = this.localStorageService.get(LocalStorageKeys.AppState);
    if(appState != null){
      this.appState = new AppState(
        appState.accessToken,
        appState.refreshToken, 
        appState.username,
        appState.email, 
        appState.roles,
        appState.firstName,
        appState.lastName, 
        appState.userId,
        appState.wonGames,
        appState.playedGames);
      this.appStateSubject.next(this.appState);
    }
  }

  public getUserDto (isWin: boolean): IUpdateuserRequest{
    let updateReq: IUpdateuserRequest = { userName: this.appState.username ? this.appState.username : '',  isWin };
    return updateReq;
  }
}
