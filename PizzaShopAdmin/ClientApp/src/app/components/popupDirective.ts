import { Directive, ViewContainerRef, Inject } from '@angular/core';

@Directive({
  selector: '[popup-host]'
})

export class PopupDirective {
  constructor(public viewContainerRef: ViewContainerRef) {
  }
}
