export class User {
    username!: string;
    firstName!: string;
    lastName!: string;
    middleName: string | undefined;
    email!: string;
    token?: string;
}