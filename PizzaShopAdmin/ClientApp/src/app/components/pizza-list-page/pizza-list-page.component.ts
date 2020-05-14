import { Component, OnInit } from '@angular/core';
import { PizzaDto } from 'src/dto/Pizza/PizzaDto';

@Component({
  selector: 'app-pizza-list-page',
  templateUrl: './pizza-list-page.component.html',
  styleUrls: ['./pizza-list-page.component.css']
})
export class PizzaListPageComponent implements OnInit {
  public pizzas: PizzaDto[];

  constructor() { }

  ngOnInit(): void {
  }

}
