import { Routes } from '@angular/router';
import { HomePageComponent } from './componetns/home-page/home-page.component';

export const routes: Routes = [
  {path: '', component: HomePageComponent},
  {path: 'auth', loadChildren: './auth/auth.module#AuthModule'}
];
