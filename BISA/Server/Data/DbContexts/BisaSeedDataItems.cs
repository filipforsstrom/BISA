using BISA.Shared.Entities;

namespace BISA.Server.Data.DbContexts
{
    public static class BisaSeedDataItems
    {
        public static void SeedItems(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>()
                .HasData(
                    new BookEntity { Id = 1, Title = "Gösta Berlings Saga", ISBN = "9789174296051", Creator = "Selma Lagerlöf", Date = "2017", Publisher = "Bonnier", Language = "Swedish", Image = "gösta.img", Description = "Gösta Berlings saga är en mustig skröna från författarinnans älskade Värmland, präglad av både burlesk humor och djupt allvar. Genom hela berättelsen löper en vemodig insikt om människans otillräcklighet och kärlekens förgänglighet." },
                    new BookEntity { Id = 3, Title = "Vredens Druvor", ISBN = "9789174290899", Creator = "John Steinbeck", Date = "2010", Publisher = "Bonnier", Language = "English", Image = "grapes.img", Description = "1930-tal och depression i USA. När Tom Joad återvänder efter fyra år i fängelset finner han att hans familj vräkts från gården. De svåra tiderna tvingar familjen att packa allt de äger i sin skraltiga bil och ge sig av till Kalifornien. Dit har de lockats av löften om jobb på bomullsfällt och persikoplantager, men drömmen om ett bättre liv finns bara i reklambladen." },
                    new BookEntity { Id = 4, Title = "Idioten", ISBN = "9789174292312", Creator = "Fjodor Dostojevskij", Date = "2012", Publisher = "Bonnier", Language = "English", Image = "idioten.img", Description = "Myskin, just hemkommen från en sjukdomsvistelse i Schweiz, kastas in i Petersburgs socitetsliv. Där förälskar han sig i Natasia och Aglaja - två av världslitteraturens mest fascinerande kvinnogestalter. Inför öppen ridå utspelas fruktansvärda tragedier, groteska farser och erotiska dramer." },
                    new BookEntity { Id = 6, Title = "1984 ", ISBN = "9789173539678", Creator = "George Orwell", Date = "2012", Publisher = "Atlantis", Language = "Swedish", Image = "1984.img", Description = "Om den ständigt pågående striden mellan de tre staterna Eurasien, Ostasien och Oceanien. Kriget upphör aldrig men konstellationerna ändras fortlöpande. Via den ständigt vakande teleskärmen hålls invånarna uppdaterade om vem som är fienden för dagen." }

                );

            modelBuilder.Entity<MovieEntity>()
                .HasData(
                    new MovieEntity { Id = 2, Title = "Top Gun", RuntimeInMinutes = 121, Creator = "Tony Scott", Language = "English", Date = "1986", Publisher = "Warner Brothers", Image  = "topgun.img", Description = "Tom Cruise briljerar i rollen som Maverick, en ung och självsäker stridspilot som älskar fart, har mycket att bevisa och än mer att lära. Actionpackat!" },
                    new MovieEntity { Id = 7, Title = "The Lost Boys", RuntimeInMinutes = 94, Creator = "Joel Schumacher", Language = "English", Date = "1989", Publisher = "Fox", Image = "lostboys.img", Description = "Två bröder flyttar till en ny stad och hamnar snart i fel gäng. Inget ovanligt med det, förutom att just dessa no-mark-punkare råkar vara blodsvällande vampyrer. Actionpackat klassiker!" }
                    );

            modelBuilder.Entity<EbookEntity>()
                .HasData(
                    new EbookEntity
                    {
                        Id = 5,
                        Date = "2020",
                        Creator = "Delia Owens",
                        Language = "English",
                        Publisher = "Forum",
                        Title = "Där kräftorna sjunger",
                        Description = "En oförglömlig berättelse om naturens krafter och ensamhetens pris. Kya Clark lever ensam och i samspel med naturen utanför en liten stad vid North Carolinas kust. Byborna kallar henne ”Träskflickan” och har i många år spridit elaka rykten om henne. När en stilig quarterback hittas död i våtmarken blir hon därför omedelbart misstänkt och en mordutredning inleds.",
                        Image = "kräftorna.img",
                        Url = "https://www.google.se/search?q=d%C3%A4r+kr%C3%A4ftorna+sjunger&sxsrf=ALiCzsaMhrSQdBmKgyD85ZVFaiyPKNOjlQ%3A1653903280127&source=hp&ei=sI-UYozUBMiNlwT1hKiICg&iflsig=AJiK0e8AAAAAYpSdwKIRvLdVFgXTygs6wpQYl937TDZR&gs_ssp=eJzj4tVP1zc0zDCoTI5PjjcxYPQSTzm8pEghu-jwkrSS_KK8RIXirNK89NQiABhRDuM&oq=d%C3%A4r&gs_lcp=Cgdnd3Mtd2l6EAMYADIECC4QQzIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQyBQgAEIAEMgUILhCABDIFCAAQgAQyBQgAEIAEMgUIABCABDoECCMQJzoGCCMQJxATOgQIABBDOgsILhCABBDHARCjAlAAWKkDYPUQaABwAHgAgAGRAYgB-QKSAQMwLjOYAQCgAQE&sclient=gws-wiz"
                    },
                    new EbookEntity
                    {
                        Id = 8,
                        Date = "2019",
                        Creator = "J.R.R. Tolkien",
                        Language = "English",
                        Publisher = "Norstedts",
                        Title = "Hobbiten",
                        Description = "Den lille hobbiten Bilbo Secker dras av trollkarlen Gandalf grå med på äventyr tillsammans med tretton dvärgar, ledda av den sturske Thorin Ekensköld. De ska röva bort en stor guldskatt som vaktas av den eldsprutande draken Smaug.",
                        Image = "hobbiten.img",
                        Url = "https://www.google.com/search?q=hobbiten&oq=hobbiten&aqs=chrome..69i57j46i512j0i512l4j69i60l2.1518j0j4&sourceid=chrome&ie=UTF-8"
                    },
                    new EbookEntity
                    {
                        Id = 9,
                        Date = "2013",
                        Creator = "Vilhelm Moberg",
                        Language = "Swedish",
                        Publisher = "Bonnier",
                        Title = "Utvandrarna ",
                        Description = "Utvandrarna är den första delen om Kristina, Karl Oskar, Robert, Arvid och alla andra i Ljuder socken som lämnar fattig-Sverige för ett drägligare liv i USA. Efter många svältår och andra umbärande bestämmer de sig för att emigrera till Amerika och en strapatsrik resa tar sin början.",
                        Image = "utvandrarna.img",
                        Url = "https://www.google.com/search?q=utvandrarna+bok&oq=utvandrarna+bok&aqs=chrome..69i57j46i67j0i512l2j46i512j0i512l2j69i60.4872j0j4&sourceid=chrome&ie=UTF-8"
                    },
                    new EbookEntity
                    {
                        Id = 10,
                        Date = "2013",
                        Creator = "Vilhelm Moberg",
                        Language = "Swedish",
                        Publisher = "Bonnier",
                        Title = "Invandrarna ",
                        Description = "När Karl Oskar och Kristina kommer fram till New York möter de ett land som tar emot dem med öppna armar. Och omgående börjar de den långa färden till Minnesota, där de ska bygga sitt nya hem. Som sällskap har de sina reskamrater från Ljuder socken och de blir också en stor trygghet.",
                        Image = "invandrarna.img",
                        Url = "https://www.google.com/search?q=invandrarna&oq=invandrarna&aqs=chrome..69i57j46i512l2j0i512j46i512j0i512l2j69i60.6485j0j4&sourceid=chrome&ie=UTF-8"
                    });

        }
        public static void SeedTags(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagEntity>()
                .HasData(
                    new TagEntity { Id = 1, Tag = "Action" },
                    new TagEntity { Id = 2, Tag = "Horror" },
                    new TagEntity { Id = 3, Tag = "Comedy" },
                    new TagEntity { Id = 4, Tag = "Romance" },
                    new TagEntity { Id = 5, Tag = "Drama" },
                    new TagEntity { Id = 6, Tag = "Fantasy" }
                );

            modelBuilder.Entity<ItemTagEntity>()
                .HasData(
                    new ItemTagEntity { ItemId = 1, TagId = 5 },
                    new ItemTagEntity { ItemId = 1, TagId = 3 },
                    new ItemTagEntity { ItemId = 2, TagId = 1 },
                    new ItemTagEntity { ItemId = 2, TagId = 4 },
                    new ItemTagEntity { ItemId = 5, TagId = 5 },
                    new ItemTagEntity { ItemId = 3, TagId = 5 },
                    new ItemTagEntity { ItemId = 4, TagId = 3 },
                    new ItemTagEntity { ItemId = 5, TagId = 1 },
                    new ItemTagEntity { ItemId = 6, TagId = 2 },
                    new ItemTagEntity { ItemId = 7, TagId = 2 },
                    new ItemTagEntity { ItemId = 8, TagId = 6 },
                    new ItemTagEntity { ItemId = 9, TagId = 5 },
                    new ItemTagEntity { ItemId = 10, TagId = 5 }
                );
        }

