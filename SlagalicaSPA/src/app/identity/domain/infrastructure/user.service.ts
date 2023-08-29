import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, switchMap, take } from 'rxjs';
import { IUserDetails } from '../model/user-details';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) {   }

  public getUserDetails(username: string) : Observable<IUserDetails>{
    return this.httpClient.get<IUserDetails>(`http://localhost:4000/api/v1/User/${username}`);
  }
}
