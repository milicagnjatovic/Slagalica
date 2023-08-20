import { Component, OnDestroy, OnInit } from '@angular/core';
import { GameServerService } from './services/game-server.service';
import { Router } from '@angular/router';
import { Question } from './models/question';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit, OnDestroy {

  constructor(private gameServerService: GameServerService, private router: Router) {
  }

  ngOnInit() {
    console.log('gmae cpomponent ngInit')
    this.gameServerService.connect();

    this.gameServerService.onReceiveMessage('SendWhoKnowsKnows', (message: string) => {
      console.log('Questions sent ', JSON.parse(message) as Question[]);
      this.gameServerService.setWhoKnows(JSON.parse(message) as Question[]);
      this.router.navigate(['play', 'who-knows'])
      }
    );
  }

  ngOnDestroy(){
    this.gameServerService.disconnect()
  }
}
