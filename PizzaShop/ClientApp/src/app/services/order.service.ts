import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { Order } from '../models/order';

@Injectable()
export class OrderService {
  private _httpService: HttpService;

  constructor(httpService: HttpService) {
    this._httpService = httpService;
   }

  public saveOrder(order: Order): Observable<Order> {
    return this._httpService.post<Order, Order>('api/Order/SaveOrder', order);
  }
}
