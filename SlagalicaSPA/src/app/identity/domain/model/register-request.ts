export interface IRegisterRequest {
    username: string,
    password: string,
    email: string, 
    firstName: string, 
    lastName: string,
    phoneNumber: string
}

export class RegisterRequest implements IRegisterRequest {
    public phoneNumber: string;
    public constructor(
        public username: string, 
        public password: string, 
        public email: string,
        public firstName: string,
        public lastName: string){
            this.phoneNumber = '123456788';
    }
}