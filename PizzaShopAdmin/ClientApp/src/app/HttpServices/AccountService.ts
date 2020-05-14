import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { AccountDto } from './../../dto/Account/AccountDto';
import { Observable } from 'rxjs';
import { HttpService } from './HttpService';
import { map } from 'rxjs/operators';

@Injectable()
export class AccountService {
  private _httpService: HttpService;

  public constructor(httpService: HttpService) {
    this._httpService = httpService;
  }

  public getAccounts(): Observable<AccountDto[]> {
    return this._httpService.get<AccountDto[]>('api/Account/GetAccounts');
  }

  public saveUser(account: AccountDto): Observable<any> {
    return this._httpService.post<AccountDto, any>('api/Account/SaveAccount', account)
    .pipe(map(response => {
      return response;
    }));
  }

  public getUser(userId: number): Observable<AccountDto> {
    const params: HttpParams = new HttpParams()
      .set('Id', userId.toString());
    return this._httpService.get<AccountDto>('api/Account/GetAccount', params);
  }

  public deleteUser(userId: number): Observable<any> {
    return this._httpService.post<number, any>('api/Account/DeleteAccount', userId);
  }
}
