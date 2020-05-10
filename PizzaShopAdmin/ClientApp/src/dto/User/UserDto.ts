import { UserRole } from './Enum/UserRole';

export class UserDto {
  public id: number;
  public username: string;
  public name: string;
  public surname: string;
  public birthday: Date;
  public phone: string;
  public address: string;
  public email: string;
  public role: UserRole;
  public CreatedAt: Date;
}
