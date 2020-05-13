import { Component, OnInit } from '@angular/core';
import { AccountDto } from 'src/dto/Account/AccountDto';
import { AccountService } from 'src/app/HttpServices/AccountService';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-user-page',
  templateUrl: './edit-user-page.component.html',
  styleUrls: ['./edit-user-page.component.css']
})
export class EditUserPageComponent implements OnInit {
  public newUser: AccountDto;

  public constructor(private accountService: AccountService, route: ActivatedRoute) {
    route.params.subscribe(params => {
      const userId: number | undefined = params['Id'] !== undefined
        ? Number(params['Id'])
        : 0;
      this.accountService.getUser(userId).subscribe(value => {
        this.newUser = value;
      });
    });
  }

  ngOnInit(): void {
  }

  public saveUser(): void {
    this.accountService.saveUser(this.newUser).subscribe(value => {
      alert('Сохранен ' + value.id + ' ' + value.username);
    });
  }
}
