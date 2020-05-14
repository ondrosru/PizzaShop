import { Component, OnInit } from '@angular/core';
import { AccountDto } from 'src/dto/Account/AccountDto';
import { AccountService } from 'src/app/HttpServices/AccountService';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from 'src/dto/Account/Enum/Role';
import { ErrorData } from 'src/dto/Error/errorData';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-edit-user-page',
  templateUrl: './edit-user-page.component.html',
  styleUrls: ['./edit-user-page.component.css']
})
export class EditUserPageComponent implements OnInit {
  private _router: Router;
  public newUser: AccountDto;
  public roles = Role;
  public errors: ErrorData[];

  public constructor(private accountService: AccountService, route: ActivatedRoute, router: Router) {
    this._router = router;
    route.params.subscribe(params => {
      const userId: number | undefined = params['Id'] !== undefined
        ? Number(params['Id'])
        : 0;
      this.accountService.getUser(userId).subscribe(value => {
        this.newUser = value;
        if (this.newUser.role == null) {
          this.newUser.role = Role.Client;
        }
      });
    });
    this.errors = [];
  }

  public hasError(errorName: string): boolean {
    const error = this.errors.find(value => value.error === errorName);
    if (error) {
      return true;
    }
    return false;
  }

  public getErrorMessage(errorName: string): string {
    const error = this.errors.find(value => value.error === errorName);
    if (error) {
      return error.message;
    }
    return '';
  }

  ngOnInit(): void {
  }

  public saveUser(): void {
    this.accountService.saveUser(this.newUser).subscribe(value => {
      this.errors = [];
      if (value.errors.length !== 0) {
        this.errors = value.errors;
      } else {
      this._router.navigate(['/admin/user-list']);
      }
    });
  }
}
