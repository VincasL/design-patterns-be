using AutoMapper;
using BattleshipsApi.DTO;
using BattleshipsApi.Entities;

namespace BattleshipsApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameSession, GameData>();
            CreateMap<Player, PlayerDto>();
            CreateMap<Board, BoardDto>()
                .ForMember(x => x.Cells, opt => opt.Ignore());
            CreateMap<Cell, CellDto>();
        }
    }
}
