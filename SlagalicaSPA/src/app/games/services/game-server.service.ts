import { Injectable } from '@angular/core';
import * as singalR from "@microsoft/signalr"
import { Question } from '../models/question';

@Injectable({
  providedIn: 'root'
})
export class GameServerService {


  private serverUrl: string = 'http://localhost:5274/gameServer';
  private serverConnection: singalR.HubConnection;

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

  public setWhoKnows(questions : Question[]){
    for(let question of questions){
      this.whoKnows.push(
        new Question(
          question.question, 
          question.answers,
          question.correctAnswer)
      );

    }
  }

  sendMessage(typeOfMessage:string, message: string) {
    this.serverConnection.invoke(typeOfMessage, message);
  }

  onReceiveMessage(typeOfMessage:string, callback: (message: string) => void) {
    this.serverConnection.on(typeOfMessage, (message: string) => {
      callback(message);
    })
  }
}
