using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using Microsoft.AspNetCore.Http;

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass()]
    public class DevisesControllerTests
    {
        private DevisesController controller;

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            controller = new DevisesController();
        }

        [TestMethod]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            // Act
            var result = controller.GetById(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsNull(result.Result, "Erreur est pas null"); // Test de l'erreur
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une Devise"); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques"); //Test de la devise récupérée
        }

        [TestMethod]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Arrange
            // Act
            var result = controller.GetById(4);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFoundResult"); // Test du type de retour.retour
            Assert.AreEqual(null, (Devise?)result.Value, "Devises pas identiques"); //Test de la devise récupérée
        }

        [TestMethod]
        public void GetAll_ReturnsRightsItems()
        {
            // Arrange
            // Act
            var result = controller.GetAll();
            List<Devise> resultList = result.ToList();
            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>), "Pas un IEnumerable"); // Test du type de retour
            CollectionAssert.AreEqual(new List<Devise> 
            {
                new Devise { Id = 1, NomDevise = "Dollar", Taux = 1.08 },
                new Devise { Id = 2, NomDevise = "Franc Suisse", Taux = 1.07 },
                new Devise { Id = 3, NomDevise = "Yen", Taux = 120 }
            }, resultList); // Test de la liste
        }

        [TestMethod]
        public void Post_ValidObjectPassed_ReturnsObject()
        {
            // Arrange
            Devise devise = new Devise(4, "Nicolas", 2.5);
            // Act
            var result = controller.Post(devise);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult<Devise>"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas un CreatedAtRouteResult"); // Test du type de retour.retour
            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
            Assert.AreEqual(StatusCodes.Status201Created, routeResult.StatusCode, "Pas de status 201"); // Test de la property StatusCode
            Assert.AreEqual(devise, (Devise)routeResult.Value, "Pas la même devise"); // Test la devise retournée
        }

        //[TestMethod]
        //public void Post_InvalidObjectPassed_ReturnsBadRequest()
        //{
        //    // Arrange
        //    DevisesController controller = new DevisesController();
        //    // Act
        //    var result = controller.Post(new Devise(4, null, 4.0));
        //    CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
        //    var deviseRetournee = (Devise)routeResult.Value;
        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult<Devise>"); // Test du type de retour
        //    Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas un CreatedAtRouteResult"); // Test du type de retour.retour
        //    Assert.AreEqual(StatusCodes.Status201Created, routeResult.StatusCode, "Pas de status 201"); // Test de la property StatusCode
        //    Assert.AreEqual(routeResult.Value, deviseRetournee, "Pas la même devise"); // Test la devise retournée
        //}

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsBadRequest()
        {
            // Arrange
            // Act
            var result = controller.Put(5, new Devise(4, "Nicolas", 2.5));
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Pas un BadRequestResult"); // Test du type de retour
        }

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsNotFound()
        {
            // Arrange
            // Act
            var result = controller.Put(100, new Devise(100, "Yin-Yang", 5));
            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult"); // Test du type de retour
        }

        [TestMethod]
        public void Put_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            // Act
            var result = controller.Put(3, new Devise(3, "Yen", 120));
            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void Delete_NotOk_ReturnsNotFound()
        {
            // Arrange
            // Act
            var result = controller.Delete(5);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFoundResult"); // Test du type de retour
        }

        [TestMethod]
        public void Delete_Ok_ReturnsRightItem()
        {
            // Arrange
            // Act
            var result = controller.Delete(2);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult<Devise>"); // Test du type de retour
        }

        [TestCleanup]
        public void NettoyageDesTests()
        {
            // Nettoyer les variables, ... après chaque test
        }
    }
}