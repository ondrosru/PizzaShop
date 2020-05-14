import { Role } from './Enum/Role';

export class AccountDto {
    public id: number;
    public username: string;
    public password: string;
    public name: string;
    public surname: string;
    public phone: string;
    public address: string;
    public email: string;
    public createdAt: Date;
    public isLoggedIn: boolean;
    public role: Role;
}
