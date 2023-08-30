
export class Question {
    constructor(
        public NumId: number,
        public Text: string,
        public Answers: string[],
        public CorrectAnswer : string,
    ) {
        this.Answers.push(CorrectAnswer);
        this.Answers.sort(
            () => Math.random() - 0.5
        )

    }

    public addCorrectAnswerToAnswers(){
        console.log('start')
        this.Answers.push(this.CorrectAnswer);
        this.Answers.sort(
            () => Math.random() - 0.5
        );
        console.log('end')

    }
}