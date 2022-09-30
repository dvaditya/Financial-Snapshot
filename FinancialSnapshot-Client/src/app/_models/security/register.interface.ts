export interface Register {
    email: string
    username: string,
    firstName: string,
    lastName: string,
    middleName: string | null,
    password: string,
    duplicatePassword: string
}