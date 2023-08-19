
export class Question {
    constructor(
        public question: string,
        public answers: string[],
        public correctAnswer: string,
    ) {
        this.answers.push(correctAnswer);
        this.answers.sort(
            () => Math.random() - 0.5
        )

    }
}