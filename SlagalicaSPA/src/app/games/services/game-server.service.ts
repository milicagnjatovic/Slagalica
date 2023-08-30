import { Injectable } from '@angular/core';
import * as singalR from "@microsoft/signalr"
import { Question } from '../models/question';

@Injectable({
  providedIn: 'root'
})
export class GameServerService {


  private serverUrl: string = 'http://localhost:5274/gameServer';
  private serverConnection: singalR.HubConnection;

  public currentGame: string = '';

  public points: number = 0;

  // who-knows
  public whoKnows: Question[] = [];

  constructor() {
    this.serverConnection = new singalR.HubConnectionBuilder()
    .withUrl(this.serverUrl, {
      skipNegotiation: true,
      transport: singalR.HttpTransportType.WebSockets
    })
    .build();
   }

  connect() {
    this.serverConnection.start()
      .then(()=> {
        console.log('Connection esatblished')
        this.serverConnection.on('ReceiveMessage', (message: string) => {
          console.log(message)
      })
      })
      .catch(err => console.error(err));
  }

  disconnect () {
    this.serverConnection.stop().catch(err => console.error(err));
  }

  public setWhoKnows(questions : {Questions: Question[]}){
    console.log('hello')
    console.log(JSON.stringify(questions))
    console.log(typeof questions)
    for(let question of questions.Questions){
      console.log(this.whoKnows)
      this.whoKnows.push(
        new Question(
          question.NumId,
          question.Text, 
          question.Answers,
          question.CorrectAnswer
          )
      );

    }
  }

  sendMessage(typeOfMessage:string, message: string) {
    console.log("Message for server ", message)
    this.serverConnection.invoke(typeOfMessage, message);
  }

  onReceiveMessage(typeOfMessage:string, callback: (message: string) => void) {
    this.serverConnection.on(typeOfMessage, (message: string) => {
      callback(message);
    })
  }

  public waitingForTheGame() : boolean{
    return this.whoKnows.length==0;
  }

  public setCurrentGame(nextGame: string){
    this.currentGame = nextGame;
  }

}
