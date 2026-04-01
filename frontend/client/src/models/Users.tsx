export interface User {
    id: string,
    FullName: string
    jti: string,
    sub: string,
    email: string
}

export interface DecodedToken extends User {
    exp: number,
    iat?: number,
}