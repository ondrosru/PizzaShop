import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { routes } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomePageComponent } from './componetns/home-page/home-page.component';
import { ErrorInterceptorService } from 'src/HttpServices/ErrorInterceptorService';
import { HttpInterceptorService } from 'src/HttpServices/HttpInterceptorService';
import { MatFormFieldModule, MatCardModule, MatInputModule, MatButtonModule } from '@angular/material';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { AuthService } from 'src/HttpServices/AuthService';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    SignInComponent
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
    MatButtonModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true},
    {provide: AuthService}],
  bootstrap: [AppComponent]
})
export class AppModule { }
