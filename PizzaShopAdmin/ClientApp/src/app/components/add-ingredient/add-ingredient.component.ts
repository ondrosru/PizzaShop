import { Component, Input, OnInit } from '@angular/core';
import { PopupSerivce } from 'src/app/Services/PopupService';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { IngredientService } from 'src/app/HttpServices/IngredientService';

export interface PopupComponent {
  data: any;
}

@Component({
  selector: 'app-add-ingredient',
  templateUrl: './add-ingredient.component.html',
  styleUrls: ['./add-ingredient.component.css']
})
export class AddIngredientComponent implements OnInit, PopupComponent {
  private form: FormGroup;
  @Input() data: {};
  constructor(private _popupService: PopupSerivce,
    private _formBuilder: FormBuilder,
    private _ingredientService: IngredientService) {}

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      id: new FormControl(0),
      name: new FormControl('', Validators.required)
    });
  }

  private saveIngredient() {
    if (!this.form.invalid) {
      this._ingredientService.saveIngredient(this.form.value).subscribe( () => {
        this.close();
      });
    }
  }

  private close() {
    this._popupService.close();
  }
}
