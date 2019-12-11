using AddressBook.DTOs;
using AddressBook.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressBook.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Person, PersonDTO>();
            Mapper.CreateMap<PersonDTO, Person>();
        }
    }
}