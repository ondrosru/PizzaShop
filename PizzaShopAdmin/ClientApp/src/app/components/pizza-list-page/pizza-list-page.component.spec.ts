import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PizzaListPageComponent } from './pizza-list-page.component';

describe('PizzaListPageComponent', () => {
  let component: PizzaListPageComponent;
  let fixture: ComponentFixture<PizzaListPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PizzaListPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PizzaListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
