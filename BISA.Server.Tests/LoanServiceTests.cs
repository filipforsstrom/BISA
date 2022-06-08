using BISA.Server.Data.DbContexts;
using BISA.Server.Exceptions;
using BISA.Server.Services.LoanService;
using BISA.Server.Services.ReservationService;
using BISA.Shared.DTO;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BISA.Server.Tests
{
    public class LoanServiceTests
    {
        private LoanService _systemUnderTest;
        private readonly BisaDbContext _context;
        private readonly Mock<IHttpContextAccessor> _contextAccessor;

        public LoanServiceTests()
        {
            DbContextOptionsBuilder<BisaDbContext> dbOptions = new DbContextOptionsBuilder<BisaDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString());
            _context = new BisaDbContext(dbOptions.Options);
            _contextAccessor = new Mock<IHttpContextAccessor>();
            _contextAccessor.Setup(h => h.HttpContext.User).Returns(user);
            _systemUnderTest = new LoanService(_context, _contextAccessor.Object);

            InitDb();
        }

        private static ClaimsPrincipal user = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new Claim[] { new Claim(ClaimTypes.NameIdentifier, "a1") })
                        );

        public UserEntity user1 { get; set; }
        public UserEntity user2 { get; set; }
        public ItemEntity item1 { get; set; }
        public ItemInventoryEntity inventoryItem1 { get; set; }
        public ItemInventoryEntity inventoryItem2 { get; set; }
        public ItemInventoryEntity inventoryItem3 { get; set; }
        public ItemInventoryEntity inventoryItem4 { get; set; }
        public ItemInventoryEntity inventoryItem5 { get; set; }
        public LoanEntity loan1 { get; set; }
        public LoanEntity loan2 { get; set; }
        public DateTime DateTimeNow { get; set; } = DateTime.Now;

        private void InitDb()
        {
            user1 = new UserEntity
            {
                Id = 1,
                UserId = "a1",
                Firstname = "michael",
                Lastname = "kors",
                Username = "mkor",
                Email = "michael@gmail.com"
            };
            user2 = new UserEntity
            {
                Id = 2,
                UserId = "b2",
                Firstname = "anders",
                Lastname = "gustavsson",
                Username = "mkor",
                Email = "anders@gmail.com"
            };

            item1 = new ItemEntity { Id = 1001, Title = "Ondskan", Date = "1980", Creator = "Jan Guillou" };
            inventoryItem1 = new ItemInventoryEntity { Id = 1001, ItemId = item1.Id, Available = false };
            inventoryItem2 = new ItemInventoryEntity { Id = 1002, ItemId = item1.Id, Available = false };
            inventoryItem3 = new ItemInventoryEntity { Id = 1003, ItemId = item1.Id, Available = false };
            inventoryItem4 = new ItemInventoryEntity { Id = 1004, ItemId = item1.Id, Available = false };
            inventoryItem5 = new ItemInventoryEntity { Id = 1005, ItemId = item1.Id, Available = false };

            loan1 = new LoanEntity
            {
                Date_From = DateTimeNow,
                Date_To = DateTimeNow.AddDays(20),
                ItemInventoryId = inventoryItem1.Id,
                UserId = user1.Id
            };
            loan2 = new LoanEntity
            {
                Date_From = DateTimeNow.AddDays(3),
                Date_To = DateTimeNow.AddDays(23),
                ItemInventoryId = inventoryItem2.Id,
                UserId = user2.Id
            };
        }

        [Fact]

        public async Task AddLoan_OnSuccess_ReturnListOfLoanDTO()
        {
            //arrange
            List<CheckoutDTO> loansToAdd = new List<CheckoutDTO>
            {
                 new CheckoutDTO
                {
                    ItemId = 1001,
                    Title = "Ondskan"
                }
            };

            List<LoanDTO> expectedLoanList = new List<LoanDTO>
            {
                new LoanDTO
                {
                    InvItemId = 1001
                }
            };

            _context.Users.Add(user1);
            _context.Items.Add(item1);
            inventoryItem1.Available = true;
            _context.ItemInventory.Add(inventoryItem1);
            _context.ItemInventory.Add(inventoryItem2);
            _context.SaveChanges();

            //act
            var result = await _systemUnderTest.AddLoan(loansToAdd);

            //assert
            Assert.IsType<List<LoanDTO>>(result);
            Assert.Equal(expectedLoanList[0].InvItemId, result[0].InvItemId);

        }

        [Fact]
        public async Task AddLoan_OnSuccess_LoanAddedToDb()
        {
            //Arrange
            List<CheckoutDTO> loansToAdd = new List<CheckoutDTO>
            {
                 new CheckoutDTO
                {
                    ItemId = 1001,
                    Title = "Ondskan"
                }
            };

            _context.Users.Add(user1);
            _context.Items.Add(item1);
            inventoryItem1.Available = true;
            _context.ItemInventory.Add(inventoryItem1);
            _context.SaveChanges();

            var loansAtStart = await _context.LoansActive.ToListAsync();


            //Act

            var addedLoan = await _systemUnderTest.AddLoan(loansToAdd);
            var loansAfterAddedLoan = await _context.LoansActive.ToListAsync();

            //Assert
            Assert.Equal(loansAtStart.Count + 1, loansAfterAddedLoan.Count);
        }

        [Fact]
        public async Task AddLoan_OnInvalidUser_ThrowUserNotFoundException()
        {
            //arrange
            string expectedErrorMessage = "User not found.";
            List<CheckoutDTO> loansToAdd = new List<CheckoutDTO>
            {
                 new CheckoutDTO
                {
                    ItemId = 1001,
                    Title = "Ondskan"
                }
            };

            _context.Items.Add(item1);
            inventoryItem1.Available = true;
            _context.ItemInventory.Add(inventoryItem1);
            _context.SaveChanges();

            //act
            Func<Task> act = async () => await _systemUnderTest.AddLoan(loansToAdd);

            //assert
            UserNotFoundException exception = await Assert.ThrowsAsync<UserNotFoundException>(act);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public async Task AddLoan_OnMaxLoansReached_ThrowsArgumentOutOfRangeException()
        {
            //arrange

            string excpectedErrorMessage = "User only eligible for 0 more loans";
            List<CheckoutDTO> loansToAdd = new List<CheckoutDTO>
            {
                 new CheckoutDTO
                {
                    ItemId = 1001,
                    Title = "Ondskan"
                }
            };
            _context.Users.Add(user1);
            _context.Items.Add(item1);
            _context.ItemInventory.AddRange(new ItemInventoryEntity[]
            {
                inventoryItem1,
                inventoryItem2,
                inventoryItem3,
                inventoryItem4,
                inventoryItem5,
                new ItemInventoryEntity{Id = 1006, ItemId = item1.Id, Available = true}
            });
            _context.LoansActive.AddRange(new LoanEntity[]
            {
                new LoanEntity
                {
                    ItemInventoryId = 1001,
                    UserId = 1
                },
                new LoanEntity
                {
                    ItemInventoryId = 1002,
                    UserId = 1
                },
                new LoanEntity
                {
                    ItemInventoryId = 1003,
                    UserId = 1
                },
                new LoanEntity
                {
                    ItemInventoryId = 1004,
                    UserId = 1
                },
                new LoanEntity
                {
                    ItemInventoryId = 1005,
                    UserId = 1
                }
            }) ;
            _context.SaveChanges();

            //act
            Func<Task> act = async () => await _systemUnderTest.AddLoan(loansToAdd);

            //assert

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal(excpectedErrorMessage, exception.Message);

        }

        [Fact]
        public async Task AddLoan_OnNoItemsAvailable_InvalidOperationException()
        {
            //arrange

            string excpectedErrorMessage = "Following items could not be loaned: Ondskan";
            List<CheckoutDTO> loansToAdd = new List<CheckoutDTO>
            {
                 new CheckoutDTO
                {
                    ItemId = 1001,
                    Title = "Ondskan"
                }
            };
            _context.Users.Add(user1);
            _context.Items.Add(item1);
            _context.ItemInventory.AddRange(new ItemInventoryEntity[]
            {
                inventoryItem1,
                inventoryItem2,
                inventoryItem3,
                inventoryItem4,
                inventoryItem5,
                
            });
            
            _context.SaveChanges();

            //act
            Func<Task> act = async () => await _systemUnderTest.AddLoan(loansToAdd);

            //assert

            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
            Assert.Equal(excpectedErrorMessage, exception.Message);

        }



        [Fact]
        public async Task GetAllLoansWithActiveLoans_OnSuccess_ReturnsAListWithLoans()
        {
            // arrange
            List<LoanDTO> returnedLoans = new List<LoanDTO>
            {
                new LoanDTO
                {
                    Date_From = DateTimeNow,
                    Date_To = DateTimeNow.AddDays(20),
                    InvItemId = inventoryItem1.Id,
                    Item = new ItemEntity
                    {

                    },
                    User_Email = user1.Email
                },
                new LoanDTO
                {
                    Date_From = DateTimeNow,
                    Date_To = DateTimeNow.AddDays(20),
                    InvItemId = inventoryItem1.Id,
                    Item = new ItemEntity
                    {

                    },
                    User_Email = user2.Email
                }
            };

            var loan1 = new LoanEntity { Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(20), ItemInventoryId = inventoryItem1.Id, UserId = user1.Id };
            var loan2 = new LoanEntity { Date_From = DateTime.Now.AddDays(3), Date_To = DateTime.Now.AddDays(23), ItemInventoryId = inventoryItem2.Id, UserId = user2.Id };

            _context.Users.Add(user1);
            _context.Users.Add(user2);
            _context.Items.Add(item1);
            _context.ItemInventory.Add(inventoryItem1);
            _context.ItemInventory.Add(inventoryItem2);
            _context.LoansActive.Add(loan1);
            _context.LoansActive.Add(loan2);

            _context.SaveChanges();

            // act
            var result = await _systemUnderTest.GetAllLoans();
            // assert
            Assert.IsType<List<LoanDTO>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllLoansWithEmptyDb_OnSuccess_ReturnsEmptyList()
        {
            // act
            var result = await _systemUnderTest.GetAllLoans();
            // act & assert
            Assert.IsType<List<LoanDTO>>(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task GetMyLoans_OnSuccess_ReturnUserLoans()
        {
            _context.Users.Add(user1);
            _context.Items.Add(item1);
            _context.ItemInventory.Add(inventoryItem1);
            _context.LoansActive.Add(loan1);

            _context.SaveChanges();
            // act
            var result = await _systemUnderTest.GetMyLoans();
            // arrange

        }

        [Fact]
        public async Task ReturnLoan_OnExistingReservations_MovesReservationToActiveLoan()
        {
            // Arrange
            _context.Users.Add(user1);
            _context.Users.Add(user2);
            _context.Items.Add(item1);
            _context.ItemInventory.Add(inventoryItem1);
            _context.LoansActive.Add(loan1);
            _context.LoansReservation.Add(new LoanReservationEntity
            {
                Id = 1,
                ItemId = item1.Id,
                Date_From = DateTime.MaxValue.AddDays(-2),
                Date_To = DateTime.MaxValue,
                UserId = user2.Id
            });

            _context.SaveChanges();

            var reservationsAtStart = _context.LoansReservation.ToList();
            // Act
            await _systemUnderTest.ReturnLoan(inventoryItem1.Id);
            // Assert
            var reservationsAtEnd = _context.LoansReservation.ToList();
            Assert.Equal(reservationsAtStart.Count - 1, reservationsAtEnd.Count);
            var newLoan = _context.LoansActive.First();
            Assert.Equal(newLoan.UserId, reservationsAtStart[0].UserId);
        }

        [Fact]
        public async Task ReturnLoan_OnSuccess_RemovesLoanFromDatabase()
        {
            // Arrange
            var invItemId = 1001;
            _context.Users.Add(user1);
            _context.Items.Add(item1);
            _context.ItemInventory.Add(inventoryItem1);
            _context.LoansActive.Add(loan1);

            _context.SaveChanges();

            var loansAtStart = await _context.LoansActive.ToListAsync();
            // Act
            await _systemUnderTest.ReturnLoan(invItemId);
            // Assert
            var loansAtEnd = await _context.LoansActive.ToListAsync();
            Assert.Equal(loansAtStart.Count - 1, loansAtEnd.Count);
        }

        [Fact]
        public async Task ReturnLoan_OnSuccess_TogglesInvItemToAvailable()
        {
            // Arrange
            var invItemId = 1001;
            _context.Users.Add(user1);
            _context.Items.Add(item1);
            _context.ItemInventory.Add(inventoryItem1);
            _context.LoansActive.Add(loan1);

            _context.SaveChanges();

            // Act
            var act = await _systemUnderTest.ReturnLoan(invItemId);
            // Assert
            var invItem = await _context.ItemInventory.FirstAsync(i => i.Id == invItemId);
            Assert.True(invItem.Available);
        }

        [Fact]
        public async Task ReturnLoan_OnSuccess_ReturnsSuccessMessageString()
        {
            // Arrange
            var invItemId = 1001;
            var expectedMessage = "Loan returned";
            _context.Users.Add(user1);
            _context.Items.Add(item1);
            _context.ItemInventory.Add(inventoryItem1);
            _context.LoansActive.Add(loan1);

            _context.SaveChanges();

            // Act
            var act = await _systemUnderTest.ReturnLoan(invItemId);
            // Assert
            Assert.Equal(expectedMessage, act);
        }

        [Fact]
        public async Task ReturnLoan_OnNoMatchingInvItem_ThrowsNotFoundExceptionWithMessage()
        {
            // Arrange
            var invItemId = 94328423;
            var expectedMessage = "No matching item found.";
            _context.Users.Add(user1);
            _context.Items.Add(item1);
            _context.ItemInventory.Add(inventoryItem1);
            _context.LoansActive.Add(loan1);

            _context.SaveChanges();

            // Act
            Func<Task> act = async () => await _systemUnderTest.ReturnLoan(invItemId);
            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
