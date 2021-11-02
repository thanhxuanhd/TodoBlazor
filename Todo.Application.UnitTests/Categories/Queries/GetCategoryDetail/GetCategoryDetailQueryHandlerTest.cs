using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Exceptions;
using Todo.Application.Features.Categories.Queries.GetCategoryDetail;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Todo.Domain.Entities;
using Xunit;

namespace Todo.Application.UnitTests.Categories.Queries.GetCategoryDetail
{
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
        public async Task Get_Category_Detail_ById_Success()
        {
            var handler = new GetCategoryDetailQueryHandler(_mockCategoryRepository.Object, _mapper);

            var result = await handler.Handle(new GetCategoryDetailQuery()
            {
                CategoryId = RepositoryMock.CategoryIdNeed
            }, CancellationToken.None);

            result.Should().NotBe(null);
            result.CategoryId.Should().Be(RepositoryMock.CategoryIdNeed);
        }

        [Fact]
        public Task Get_Category_Detail_ById_Not_Found()
        {
            var handler = new GetCategoryDetailQueryHandler(_mockCategoryRepository.Object, _mapper);

            Func<Task> action = async () =>
            {
                var result = await handler.Handle(new GetCategoryDetailQuery()
                {
                    CategoryId = Guid.Empty
                }, CancellationToken.None);

            };

            action.Should().ThrowAsync<NotFoundException>();
            return Task.CompletedTask;
        }
    }
}
