import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { HttpService } from './services/http.service';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ImageService } from './services/image.service';
import { routes } from './app-routing.module';
import { BasketPageComponent } from './basket-page/basket-page.component';
import { PizzaCardComponent } from './components/pizza-card/pizza-card.component';
import { CartService } from './services/cart.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomePageComponent,
    BasketPageComponent,
    PizzaCardComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [
    HttpService,
    ImageService,
    CartService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
