using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile: Profile
    {
        public RegionsProfile()
        {
            //CreateMap takes 2 type source & destination
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();
        }
    }
}
