using AutoMapper;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Features.Categories.Queries.GetCategoriesList;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Xunit;

namespace Todo.Application.UnitTests.Categories.Queries.GetCategoriesList;

public class GetCategoriesListQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;

    public GetCategoriesListQueryHandlerTest()
    {
        _mockCategoryRepository = RepositoryMock.MockCategoryRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = new Mapper(configurationProvider);
    }

    [Fact]
    public async Task Handle_GetAllCategories_Sucess()
    {
        // Arrange
        var handler = new GetCategoriesListQueryHandler(_mapper, _mockCategoryRepository.Object);

        // Act
        var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<List<CategoryListVm>>();

        result.Count.Should().Be(2);
    }
}