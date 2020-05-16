import { PizzaSize } from './Enums/PizzaSize';
import { DoughThickness } from './Enums/DoughThickness';

export class PriceDto {
    public id: number;
    public size: PizzaSize;
    public doughThickness: DoughThickness;
    public cost: number;
    public weight: number;
    public checked?: boolean;
}
