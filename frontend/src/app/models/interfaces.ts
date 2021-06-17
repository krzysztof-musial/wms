// Response
export interface IResponse {
    success: boolean,
    message: any,
    data: any
}
// Token decoded
export interface IToken {
    exp: number,
    iat: number,
    nbf: number,
    userId: string,
    userFirstName: string,
    userLastName: string,
    userEmail: string,
    warehouseId: string,
    role: string
}