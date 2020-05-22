import { Component, OnInit } from '@angular/core';
import { Order } from 'src/dto/Order/OrderDto';
import { OrderService } from 'src/app/HttpServices/OrderService';

@Component({
  selector: 'app-order-list-page',
  templateUrl: './order-list-page.component.html',
  styleUrls: ['./order-list-page.component.css'],
  providers: [OrderService]
})
export class OrderListPageComponent implements OnInit {
  orders: Order[];

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.orderService.getOrders().subscribe(values => {
      this.orders = values;
    });
  }

}
