using AutoMapper;
using Cards.API.DTOdomainModel;
using Cards.API.Models;

namespace Cards.API.HelperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //type of source is Card and type of Destination is CardDto and vice-versa
           CreateMap<Card, CardDto>().ReverseMap();
        }
    }
}
