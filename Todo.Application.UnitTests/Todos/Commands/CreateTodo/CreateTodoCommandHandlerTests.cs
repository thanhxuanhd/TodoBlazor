using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Features.Todo.Commands.CreateTodo;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Xunit;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.UnitTests.Todos.Commands.CreateTodo;

public class CreateTodoCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<Entities.Todo>> _mockTodoRepository;

    public CreateTodoCommandHandlerTests()
    {
        _mockTodoRepository = RepositoryMock.MockAsyncTodoRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = new Mapper(configurationProvider);
    }

    [Fact]
    public async Task Handle_CreateTodo_Invalid_NameIsNull_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new CreateTodoCommandHandler(_mockTodoRepository.Object, _mapper);

        // Act
        var response = await createCategoryHandler.Handle(new CreateTodoCommand()
        {
            Title = null,
            Description = null,
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(2);
        response.ValidationErrors.Should().Contain("Title is required.");
        response.ValidationErrors.Should().Contain("'Title' must not be empty.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateTodo_Invalid_NameIsEmpty_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new CreateTodoCommandHandler(_mockTodoRepository.Object, _mapper);

        // Act
        var response = await createCategoryHandler.Handle(new CreateTodoCommand()
        {
            Title = string.Empty,
            Description = null,
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Title is required.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateTodo_Invalid_Name_MaxLength_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new CreateTodoCommandHandler(_mockTodoRepository.Object, _mapper);

        // Act
        var response = await createCategoryHandler.Handle(new CreateTodoCommand()
        {
            Title = Helpers.RandomString(1010),
            Description = null,
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Title must not exceed 1000 characters.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateTodo_Invalid_Description_MaxLength_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new CreateTodoCommandHandler(_mockTodoRepository.Object, _mapper);

        // Act
        var response = await createCategoryHandler.Handle(new CreateTodoCommand()
        {
            Title = Helpers.RandomString(100),
            Description = Helpers.RandomString(5100),
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Description must not exceed 1000 characters.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_CreateTodo_Valid_Todo_SuccessResponse()
    {
        // Arrange
        var createCategoryHandler = new CreateTodoCommandHandler(_mockTodoRepository.Object, _mapper);
        var categoryId = Guid.NewGuid();

        // Act
        var response = await createCategoryHandler.Handle(new CreateTodoCommand()
        {
            Title = "Todo 1",
            Description = "Description",
            CategoryId = categoryId
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(0);
        response.Todo.Should().NotBeNull();
        response.Todo.Title.Should().Be("Todo 1");
        response.Todo.Description.Should().Be("Description");
        response.Todo.CategoryId.Should().Be(categoryId);
        response.Todo.TodoId.Should().NotBe(Guid.Empty);
        response.Success.Should().BeTrue();
    }
}