export interface DecodedToken {
    nameid: string;
    exp: number;
    iat: number;
    nbf: number;
    role: string[];
};