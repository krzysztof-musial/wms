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
