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
    }
}
