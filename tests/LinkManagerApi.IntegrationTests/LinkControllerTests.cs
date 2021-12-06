using System;
using System.Runtime.Serialization;
using System.Net;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;
using LinkManagerApi.Routes;
using FluentAssertions;
using System.Collections.Generic;
using LinkManagerApi.Dtos;
using System.Text.Json;
using System.Net.Http.Json;
using LinkManagerApi.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LinkManagerApi.IntegrationTests
{
    public class LinkControllerTests : IntegrationTest
    {
        
        [Fact]
        public async Task GetAll_WithNoParameter_ReturnEmptyResponse()
        {
            var response= await client.GetAsync(ApiRoutes.Links.GetAll);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content =await response.Content.ReadFromJsonAsync<List<LinkResponseDto>>() ;
            // content.Should().BeEmpty();
            content.Count.Should().Be(3);
        }

        [Fact]
        public async Task Get_ExistingLink_ReturnALinkResponseDto()
        {
            //Given
            var command= new CreateLinkCommand{
                Title=Guid.NewGuid().ToString(),
                Argument=Guid.NewGuid().ToString(),
                FileName=Guid.NewGuid().ToString(),
                Order=1,
                ShortDescription=Guid.NewGuid().ToString()
            };
            var createdLink=await CreateALink(command);
            //When
            var response= await client.GetAsync(ApiRoutes.Links.Get.Replace("{id}",createdLink.Id.ToString()));
            //Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var deserializedResponse=await response.Content.ReadFromJsonAsync<LinkResponseDto>();
            deserializedResponse.Should().BeEquivalentTo(createdLink,options=> 
                options.ComparingByMembers<LinkResponseDto>().ExcludingMissingMembers());
        }
    }
}