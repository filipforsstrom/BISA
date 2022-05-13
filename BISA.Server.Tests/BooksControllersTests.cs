using BISA.Server.Controllers;
using BISA.Server.Services.BookService;
using BISA.Shared.DTO;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BISA.Server.Tests
{
    public class BooksControllersTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public BooksControllersTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        [Fact]
        public async Task GetBookReturnsAValidBook()
        {

            // arrange
            // var fakeBook = A.Dummy<BookDTO>();
            // _outputHelper.WriteLine(fakeBook.Title.ToString());

            var fakeServiceBook = A.Dummy<ServiceResponseDTO<BookDTO>>();
            var service = A.Fake<IBookService>();
            // fakeServiceBook.Data = fakeBook;
            //A.CallTo(() => service.GetBook(fakeServiceBook)).Returns(Task.FromResult(fakeServiceBook));
            var controller = new BooksController(service);

            // act
            var iActionResult = await controller.Get(fakeServiceBook.Data.Id);
            // assert
            //var result = iActionResult as OkObjectResult;

            //var returnBook = result.Value as BookDTO;
            //Assert.Equal(fakeServiceBook.Data, returnBook);


        }
    }
}