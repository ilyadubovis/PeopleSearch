using System.Collections.Generic;

namespace PeopleSearch.Models
{
    public class SQLPersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;
        public SQLPersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public Person Add(Person person)
        {
            _context.Add(person);
            _context.SaveChangesAsync().GetAwaiter();
            return person;
        }

        public void Delete(int id)
        {
            var person = _context.People.Find(id);
            if (person != null)
            {
                _context.People.Remove(person);
                _context.SaveChangesAsync().GetAwaiter();
            }
        }

        public IEnumerable<Person> GetPeople() => _context.People;

        public Person GetPerson(int id) => _context.People.Find(id); 

        public void Update(int personId, Person personChanges)
        {
            var person = _context.People.Attach(personChanges);
            person.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChangesAsync().GetAwaiter();
        }
    }
}
