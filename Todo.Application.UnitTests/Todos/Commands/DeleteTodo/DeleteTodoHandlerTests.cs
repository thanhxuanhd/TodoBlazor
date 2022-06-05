using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Features.Todo.Commands.DeleteTodo;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Xunit;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.UnitTests.Todos.Commands.DeleteTodo;

public class DeleteTodoHandlerTests
{
    private Mock<IAsyncRepository<Entities.Todo>> _mockTodoRepository;

    public DeleteTodoHandlerTests()
    {
        _mockTodoRepository = RepositoryMock.MockAsyncTodoRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
    }

    [Fact]
    public async Task Handle_DeleteCategory_NotFound_Remove_Fail()
    {
        // Arrange
        var deleteTodoHandler = new DeleteTodoCommandHandler(_mockTodoRepository.Object);
        var todoId = Guid.NewGuid();

        // Act
        var response = await deleteTodoHandler.Handle(new DeletedTodoCommand()
        {
            TodoId = todoId
        }, CancellationToken.None);

        // Assert
        response.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_DeleteTodo_FoundTodo_Update_Success()
    {
        // Arrange
        var deleteTodoHandler = new DeleteTodoCommandHandler(_mockTodoRepository.Object);
        var todoId = RepositoryMock.GetTodoId();

        // Act
        var response = await deleteTodoHandler.Handle(new DeletedTodoCommand()
        {
            TodoId = todoId
        }, CancellationToken.None);

        // Assert
        response.Should().BeTrue();
    }
}