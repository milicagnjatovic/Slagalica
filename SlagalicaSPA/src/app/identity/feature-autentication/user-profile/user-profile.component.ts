import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { IAppState } from 'src/app/shared/app-state/app-state';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  public appStateOb: Observable<IAppState>;

  constructor(private appStateService: AppStateService){
    this.appStateOb = this.appStateService.getAppState();
  }

  ngOnInit(): void {
    this.appStateOb = this.appStateService.getAppState();
  }

}
