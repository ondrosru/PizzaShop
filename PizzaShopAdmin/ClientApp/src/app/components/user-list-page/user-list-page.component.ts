import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/HttpServices/AccountService';
import { AccountDto } from 'src/dto/Account/AccountDto';

@Component({
  selector: 'app-user-list-page',
  templateUrl: './user-list-page.component.html',
  styleUrls: ['./user-list-page.component.css']
})
export class UserListPageComponent implements OnInit {
  public accounts: AccountDto[];

  constructor(private accountService: AccountService) {
    this.accountService.getAccounts().subscribe( values => {
      this.accounts = values;
    });
   }

  ngOnInit(): void {
  }
}
