import { Component, OnInit } from '@angular/core';
import { Person } from '../models/person.model';
import { PersonService } from './person.service';
import { Observable } from 'rxjs';
import { MatProgressBar } from '@angular/material/progress-bar';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatInput } from '@angular/material/input';

@Component({
  selector: 'app-list-people',
  templateUrl: './list-people.component.html',
  styleUrls: ['./list-people.component.css'],
})
export class ListPeopleComponent implements OnInit {
  people: Person[];
  searchString: string = "";

  constructor(private personService: PersonService) { }

  ngOnInit() {
    this.refresh();
  }

  refresh() {
    this.people = null;
    this.personService.getPeople(this.searchString)
      .subscribe(people => this.people = people);
  }

  applySearch(searchString: string) {
    this.searchString = searchString;
    this.refresh();
  }
}
