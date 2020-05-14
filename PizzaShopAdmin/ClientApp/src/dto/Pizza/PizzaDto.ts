import { PriceDto } from './PriceDto';
import { IngredientDto } from './IngredientDto';

export class PizzaDto {
    public id: number;
    public name: string;
    public description: string;
    public prices?: PriceDto[];
    public imgPath: string;
    public ingredients?: IngredientDto[];
}
