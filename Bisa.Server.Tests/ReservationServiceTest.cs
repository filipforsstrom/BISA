using BISA.Server.Data.DbContexts;
using BISA.Server.Exceptions;
using BISA.Server.Services.ReservationService;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BISA.Server.Tests
{
    public class ReservationServiceTest
    {
        private readonly BisaDbContext _context;
        private readonly Mock<IHttpContextAccessor> _contextAccessor;
        private readonly ReservationService _sut;

        public ReservationServiceTest()
        {
            DbContextOptionsBuilder<BisaDbContext> dbOptions = new DbContextOptionsBuilder<BisaDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString());
            _context = new BisaDbContext(dbOptions.Options);
            _contextAccessor = new Mock<IHttpContextAccessor>();
            _contextAccessor.Setup(h => h.HttpContext.User).Returns(user);
            _sut = new ReservationService(_context, _contextAccessor.Object);

            InitDb();
        }

        private static ClaimsPrincipal user = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new Claim[] { new Claim(ClaimTypes.NameIdentifier, "b74ddd14-6340-4840-95c2-db12554843e6") })
                        );

        private void InitDb()
        {
            var user = new UserEntity { Id = 1000, UserId = "b74ddd14-6340-4840-95c2-db12554843e6", Firstname = "michael", Lastname = "kors", Username = "mkor", Email = "michael@gmail.com" };
            var user_two = new UserEntity { Id = 1001, UserId = "f156", Firstname = "michael", Lastname = "kors", Username = "mkor", Email = "michael@gmail.com" };

            var item = new ItemEntity { Id = 1001, Title = "Ondskan", Date = "1980", Creator = "Jan Guillou" };
            var invItem = new ItemInventoryEntity { Id = 1001, ItemId = item.Id, Available = false };
            var invItem_two = new ItemInventoryEntity { Id = 1002, ItemId = item.Id, Available = false };

            var loan = new LoanEntity { Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(20), ItemInventoryId = invItem.Id, UserId = user.Id };
            var loan_two = new LoanEntity { Date_From = DateTime.Now.AddDays(3), Date_To = DateTime.Now.AddDays(23), ItemInventoryId = invItem_two.Id, UserId = user_two.Id };

            _context.Users.Add(user);
            _context.Items.Add(item);
            _context.ItemInventory.Add(invItem);
            _context.ItemInventory.Add(invItem_two);
            _context.LoansActive.Add(loan);
            _context.LoansActive.Add(loan_two);

            _context.SaveChanges();
        }


        [Fact]
        public async Task AddReservation_OnSuccess_ReturnsServiceResponseTrueAndWithData()
        {
            // Arrange
            var itemIdToReserve = 1001;
            var expectedReservation = new LoanReservationEntity
            {
                ItemId = itemIdToReserve,
                UserId = 1000
            };
            // Act
            var result = await _sut.AddReservation(itemIdToReserve);
            // Assert
            Assert.Equal(expectedReservation.ItemId, result.ItemId);
            Assert.Equal(expectedReservation.UserId, result.UserId);
        }
        [Fact]
        public async Task AddReservation_OnDuplicateFail_ThrowsArgumentExceptionAndWithMessage()
        {
            // Arrange
            var itemIdToReserve = 1001;
            var expectedErrorMessage = "This item is allready reserved by user";
            // Act
            var firstReservation = await _sut.AddReservation(itemIdToReserve);
            Func<Task> act = async () => await _sut.AddReservation(itemIdToReserve);

            // Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public async Task AddReservation_OnWrongItemId_ThrowsArgumentExceptionAndWithMessage()
        {
            // Arrange
            var itemIdToReserve = 5000;
            var expectedErrorMessage = "No item with matching id";
            // Act
            Func<Task> act = async () => await _sut.AddReservation(itemIdToReserve);

            // Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public async Task GetItemReservations_OnSuccess_ReturnsAListWithReservations()
        {
            // Arrange
            var itemId = 1001;
            _context.LoansReservation.AddRange(new LoanReservationEntity[]
            {
                new LoanReservationEntity
                {
                    Id = 1001,
                    ItemId = itemId,
                    UserId = 1,
                    Date_From = DateTime.Now,
                    Date_To = DateTime.Now.AddDays(20)
                },
                new LoanReservationEntity
                {
                    Id = 1002,
                    ItemId = itemId,
                    UserId = 2,
                    Date_From = DateTime.Now.AddDays(20),
                    Date_To = DateTime.Now.AddDays(7)
                },
            });
            _context.SaveChanges();
            // Act
            var result = await _sut.GetItemReservations(itemId);
            // Assert
            Assert.IsType<List<LoanReservationEntity>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetItemReservations_OnMissingItem_ThrowsArgumentExceptionWithMessage()
        {
            // Arrange
            var itemId = 200;

            var expectedErrorMessage = "No item with matching id";
            // Act
            Func<Task> act = async () => await _sut.GetItemReservations(itemId);

            // Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public async Task GetMyReservations_OnSuccess_ReturnsAListOfAUsersReservations()
        {
            // Arrange
            var userId = 1000;
            var reservationsToAdd = new LoanReservationEntity[]
                {
                new LoanReservationEntity
                {
                    Id = 1,
                    ItemId = 1001,
                    Date_From = DateTime.Now,
                    Date_To = DateTime.Now.AddDays(20),
                    UserId = 1000
                },
                new LoanReservationEntity
                {
                    Id = 2,
                    ItemId = 1001,
                    Date_From = DateTime.Now.AddDays(21),
                    Date_To = DateTime.Now.AddDays(41),
                    UserId = 1001
                },
                new LoanReservationEntity
                {
                    Id = 3,
                    ItemId = 1001,
                    Date_From = DateTime.Now.AddDays(21),
                    Date_To = DateTime.Now.AddDays(41),
                    UserId = 1000
                },
                new LoanReservationEntity
                {
                    Id = 4,
                    ItemId = 1001,
                    Date_From = DateTime.Now.AddDays(21),
                    Date_To = DateTime.Now.AddDays(41),
                    UserId = 1002
                },
            };
            _context.LoansReservation.AddRange(reservationsToAdd);
            _context.SaveChanges();

            // Act
            var result = await _sut.GetMyReservations();

            // Assert
            Assert.IsType<List<LoanReservationEntity>>(result);
            var userIds = result.Select(reservation => reservation.UserId);
            foreach (var res in userIds)
            {
                Assert.Equal(userId, res);
            }
        }

        [Fact]
        public async Task GetMyReservations_OnMissingUser_ThrowsArgumentExceptionWithMessage()
        {
            // Arrange
            _contextAccessor.Setup(h => h.HttpContext.User).Returns(new ClaimsPrincipal());
            var expectedErrorMessage = "No matching user";

            _context.LoansReservation.AddRange(new LoanReservationEntity[]
            {
                new LoanReservationEntity
                {
                    Id = 1,
                    ItemId = 1001,
                    Date_From = DateTime.Now,
                    Date_To = DateTime.Now.AddDays(20),
                    UserId = 1000
                },
                new LoanReservationEntity
                {
                    Id = 2,
                    ItemId = 1001,
                    Date_From = DateTime.Now.AddDays(21),
                    Date_To = DateTime.Now.AddDays(41),
                    UserId = 1001
                },
            });
            _context.SaveChanges();

            // Act
            Func<Task> act = async () => await _sut.GetMyReservations();

            // Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public async Task RemoveReservation_OnSuccess_ReturnsServiceResponseTrueAndWithMessage()
        {
            // Arrange
            _context.LoansReservation.Add(new LoanReservationEntity
            {
                Id = 1,
                ItemId = 1,
                UserId = 1000,
                Date_From = DateTime.Now.AddDays(20),
                Date_To = DateTime.Now.AddDays(40)
            });
            _context.SaveChanges();

            var reservationsAtStart = _context.LoansReservation.ToList();

            // Act
            await _sut.RemoveReservation(1);

            // Assert
            var reservationsAtEnd = _context.LoansReservation.ToList();

            Assert.Equal(reservationsAtEnd.Count, reservationsAtStart.Count - 1);
        }

        [Fact]
        public async void RemoveReservation_OnInvalidUser_ThrowsUnauthorzedAccessExceptionWithMessage()
        {
            // Arrange
            _context.LoansReservation.Add(new LoanReservationEntity
            {
                Id = 1,
                ItemId = 1,
                UserId = 1001,
                Date_From = DateTime.Now.AddDays(20),
                Date_To = DateTime.Now.AddDays(40)
            });
            _context.SaveChanges();
            var expectedErrorMessage = "User does not have the right to delete this item";

            // Act            
            Func<Task> act = async () => await _sut.RemoveReservation(1);

            // Assert
            UnauthorizedAccessException exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(act);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public async void RemoveReservation_WhenNoMatchingReservationIsFound_ReturnsResponseFalseWithMessage()
        {
            // Arrange
            _context.LoansReservation.Add(new LoanReservationEntity
            {
                Id = 1,
                ItemId = 1,
                UserId = 1001,
                Date_From = DateTime.Now.AddDays(20),
                Date_To = DateTime.Now.AddDays(40)
            });
            _context.SaveChanges();
            var expectedErrorMessage = "No reservation with matching id";

            // Act
            Func<Task> act = async () => await _sut.RemoveReservation(2);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void CheckTimeAvailable_WhenThereAreNoReservations_ReturnsTheFirstEndingLoanDatePlusOne()
        {
            //  Arrange
            var item = _context.Items.FirstOrDefault(item => item.Id == 1001);
            var loanTime = 20;

            var expected = DateTime.Now.AddDays(loanTime + 1);
            //  Act
            var result = _sut.CheckTimeAvailable(item.Id);
            //  Assert

            Assert.Equal(expected.DayOfYear, result.DayOfYear);
        }

        [Theory]
        [InlineData(1001)]
        public void CheckTimeAvailable_WhenThereIsReservations_ReturnsTimeBasedOnEarliestReservation(int id)
        {
            //  Arrange

            var item = _context.Items.FirstOrDefault(item => item.Id == id);
            var loanTime = 20;
            var reservation = new LoanReservationEntity { Id = 1003, ItemId = item.Id, Date_From = DateTime.Now.AddDays(loanTime + 1), Date_To = DateTime.Now.AddDays((loanTime * 2) + 1) };
            var reservation_two = new LoanReservationEntity { Id = 1004, ItemId = item.Id, Date_From = DateTime.Now.AddDays(loanTime + 2), Date_To = DateTime.Now.AddDays((loanTime * 2) + 2) };

            _context.LoansReservation.AddRange(new LoanReservationEntity[] { reservation, reservation_two });
            _context.SaveChanges();

            var expected = reservation.Date_To.AddDays(1);
            //  Act

            var result = _sut.CheckTimeAvailable(item.Id);
            //  Assert

            Assert.Equal(expected.DayOfYear, result.DayOfYear);
        }

        [Theory]
        [InlineData("Book", 20)]
        [InlineData("Ebook", 7)]
        [InlineData("Movie", 7)]
        [InlineData("", 20)]
        public void GetItemLoanTime_OnValidString_ReturnsDouble(string type, double expected)
        {
            // Arrange

            // Act
            var actual = _sut.GetItemLoanTime(type);
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
