export interface ILoginRequest {
    username: string,
    password: string
}

export class LoginRequest implements ILoginRequest {
    public username: string;
    public password: string;

    public constructor(username:string, password:string){
        this.username = username;
        this.password = password;
    }
}