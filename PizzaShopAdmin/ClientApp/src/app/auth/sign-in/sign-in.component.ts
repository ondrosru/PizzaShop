import { Component, OnInit } from '@angular/core';
import { LoginDto } from 'src/dto/User/LoginDto';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  loginData: LoginDto;

  constructor() { }

  ngOnInit(): void {
  }

}
