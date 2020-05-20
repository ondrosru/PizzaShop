import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class HttpService {
  constructor(private httpClient: HttpClient) { }

  public get<T>(url: string, parameters?: HttpParams): Observable<T> {
    const httpHeader = new HttpHeaders().set('Accept', 'aplication/json');
    return this.httpClient.get<T>(url, {headers: httpHeader, params: parameters});
  }

  public post<TRq, TRs>(url: string, body: TRq): Observable<TRs> {
    return this.httpClient.post<TRs>(url, body);
  }
}
