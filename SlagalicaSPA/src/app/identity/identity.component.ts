import { Component } from '@angular/core';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';
import { Observable } from 'rxjs';
import { IAppState } from 'src/app/shared/app-state/app-state';

@Component({
  selector: 'app-identity',
  templateUrl: './identity.component.html',
  styleUrls: ['./identity.component.css']
})
export class IdentityComponent {
  public appStateOb: Observable<IAppState>;

  constructor(private appStateService: AppStateService){
    this.appStateOb = this.appStateService.getAppState();
  }

}
