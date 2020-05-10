import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { SignInComponent } from './sign-in/sign-in.component';
import { MatFormFieldModule } from '@angular/material/form-field';


@NgModule({
  declarations: [SignInComponent],
  imports: [
    CommonModule,
    AuthRoutingModule,
    MatFormFieldModule
  ]
})
export class AuthModule { }
