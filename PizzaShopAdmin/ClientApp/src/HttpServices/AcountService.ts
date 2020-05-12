import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AcountService {
  myAppUrl = '';

  constructor(private http: HttpClient) {
  }

  public getUserData() {
    return this.http.get('api/User/GetUserData').pipe(map(result => result));
  }

  public getAdminData() {
    return this.http.get('api/User/GetAdminData').pipe(map(result => result));
  }
}
