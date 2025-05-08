using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.Extensions.Hosting;

namespace GadevangDBTest
{
    [TestClass]
    public class GadevangServiceTest
    {
        //#region Bruger Test
        //[TestMethod]
        //public void TestReadBruger()
        //{
        //    //Arrange
        //    IBrugerService brugerService = new BrugerService();
        //    List<Bruger> bruger = brugerService.GetAllBrugerAsync().Result;

        //    //Act
        //    int numOfBruger = bruger.Count;

        //    //Assert
        //    Assert.IsNotNull(numOfBruger);
        //}

        //[TestMethod]
        //public void TestCreateBruger()
        //{
        //    //Arrange
        //    IBrugerService brugerService = new BrugerService();
        //    List<Bruger> brugere = brugerService.GetAllBrugerAsync().Result;

        //    //Act
        //    int numOfBrugerBefore = brugere.Count;
        //    Bruger testBruger = new Bruger(999, "Vibeke Sandau", "JKL82", "VISA@gtmail.dk", "33333333", MedlemskabsType.Seniorer, Position.Medlem, true);
        //    bool ok = brugerService.CreateBrugerAsync(testBruger).Result;
        //    brugere = brugerService.GetAllBrugerAsync().Result;
        //    int numOfBrugerAfter = brugere.Count;
        //    Bruger? removedBruger = brugerService.DeleteBrugerAsync(testBruger.BrugerId).Result;

        //    //Assert
        //    Assert.AreEqual(numOfBrugerBefore + 1, numOfBrugerAfter);
        //    Assert.IsTrue(ok);
        //    Assert.AreEqual(removedBruger.BrugerId, testBruger.BrugerId);

        //}
        
        //[TestMethod]
        //public void TestUpdateBruger()
        //{
        //    //Arrange
        //    IBrugerService brugerService = new BrugerService();
        //    List<Bruger> brugere = brugerService.GetAllBrugerAsync().Result;

        //    //Act
        //    int numOfBrugerBefore = brugere.Count;
        //    Bruger testBruger = new Bruger(999, "Vibeke Sandau", "JKL82", "VISA@gtmail.dk", "33333333", MedlemskabsType.Seniorer, Position.Medlem, true);
        //    bool ok = brugerService.CreateBrugerAsync(testBruger).Result;
        //    Bruger updateBruger = new Bruger(999, "Jytte Hermsgervørdenbrøtbørda", "JKL82", "JYHE@gtmail.dk", "33333333", MedlemskabsType.Seniorer, Position.Medlem, true);
        //    bool updateOk = brugerService.UpdateBrugerAsync(testBruger.BrugerId, updateBruger).Result;
        //    brugere = brugerService.GetAllBrugerAsync().Result;
        //    int numOfBrugerAfter = brugere.Count;
        //    Bruger? removedBruger = brugerService.DeleteBrugerAsync(testBruger.BrugerId).Result;

        //    //Assert
        //    Assert.AreEqual(numOfBrugerBefore + 1, numOfBrugerAfter);
        //    Assert.IsTrue(ok);
        //    Assert.IsTrue(updateOk);
        //    Assert.AreEqual(removedBruger.Brugernavn, updateBruger.Brugernavn);

        //}
        //#endregion

        #region Bane Test
        [TestMethod]
        public void TestGetAllBaner() 
        {
            //Arrange
            IBaneService bs = new BaneService();
            List<Bane> baner = bs.GetAllBaneAsync().Result;

            //Act
            int numbersOfBaner = baner.Count();

            //assert
            Assert.IsNotNull(numbersOfBaner);
        }

        [TestMethod]
        public void TestCreateBaner()
        {
            // Arrange
            IBaneService bs = new BaneService();
            List<Bane> baner = bs.GetAllBaneAsync().Result;

            // Act
            int numberOfbanerBefore = baner.Count();
            Bane b1 = new Bane(10, BaneType.Tennis, BaneMiljø.Udendørs, "Grusbane til dobbbelt spil");
            bs.CreateBaneAsync(b1);
            int numbersOgBanerAfter = baner.Count();

            // Assert
            Assert.AreEqual(numberOfbanerBefore, numbersOgBanerAfter); 
        }

        [TestMethod]
        public void TestGetBenaByIdAndUpdate()
        {
            //Arrange
            IBaneService bs = new BaneService();
            List<Bane> baner = bs.GetAllBaneAsync().Result;

            //Act
            Bane b1 = new Bane(10, BaneType.Tennis, BaneMiljø.Udendørs, "Grusbane til dobbbelt spil");
            bs.CreateBaneAsync(b1);
            Bane b2 = new Bane(10, BaneType.Paddel, BaneMiljø.Indendørs, "Nylavet bane");
            bs.UpdateBaneAsync(10, b2);
            
            //Assert
            Assert.AreNotEqual(b1, b2);
        }

