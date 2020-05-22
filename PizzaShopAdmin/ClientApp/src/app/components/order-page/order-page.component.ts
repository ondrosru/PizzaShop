import { Component, OnInit } from '@angular/core';
import { PizzaService } from 'src/app/HttpServices/PizzaSerivce';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from 'src/app/HttpServices/OrderService';
import { Order } from 'src/dto/Order/OrderDto';
import { PizzaDto } from 'src/dto/Pizza/PizzaDto';
import { PriceDto } from 'src/dto/Pizza/PriceDto';
import { DoughThickness } from 'src/dto/Pizza/Enums/DoughThickness';
import { PizzaSize } from 'src/dto/Pizza/Enums/PizzaSize';

@Component({
  selector: 'app-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
  providers: [PizzaService, OrderService]
})
export class OrderPageComponent implements OnInit {
  private order: Order;
  private pizzas: PizzaDto[];

  constructor(private orderService: OrderService,
     private pizzaService: PizzaService,
     private route: ActivatedRoute,
     private router: Router
  ) {
    route.params.subscribe(params => {
      const orderId: number | undefined = params['Id'] !== undefined
        ? Number(params['Id'])
        : 0;
      this.orderService.getOrder(orderId).subscribe(order => {
        this.order = order;
        this.pizzaService.GetPizzasAtPriceOrderIds(this.order.orderPriceId).subscribe(values => {
          this.pizzas = values;
        });
      });
    });
  }

  ngOnInit(): void {
  }

  getPrices(): PriceDto[] {
    const prices = [];
    if (this.pizzas != null) {
      this.pizzas.forEach(pizza => {
        pizza.prices.forEach(price => {
          prices.push(price);
        });
      });
    }
    return prices;
  }

  getPizzaName(price: PriceDto): string {
    const foundPizza = this.pizzas.find(pizza => {
      return pizza.prices.find(findPrice => findPrice.id === price.id);
    });
    if (foundPizza != null) {
      return foundPizza.name;
    }
    return '';
  }

  thicknessConvertToString(thickness: DoughThickness): string {
    let result = 'Неопределенно';
    if (thickness === DoughThickness.Thin) {
      result = 'Тонкий';
    } else if (thickness === DoughThickness.Traditional) {
      result = 'Традиционный';
    } else if (thickness === DoughThickness.Fat) {
      result = 'Толстый';
    }
    return result;
  }

  sizeConvertToString(size: PizzaSize): string {
    let result = 'Неопределенно';
    if (size === PizzaSize.Small) {
      result = 'Маленький';
    } else if (size === PizzaSize.Middle) {
      result = 'Средний';
    } else if (size === PizzaSize.Big) {
      result = 'Большой';
    }
    return result;
  }
}
