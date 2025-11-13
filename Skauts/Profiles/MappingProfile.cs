using AutoMapper;
using Skauts.DTOs;
using Skauts.Models;

namespace Skauts.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Organization
            CreateMap<Organization, OrganizationDto>(); // De Entidade para DTO
            CreateMap<SalvarOrganizationDto, Organization>(); // De DTO de Salvar para Entidade

            // Championship
            CreateMap<Championship, ChampionshipDto>();
            CreateMap<SalvarChampionshipDto, Championship>();

            // EventType
            CreateMap<EventType, EventTypeDto>();
            CreateMap<SalvarEventTypeDto, EventType>();

            // Match
            CreateMap<Match, MatchDto>();
            CreateMap<SalvarMatchDto, Match>();

            // Event
            CreateMap<Event, EventDto>();
            CreateMap<SalvarEventDto, Event>();

            // Player
            CreateMap<Player, PlayerDto>();
            CreateMap<SalvarPlayerDto, Player>();

            // Role
            CreateMap<Role, RoleDto>();
            CreateMap<SalvarRoleDto, Role>();

            // PrizeType
            CreateMap<PrizeType, PrizeTypeDto>();
            CreateMap<SalvarPrizeTypeDto, PrizeType>();

            // PlayersPrize
            CreateMap<PlayersPrize, PlayersPrizeDto>();
            CreateMap<SalvarPlayersPrizeDto, PlayersPrize>();

            // TeamPlayer (Relação N:N)
            // Usa o próprio DTO para adicionar/atualizar
            CreateMap<TeamPlayer, TeamPlayerDto>().ReverseMap();

            // Team
            CreateMap<Team, TeamDto>();
            CreateMap<SalvarTeamDto, Team>();

            // User
            CreateMap<User, UserDto>();
            CreateMap<SalvarUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // O hash será tratado manualmente

            // UsersOrganization (Relação N:N)
            CreateMap<UsersOrganization, UsersOrganizationDto>().ReverseMap();
        }
    }
}