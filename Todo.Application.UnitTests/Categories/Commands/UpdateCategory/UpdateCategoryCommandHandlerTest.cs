using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Features.Categories.Commands.UpdateCategory;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Xunit;

namespace Todo.Application.UnitTests.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;

    public UpdateCategoryCommandHandlerTest()
    {
        _mockCategoryRepository = RepositoryMock.MockCategoryRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = new Mapper(configurationProvider);
    }

    [Fact]
    public async Task Handle_UpdateCategory_Invalid_NameAndCategoryId_ErrorResponse()
    {
        // Arrange
        var updateCategoryHandler = new UpdateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        // Act
        var response = await updateCategoryHandler.Handle(new UpdateCategoryCommand()
        {
            Name = null
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(4);
        response.ValidationErrors.Should().Contain("Name is required.");
        response.ValidationErrors.Should().Contain("'Name' must not be empty.");
        response.ValidationErrors.Should().Contain("Category Id is required.");
        response.ValidationErrors.Should().Contain("'Category Id' must not be equal to '00000000-0000-0000-0000-000000000000'.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_UpdateCategory_Invalid_NameIsNull_ErrorResponse()
    {
        // Arrange
        var updateCategoryHandler = new UpdateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        // Act
        var response = await updateCategoryHandler.Handle(new UpdateCategoryCommand()
        {
            CategoryId = Guid.NewGuid(),
            Name = null
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(2);
        response.ValidationErrors.Should().Contain("Name is required.");
        response.ValidationErrors.Should().Contain("'Name' must not be empty.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_UpdateCategory_Invalid_NameIsEmpty_ErrorResponse()
    {
        // Arrange
        var updateCategoryHandler = new UpdateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        // Act
        var response = await updateCategoryHandler.Handle(new UpdateCategoryCommand()
        {
            CategoryId = Guid.NewGuid(),
            Name = string.Empty
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Name is required.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateCategory_Invalid_NameIsDuplicate_ErrorResponse()
    {
        // Arrange
        var updateCategoryHandler = new UpdateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        _mockCategoryRepository.Setup(c => c.CheckCategoryDuplicate(It.IsAny<string>()))
            .Returns(true);

        var categoryId = RepositoryMock.GetCategoryId();
        // Act
        var response = await updateCategoryHandler.Handle(new UpdateCategoryCommand()
        {
            CategoryId = categoryId,
            Name = "My Todo"
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Name is duplicate.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateCategory_ValidRequest_CategoryNotFound_ErrorResponse()
    {
        // Arrange
        var updateCategoryHandler = new UpdateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);
        var categoryId = Guid.NewGuid();

        // Act
        var response = await updateCategoryHandler.Handle(new UpdateCategoryCommand()
        {
            CategoryId = categoryId,
            Name = "My Todo Update"
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain($"Category not found id {categoryId}");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateCategory_ValidRequest_CategoryUpdate_SuccessResponse()
    {
        // Arrange
        var updateCategoryHandler = new UpdateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);
        var categoryId = RepositoryMock.GetCategoryId();

        // Act
        var response = await updateCategoryHandler.Handle(new UpdateCategoryCommand()
        {
            CategoryId = categoryId,
            Name = "My Todo Update"
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(0);
        response.Success.Should().BeTrue();
    }
}