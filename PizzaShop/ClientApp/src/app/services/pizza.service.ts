import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { Pizza } from '../models/pizza';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class PizzaService {
  private _httpService: HttpService;

  constructor(httpService: HttpService) {
    this._httpService = httpService;
   }

  public getPizzas(): Observable<Pizza[]> {
    return this._httpService.get('api/Pizza/GetPizzas');
  }

  public GetPizza(pizzaId: number): Observable<Pizza> {
    const params: HttpParams = new HttpParams()
      .set('Id', pizzaId.toString());
    return this._httpService.get<Pizza>('api/Pizza/GetPizza', params);
  }
}
