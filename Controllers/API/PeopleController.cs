using AddressBook.DAL;
using AddressBook.DTOs;
using AddressBook.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AddressBook.Controllers.API
{
    public class PeopleController : ApiController
    {
        private AddressContext _context;

        public PeopleController()
        {
            _context = new AddressContext();

        }
        
        // GET /api/people
        public IEnumerable<PersonDTO> GetPeople()
        {
            return _context.Person.ToList().Select(Mapper.Map<Person, PersonDTO>);
        }

        // GET /api/people/1
        public PersonDTO GetPerson(int id)
        {
            var person = _context.Person.SingleOrDefault(c => c.Id == id);
            if (person == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Person, PersonDTO>(person);
        }

        // POST /api/customers
        [HttpPost]
        public PersonDTO CreatePerson(PersonDTO personDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var person = Mapper.Map<PersonDTO, Person>(personDto);
            _context.Person.Add(person);
            _context.SaveChanges();

            personDto.Id = person.Id;
            return personDto;
        }

        // PUT /api/person/1
        [HttpPut]
        public void UpdatePerson(int id, PersonDTO personDTO)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var personInDB = _context.Person.SingleOrDefault(c=>c.Id == id);
            if(personInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(personDTO, personInDB);
            _context.SaveChanges(); 
        }

        //DELETE /api/person/1
        [HttpDelete]
        public void DeletePerson(int id)
        {
            var personInDB = _context.Person.SingleOrDefault(c => c.Id == id);
            if (personInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Person.Remove(personInDB);
            _context.SaveChanges();
        }
    }
}
