import { PizzaSize } from './Enums/pizza-size';
import { PizzaThickness } from './Enums/pizza-thickness';

export class PizzaPrice {
    public id: number;
    public size: PizzaSize;
    public thickness: PizzaThickness;
    public cost: number;
    public weight: number;
    public count?: number;
}
