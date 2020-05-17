import { Injectable } from '@angular/core';
import { HttpService } from './HttpService';
import { Observable } from 'rxjs';
import { HttpParams, HttpClient } from '@angular/common/http';

@Injectable()
export class ImageService {
  constructor(private _httpService: HttpService,
    private _httpClient: HttpClient) { }

  public saveImage(image: FormData): Observable<any> {
    return this._httpService.post<FormData, any>('api/Images/SaveImage', image);
  }

  public getImage(name: string): Observable<Blob> {
    const params: HttpParams = new HttpParams()
      .set('name', name);
    return this._httpClient.get('api/Images/GetImage', {responseType: 'blob', params: params});
  }
}
