import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LocalStorageKeys } from '../local-storage/local-storage-keys';
import { LocalStorageService } from '../local-storage/local-storage.service';
import { AppState, IAppState } from './app-state';
import { Role } from './roles';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  private appState: IAppState = new AppState();
  private appStateSuject: BehaviorSubject<IAppState> = new BehaviorSubject<IAppState>(this.appState);
  private appStateObservable: Observable<IAppState> = this.appStateSuject.asObservable();
  
  constructor(private localStorageService: LocalStorageService) {
    this.restoreFromLocalStorage();
  }

  public getAppState(): Observable<IAppState> {
    return this.appStateObservable;
  }

  setAccessToken(accessToken: string): void{
    this.appState = this.appState.clone();
    this.appState.accessToken = accessToken;
    this.appStateSuject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }
  
  setRefreshToken(refreshToken: string): void{
    this.appState = this.appState.clone();
    this.appState.refreshToken = refreshToken;
    this.appStateSuject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }
  
  setUsername(username: string): void{
    this.appState = this.appState.clone();
    this.appState.username = username;
    this.appStateSuject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }
  
  setEmail(email: string): void{
    this.appState = this.appState.clone();
    this.appState.email = email;
    this.appStateSuject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }
  
  setRole(roles: Role | Role[]): void{
    this.appState = this.appState.clone();
    this.appState.roles = roles;
    this.appStateSuject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }


  setFirstName(name: string): void{
    this.appState = this.appState.clone();
    this.appState.firstName = name;
    this.appStateSuject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  setLastName(name: string): void{
    this.appState = this.appState.clone();
    this.appState.lastName = name;
    this.appStateSuject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  setUserId(id: string): void{
    this.appState = this.appState.clone();
    this.appState.userId = id;
    this.appStateSuject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public clearAppState(): void {
    this.localStorageService.clear(LocalStorageKeys.AppState); 
    this.appState = new AppState();
    this.appStateSuject.next(this.appState);
  }

  private restoreFromLocalStorage(): void {
    const appState: IAppState | null = this.localStorageService.get(LocalStorageKeys.AppState);
    if(appState != null){
      this.appState = new AppState(appState.accessToken, appState.refreshToken, appState.username, appState.email, appState.roles,
        appState.firstName, appState.lastName, appState.userId);
      this.appStateSuject.next(this.appState);
    }
  }
}
