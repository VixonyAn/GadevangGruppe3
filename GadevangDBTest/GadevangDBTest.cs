using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;

namespace GadevangDBTest
{
    [TestClass]
    public class GadevangServiceTest
    {
        #region Bruger Test
        #endregion

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
            int numbersOgBanerAfter = baner.Count(); // <- Fejlen er her!

            // Assert
            Assert.AreEqual(numberOfbanerBefore, numbersOgBanerAfter); // <- dette vil ALTID fejle, hvis oprettelsen lykkes
        }

        #endregion

        #region Event Test
        [TestMethod]
        public void TestCreateBegivenhed()
        {
            //Arrange
            IBegivenhedService begivenhedService = new BegivenhedService();
            List<Begivenhed> begivenheder = begivenhedService.GetAllBegivenhedAsync().Result;

            //Act
            int numOfBegivenhedBefore = begivenheder.Count;
            Begivenhed newBegivenhed = new Begivenhed(3, "General Forsamling", "Klubhuset", new DateTime(2025, 02, 16, 18, 30, 00), "Vi mødes for at diskutere budgettet og begivenheder til den kommende sæson", 40, 0);
            bool ok = begivenhedService.CreateBegivenhedAsync(newBegivenhed).Result;
            begivenheder = begivenhedService.GetAllBegivenhedAsync().Result;
            int numOfBegivenhedAfter = begivenheder.Count;

            //Assert
            Assert.AreEqual(numOfBegivenhedBefore + 1, numOfBegivenhedAfter);
            Assert.IsTrue(ok);
        }
        #endregion
    }
}