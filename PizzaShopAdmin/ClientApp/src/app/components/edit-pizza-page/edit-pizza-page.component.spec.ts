import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPizzaPageComponent } from './edit-pizza-page.component';

describe('EditPizzaPageComponent', () => {
  let component: EditPizzaPageComponent;
  let fixture: ComponentFixture<EditPizzaPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPizzaPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPizzaPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
