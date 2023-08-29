import { Injectable } from '@angular/core';
import { IJwtPayload } from './jwt-payload';

@Injectable({
  providedIn: 'root'
})
export class JwtService {

  constructor() { }

  public parsePayload(jwtString: string): IJwtPayload {
    const payloadString: string = jwtString.split('.')[1];
    return JSON.parse(atob(payloadString)) as IJwtPayload;
  }
}
