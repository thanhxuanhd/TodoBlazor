using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Features.Categories.Commands.DeleteCategory;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Xunit;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.UnitTests.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<Entities.Category>> _mockCategoryRepository;

    public DeleteCategoryHandlerTests()
    {
        _mockCategoryRepository = RepositoryMock.MockAsyncCategoryRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = new Mapper(configurationProvider);
    }

    [Fact]
    public async Task Handle_DeleteCategory_CategoryNotFound_Update_Fail()
    {
        // Arrange
        var deleteCategoryHandler = new DeleteCategoryHandler(_mockCategoryRepository.Object, _mapper);
        var categoryId = Guid.NewGuid();

        // Act
        var response = await deleteCategoryHandler.Handle(new DeleteCategoryCommand()
        {
            CategoryId = categoryId
        }, CancellationToken.None);

        // Assert
        response.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_DeleteCategory_CategoryFound_Update_Success()
    {
        // Arrange
        var deleteCategoryHandler = new DeleteCategoryHandler(_mockCategoryRepository.Object, _mapper);
        var categoryId = RepositoryMock.GetCategoryId();

        // Act
        var response = await deleteCategoryHandler.Handle(new DeleteCategoryCommand()
        {
            CategoryId = categoryId
        }, CancellationToken.None);

        // Assert
        response.Should().BeTrue();
    }
}