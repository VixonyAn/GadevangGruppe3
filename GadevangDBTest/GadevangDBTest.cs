using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;

namespace GadevangDBTest
{
    [TestClass]
    public class GadevangServiceTest
    {
        #region Bruger Test
        [TestMethod]
        public void TestCreateBruger()
        {
            //Arrange
            IBrugerService brugerService = new BrugerService();
            List<Bruger> brugere = brugerService.GetAllBrugerAsync().Result;

            //Act
            int numOfBrugerBefore = brugere.Count;
            Bruger testBruger = new Bruger(3, "Vibeke Sandau", "JKL82", "VISA@gtmail.dk", "33333333", MedlemskabsType.Seniorer, Position.Medlem, true);
            bool ok = brugerService.CreateBrugerAsync(testBruger).Result;
            brugere = brugerService.GetAllBrugerAsync().Result;
            int numOfBrugerAfter = brugere.Count;
            Bruger removedBruger = brugerService.DeleteBrugerAsync(testBruger.BrugerId).Result;

            //Assert
            Assert.AreEqual(numOfBrugerBefore + 1, numOfBrugerAfter);
            Assert.IsTrue(ok);
            Assert.AreEqual(removedBruger.BrugerId, testBruger.BrugerId);

        }

        [TestMethod]
        public void TestUpdateBruger()
        {
            //Arrange
            IBrugerService brugerService = new BrugerService();
            List<Bruger> brugere = brugerService.GetAllBrugerAsync().Result;

            //Act
            int numOfBrugerBefore = brugere.Count;
            Bruger testBruger = new Bruger(3, "Vibeke Sandau", "JKL82", "VISA@gtmail.dk", "33333333", MedlemskabsType.Seniorer, Position.Medlem, true);
            bool ok = brugerService.CreateBrugerAsync(testBruger).Result;
            Bruger updateBruger = new Bruger(3, "Jytte Hermsgervørdenbrøtbørda", "JKL82", "JYHE@gtmail.dk", "33333333", MedlemskabsType.Seniorer, Position.Medlem, true);
            bool updateOk = brugerService.UpdateBrugerAsync(testBruger.BrugerId, updateBruger).Result;
            brugere = brugerService.GetAllBrugerAsync().Result;
            int numOfBrugerAfter = brugere.Count;
            Bruger? removedBruger = brugerService.DeleteBrugerAsync(testBruger.BrugerId).Result;

            //Assert
            Assert.AreEqual(numOfBrugerBefore + 1, numOfBrugerAfter);
            Assert.IsTrue(ok);
            Assert.IsTrue(updateOk);
            Assert.AreEqual(removedBruger.Brugernavn, updateBruger.Brugernavn);

        }
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