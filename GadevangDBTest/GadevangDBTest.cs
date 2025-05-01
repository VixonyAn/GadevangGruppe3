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