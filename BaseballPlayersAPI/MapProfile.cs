using AutoMapper;
using BaseballPlayersAPI.DbModels;
using BaseballPlayersAPI.Models;

namespace BaseballPlayersAPI;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<PlayerInfo, PlayerInfoDb>();
        CreateMap<PlayerInfoDb, PlayerInfo>();
    }
}