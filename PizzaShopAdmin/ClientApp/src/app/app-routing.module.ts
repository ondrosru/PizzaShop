import { Routes } from '@angular/router';
import { HomePageComponent } from './componetns/home-page/home-page.component';
import { AdminGuard } from './guards/admin.guard';
import { SignInComponent } from './auth/sign-in/sign-in.component';

export const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: 'auth', component: SignInComponent},
  {path: 'home', component: HomePageComponent, canActivate: [AdminGuard]}
];
