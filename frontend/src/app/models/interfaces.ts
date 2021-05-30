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
    roleId: string
}