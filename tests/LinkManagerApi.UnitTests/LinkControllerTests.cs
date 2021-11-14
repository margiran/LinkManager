using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using LinkManagerApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using LinkManagerApi.Dtos;
using System.Linq;
using LinkManagerApi.Queries;
using LinkManagerApi.Commands;

namespace LinkManagerApi.UnitTests;

public class LinkControllerTests
{
    private readonly Mock<IMediator> mediatorStub = new();

    public LinkControllerTests()
    {

    }

    [Fact]
    public async Task GetAllLinksAsync_WithExistingLinks_ReturnAllLinks()
    {
        var expected = new[] { GenerateLink(), GenerateLink() };

        mediatorStub.Setup(m => m.Send(It.IsAny<GetAllLinksQuery>(), CancellationToken.None)).ReturnsAsync(expected);

        var sut = new LinkController(mediatorStub.Object);
        var result = await sut.GetAll("");

        var objectResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<LinkResponseDto>>(objectResult.Value);
        Assert.Equal(2, model.Count());
    }
    [Fact]
    public async Task GetLinkByIdAsync_WithExistingLink_ReturnLink()
    {
        var expected = GenerateLink();

        mediatorStub.Setup(m => m.Send(It.IsAny<GetLinkByIdQuery>(), CancellationToken.None))
            .ReturnsAsync(expected);

        var sut = new LinkController(mediatorStub.Object);
        var result = await sut.Get(Guid.NewGuid());

        var objectResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<LinkResponseDto>(objectResult.Value);
        model.Should().Be(expected);
    }
    [Fact]
    public async Task GetLinkByIdAsync_WithNotExistingLink_ReturnNotFound()
    {
        var expected = GenerateLink();

        mediatorStub.Setup(m => m.Send(It.IsAny<GetLinkByIdQuery>(), CancellationToken.None))
            .ReturnsAsync((LinkResponseDto)null);

        var sut = new LinkController(mediatorStub.Object);
        var result = await sut.Get(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task CreateLinkAsync_WithALink_ReturnLink()
    {
        var itemToCreate = new CreateLinkCommand
        {
            FileName = Guid.NewGuid().ToString(),
            Argument = Guid.NewGuid().ToString(),
            Order = 1,
            Title = Guid.NewGuid().ToString(),
            ShortDescription = Guid.NewGuid().ToString()
        };
        mediatorStub.Setup(m =>  m.Send(It.IsAny<CreateLinkCommand>(),CancellationToken.None))
        .ReturnsAsync(new LinkResponseDto{ 
            Id=Guid.NewGuid(),
            FileName= itemToCreate.FileName,
            Argument= itemToCreate.Argument,
            Order= itemToCreate.Order,
            Title=itemToCreate.Title,
            ShortDescription=itemToCreate.ShortDescription
        });

        var sut = new LinkController(mediatorStub.Object);
        var result = await sut.Create(itemToCreate);
            var createdItem = (result as CreatedAtRouteResult).Value as LinkResponseDto;
            itemToCreate.Should().BeEquivalentTo(
                createdItem,
                options => options.ComparingByMembers<LinkResponseDto>().ExcludingMissingMembers()
            );
            createdItem.Id.Should().NotBeEmpty();
    }
    [Fact]
    public async Task CreateLinkAsync_WithANullCommand_ReturnBadRequest()
    {
        var sut = new LinkController(mediatorStub.Object);
        var result = await sut.Create((CreateLinkCommand) null);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    private LinkResponseDto GenerateLink()
    {
        return new LinkResponseDto
        {
            Id = Guid.NewGuid(),
            FileName = Guid.NewGuid().ToString(),
            Argument = Guid.NewGuid().ToString(),
            Order = 1,
            Title = Guid.NewGuid().ToString(),
            ShortDescription = Guid.NewGuid().ToString()
        };
    }
}
