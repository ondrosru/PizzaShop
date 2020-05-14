import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountDto } from 'src/dto/Account/AccountDto';
import { AuthService } from 'src/app/HttpServices/AuthService';
import { Role } from 'src/dto/Account/Enum/Role';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  public accountDataSubscribtion: any;
  public accountData = new AccountDto();

  constructor(private router: Router, private authService: AuthService) {
    this.accountDataSubscribtion = authService.user.subscribe(data => {
      this.accountData = data;
    });
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (this.accountData.role === Role.Admin) {
        return true;
      }
      this.router.navigate(['/auth'], {queryParams: {returnUrl: state.url}});
      return false;
    }
}
