import { ReplaySubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class PopupSerivce {
    private popupDialog = new ReplaySubject<{popupEvent: string, component?: any, options?: {}}>();
    public popupDialog$ = this.popupDialog.asObservable();

    open(component: any, options?: any) {
        this.popupDialog.next({popupEvent: 'open', component: component, options: options});
    }

    close() {
        this.popupDialog.next({popupEvent: 'close'});
    }
}
