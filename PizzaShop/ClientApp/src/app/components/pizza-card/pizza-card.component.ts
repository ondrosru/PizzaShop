import { Component, OnInit, Input, AfterViewChecked } from '@angular/core';
import { Pizza } from 'src/app/models/pizza';
import { ImageService } from 'src/app/services/image.service';
import { CartService } from 'src/app/services/cart.service';
import { PizzaSize } from 'src/app/models/Enums/pizza-size';
import { PizzaThickness } from 'src/app/models/Enums/pizza-thickness';

@Component({
  selector: 'app-pizza-card',
  templateUrl: './pizza-card.component.html',
  styleUrls: ['./pizza-card.component.css'],
})
export class PizzaCardComponent implements OnInit {
  @Input() pizzaData: Pizza;
  public image: ArrayBuffer;
  public selectedSize: PizzaSize;
  public selectedThickness: PizzaThickness;
  public selectSizes: PizzaSize[];
  public selectThickness: PizzaThickness[];

  constructor
  (
    private imageService: ImageService,
    private cartService: CartService,
  ) {
  }

  ngOnInit() {
    if (this.pizzaData != null) {
      this.selectedSize = this.pizzaData.prices[0].size;
      this.selectedThickness = this.pizzaData.prices[0].thickness;
      this.selectSizes = [];
      this.selectThickness = [];
      this.pizzaData.prices.forEach(value => {
        if (!this.selectSizes.includes(value.size)) {
          this.selectSizes.push(value.size);
        }
        if (!this.selectThickness.includes(value.thickness)) {
          this.selectThickness.push(value.thickness);
        }
      });
      this.imageService.getImage(this.pizzaData.imgPath).subscribe(image => {
        const reader = new FileReader();
        reader.readAsDataURL(image);
        reader.onload = (event) => {
          this.image = reader.result as ArrayBuffer;
        };
      });
    }
  }

  get selectedCost(): number {
    const price = this.pizzaData.prices.find(value => value.thickness === this.selectedThickness && value.size === this.selectedSize);
    if (price === undefined) {
      return 0;
    } else {
      return price.cost;
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

  toOrder() {
    this.cartService.addToCart(this.pizzaData.id, this.selectedSize, this.selectedThickness, 1);
  }
}
