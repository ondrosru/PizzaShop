import { Routes, RouterModule } from '@angular/router';
import { AdminGuard } from './guards/admin.guard';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { AdminPageComponent } from './components/admin-page/admin-page.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { UserListPageComponent } from './components/user-list-page/user-list-page.component';
import { PizzaListPageComponent } from './components/pizza-list-page/pizza-list-page.component';
import { OrderListPageComponent } from './components/order-list-page/order-list-page.component';
import { EditUserPageComponent } from './components/edit-user-page/edit-user-page.component';

export const routes: Routes = [
  {path: '', redirectTo: 'admin/home', pathMatch: 'full'},
  {path: 'auth', component: SignInComponent},
  {path: 'admin', component: AdminPageComponent, canActivate: [AdminGuard],
      children: [
        {path: '', redirectTo: 'admin/home', pathMatch: 'full'},
        {path: '**', redirectTo: 'admin/home', pathMatch: 'full'},
        {path: 'home', component: HomePageComponent},
        {path: 'user-list', component: UserListPageComponent},
        {path: 'pizza-list', component: PizzaListPageComponent},
        {path: 'order-list', component: OrderListPageComponent},
        {path: 'user-list/edit-user', component: EditUserPageComponent},
        {path: 'user-list/edit-user/:Id', component: EditUserPageComponent},
      ]}
];
