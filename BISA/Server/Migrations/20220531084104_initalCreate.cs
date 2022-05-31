using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BISA.Server.Migrations
{
    public partial class initalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuntimeInMinutes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Organizer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_EventType_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemInventory_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemTags",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTags", x => new { x.ItemId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ItemTags_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Date_From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanReservations_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanReservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemInventoryId = table.Column<int>(type: "int", nullable: false),
                    Date_From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanHistory_ItemInventory_ItemInventoryId",
                        column: x => x.ItemInventoryId,
                        principalTable: "ItemInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoansActive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemInventoryId = table.Column<int>(type: "int", nullable: false),
                    Date_From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoansActive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoansActive_ItemInventory_ItemInventoryId",
                        column: x => x.ItemInventoryId,
                        principalTable: "ItemInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoansActive_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventType",
                columns: new[] { "Id", "Capacity", "Description", "Image", "Type" },
                values: new object[,]
                {
                    { 1, 500, "Barnaktivitet", "högläsning.img", "Högläsning för barn" },
                    { 2, 300, "Musikevenemang", "musik.img", "Konsert" },
                    { 3, 500, "Insamlimg", "insamling.img", "Välgörenhet" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Creator", "Date", "Description", "ISBN", "Image", "Language", "Publisher", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "Selma Lagerlöf", "2017", "Gösta Berlings saga är en mustig skröna från författarinnans älskade Värmland, präglad av både burlesk humor och djupt allvar. Genom hela berättelsen löper en vemodig insikt om människans otillräcklighet och kärlekens förgänglighet.", "9789174296051", "gösta.img", "Swedish", "Bonnier", "Gösta Berlings Saga", "Book" },
                    { 3, "John Steinbeck", "2010", "1930-tal och depression i USA. När Tom Joad återvänder efter fyra år i fängelset finner han att hans familj vräkts från gården. De svåra tiderna tvingar familjen att packa allt de äger i sin skraltiga bil och ge sig av till Kalifornien. Dit har de lockats av löften om jobb på bomullsfällt och persikoplantager, men drömmen om ett bättre liv finns bara i reklambladen.", "9789174290899", "grapes.img", "English", "Bonnier", "Vredens Druvor", "Book" },
                    { 4, "Fjodor Dostojevskij", "2012", "Myskin, just hemkommen från en sjukdomsvistelse i Schweiz, kastas in i Petersburgs socitetsliv. Där förälskar han sig i Natasia och Aglaja - två av världslitteraturens mest fascinerande kvinnogestalter. Inför öppen ridå utspelas fruktansvärda tragedier, groteska farser och erotiska dramer.", "9789174292312", "idioten.img", "English", "Bonnier", "Idioten", "Book" },
                    { 6, "George Orwell", "2012", "Om den ständigt pågående striden mellan de tre staterna Eurasien, Ostasien och Oceanien. Kriget upphör aldrig men konstellationerna ändras fortlöpande. Via den ständigt vakande teleskärmen hålls invånarna uppdaterade om vem som är fienden för dagen.", "9789173539678", "1984.img", "Swedish", "Atlantis", "1984 ", "Book" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Creator", "Date", "Description", "Image", "Language", "Publisher", "Title", "Type", "Url" },
                values: new object[,]
                {
                    { 5, "Delia Owens", "2020", "En oförglömlig berättelse om naturens krafter och ensamhetens pris. Kya Clark lever ensam och i samspel med naturen utanför en liten stad vid North Carolinas kust. Byborna kallar henne ”Träskflickan” och har i många år spridit elaka rykten om henne. När en stilig quarterback hittas död i våtmarken blir hon därför omedelbart misstänkt och en mordutredning inleds.", "kräftorna.img", "English", "Forum", "Där kräftorna sjunger", "Ebook", "https://www.google.se/search?q=d%C3%A4r+kr%C3%A4ftorna+sjunger&sxsrf=ALiCzsaMhrSQdBmKgyD85ZVFaiyPKNOjlQ%3A1653903280127&source=hp&ei=sI-UYozUBMiNlwT1hKiICg&iflsig=AJiK0e8AAAAAYpSdwKIRvLdVFgXTygs6wpQYl937TDZR&gs_ssp=eJzj4tVP1zc0zDCoTI5PjjcxYPQSTzm8pEghu-jwkrSS_KK8RIXirNK89NQiABhRDuM&oq=d%C3%A4r&gs_lcp=Cgdnd3Mtd2l6EAMYADIECC4QQzIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQyBQgAEIAEMgUILhCABDIFCAAQgAQyBQgAEIAEMgUIABCABDoECCMQJzoGCCMQJxATOgQIABBDOgsILhCABBDHARCjAlAAWKkDYPUQaABwAHgAgAGRAYgB-QKSAQMwLjOYAQCgAQE&sclient=gws-wiz" },
                    { 8, "J.R.R. Tolkien", "2019", "Den lille hobbiten Bilbo Secker dras av trollkarlen Gandalf grå med på äventyr tillsammans med tretton dvärgar, ledda av den sturske Thorin Ekensköld. De ska röva bort en stor guldskatt som vaktas av den eldsprutande draken Smaug.", "hobbiten.img", "English", "Norstedts", "Hobbiten", "Ebook", "https://www.google.com/search?q=hobbiten&oq=hobbiten&aqs=chrome..69i57j46i512j0i512l4j69i60l2.1518j0j4&sourceid=chrome&ie=UTF-8" },
                    { 9, "Vilhelm Moberg", "2013", "Utvandrarna är den första delen om Kristina, Karl Oskar, Robert, Arvid och alla andra i Ljuder socken som lämnar fattig-Sverige för ett drägligare liv i USA. Efter många svältår och andra umbärande bestämmer de sig för att emigrera till Amerika och en strapatsrik resa tar sin början.", "utvandrarna.img", "Swedish", "Bonnier", "Utvandrarna ", "Ebook", "https://www.google.com/search?q=utvandrarna+bok&oq=utvandrarna+bok&aqs=chrome..69i57j46i67j0i512l2j46i512j0i512l2j69i60.4872j0j4&sourceid=chrome&ie=UTF-8" },
                    { 10, "Vilhelm Moberg", "2013", "När Karl Oskar och Kristina kommer fram till New York möter de ett land som tar emot dem med öppna armar. Och omgående börjar de den långa färden till Minnesota, där de ska bygga sitt nya hem. Som sällskap har de sina reskamrater från Ljuder socken och de blir också en stor trygghet.", "invandrarna.img", "Swedish", "Bonnier", "Invandrarna ", "Ebook", "https://www.google.com/search?q=invandrarna&oq=invandrarna&aqs=chrome..69i57j46i512l2j0i512j46i512j0i512l2j69i60.6485j0j4&sourceid=chrome&ie=UTF-8" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Creator", "Date", "Description", "Image", "Language", "Publisher", "RuntimeInMinutes", "Title", "Type" },
                values: new object[,]
                {
                    { 2, "Tony Scott", "1986", "Tom Cruise briljerar i rollen som Maverick, en ung och självsäker stridspilot som älskar fart, har mycket att bevisa och än mer att lära. Actionpackat!", "topgun.img", "English", "Warner Brothers", 121, "Top Gun", "Movie" },
                    { 7, "Joel Schumacher", "1989", "Två bröder flyttar till en ny stad och hamnar snart i fel gäng. Inget ovanligt med det, förutom att just dessa no-mark-punkare råkar vara blodsvällande vampyrer. Actionpackat klassiker!", "lostboys.img", "English", "Fox", 94, "The Lost Boys", "Movie" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Tag" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Horror" },
                    { 3, "Comedy" },
                    { 4, "Romance" },
                    { 5, "Drama" },
                    { 6, "Fantasy" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Firstname", "Lastname", "UserId", "Username" },
                values: new object[,]
                {
                    { 1, "jaff@gmail.com", "Jeffrey", "Anderson", "b74ddd14-6340-4840-95c2-db12554843e6", "jaff" },
                    { 2, "micke@gmail.com", "Mikael", "Kinder", "b74ddd14-6340-4840-95c2-db12554843e7", "micke" },
                    { 3, "junne@gmail.com", "Kalle", "Ljungberg", "b74ddd14-6340-4840-95c2-db12554843e8", "junne" },
                    { 4, "admin@gmail.com", "Ralf", "Gyllenhammarströmfors", "b74ddd14-6340-4840-95c2-db12554843e5", "admin" },
                    { 5, "alex@hotmail.com", "Alex", "Urtti", "5a389f27-9fc1-4505-b779-ccb3020af009", "alexurtti" },
                    { 6, "bosse@hotmail.com", "Boris", "Kogen", "c11686e5-daaa-4d0d-91de-cdcd6c618bbc", "bosse" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "EventTypeId", "Location", "Organizer", "Subject" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Staffan läser från sin bok 'Den vilda naturen'.", 1, "Malmö", "Läsklubben", "Exotiska djur och växter" },
                    { 2, new DateTime(2022, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fora besöker vårt bibliotek för en Österrikisk musikresa bakåt i tiden", 2, "Tranås", "Fora", "Klassisk musik" },
                    { 3, new DateTime(2022, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malmös musik community samlar in pengar till förmån för utsatta människor i Yemen", 3, "Folkets Park Malmö", "Röda Korset", "Kriget i Yemen" }
                });

            migrationBuilder.InsertData(
                table: "ItemInventory",
                columns: new[] { "Id", "Available", "ItemId" },
                values: new object[,]
                {
                    { 1, false, 1 },
                    { 2, false, 1 },
                    { 3, true, 2 },
                    { 4, false, 2 },
                    { 5, true, 3 },
                    { 6, false, 3 },
                    { 7, true, 5 },
                    { 10, true, 5 },
                    { 11, true, 4 },
                    { 12, true, 4 },
                    { 13, true, 6 },
                    { 14, false, 6 },
                    { 15, false, 7 },
                    { 16, true, 8 },
                    { 17, true, 8 },
                    { 18, false, 9 }
                });

            migrationBuilder.InsertData(
                table: "ItemTags",
                columns: new[] { "ItemId", "TagId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 5 },
                    { 2, 1 },
                    { 2, 4 },
                    { 3, 5 },
                    { 4, 3 },
                    { 5, 1 },
                    { 5, 5 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 6 },
                    { 9, 5 },
                    { 10, 5 }
                });

            migrationBuilder.InsertData(
                table: "LoanReservations",
                columns: new[] { "Id", "Date_From", "Date_To", "ItemId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2870), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2873), 1, 3 },
                    { 2, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2877), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2879), 1, 4 },
                    { 4, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2882), new DateTime(2022, 6, 7, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2884), 9, 1 },
                    { 5, new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2887), new DateTime(2022, 6, 27, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2889), 9, 3 }
                });

            migrationBuilder.InsertData(
                table: "LoanHistory",
                columns: new[] { "Id", "Date_From", "Date_To", "ItemInventoryId", "UserId" },
                values: new object[,]
                {
                    { 5, new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 6, new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1 },
                    { 7, new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 },
                    { 8, new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2 },
                    { 9, new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1 },
                    { 10, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 11, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 3 },
                    { 12, new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "LoansActive",
                columns: new[] { "Id", "Date_From", "Date_To", "ItemInventoryId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2710), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2750), 1, 2 },
                    { 2, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2755), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2757), 2, 5 },
                    { 3, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2760), new DateTime(2022, 6, 7, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2762), 4, 3 },
                    { 4, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2765), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2767), 6, 5 },
                    { 5, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2770), new DateTime(2022, 6, 7, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2772), 18, 5 },
                    { 6, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2775), new DateTime(2022, 6, 7, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2777), 15, 5 },
                    { 7, new DateTime(2022, 5, 31, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2780), new DateTime(2022, 6, 20, 10, 41, 4, 379, DateTimeKind.Local).AddTicks(2782), 14, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventory_ItemId",
                table: "ItemInventory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTags_TagId",
                table: "ItemTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanHistory_ItemInventoryId",
                table: "LoanHistory",
                column: "ItemInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanHistory_UserId",
                table: "LoanHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanReservations_ItemId",
                table: "LoanReservations",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanReservations_UserId",
                table: "LoanReservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoansActive_ItemInventoryId",
                table: "LoansActive",
                column: "ItemInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LoansActive_UserId",
                table: "LoansActive",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "ItemTags");

            migrationBuilder.DropTable(
                name: "LoanHistory");

            migrationBuilder.DropTable(
                name: "LoanReservations");

            migrationBuilder.DropTable(
                name: "LoansActive");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ItemInventory");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
