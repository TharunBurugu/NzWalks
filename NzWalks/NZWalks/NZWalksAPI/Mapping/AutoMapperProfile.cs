using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionDto,Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walks>().ReverseMap();
            CreateMap<Walks, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DiffucultyDto>().ReverseMap();
            CreateMap<UpdateWalkDto, Walks>().ReverseMap();
        }
    }
}
