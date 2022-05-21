using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Features.Categories.Commands.CreateCategory;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Xunit;

namespace Todo.Application.UnitTests.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;

    public CreateCategoryCommandHandlerTests()
    {
        _mockCategoryRepository = RepositoryMock.MockCategoryRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = new Mapper(configurationProvider);
    }

    [Fact]
    public async Task Handle_CreateCategory_Invalid_NameIsNull_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        // Act
        var response = await createCategoryHandler.Handle(new CreateCategoryCommand()
        {
            Name = null
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(2);
        response.ValidationErrors.Should().Contain("Name is required.");
        response.ValidationErrors.Should().Contain("'Name' must not be empty.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateCategory_Invalid_NameIsEmpty_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        // Act
        var response = await createCategoryHandler.Handle(new CreateCategoryCommand()
        {
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
        var createCategoryHandler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        _mockCategoryRepository.Setup(c => c.CheckCategoryDuplicate(It.IsAny<string>()))
            .Returns(true);
        // Act
        var response = await createCategoryHandler.Handle(new CreateCategoryCommand()
        {
            Name = "My Todo"
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Name is duplicate.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateCategory_InvalidName_MaxLength_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        // Act
        var response = await createCategoryHandler.Handle(new CreateCategoryCommand()
        {
            Name = Helpers.RandomString(1001)
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Name must not exceed 1000 characters.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateCategory_ValidName_SuccessResponse()
    {
        // Arrange
        var createCategoryHandler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        var categoryName = "My Todo";

        // Act
        var response = await createCategoryHandler.Handle(new CreateCategoryCommand()
        {
            Name = categoryName
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().NotBeNull();
        response.ValidationErrors.Should().HaveCount(0);
        response.Success.Should().BeTrue();
        response.Category.Should().NotBeNull();
        response.Category.CategoryId.Should().NotBe(Guid.Empty);
        response.Category.Name.Should().Be(categoryName);
    }
}