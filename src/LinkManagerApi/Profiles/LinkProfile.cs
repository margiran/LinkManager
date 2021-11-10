using AutoMapper;
using LinkManagerApi.Contracts.V1.Requests;
using LinkManagerApi.Contracts.V1.Responses;
using LinkManagerApi.Domain;

namespace LinkManagerApi.Profiles
{
    public class LinkProfile:Profile
    {
        public LinkProfile()
        {
            // s -> t
            CreateMap< Link, LinkResponse>();
            CreateMap< CreateLinkRequest, Link>();
            CreateMap< UpdateLinkRequest ,Link>();
            
        }
    }
}