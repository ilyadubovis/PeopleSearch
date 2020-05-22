import { Component, Output, EventEmitter } from '@angular/core';

import { ListPeopleComponent } from '../people/list-people.component';


@Component({
  providers: [ListPeopleComponent],
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  
}
