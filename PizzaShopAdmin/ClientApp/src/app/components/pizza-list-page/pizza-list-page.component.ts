import { Component, OnInit } from '@angular/core';
import { PizzaDto } from 'src/dto/Pizza/PizzaDto';
import { PizzaService } from 'src/app/HttpServices/PizzaSerivce';

@Component({
  selector: 'app-pizza-list-page',
  templateUrl: './pizza-list-page.component.html',
  styleUrls: ['./pizza-list-page.component.css'],
  providers: [PizzaService]
})
export class PizzaListPageComponent implements OnInit {
  public pizzas: PizzaDto[];

  constructor(private _pizzaService: PizzaService) {
    this._pizzaService.GetPizzas().subscribe( values =>
      this.pizzas = values
    );
   }

  ngOnInit(): void {
  }

}
