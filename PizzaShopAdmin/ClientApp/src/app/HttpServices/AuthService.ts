import { Injectable } from '@angular/core';
import { AccountDto } from 'src/dto/Account/AccountDto';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthService {
  private userData = new BehaviorSubject<AccountDto>(new AccountDto());
  public user: Observable<AccountDto>;

  constructor(private router: Router, private http: HttpClient) {
    this.userData.value.isLoggedIn = false;
    this.user = this.userData.asObservable();
  }

  public signIn(accountDetails) {
    return this.http.post<any>('api/Login/SingIn', accountDetails)
    .pipe(map(response => {
      localStorage.setItem('authToken', response.token);
      this.setUserDetails();
      return response;
    }));
  }

  public get userValue(): AccountDto {
    return this.userData.value;
  }

  public setUserDetails() {
    if (localStorage.getItem('authToken')) {
      const accountDetails = new AccountDto();
      const decodeAccountDetails = JSON.parse(window.atob(localStorage.getItem('authToken').split('.')[1]));
      accountDetails.id = +decodeAccountDetails.sub;
      accountDetails.role = decodeAccountDetails.role;
      accountDetails.isLoggedIn = true;
      this.userData.next(accountDetails);
    }
  }

  public logout() {
    localStorage.removeItem('authToken');
    this.router.navigate(['/auth']);
  }

}
