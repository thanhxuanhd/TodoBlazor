using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Exceptions;
using Todo.Application.Features.Todo.Queries.GetTodoDetail;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Xunit;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.UnitTests.Todos.Queries.GetTodoDetail;

public class GetTodoDetailQueryHandlerTests
{
    public readonly Mock<IAsyncRepository<Entities.Todo>> _mockTodoRepository;
    public readonly IMapper _mapper;

    public GetTodoDetailQueryHandlerTests()
    {
        _mockTodoRepository = RepositoryMock.MockAsynTodoRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = new Mapper(configurationProvider);
    }

    [Fact]
    public async Task Handle_GetTodoDetailById_Success()
    {
        // Arrange
        var handler = new GetTodoDetailQueryHandler(_mockTodoRepository.Object, _mapper);
        var todoId = RepositoryMock.GetTodoId();
        var categoryId = RepositoryMock.GetCategoryId();

        // Act
        var result = await handler.Handle(new GetTodoDetailQuery()
        {
            TodoId = todoId,
        }, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.TodoId.Should().Be(todoId);
        result.Title.Should().Be("Todo 1");
        result.IsCompleted.Should().BeFalse();
        result.CategoryId.Should().Be(categoryId);
    }

    [Fact]
    public async Task Handle_GetTodoDetailById_NotFound_Error()
    {
        // Arrange
        var handler = new GetTodoDetailQueryHandler(_mockTodoRepository.Object, _mapper);

        // Act
        Func<Task> action = async () =>
        {
            var result = await handler.Handle(new GetTodoDetailQuery()
            {
                TodoId = Guid.NewGuid(),
            }, CancellationToken.None);
        };

        // Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
}