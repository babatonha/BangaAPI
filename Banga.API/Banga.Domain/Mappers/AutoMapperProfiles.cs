using AutoMapper;
using Banga.Domain.DTOs;
using Banga.Domain.Models;

namespace Banga.Domain.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Message, MessageDTO>();
                //.ForMember(d => d.SenderPhotoUrl, o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}
