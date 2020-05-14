import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { routes } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ErrorInterceptorService } from 'src/app/HttpServices/ErrorInterceptorService';
import { HttpInterceptorService } from 'src/app/HttpServices/HttpInterceptorService';
import { MatFormFieldModule, MatCardModule, MatInputModule, MatButtonModule } from '@angular/material';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { AuthService } from 'src/app/HttpServices/AuthService';
import { AccountService } from './HttpServices/AccountService';
import { AdminPageComponent } from './components/admin-page/admin-page.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { MenuNavComponent } from './components/menu-nav/menu-nav.component';
import { EditUserPageComponent } from './components/edit-user-page/edit-user-page.component';
import { UserListPageComponent } from './components/user-list-page/user-list-page.component';
import { PizzaListPageComponent } from './components/pizza-list-page/pizza-list-page.component';
import { EditPizzaPageComponent } from './components/edit-pizza-page/edit-pizza-page.component';
import { OrderListPageComponent } from './components/order-list-page/order-list-page.component';
import { MatSelectModule } from '@angular/material/select';
import { HttpService } from './HttpServices/HttpService';


@NgModule({
  declarations: [
    AppComponent,
    AdminPageComponent,
    SignInComponent,
    HomePageComponent,
    MenuNavComponent,
    EditUserPageComponent,
    UserListPageComponent,
    PizzaListPageComponent,
    EditPizzaPageComponent,
    OrderListPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
    MatFormFieldModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true},
    {provide: AuthService}, {provide: AccountService}, {provide: HttpService}],
  bootstrap: [AppComponent]
})
export class AppModule { }
