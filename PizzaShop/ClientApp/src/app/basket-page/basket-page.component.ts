import { Component, OnInit } from '@angular/core';
import { PizzaService } from '../services/pizza.service';
import { CartService } from '../services/cart.service';
import { Pizza } from '../models/pizza';
import { PizzaPrice } from '../models/pizza-price';
import { PizzaThickness } from '../models/Enums/pizza-thickness';
import { PizzaSize } from '../models/Enums/pizza-size';

@Component({
  selector: 'app-basket-page',
  templateUrl: './basket-page.component.html',
  styleUrls: ['./basket-page.component.css'],
  providers: [PizzaService]
})
export class BasketPageComponent implements OnInit {
  private selectedPizzas: Pizza[];

  constructor(
    private pizzaService: PizzaService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.selectedPizzas = [];
    const selectedPizzas = this.cartService.getPizzas();
    selectedPizzas.forEach(selectedPizza => {
      this.pizzaService.GetPizza(selectedPizza.pizzaId).subscribe( pizza => {
        selectedPizza.selectedPrices.forEach(priceInOrder => {
          const priceForAdding = pizza.prices.find(price =>
            price.thickness === priceInOrder.thickness
            && price.size === priceInOrder.size
          );
          console.log(priceForAdding);
          if (priceForAdding) {
            console.log(priceForAdding);
            const pizzaForAdding = new Pizza();
            pizzaForAdding.id = pizza.id;
            pizzaForAdding.name = pizza.name;
            pizzaForAdding.description = pizza.description;
            pizzaForAdding.imgPath = pizza.imgPath;
            pizzaForAdding.ingredients = pizza.ingredients;
            pizzaForAdding.prices = [];
            priceForAdding.count = priceInOrder.count;
            pizzaForAdding.prices.push(priceForAdding);
            this.selectedPizzas.push(pizzaForAdding);
          }
        });
      });
    });
  }

  getPrice(prices: PizzaPrice[]): PizzaPrice {
    return prices[0];
  }

  reduceAmount(pizza: Pizza, price: PizzaPrice): void {
    const currentPizza = this.selectedPizzas.find(value => pizza.id === value.id);
    if (currentPizza) {
      const currentPrice = currentPizza.prices.find(value => value.size === price.size && price.thickness === value.thickness);
      if (currentPrice) {
        if (currentPrice.count === 1) {
          const indexPizza = this.selectedPizzas.findIndex(value => pizza.id === value.id);
          if (indexPizza !== -1) {
            this.selectedPizzas.splice(indexPizza, 1);
          }
        } else {
          currentPrice.count -= 1;
        }
        this.cartService.removeToCart(pizza.id, price.size, price.thickness, 1);
      }
    }
  }

  increaseAmount(pizza: Pizza, price: PizzaPrice): void {
    const currentPizza = this.selectedPizzas.find(value => pizza.id === value.id);
    if (currentPizza) {
      const currentPrice = currentPizza.prices.find(value => value.size === price.size && price.thickness === value.thickness);
      if (currentPrice) {
        currentPrice.count += 1;
        this.cartService.addToCart(pizza.id, price.size, price.thickness, 1);
      }
    }
  }

  thicknessConvertToString(thickness: PizzaThickness): string {
    let result = 'Неопределенно';
    if (thickness === PizzaThickness.Thin) {
      result = 'Тонкий';
    } else if (thickness === PizzaThickness.Traditional) {
      result = 'Традиционный';
    } else if (thickness === PizzaThickness.Fat) {
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
