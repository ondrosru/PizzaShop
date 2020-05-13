import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable, NgZone } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class HttpService {
    private readonly _httpClient: HttpClient;

    public constructor(httpService: HttpClient, private zone: NgZone) {
        this._httpClient = httpService;
    }

    public get<T>(url: string, params?: HttpParams): Observable<T> {
        const httpHeaders = new HttpHeaders().set('Accept', 'application/json');
        return this._httpClient.get<T>(url, {headers: httpHeaders, params: params});
    }

    public post<TRq, TRs>(url: string, body: TRq): Observable<TRs> {
        return this._httpClient.post<TRs>(url, body);
    }
}
