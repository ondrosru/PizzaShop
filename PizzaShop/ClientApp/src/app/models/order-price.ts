import { PizzaThickness } from './Enums/pizza-thickness';
import { PizzaSize } from './Enums/pizza-size';

export class OrderPrice {
    public size: PizzaSize;
    public thickness: PizzaThickness;
    public count: number;
}
