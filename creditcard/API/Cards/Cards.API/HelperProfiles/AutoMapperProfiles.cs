﻿using AutoMapper;
using Cards.API.DTO_domainModels;
using Cards.API.Models;

namespace Cards.API.HelperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Card, CardDto>().ReverseMap();
        }
    }
}