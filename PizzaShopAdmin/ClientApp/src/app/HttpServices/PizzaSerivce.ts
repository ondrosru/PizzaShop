import { HttpService } from './HttpService';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PizzaDto } from 'src/dto/Pizza/PizzaDto';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class PizzaService {
  private _httpService: HttpService;

  public constructor(httpService: HttpService) {
    this._httpService = httpService;
  }

  public GetPizzas(): Observable<PizzaDto[]> {
    return this._httpService.get<PizzaDto[]>('api/Pizza/GetPizzas');
  }

  public GetPizza(pizzaId: number): Observable<PizzaDto> {
    const params: HttpParams = new HttpParams()
      .set('Id', pizzaId.toString());
    return this._httpService.get<PizzaDto>('api/Pizza/GetPizza', params);
  }
}
