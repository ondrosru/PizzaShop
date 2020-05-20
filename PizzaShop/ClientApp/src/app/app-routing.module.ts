import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { BasketPageComponent } from './basket-page/basket-page.component';


export const routes: Routes = [
  {path: '', component: HomePageComponent},
  {path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule)},
  {path: 'basket', component: BasketPageComponent}
];
