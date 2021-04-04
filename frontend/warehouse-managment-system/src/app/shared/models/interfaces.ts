// USER
// export interface IUser {
//     user_id: number,
//     warehouse_id?: number,
//     role_id?: number,
//     user_first_name: string,
//     user_last_name: string,
//     user_email: string,
//     user_password?: string,
//     user_is_deleted: boolean
// }

export interface IUserRegister {
    FirstName: string,
    LastName: string,
    Username: string,
    Password: string,
    PasswordConfirmation: string,
    Agreement: boolean
}

export interface IUserLogin {
    username: string,
    password: string
}

// TOKEN:
export interface IToken {
    exp: number,
    iat: number,
    nbf: number,
    userId: string,
    username: string
}