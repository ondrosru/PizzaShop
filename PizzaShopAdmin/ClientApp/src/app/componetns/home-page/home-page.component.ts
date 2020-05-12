import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/HttpServices/AccountService';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
  providers: [AccountService]
})
export class HomePageComponent implements OnInit {
  userData: string;

  constructor(private userService: AccountService) { }

  ngOnInit(): void {
  }

  fetchUserData() {
    this.userService.getAccountData().subscribe(
      (result: string) => {
        this.userData = result;
      }
    );
  }

}
