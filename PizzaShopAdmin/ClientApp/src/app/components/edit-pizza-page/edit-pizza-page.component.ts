import { Component, OnInit, AfterViewInit, OnDestroy, ViewChild, ComponentFactoryResolver, ViewContainerRef } from '@angular/core';
import { Subscription, BehaviorSubject, Observable } from 'rxjs';
import { PopupDirective } from '../popupDirective';
import { PopupSerivce } from 'src/app/Services/PopupService';
import { AddIngredientComponent } from '../add-ingredient/add-ingredient.component';
import { IngredientService } from 'src/app/HttpServices/IngredientService';
import { IngredientDto } from 'src/dto/Pizza/IngredientDto';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';

export interface PopupComponent {
  data: any;
  close: any;
}
@Component({
  selector: 'app-edit-pizza-page',
  templateUrl: './edit-pizza-page.component.html',
  styleUrls: ['./edit-pizza-page.component.css'],
  providers: [IngredientService]
})
export class EditPizzaPageComponent implements OnInit, AfterViewInit, OnDestroy {
  subscription: Subscription;
  private ingredietns: IngredientDto[];
  private pizzaForm: FormGroup;
  @ViewChild(PopupDirective) popupHost: PopupDirective;
  selectedFile: File = null;
  url: string;

  constructor(private popupService: PopupSerivce,
    private componentFactoryResolver: ComponentFactoryResolver,
    private viewContainerRef: ViewContainerRef,
    private ingredientService: IngredientService,
    private fromBuilder: FormBuilder) {
      this.ingredientService.getIngredients().subscribe(values => {
        this.ingredietns = values;
      });
      this.pizzaForm = this.fromBuilder.group({
        id: 0,
        name: new FormControl('', Validators.required),
        description: new FormControl('', Validators.required),
        prices: this.fromBuilder.array([], Validators.minLength(1)),
        imgPath: '',
        ingredients: this.fromBuilder.array([], Validators.minLength(1))
      });
  }

  get selectedIngredients(): FormArray {
    return <FormArray>this.pizzaForm.controls.ingredients;
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
      } else if ( data.popupEvent === 'close' ) {
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
    this.ingredientService.getIngredients().subscribe( values => {
      this.ingredietns = values;
    });
    this.viewContainerRef.detach(0);
  }

  selectIngredient(id: number) {
    const index = this.ingredietns.findIndex(value => {
       return value.id === id;
      });
    this.selectedIngredients.push(new FormControl(this.ingredietns[index]));
    this.ingredietns.splice(index, 1);
  }

  deselectIngredient(id: number) {
    const index = this.selectedIngredients.value.findIndex(value => value.id === id);
    this.ingredietns.push(this.selectedIngredients.value[index]);
    this.selectedIngredients.removeAt(index);
  }

  addIngredient() {
    this.popupService.open(AddIngredientComponent, {});
  }

  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }

  onUpload() {
    const fd = new FormData();
    const reader = new FileReader();
    reader.readAsDataURL(this.selectedFile);

    reader.onload = (event) => {
      this.url = event.target.result as string;
    };
    // https://developer.mozilla.org/ru/docs/Web/API/FormData/append
    fd.append('image', this.selectedFile, this.selectedFile.name);

    // this.http.post('./api/test-api-for-upload', fd)
    //   .subscribe(res => {
    //     console.log('res: ', res);
    //   });


    // II
    // если ваш сервер поддерживает прием бинарных файлов, то вы можете отправить файл следующим образом:
    // this.http.post('./api/test-api-for-upload', this.selectedFile)

  }

}
