import { Component, OnInit } from '@angular/core';
import { GameServerService } from '../services/game-server.service';
import { AutenticationFacadeService } from '../../identity/domain/aplication-services/autentication-facade.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {
  constructor (public gameService : GameServerService, private authenticationService: AutenticationFacadeService,
    private router: Router) {}

  ngOnInit() {
    let win : boolean = this.gameService.points > this.gameService.opponentsPoints;
    this.authenticationService.gameOverUserUpdate(win).subscribe(
      (success: boolean) => {
        if(success) {
          // this.router.navigate(['/profile']);
        } else {
          window.alert('Error occured!')
        }
      }
    );
  }
}
