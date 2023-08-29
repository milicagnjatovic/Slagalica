import { Component, OnInit } from '@angular/core';
import { AutenticationFacadeService } from '../../domain/aplication-services/autentication-facade.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  public logoutSuccess: Observable<boolean>;

  constructor(private authenticationService: AutenticationFacadeService, private router: Router){
    this.logoutSuccess = this.authenticationService.logout();
    this.logoutSuccess.subscribe();
    router.navigate(['/'])
  }

  ngOnInit(): void {
    
    }
}
