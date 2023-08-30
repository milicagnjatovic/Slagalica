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

  constructor(public gameServerService: GameServerService, private router: Router) {
  }

  ngOnInit() {
    console.log('gmae cpomponent ngInit')
    this.gameServerService.connect();

    this.gameServerService.onReceiveMessage('SendWhoKnowsKnows', (message: string) => {
      console.log('Questions sent ', JSON.parse(message) as Question[]);
      this.gameServerService.setWhoKnows(JSON.parse(message) as {Questions: Question[]});
      this.gameServerService.setCurrentGame('Ko zna zna');
      this.router.navigate(['play', 'who-knows'])
      }
    );

    this.gameServerService.onReceiveMessage('SendPoints', (message: string) => {
      console.log('Rezultat ', message);
      this.gameServerService.points += Number.parseInt(message);
      this.router.navigate(['play', 'result'])

    })
  }

  ngOnDestroy(){
    this.gameServerService.disconnect()
  }

}
