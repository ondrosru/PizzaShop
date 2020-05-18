import { Component, OnInit, AfterViewInit, OnDestroy, ViewChild, ComponentFactoryResolver, ViewContainerRef } from '@angular/core';
import { Subscription, BehaviorSubject, Observable } from 'rxjs';
import { PopupDirective } from '../popupDirective';
import { PopupSerivce } from 'src/app/Services/PopupService';
import { AddIngredientComponent } from '../add-ingredient/add-ingredient.component';
import { IngredientService } from 'src/app/HttpServices/IngredientService';
import { IngredientDto } from 'src/dto/Pizza/IngredientDto';
import { PizzaSize } from 'src/dto/Pizza/Enums/PizzaSize';
import { PizzaDto } from 'src/dto/Pizza/PizzaDto';
import { PriceDto } from 'src/dto/Pizza/PriceDto';
import { DoughThickness } from 'src/dto/Pizza/Enums/DoughThickness';
import { ImageService } from 'src/app/HttpServices/ImageService';
import { Router, ActivatedRoute } from '@angular/router';
import { PizzaService } from 'src/app/HttpServices/PizzaSerivce';
import { FormControl, FormBuilder } from '@angular/forms';

export interface PopupComponent {
  data: any;
  close: any;
}
@Component({
  selector: 'app-edit-pizza-page',
  templateUrl: './edit-pizza-page.component.html',
  styleUrls: ['./edit-pizza-page.component.css'],
  providers: [IngredientService, ImageService, PizzaService]
})
export class EditPizzaPageComponent implements OnInit, AfterViewInit, OnDestroy {
  subscription: Subscription;
  private sizes = PizzaSize;
  private prices: PriceDto[];
  private thickness = DoughThickness;
  private ingredietns: IngredientDto[];
  private pizza: PizzaDto;
  @ViewChild(PopupDirective) popupHost: PopupDirective;
  private imageFile: File;
  private imageСhanged: boolean;
  image: ArrayBuffer;
  private smallPizza: boolean;
  private mediumPizza: boolean;
  private bigPizza: boolean;
  public error: boolean;

  constructor(private popupService: PopupSerivce,
    private componentFactoryResolver: ComponentFactoryResolver,
    private viewContainerRef: ViewContainerRef,
    private ingredientService: IngredientService,
    private imageSerivce: ImageService,
    private pizzaService: PizzaService,
    private route: ActivatedRoute,
    private _router: Router) {
      this.pizza = new PizzaDto();
      this.imageСhanged = false;
      this.smallPizza = false;
      this.mediumPizza = false;
      this.bigPizza = false;
      this.imageFile = null;
      this.ingredietns = [];
      this.prices = [];
      Object.keys(PizzaSize).filter((type) => isNaN(<any>type) && type !== 'values').forEach(size => {
        Object.keys(DoughThickness).filter((type) => isNaN(<any>type) && type !== 'values').forEach(thicknes => {
          const price = new PriceDto();
          price.doughThickness = DoughThickness[thicknes];
          price.id = 0,
          price.size = PizzaSize[size];
          price.cost = 0;
          price.weight = 0;
          price.checked = false;
          this.prices.push(price);
        });
      });
      route.params.subscribe(params => {
        const pizzaId: number | undefined = params['Id'] !== undefined
          ? Number(params['Id'])
          : 0;
        this.pizzaService.GetPizza(pizzaId).subscribe(value => {
          this.pizza = value;
          this.ingredientService.getIngredients().subscribe(values => {
            values.forEach(element => {
              if (this.pizza.ingredients.findIndex(ingredient => ingredient.id === element.id) === -1) {
                this.ingredietns.push(element);
              }
            });
          });
          this.pizza.prices.forEach(price => {
            const index = this.prices.findIndex(oldPrice =>
              oldPrice.doughThickness === price.doughThickness && oldPrice.size === price.size);
            if (index !== -1) {
              price.checked = true;
              if (price.size === PizzaSize.Small) {
                this.smallPizza = true;
              } else if (price.size === PizzaSize.Middle) {
                this.mediumPizza = true;
              } else if (price.size === PizzaSize.Big) {
                this.bigPizza = true;
              }
              this.prices[index] = price;
            }
          });
          if (this.pizza.imgPath !== '' && this.pizza.imgPath ) {
            this.imageSerivce.getImage(this.pizza.imgPath).subscribe(
                imageValue => {
                  const reader = new FileReader();
                  reader.readAsDataURL(imageValue);
                  reader.onload = (event1) => {
                    this.image = event1.target.result as ArrayBuffer;
                  };
                }
            );
          }
        });
      });
  }

  public getPrice(thickness: DoughThickness, size: PizzaSize): PriceDto {
    return this.prices.find(value => {
      return value.doughThickness === thickness && value.size === size;
    });
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngAfterViewInit() {
    this.subscription = this.popupService.popupDialog$.subscribe((data) => {
      if (!!data && data.popupEvent === 'open') {
        this.open(data);
      } else if (data.popupEvent === 'close') {
        this.close();
      }
    });
  }

  open(data) {
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(data.component);
    this.viewContainerRef = this.popupHost.viewContainerRef;
    this.viewContainerRef.clear();
    const componentRef = this.viewContainerRef.createComponent(componentFactory);
    (<PopupComponent>componentRef.instance).data = data.options;
  }

  close() {
    this.ingredientService.getIngredients().subscribe(values => {
      this.ingredietns = values;
    });
    this.viewContainerRef.detach(0);
  }

  selectIngredient(id: number) {
    const index = this.ingredietns.findIndex(value => value.id === id);
    this.pizza.ingredients.push(this.ingredietns[index]);
    this.ingredietns.splice(index, 1);
  }

  deselectIngredient(id: number) {
    const index = this.pizza.ingredients.findIndex(value => value.id === id);
    this.ingredietns.push(this.pizza.ingredients[index]);
    this.pizza.ingredients.splice(index, 1);
  }

  addIngredient() {
    this.popupService.open(AddIngredientComponent, {});
  }

  onFileSelected(event) {
    this.imageFile = <File>event.target.files[0];
    const fd = new FormData();
    const reader = new FileReader();
    reader.readAsDataURL(this.imageFile);

    reader.onload = (event1) => {
      this.image = event1.target.result as ArrayBuffer;
    };
    this.imageСhanged = true;
  }

  public savePizza() {
    this.pizza.prices = [];
    this.prices.forEach(value => {
      if (value.checked) {
        this.pizza.prices.push(value);
      }
    });
    if (this.pizza.ingredients.length === 0
      || this.pizza.prices.length === 0
      || this.pizza.name === ''
      || this.pizza.description === '') {
      this.error = true;
      return;
    }
    if (this.imageСhanged) {
        const fd = new FormData();
        fd.append(this.imageFile.name, this.imageFile);
        this.imageSerivce.saveImage(fd).subscribe(value => {
          this.pizza.imgPath = value;
          this.pizzaService.SavePizza(this.pizza).subscribe( responsValue => {
            this._router.navigate(['/admin/pizza-list']);
          });
        });
    } else {
      if (this.image) {
        this.pizzaService.SavePizza(this.pizza).subscribe( responsValue => {
          this._router.navigate(['/admin/pizza-list']);
        });
      } else {
        this.pizza.imgPath = '';
        this.pizzaService.SavePizza(this.pizza).subscribe( responsValue => {
          this._router.navigate(['/admin/pizza-list']);
        });
      }
    }
    this.error = false;
  }
}
