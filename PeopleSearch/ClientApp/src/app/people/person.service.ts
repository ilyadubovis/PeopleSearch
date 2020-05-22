import { Injectable, Inject } from "@angular/core";
import { Person } from "../models/person.model";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class PersonService  {
  private httpClient: HttpClient;
  private baseUrl: string;

  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = httpClient;
    this.baseUrl = baseUrl;
  }
  
  // CRUD requests

  getPeople(searchString: string = null): Observable<Person[]> {
    let query = 'api/person';
    if (searchString) query += '/' + searchString;
    return this.httpClient.get<Person[]>(`${this.baseUrl}${query}`)
      .pipe(catchError(this.handleError));
  }
  
  addPerson(person: Person) : Observable<Person> {
    return this.httpClient.post<Person>(`${this.baseUrl}/api/person`, person,
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }).
      pipe(catchError(this.handleError));
  }

  updatePerson(person: Person): Observable<void> {
    return this.httpClient.put<void>(`${this.baseUrl}/api/person/${person.id}`, person,
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }).
      pipe(catchError(this.handleError));
  }

  deletePerson(person: Person): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/api/person/${person.id}`,
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }).
      pipe(catchError(this.handleError));
  }

  private handleError(errorResponse: HttpErrorResponse): Observable<any> {
    if (errorResponse.error instanceof ErrorEvent) {
      console.error('Client side error:', errorResponse.error.message);
    }
    else {
      console.error('Server side error:', errorResponse);
    }
    return new Observable(null);
  }
}

