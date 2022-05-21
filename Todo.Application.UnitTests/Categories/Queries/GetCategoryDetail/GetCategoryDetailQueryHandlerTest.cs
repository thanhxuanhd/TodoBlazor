using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Exceptions;
using Todo.Application.Features.Categories.Queries.GetCategoryDetail;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Todo.Domain.Entities;
using Xunit;

namespace Todo.Application.UnitTests.Categories.Queries.GetCategoryDetail;

public class GetCategoryDetailQueryHandlerTest
{
    private readonly IMapper _mapper;
    public readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;

    public GetCategoryDetailQueryHandlerTest()
    {
        _mockCategoryRepository = RepositoryMock.MockAsyncCategoryRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = new Mapper(configurationProvider);
    }

    [Fact]
    public async Task Handle_GetCategoryDetailById_Success()
    {
        // Arrange
        var handler = new GetCategoryDetailQueryHandler(_mockCategoryRepository.Object, _mapper);
        var categoryId = RepositoryMock.GetCategoryId();

        // Act
        var result = await handler.Handle(new GetCategoryDetailQuery()
        {
            CategoryId = categoryId,
        }, CancellationToken.None);

        // Assert
        result.Should().NotBe(null);
        result.CategoryId.Should().Be(categoryId);
    }

    [Fact]
    public Task Handle_GetCategoryDetailById_NotFound()
    {
        // Arrange
        var handler = new GetCategoryDetailQueryHandler(_mockCategoryRepository.Object, _mapper);

        // Act
        Func<Task> action = async () =>
        {
            var result = await handler.Handle(new GetCategoryDetailQuery()
            {
                CategoryId = Guid.Empty
            }, CancellationToken.None);
        };

        // Assert
        action.Should().ThrowAsync<NotFoundException>();
        return Task.CompletedTask;
    }
}