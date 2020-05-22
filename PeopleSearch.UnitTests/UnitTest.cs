using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearch.Controllers;
using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace PeopleSearch.UnitTests
{
    [TestClass]
    public class UnitTest
    {
        private PersonController _controller;

        [TestInitialize]
        public void TestInit()
        {
            _controller = new PersonController(new TestPersonRepository(), null, null);
        }

        [DataRow("Jo", 1)]
        [DataRow("Jane", 1)]
        [DataRow("Doe", 2)]
        [DataRow("", 2)]
        [DataRow("Abc", 0)]
        [DataTestMethod]
        public void DataTestMethod(string searchString, int expectedItemsCount)
        {
            var result = _controller.Get(searchString).Result;
            var value = (result as OkObjectResult).Value as IEnumerable<Person>;
            Assert.AreEqual(value.Count(), expectedItemsCount);
        }

        [TestMethod]
        public void TestAdd()
        {
            var result = _controller.Create(null).GetAwaiter().GetResult().Result;
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            var person = new Person
            {
                FirstName = "Joe",
                LastName = "Smith",
                BirthDate = new DateTime(1977, 3, 27),
                StreetAddress = "111 Main Steet",
                City = "Chicago",
                State = "IL",
                ZipCode = "60606",
                Interests = new List<Interest>()
                {
                    new Interest
                    {
                        Id = 1,
                        Name = "Fishing"
                    }
                }
            };

            result = _controller.Create(person).GetAwaiter().GetResult().Result;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            result = _controller.Get().Result;
            var value = (result as OkObjectResult).Value as IEnumerable<Person>;
            Assert.AreEqual(value.Count(), 3);

            result = _controller.Get("Smith").Result;
            value = (result as OkObjectResult).Value as IEnumerable<Person>;
            Assert.AreEqual(value.Count(), 1);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var person = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1963, 3, 27),
                StreetAddress = "555 North Ave",
                City = "Los Angeles",
                State = "CA",
                ZipCode = "90005",
                Interests = new List<Interest>()
                {
                    new Interest
                    {
                        Id = 1,
                        Name = "Fishing"
                    },
                    new Interest
                    {
                        Id = 2,
                        Name = "Biking"
                    },
                    new Interest
                    {
                        Id = 3,
                        Name = "Hunting"
                    },
                }
            };

            _controller.Update(1, person);

            var result = _controller.Get("John").Result;
            var value = (result as OkObjectResult).Value as IEnumerable<Person>;
            Assert.AreEqual(value.Count(), 1);
            Assert.AreEqual(value.ElementAt(0).StreetAddress, "555 North Ave");
            Assert.AreEqual(value.ElementAt(0).Interests.Count(), 3);
        }

        [TestMethod]
        public void TestDelete()
        {
            _controller.Delete(1);

            var result = _controller.Get("John").Result;
            var value = (result as OkObjectResult).Value as IEnumerable<Person>;
            Assert.AreEqual(value.Count(), 0);
        }
    }

    class TestPersonRepository : IPersonRepository
    {
        private List<Person> _people = new List<Person>()
        {
            new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1963, 3, 27),
                StreetAddress = "234 Main Steet",
                City = "Los Angeles",
                State = "CA",
                ZipCode = "90005",
                Interests = new List<Interest>()
                {
                    new Interest
                    {
                        Id = 1,
                        Name = "Fishing"
                    },
                    new Interest
                    {
                        Id = 2,
                        Name = "Biking"
                    }
                }
            },
            new Person
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                BirthDate = new DateTime(1975, 11, 2),
                StreetAddress = "123 Church Steet",
                City = "Chicago",
                State = "IL",
                ZipCode = "60601",
                Interests = new List<Interest>()
                {
                    new Interest
                    {
                        Id = 3,
                        Name = "Cooking"
                    },
                    new Interest
                    {
                        Id = 4,
                        Name = "Reading"
                    },
                    new Interest
                    {
                        Id = 5,
                        Name = "Dancing"
                    }
                }
            }
        };
        
        public Person Add(Person person)
        {
            person.Id = _people.Max(p => p.Id) + 1;
            _people.Add(person);
            return person;
        }

        public void Delete(int id) => _people.RemoveAll(p => p.Id == id);

        public IEnumerable<Person> GetPeople() => _people;

        public Person GetPerson(int id) => _people.SingleOrDefault(p => p.Id == id);
       
        public void Update(int personId, Person personChanges)
        {
            var index = _people.FindIndex(p => p.Id == personId);
            if(index >= 0)
            _people[index] = personChanges;
        }
    }
}
