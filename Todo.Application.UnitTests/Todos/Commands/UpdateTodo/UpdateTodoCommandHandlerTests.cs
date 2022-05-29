using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Features.Todo.Commands.UpdateTodo;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Xunit;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.UnitTests.Todos.Commands.UpdateTodo;

public class UpdateTodoCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<Entities.Todo>> _mockTodoRepository;

    public UpdateTodoCommandHandlerTests()
    {
        _mockTodoRepository = RepositoryMock.MockAsyncTodoRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = new Mapper(configurationProvider);
    }

    [Fact]
    public async Task Handle_UpdateTodo_Invalid_NameIsNull_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new UpdateTodoCommandHandler(_mockTodoRepository.Object, _mapper);

        // Act
        var response = await createCategoryHandler.Handle(new UpdateTodoCommand()
        {
            Title = null,
            Description = null,
            TodoId = Guid.NewGuid()
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(2);
        response.ValidationErrors.Should().Contain("Title is required.");
        response.ValidationErrors.Should().Contain("'Title' must not be empty.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_UpdateTodo_Invalid_NameIsEmpty_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new UpdateTodoCommandHandler(_mockTodoRepository.Object, _mapper);

        // Act
        var response = await createCategoryHandler.Handle(new UpdateTodoCommand()
        {
            Title = string.Empty,
            Description = null,
            TodoId = Guid.NewGuid()
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Title is required.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_UpdateTodo_Invalid_TodoIdIsEmpty_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new UpdateTodoCommandHandler(_mockTodoRepository.Object, _mapper);

        // Act
        var response = await createCategoryHandler.Handle(new UpdateTodoCommand()
        {
            Title = Helpers.RandomString(100),
            Description = Helpers.RandomString(100),
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Todo Id is required.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_UpdateTodo_Invalid_Name_MaxLength_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new UpdateTodoCommandHandler(_mockTodoRepository.Object, _mapper);

        // Act
        var response = await createCategoryHandler.Handle(new UpdateTodoCommand()
        {
            Title = Helpers.RandomString(1010),
            Description = null,
            TodoId = Guid.NewGuid()
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Title must not exceed 1000 characters.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_UpdateTodo_Invalid_Description_MaxLength_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new UpdateTodoCommandHandler(_mockTodoRepository.Object, _mapper);

        // Act
        var response = await createCategoryHandler.Handle(new UpdateTodoCommand()
        {
            Title = Helpers.RandomString(100),
            Description = Helpers.RandomString(5100),
            TodoId = Guid.NewGuid()
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain("Description must not exceed 1000 characters.");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_UpdateTodo_Invalid_Todo_NotFound_ErrorResponse()
    {
        // Arrange
        var createCategoryHandler = new UpdateTodoCommandHandler(_mockTodoRepository.Object, _mapper);
        var todoId = Guid.NewGuid();
        // Act
        var response = await createCategoryHandler.Handle(new UpdateTodoCommand()
        {
            Title = Helpers.RandomString(100),
            Description = Helpers.RandomString(100),
            TodoId = todoId
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(1);
        response.ValidationErrors.Should().Contain($"Todo not found id {todoId}");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_UpdateTodo_Valid_Todo_SuccessResponse()
    {
        // Arrange
        var createCategoryHandler = new UpdateTodoCommandHandler(_mockTodoRepository.Object, _mapper);
        var todoId = RepositoryMock.GetTodoId();
        // Act
        var response = await createCategoryHandler.Handle(new UpdateTodoCommand()
        {
            Title = Helpers.RandomString(100),
            Description = Helpers.RandomString(100),
            TodoId = todoId
        }, CancellationToken.None);

        // Assert
        response.ValidationErrors.Should().HaveCount(0);
        response.Success.Should().BeTrue();
    }
}