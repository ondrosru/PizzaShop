import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { AccountDto } from './../../dto/Account/AccountDto';
import { Observable } from 'rxjs';
import { HttpService } from './HttpService';

@Injectable()
export class AccountService {
  private _httpService: HttpService;

  public constructor(httpService: HttpService) {
    this._httpService = httpService;
  }

  public getAccounts(): Observable<AccountDto[]> {
    return this._httpService.get<AccountDto[]>('api/Account/GetAccounts');
  }

  public saveUser(account: AccountDto): Observable<AccountDto> {
    return this._httpService.post<AccountDto, AccountDto>('api/Account/SaveAccounts', account);
  }

  public getUser(userId: number): Observable<AccountDto> {
    const params: HttpParams = new HttpParams()
      .set('Id', userId.toString());
    return this._httpService.get<AccountDto>('api/Account/GetAccount', params);
  }
}
