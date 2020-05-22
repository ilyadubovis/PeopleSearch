import { PersonService } from './person.service';
import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('Person Service', () => {
  let injector: TestBed;
  let service: PersonService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PersonService]
    });

    injector = getTestBed();
    service = injector.get(PersonService);
  });

  it('should get people for simple search string / get more than 0 items', () => {
      let peopleCount = service.getPeople("a");
      expect(peopleCount).toBeGreaterThan(0);
    });

  it('should get people for long search string / get 0 items', () => {
      let peopleCount = service.getPeople("verylongsearchstring");
      expect(peopleCount).toEqual(0);
    });
});
