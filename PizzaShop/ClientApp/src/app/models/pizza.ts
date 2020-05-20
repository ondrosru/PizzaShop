import { PizzaPrice } from './pizza-price';
import { PizzaIngredient } from './pizza-ingredient';

export class Pizza {
    public id: number;
    public name: string;
    public description: string;
    public imgPath: string;
    public prices: PizzaPrice[];
    public ingredients: PizzaIngredient[];
    public image?: ArrayBuffer;
}
