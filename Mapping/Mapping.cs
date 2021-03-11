using System.Linq;
using AutoMapper;
using epoll.Domain.Models;
using epoll.Resources;

namespace epoll.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            // Map models to resources to send to the client


            CreateMap<Poll, GetAllPollResource>();

            CreateMap<PollOption, PollOptionResource>();

            CreateMap<Poll, GetSinglePollResource>();
        }
    }

    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            // Map new poll from client to the model
            CreateMap<SavePollResource, Poll>()
                .ForMember(
                    dest => dest.Options,
                    m => m.MapFrom(
                        src => src.Options.Select(title => new PollOption{ Title =  title })
                    )
                );
        }
    }
}