        public static void SeedItemInventory(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemInventoryEntity>()
                .HasData(
                    new ItemInventoryEntity { Id = 1, ItemId = 1, Available = false },
                    new ItemInventoryEntity { Id = 2, ItemId = 1, Available = false },
                    new ItemInventoryEntity { Id = 3, ItemId = 2, Available = true },
                    new ItemInventoryEntity { Id = 4, ItemId = 2, Available = false },
                    new ItemInventoryEntity { Id = 5, ItemId = 3, Available = true },
                    new ItemInventoryEntity { Id = 6, ItemId = 3, Available = false },
                    new ItemInventoryEntity { Id = 7, ItemId = 5, Available = true },
                    new ItemInventoryEntity { Id = 10, ItemId = 5, Available = true },
                    new ItemInventoryEntity { Id = 11, ItemId = 4, Available = true },
                    new ItemInventoryEntity { Id = 12, ItemId = 4, Available = true },
                    new ItemInventoryEntity { Id = 13, ItemId = 6, Available = true },
                    new ItemInventoryEntity { Id = 14, ItemId = 6, Available = false },
                    new ItemInventoryEntity { Id = 15, ItemId = 7, Available = false },
                    new ItemInventoryEntity { Id = 16, ItemId = 8, Available = true },
                    new ItemInventoryEntity { Id = 17, ItemId = 8, Available = true },
                    new ItemInventoryEntity { Id = 18, ItemId = 9, Available = false }
                );
        }
        public static void SeedLoans(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanEntity>()
                .HasData(
                    new LoanEntity { Id = 1, ItemInventoryId = 1, UserId = 2, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(20)},
                    new LoanEntity { Id = 2, ItemInventoryId = 2, UserId = 5, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(20) },
                    new LoanEntity { Id = 3, ItemInventoryId = 4, UserId = 3, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(7) },
                    new LoanEntity { Id = 4, ItemInventoryId = 6, UserId = 5, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(20) },
                    new LoanEntity { Id = 5, ItemInventoryId = 18, UserId = 5, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(7) },
                    new LoanEntity { Id = 6, ItemInventoryId = 15, UserId = 5, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(7) },
                    new LoanEntity { Id = 7, ItemInventoryId = 14, UserId = 5, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(20) }
                );
            modelBuilder.Entity<LoanHistoryEntity>()
                .HasData(
                    new LoanHistoryEntity { Id = 5, ItemInventoryId = 1, UserId = 1, Date_From = new DateTime(2021, 05, 14), Date_To = new DateTime(2021, 06, 03)},
                    new LoanHistoryEntity { Id = 6, ItemInventoryId = 6, UserId = 1, Date_From = new DateTime(2021, 05, 14), Date_To = new DateTime(2021, 06, 03) },
                    new LoanHistoryEntity { Id = 7, ItemInventoryId = 3, UserId = 2, Date_From = new DateTime(2021, 08, 01), Date_To = new DateTime(2021, 08, 08) },
                    new LoanHistoryEntity { Id = 8, ItemInventoryId = 6, UserId = 2, Date_From = new DateTime(2021, 08, 01), Date_To = new DateTime(2021, 08, 21) },
                    new LoanHistoryEntity { Id = 9, ItemInventoryId = 6, UserId = 1, Date_From = new DateTime(2021,09,01), Date_To = new DateTime(2021,09,21) },
                    new LoanHistoryEntity { Id = 10, ItemInventoryId = 1, UserId = 3, Date_From = new DateTime(2022,02,01), Date_To = new DateTime(2022,02,21) },
                    new LoanHistoryEntity { Id = 11, ItemInventoryId = 6, UserId = 3, Date_From = new DateTime(2022,02, 01), Date_To = new DateTime(2022,02,21) },
                    new LoanHistoryEntity { Id = 12, ItemInventoryId = 3, UserId = 1, Date_From = new DateTime(2022,01,10), Date_To = new DateTime(2022,01,17) }
                );
        }
        public static void SeedReservations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanReservationEntity>()
                .HasData(
                    new LoanReservationEntity { Id = 1, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(20), ItemId = 1, UserId = 3 },
                    new LoanReservationEntity { Id = 2, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(20), ItemId = 1, UserId = 4 },
                    new LoanReservationEntity { Id = 4, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(7), ItemId = 9, UserId = 1},
                    new LoanReservationEntity { Id = 5, Date_From = DateTime.Now.AddDays(20), Date_To = DateTime.Now.AddDays(27), ItemId = 9, UserId = 3}
                );
        }
    }
}
