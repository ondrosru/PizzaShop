import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from 'src/app/HttpServices/AuthService';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  providers: [AuthService]
})
export class SignInComponent implements OnInit {
  private readonly _authService: AuthService;
  private readonly _formBuilder: FormBuilder;
  private readonly _route: ActivatedRoute;
  private readonly _router: Router;
  private form: FormGroup;
  private loading = false;
  private submitted = false;

  constructor(formBuilder: FormBuilder,
    authService: AuthService,
    route: ActivatedRoute,
    router: Router) {
    this._formBuilder = formBuilder;
    this._authService = authService;
    this._route = route;
    this._router = router;
  }

  get loginFormControl() { return this.form.controls; }

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      username: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(25),
        Validators.pattern('^(?![0-9_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$')
      ])),
      password: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern('^(?=.*[a-zA-Z])(?=.*[0-9]).{8,}$')
      ]))
    });
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].hasError(errorName);
  }

  public signIn(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    const returnUrl = '/admin/home';
    this._authService.signIn(this.form.value)
    .pipe(first())
    .subscribe(
      () => {
        this._router.navigate([returnUrl]);
      },
      () => {
        this.loading = false;
        this.form.setErrors({
          invalidLogin: true
        });
      }
    );
  }
}
