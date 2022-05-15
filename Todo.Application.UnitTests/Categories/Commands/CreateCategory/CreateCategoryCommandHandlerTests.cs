using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Features.Categories.Commands.CreateCategory;
using Todo.Application.Profiles;
using Todo.Application.UnitTests.Mocks;
using Xunit;

namespace Todo.Application.UnitTests.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        private readonly Random _random = new();

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
        public async Task Handle_CreateCategory_Invalid_NameNull_ErrorResponse()
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
        public async Task Handle_CreateCategory_InvalidName_Empty_ErrorResponse()
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
        public async Task Handle_CreateCategory_InvalidName_Duplicate_ErrorResponse()
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
                Name = RandomString(1001)
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

        // Generates a random string with a given size.    
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}