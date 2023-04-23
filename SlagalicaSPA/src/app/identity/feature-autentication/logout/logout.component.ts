import { Component, OnInit } from '@angular/core';
import { AutenticationFacadeService } from '../../domain/aplication-services/autentication-facade.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  public logoutSuccess: Observable<boolean>;

  constructor(private authenticationService: AutenticationFacadeService){
    this.logoutSuccess = this.authenticationService.logout();
  }

  ngOnInit(): void {
    
  }
}
