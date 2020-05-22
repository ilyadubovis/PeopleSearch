using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PeopleSearch.Models;

namespace PeopleSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly string _photoFolder;
        public PersonController(IPersonRepository repository, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
            if (hostEnvironment != null)
                _photoFolder = Path.Combine(hostEnvironment.WebRootPath, "assets", "images", "photos");
        }

        [HttpGet("{filter?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Person>> Get(string filter = null)
        {
            FakeDataRetrievalDelay();
            var people  = _repository.GetPeople();
            if (filter != null)
            {
                people = people.ToList().FindAll(p =>
                    p.FirstName.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                    p.LastName.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                    $"{p.FirstName} {p.LastName }".Contains(filter, StringComparison.InvariantCultureIgnoreCase));
            }
            return Ok(people);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Person>> Create(Person person)
        {
            if(person != null && ModelState.IsValid)
            {
                string photoFileName = null;
                if(person.Photo != null)
                {
                    photoFileName = $"{Guid.NewGuid().ToString()}_{person.Photo.FileName}";
                    var photoFilePath = Path.Combine(_photoFolder, photoFileName);
                    await person.Photo.CopyToAsync(new FileStream(photoFilePath, FileMode.Create));
                }
                person.PhotoFile = photoFileName;
                _repository.Add(person);
                return Ok(person);
            }
            return BadRequest("Person data is null.");
        }

        [HttpPut]
        public async void Update(int personId, Person person)
        {
            if (person != null && ModelState.IsValid)
            {
                if (person.Photo != null)
                {
                    var photoFilePath = Path.Combine(_photoFolder, person.PhotoFile);
                    await person.Photo.CopyToAsync(new FileStream(photoFilePath, FileMode.Create));
                }
                _repository.Update(personId, person);
            }
        }

        [HttpDelete]
        public void Delete(int personID) =>
            _repository.Delete(personID);

        private void FakeDataRetrievalDelay()
        {
            if (_configuration == null) return;
            try
            {
                var dataRetrievalDelay = int.Parse(_configuration["DataRetrievalDelay"]);
                Thread.Sleep(dataRetrievalDelay);
            }
            catch (Exception) { } // if DataRetrievalDelay value is missing or invalid => do nothing
        }
    }
}