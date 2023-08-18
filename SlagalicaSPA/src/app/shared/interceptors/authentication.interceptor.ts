import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, filter, switchMap, take, throwError } from 'rxjs';
import { AppStateService } from '../app-state/app-state.service';
import { AppState, IAppState } from '../app-state/app-state';
import { AutenticationFacadeService } from 'src/app/identity/domain/aplication-services/autentication-facade.service';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  private readonly whitelistUrls: string[] = [
    '/api/v1/Authentication/Login',
    '/api/v1/Authentication/Refresh',
    '/api/v1/Authentication/User',
  ];

  private isRefreshing: boolean = false;
  private refreshedAccessTokenSubject: BehaviorSubject<string|null> = new BehaviorSubject<string | null> (null);

  constructor(private appStateService: AppStateService, private authenticationService: AutenticationFacadeService ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if(this.isWhitelisted(request.url)) {
      return next.handle(request);
    }

    return this.appStateService.getAppState().pipe(
      take(1),
      switchMap((appState: IAppState) => {
        if (appState.accessToken !== undefined) {
          request = this.addToken(request, appState.accessToken);
        }

        return next.handle(request);
      }),
      catchError((err) => {
        if(err instanceof HttpErrorResponse && err.status===402){
          return this.handle401Erro(request, next);
        }
        return throwError(()=>err);
      })
    );
  }

  private isWhitelisted(url: string): boolean {
    return this.whitelistUrls.some( (whitelistedUrl: string) =>  url.includes(whitelistedUrl));
  }

  private addToken(request: HttpRequest<unknown>, accessToken: string) : HttpRequest<unknown> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`,
      }
    })
  }

  private handle401Erro(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>>{
    if(this.isRefreshing){
      this.isRefreshing = true;
      this.refreshedAccessTokenSubject.next(null);

      return this.authenticationService.refreshToken().pipe(
        switchMap((accessToken: string | null) => {
          if(accessToken==null) {
            return throwError(()=>new Error('Refresh token flow failed'));
          }
        this.isRefreshing = false;
        this.refreshedAccessTokenSubject.next(accessToken);
        return next.handle(this.addToken(request, accessToken));
        }
        )
      );
    }

    return this.refreshedAccessTokenSubject.pipe(
      filter((token: string | null) => token !== null),
      take(1),
      switchMap((accessToken: string | null) => next.handle(this.addToken(request, accessToken as string)))
    );
  }
}
