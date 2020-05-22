using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeopleSearch.Models
{
    public interface IPersonRepository
    {
        Person GetPerson(int id);
        IEnumerable<Person> GetPeople();
        Person Add(Person person);
        void Update(int id, Person personChanges);
        void Delete(int id);
    }
}
