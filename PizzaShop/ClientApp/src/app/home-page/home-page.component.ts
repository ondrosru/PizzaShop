import { Component, OnInit } from '@angular/core';
import { Pizza } from '../models/pizza';
import { PizzaService } from '../services/pizza.service';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
  providers: [PizzaService]
})
export class HomePageComponent implements OnInit {
  public pizzas: Pizza[] = [];

  constructor(
    private pizzaService: PizzaService,
    private cartService: CartService
  ) {
    pizzaService.getPizzas().subscribe(values => {
      this.pizzas = values;
    });
  }

  ngOnInit(): void {
  }
}
