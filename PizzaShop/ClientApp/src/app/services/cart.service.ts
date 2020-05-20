import { Injectable } from '@angular/core';
import { OrderPizza } from '../models/order-pizza';
import { PizzaSize } from '../models/Enums/pizza-size';
import { PizzaThickness } from '../models/Enums/pizza-thickness';
import { OrderPrice } from '../models/order-price';

@Injectable()
export class CartService {
  private pizzas: OrderPizza[];

  constructor() {
    this.pizzas = [];
  }

  addToCart( pizzaId: number, size: PizzaSize, thickness: PizzaThickness, count: number ): OrderPizza  {
    const foundPizza = this.pizzas.find(value => value.pizzaId === pizzaId);
    if (foundPizza) {
      const priceIndex = foundPizza.selectedPrices.findIndex(value =>
        value.size === size
        && value.thickness === thickness
      );
      if (priceIndex === -1) {
        const newPrice = new OrderPrice();
        newPrice.count = count;
        newPrice.size = size;
        newPrice.thickness = thickness;
        foundPizza.selectedPrices.push(newPrice);
      } else {
        foundPizza.selectedPrices[priceIndex].count += count;
      }
    } else {
      const newPizza = new OrderPizza();
      newPizza.pizzaId = pizzaId;
      const newPrice = new OrderPrice();
      newPrice.count = count;
      newPrice.size = size;
      newPrice.thickness = thickness;
      newPizza.selectedPrices = [];
      newPizza.selectedPrices.push(newPrice);
      this.pizzas.push(newPizza);
    }
    return foundPizza;
  }

  setPizzaCount( pizzaId: number, size: PizzaSize, thickness: PizzaThickness, count: number )  {
    const foundPizza = this.pizzas.find(value => value.pizzaId === pizzaId);
    if (foundPizza) {
      const priceIndex = foundPizza.selectedPrices.findIndex(value =>
        value.size === size
        && value.thickness === thickness
      );
      if (priceIndex === -1) {
        const newPrice = new OrderPrice();
        newPrice.count = count;
        newPrice.size = size;
        newPrice.thickness = thickness;
        foundPizza.selectedPrices.push(newPrice);
      } else {
        foundPizza.selectedPrices[priceIndex].count = count;
      }
    } else {
      const newPizza = new OrderPizza();
      newPizza.pizzaId = pizzaId;
      const newPrice = new OrderPrice();
      newPrice.count = count;
      newPrice.size = size;
      newPrice.thickness = thickness;
      newPizza.selectedPrices = [];
      newPizza.selectedPrices.push(newPrice);
      this.pizzas.push(newPizza);
    }
    return foundPizza;
  }

  removeToCart( pizzaId: number, size: PizzaSize, thickness: PizzaThickness, count: number ): boolean {
    const foundPizza = this.pizzas.find(value => value.pizzaId === pizzaId);
    if (foundPizza) {
      const priceIndex = foundPizza.selectedPrices.findIndex(price => price.size === size && thickness === price.thickness);
      if (priceIndex !== -1) {
        if (foundPizza.selectedPrices[priceIndex].count <= count) {
          foundPizza.selectedPrices.splice(priceIndex, 1);
        } else {
          foundPizza.selectedPrices[priceIndex].count -= count;
        }
        return true;
      }
    }
    return false;
  }

  getPizzas(): OrderPizza[] {
    return this.pizzas;
  }

  clearCart() {
    this.pizzas = [];
  }
}
