import { Component, OnInit } from '@angular/core';
import { PizzaService } from '../services/pizza.service';
import { CartService } from '../services/cart.service';
import { Pizza } from '../models/pizza';
import { PizzaPrice } from '../models/pizza-price';
import { PizzaThickness } from '../models/Enums/pizza-thickness';
import { PizzaSize } from '../models/Enums/pizza-size';
import { Order } from '../models/order';
import { OrderService } from '../services/order.service';
import { FormControl, FormBuilder, Validators, FormGroup, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-basket-page',
  templateUrl: './basket-page.component.html',
  styleUrls: ['./basket-page.component.css'],
  providers: [PizzaService, OrderService]
})
export class BasketPageComponent implements OnInit {
  private order: Order;
  private form: FormGroup;

  constructor(
    private pizzaService: PizzaService,
    private cartService: CartService,
    private formBuilder: FormBuilder,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.buildForm();
    this.order = new Order();
    this.order.pizzas = [];
    const selectedPizzas = this.cartService.getPizzas();
    selectedPizzas.forEach(selectedPizza => {
      this.pizzaService.GetPizza(selectedPizza.pizzaId).subscribe( pizza => {
        selectedPizza.selectedPrices.forEach(priceInOrder => {
          const priceForAdding = pizza.prices.find(price =>
            price.thickness === priceInOrder.thickness
            && price.size === priceInOrder.size
          );
          if (priceForAdding) {
            const pizzaForAdding = new Pizza();
            pizzaForAdding.id = pizza.id;
            pizzaForAdding.name = pizza.name;
            pizzaForAdding.description = pizza.description;
            pizzaForAdding.imgPath = pizza.imgPath;
            pizzaForAdding.ingredients = pizza.ingredients;
            pizzaForAdding.prices = [];
            priceForAdding.count = priceInOrder.count;
            pizzaForAdding.prices.push(priceForAdding);
            this.order.pizzas.push(pizzaForAdding);
          }
        });
      });
    });
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].hasError(errorName);
  }

  private buildForm(): void {
    this.form = this.formBuilder.group({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required),
    });
  }

  getPrice(prices: PizzaPrice[]): PizzaPrice {
    return prices[0];
  }

  reduceAmount(pizza: Pizza, price: PizzaPrice): void {
    const currentPizza = this.order.pizzas.find(value => {
      const currentPrice = value.prices.find(priceValue => priceValue.size === price.size && priceValue.thickness === price.thickness);
      return pizza.id === value.id && currentPrice;
    });
    if (currentPizza) {
      const currentPrice = currentPizza.prices.find(value => value.size === price.size && price.thickness === value.thickness);
      if (currentPrice) {
        if (currentPrice.count === 1) {
          const indexPizza = this.order.pizzas.findIndex(value => pizza.id === value.id);
          if (indexPizza !== -1) {
            this.order.pizzas.splice(indexPizza, 1);
          }
        } else {
          currentPrice.count -= 1;
        }
        this.cartService.removeToCart(pizza.id, price.size, price.thickness, 1);
      }
    }
  }

  increaseAmount(pizza: Pizza, price: PizzaPrice): void {
    const currentPizza = this.order.pizzas.find(value => {
      const currentPrice = value.prices.find(priceValue => priceValue.size === price.size && priceValue.thickness === price.thickness);
      return pizza.id === value.id && currentPrice;
    });
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

  getTotalPrice() {
    let total = 0;
    this.order.pizzas.forEach(pizza => {
      total += pizza.prices[0].cost * pizza.prices[0].count;
    });
    return total;
  }

  saveOrder() {
    if (this.form.valid && this.order.pizzas.length !== 0) {
      this.order.total = 0;
      this.order.accountId = 0;
      this.order.name = this.form.controls['name'].value;
      this.order.surname = this.form.controls['surname'].value;
      this.order.phone = this.form.controls['phone'].value;
      this.order.address = this.form.controls['address'].value;
      this.order.pizzas.forEach(pizza => {
        this.order.total += pizza.prices[0].cost * pizza.prices[0].count;
      });
      this.orderService.saveOrder(this.order).subscribe( () => {
        alert('Ваш заказ принят');
        this.order = new Order();
        this.order.pizzas = [];
        this.buildForm();
      });
    }
  }
}
