// Database
export interface IUser {
    user_id: number,
    warehouse_id?: number,
    role_id?: number,
    user_first_name: string,
    user_last_name: string,
    user_email: string,
    user_password?: string,
    user_is_deleted: boolean
}

// Register form
export interface IUserRegister {
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    passwordConfirmation: string,
    agreement: boolean
}

// Login form
export interface IUserLogin {
    email: string,
    password: string
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
    roleId: string
}