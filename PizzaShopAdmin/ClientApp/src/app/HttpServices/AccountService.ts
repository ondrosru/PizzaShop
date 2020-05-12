import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AccountService {
  myAppUrl = '';

  constructor(private http: HttpClient) {
  }

  public getAccountData() {
    return this.http.get('api/Account/GetAccount').pipe(map(result => result));
  }
}
