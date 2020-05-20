import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable()
export class ImageService {
  constructor(private httpClient: HttpClient) {
  }

  public getImage(name: string): Observable<Blob> {
    const parameters: HttpParams = new HttpParams().set('name', name);
    return this.httpClient.get('api/Images/GetImage', {responseType: 'blob', params: parameters});
  }
}
