import { HttpService } from './HttpService';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserDto } from '../dto/User/UserDto'
import { HttpParams } from '@angular/common/http';

@Injectable()
export class UsersDataService {
  private readonly _httpService: HttpService;

  public constructor(httpService: HttpService) {
    this._httpService = httpService;
  }

  public getUsers(): Observable<UserDto[]> {
    return this._httpService.get<UserDto[]>('api/Users/Users');
  }
}
