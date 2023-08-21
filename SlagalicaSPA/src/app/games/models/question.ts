
export class Question {
    constructor(
        public numId: number,
        public question: string,
        public answers: string[],
        public correctAnswer: string,
    ) {
        this.answers.push(correctAnswer);
        this.answers.sort(
            () => Math.random() - 0.5
        )

    }

    public addCorrectAnswerToAnswers(){
        console.log('start')
        this.answers.push(this.correctAnswer);
        this.answers.sort(
            () => Math.random() - 0.5
        );
        console.log('end')

    }
}