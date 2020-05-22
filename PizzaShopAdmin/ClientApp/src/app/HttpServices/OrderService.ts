import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './HttpService';
import { Order } from 'src/dto/Order/OrderDto';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class OrderService {
  private _httpService: HttpService;

  public constructor(httpService: HttpService) {
    this._httpService = httpService;
  }

  public getOrders(): Observable<Order[]> {
    return this._httpService.get<Order[]>('api/Order/GetOrders');
  }

  public getOrder(orderId: number): Observable<Order> {
    const params: HttpParams = new HttpParams()
      .set('Id', orderId.toString());
    return this._httpService.get<Order>('api/Order/GetOrder', params);
  }
}
