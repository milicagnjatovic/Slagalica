import { Role } from "../app-state/roles";
import { JwtPayloadKeys } from "./jwt-payload-keys";

export interface IJwtPayload {
    [JwtPayloadKeys.Username]: string;
    [JwtPayloadKeys.Email]: string;
    [JwtPayloadKeys.Role]: Role | Role[];
    [JwtPayloadKeys.PlayedGames]: number,
    [JwtPayloadKeys.WonGames]: number,
    exp: number;
    iss: string;
    aud: string;
}