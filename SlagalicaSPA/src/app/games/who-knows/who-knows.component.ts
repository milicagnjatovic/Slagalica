import { Component, OnInit, Query } from '@angular/core';
import { Question } from '../models/question';
import { GameServerService } from '../services/game-server.service';

class Answer {
  constructor (
    public numId: number,
    public answer: string
  ) {}
}

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

  private answers: Answer[] = [];

  public response = '';
  public show = "display: none"

  constructor (public gameService : GameServerService) {
    this.questions = [
      // new Question('q1', ['w11', 'w12', 'w13'], 'c'),
      // new Question('q2', ['w21', 'w22', 'w23'], 'c'),
      // new Question('q3', ['w31', 'w32', 'w33'], 'c'),
    ];
  }

  public setNextQuestion () {
    console.log(this.answers)
    this.currentQuestion = this.currentQuestion + 1;
    if (this.currentQuestion >= this.questions.length) {      
      this.response = `Rezultat: ${this.points}/${this.questions.length}`;
      
      console.log(JSON.stringify(this.answers))

      this.gameService.sendMessage(
        'SubmitWhoKnowsKnows', 
        JSON.stringify(this.answers));

      return;
    }
    this.show = this.show + 'none'
    this.response = '';
  }

  checkAnswer(event: Event){
    if(this.response != '') {
      return
    }

    let clicked = (event.target as HTMLButtonElement).innerHTML;
    this.answers.push(new Answer(this.questions[this.currentQuestion].NumId, clicked));
    if (clicked == this.questions[this.currentQuestion].CorrectAnswer) {
      this.points = this.points + 1;
      this.response = 'Odlicno! >'
    } else {
      this.response = `Pogresno, tacan odgovor je: ${this.questions[this.currentQuestion].CorrectAnswer} >`
    }
    this.show = this.show.replace('none', '')
  }


}
