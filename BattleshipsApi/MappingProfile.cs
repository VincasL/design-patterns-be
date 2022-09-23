using AutoMapper;
using BattleshipsApi.DTO;
using BattleshipsApi.Models;

namespace BattleshipsApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Player, AddPlayerDto>();
            CreateMap<AddPlayerDto, Player>();
        }
    }
}
