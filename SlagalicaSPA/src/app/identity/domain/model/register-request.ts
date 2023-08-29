export interface IRegisterRequest {
    username: string,
    password: string,
    email: string, 
    firstName: string, 
    lastName: string,
    playedGames: number,
    wonGames: number,
}

export class RegisterRequest implements IRegisterRequest {
    public constructor(
        public username: string, 
        public password: string, 
        public email: string,
        public firstName: string,
        public lastName: string,
        public playedGames: number = 0,
        public wonGames: number = 0){
    }
}