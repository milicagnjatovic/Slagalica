import { Component, OnInit, Query } from '@angular/core';
import { Question } from '../models/question';
import { GameServerService } from '../services/game-server.service';

@Component({
  selector: 'app-who-knows',
  templateUrl: './who-knows.component.html',
  styleUrls: ['./who-knows.component.css']
})

export class WhoKnowsComponent implements OnInit{
  ngOnInit(): void {
    this.questions = this.gameService.whoKnows;
    this.currentQuestion = 0
  }

  public questions: Question[] = [];
  public currentQuestion: number = 0;

  private points: number = 0;

  public response = '';
  public show = "display: none"

  constructor (private gameService : GameServerService) {
    this.questions = [
      // new Question('q1', ['w11', 'w12', 'w13'], 'c'),
      // new Question('q2', ['w21', 'w22', 'w23'], 'c'),
      // new Question('q3', ['w31', 'w32', 'w33'], 'c'),
    ];
  }

  public setNextQuestion () {
    this.currentQuestion = this.currentQuestion + 1;
    if (this.currentQuestion >= this.questions.length) {      
      this.response = `Rezultat: ${this.points}/${this.questions.length}`;
      return
    }
    this.show = this.show + 'none'
    this.response = '';
  }

  checkAnswer(event: Event){
    if(this.response != '') 
      return

    let clicked = (event.target as HTMLButtonElement).innerHTML;
    if (clicked == this.questions[this.currentQuestion].correctAnswer) {
      this.points = this.points + 1;
      this.response = 'Odlicno! >'
    } else {
      this.response = `Pogresno, tacan odgovor je: ${this.questions[this.currentQuestion].correctAnswer} >`
    }
    this.show = this.show.replace('none', '')
  }

}