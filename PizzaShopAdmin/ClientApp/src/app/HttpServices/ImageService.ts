import { Injectable } from '@angular/core';
import { HttpService } from './HttpService';
import { Observable } from 'rxjs';

@Injectable()
export class ImageService {
  constructor(private _httpService: HttpService) { }

  public saveImage(image: FormData): Observable<any> {
    return this._httpService.post<FormData, any>('api/Images/SaveImage', image);
  }
}
