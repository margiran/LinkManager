using AutoMapper;
using LinkManagerApi.Dtos;
using LinkManagerApi.Domain;
using LinkManagerApi.Commands;

namespace LinkManagerApi.Profiles;

public class LinkProfile : Profile
{
    public LinkProfile()
    {
        // s -> t
        CreateMap<Link, LinkResponseDto>();
        CreateMap<CreateLinkCommand, Link>();
        CreateMap<UpdateLinkCommand, Link>();

    }
}
