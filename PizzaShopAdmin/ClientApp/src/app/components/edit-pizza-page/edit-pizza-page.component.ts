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

export interface PopupComponent {
  data: any;
  close: any;
}
@Component({
  selector: 'app-edit-pizza-page',
  templateUrl: './edit-pizza-page.component.html',
  styleUrls: ['./edit-pizza-page.component.css'],
  providers: [IngredientService, ImageService]
})
export class EditPizzaPageComponent implements OnInit, AfterViewInit, OnDestroy {
  private _router: Router;
  subscription: Subscription;
  private sizes = PizzaSize;
  private prices: PriceDto[];
  private thickness = DoughThickness;
  private ingredietns: IngredientDto[];
  private pizza: PizzaDto = new PizzaDto();
  @ViewChild(PopupDirective) popupHost: PopupDirective;
  private imageFile: File;
  image: ArrayBuffer;
  private smallPizza: boolean;
  private mediumPizza: boolean;
  private bigPizza: boolean;
  public error: boolean;

  constructor(private popupService: PopupSerivce,
    private componentFactoryResolver: ComponentFactoryResolver,
    private viewContainerRef: ViewContainerRef,
    private ingredientService: IngredientService,
    private imageSerivce: ImageService) {
    this.imageFile = null;
    this.ingredientService.getIngredients().subscribe(values => {
      this.ingredietns = values;
    });
    this.prices = [];
    this.pizza.name = '';
    this.pizza.description = '';
    Object.keys(PizzaSize).filter((type) => isNaN(<any>type) && type !== 'values').forEach(size => {
      Object.keys(DoughThickness).filter((type) => isNaN(<any>type) && type !== 'values').forEach(thicknes => {
        const price = new PriceDto();
        price.doughThickness = DoughThickness[thicknes];
        price.size = PizzaSize[size];
        price.cost = 0;
        price.weight = 0;
        price.checked = false;
        this.prices.push(price);
      });
    });
  }

  public getPrice(thickness: DoughThickness, size: PizzaSize): PriceDto {
    return this.prices.find(value => {
      return value.doughThickness === thickness && value.size === size;
    });
  }

  ngOnInit(): void {
    this.smallPizza = false;
    this.mediumPizza = false;
    this.bigPizza = false;
    this.pizza.ingredients = [];
    this.pizza.prices = [];
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
  }

  public savePizza() {
    this.pizza.prices = [];
    this.pizza.imgPath = '';
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
    if (this.image) {
      const fd = new FormData();
      fd.append(this.imageFile.name, this.imageFile);
      this.imageSerivce.saveImage(fd).subscribe(value => {
        this.pizza.imgPath = value;
      });
    }
    this.error = false;
    console.log(this.pizza);
  }
}
