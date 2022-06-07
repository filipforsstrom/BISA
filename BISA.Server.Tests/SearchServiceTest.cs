using BISA.Server.Services.SearchService;
using BISA.Server.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISA.Shared.Entities;
using Xunit;
using BISA.Shared.DTO;
using BISA.Server.Exceptions;

namespace BISA.Server.Tests
{
    public class SearchServiceTest
    {
        private readonly BisaDbContext _context;
        private readonly SearchService _sut;

        public SearchServiceTest()
        {
            DbContextOptionsBuilder<BisaDbContext> dbOptions = new DbContextOptionsBuilder<BisaDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString());
            _context = new BisaDbContext(dbOptions.Options);
            _sut = new SearchService(_context);
            
            _context.Database.EnsureCreated();
        }

      

        [Fact]

        public async Task SearchByTitle_OnSuccess_ReturnListOfItems()
        {
            //Arrange
            string testTitle = "Top Gun";

            //Act
            var result = await _sut.SearchByTitle(testTitle);

            //Assert
            Assert.IsType<List<ItemDTO>>(result);

        }

        [Fact]
        public async Task SearchByTitle_OnSuccess_ReturnsCorrectErrorMessage()
        {
            //Arrange
            string testTitle = "Vargungen";
            var expectedErrorMessage = "No matching results";

            //Act
            Func<Task> act = async () => await _sut.SearchByTitle(testTitle);

            //Assert

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public async Task SearchByTag_OnSuccess_ReturnCorrectNumberOfItems()
        {
            //Arrange
            int testTagId = 1;
            string testTagTitle = "Action";

            var numberOfItemsWithTag = await _context.Items.Where(i => i.ItemTags.Any(it => it.TagId == testTagId)).ToListAsync();
            //Act

            var result = await _sut.SearchByTags(testTagTitle);

            //Assert

            Assert.Equal(numberOfItemsWithTag.Count(), result.Count());
        }

        [Fact]
        public async Task SearchByTags_OnSuccess_ReturnsCorrectErrorMessage()
        {
            
            string testTitle = "Thriller";
            var expectedErrorMessage = "No matching results";

            var exceptionResult = await Assert.ThrowsAsync<NotFoundException>(() => _sut.SearchByTags(testTitle));
            
            Assert.Equal(expectedErrorMessage, exceptionResult.Message);
        }

        [Theory]
        [InlineData("Bonnier")]
        [InlineData("Fox")]
        public async Task SearchByAll_OnSuccess_ReturnsCorrectNumberOfItemsWithTheRightData(string publisher)
        {
            //Arrange
            var numberOfItems = await _context.Items.Where(i => i.Publisher == publisher).ToListAsync();
            
            //Act
            var result = await _sut.SearchByAll(publisher);

            //Assert
            Assert.Equal(numberOfItems.Count(), result.Count());

        }

        [Fact]
        public async Task SearchByAll_OnSuccess_ReturnsCorrectErrorMessage()
        {

            string testTitle = "Kakkajakko";
            var expectedErrorMessage = "No matching results";

            var exceptionResult = await Assert.ThrowsAsync<NotFoundException>(() => _sut.SearchByAll(testTitle));

            Assert.Equal(expectedErrorMessage, exceptionResult.Message);
        }
    }
}