        [TestMethod]
        public void TestDeleteBane()
        {

            // Arrange
            IBaneService bs = new BaneService();
            List<Bane> baner = bs.GetAllBaneAsync().Result;

            // Act 
                   //Der oprettes en ny bane, hvorefter der opretttes en ny liste BanerAfterCreate,
                   //hvor count gemmes som numberOFBanerAfter
            Bane bd = new Bane(20, BaneType.Paddel, BaneMiljø.Udendørs, "Nylavet");
            bs.CreateBaneAsync(bd);
            List<Bane> banerAfterCreate = bs.GetAllBaneAsync().Result;
            int numbersOfBanerAfterCreate = banerAfterCreate.Count();

                   //Den nye bane slettes, hvorefter der opretttes en ny liste BanerAfterDelete,
                  //hvor count gemmes som numberOgBanerAfterDeletee
            bs.DeleteBaneAsync(bd.BaneId);

            List<Bane> banerAfter = bs.GetAllBaneAsync().Result;
            int numberOfBanerAfterDelete = banerAfter.Count();

            // Assert
            Assert.AreEqual(banerAfterCreate.Count()-1, banerAfter.Count());
        }
        #endregion

        #region Booking Test
        [TestMethod]
        public void TestCreateBookingAngGetAlll() 
        {
            //Arrange
            IBookingService bs = new BookingService();
            List<Booking> bookinger = new List<Booking>();

            //Act
            int BookingerBefore = bookinger.Count();
            Booking b = new Booking(10,1,new DateOnly(2025,06,11),10,1,2,"Personlig");
            bool ok = bs.CreateBookingAsync(b).Result;
            //Assert
            Assert.IsNotNull(BookingerBefore);
            Assert.IsTrue(ok);

        }
        #endregion

        #region Event Test
        [TestMethod]
        public void TestReadBegivenhed()
        {
            //Arrange
            IBegivenhedService begivenhedService = new BegivenhedService();
            List<Begivenhed> begivenheder = begivenhedService.GetAllBegivenhedAsync().Result;

            //Act
            int numOfBegivenhed = begivenheder.Count;

            //Assert
            Assert.IsNotNull(numOfBegivenhed);
        }

        [TestMethod]
        public void TestCreateDeleteBegivenhed()
        {
            //Arrange
            IBegivenhedService begivenhedService = new BegivenhedService();
            List<Begivenhed> begivenheder = begivenhedService.GetAllBegivenhedAsync().Result;

            //Act
            int numOfBegivenhedBefore = begivenheder.Count;
            Begivenhed newBegivenhed = new Begivenhed(999, "General Forsamling", "Klubhuset", new DateTime(2025, 02, 16, 18, 30, 00), "Vi mødes for at diskutere budgettet og begivenheder til den kommende sæson", 40, 0);
            bool ok = begivenhedService.CreateBegivenhedAsync(newBegivenhed).Result;
            begivenheder = begivenhedService.GetAllBegivenhedAsync().Result;
            int numOfBegivenhedAfter = begivenheder.Count;
            Begivenhed? b = begivenhedService.DeleteBegivenhedAsync(newBegivenhed.EventId).Result;

            //Assert
            Assert.AreEqual(numOfBegivenhedBefore + 1, numOfBegivenhedAfter);
            Assert.IsTrue(ok);
            Assert.AreEqual(b?.EventId, newBegivenhed.EventId);
        }

        [TestMethod]
        public void TestUpdateBegivenhed()
        {
            //Arrange
            IBegivenhedService begivenhedService = new BegivenhedService();
            List<Begivenhed> begivenheder = begivenhedService.GetAllBegivenhedAsync().Result;

            //Act
            int numOfBegivenhedBefore = begivenheder.Count;

            Begivenhed newBegivenhed = new Begivenhed(999, "General Forsamling", "Klubhuset", new DateTime(2025, 02, 16, 18, 30, 00), "Vi mødes for at diskutere budgettet og begivenheder til den kommende sæson", 40, 0);
            bool ok = begivenhedService.CreateBegivenhedAsync(newBegivenhed).Result;

            Begivenhed updateBegivenhed = new Begivenhed(999, "Forsamling", "Gadevang Tennisklub", new DateTime(2025, 06, 20, 19, 45, 00), "Vi mødes for at diskutere budgettet og begivenheder til den kommende sæson", 40, 0);
            bool updOk = begivenhedService.UpdateBegivenhedAsync(updateBegivenhed, newBegivenhed.EventId).Result;

            begivenheder = begivenhedService.GetAllBegivenhedAsync().Result;
            int numOfBegivenhedAfter = begivenheder.Count;

            Begivenhed? b = begivenhedService.DeleteBegivenhedAsync(newBegivenhed.EventId).Result;

            //Assert
            Assert.AreEqual(numOfBegivenhedBefore + 1, numOfBegivenhedAfter);
            Assert.IsTrue(ok);
            Assert.IsTrue(updOk);
            Assert.AreEqual(b.Titel, updateBegivenhed.Titel);
            #endregion
        }
    }
}