using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R401TP2.Controllers;
using R401TP2.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R401TP2.Models.EntityFramework.Tests
{
    [TestClass()]
    public class SeriesDbContextTests
    {
        SeriesDbContext context;
        public SeriesController controller;
        Serie serie1, serie2, error1;



        [TestInitialize()]
        public void InitialisationDesTests()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB;uid=postgres;password=postgres;"); // Chaine de connexion à mettre dans les ( )
            SeriesDbContext context = new SeriesDbContext(builder.Options);
            controller = new SeriesController(context);
            serie1 = new Serie(
                1,
                "Scrubs",
                "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                9,
                184,
                2001,
                "ABC (US)"
            );
            serie2 = new Serie(
                138,
                "Charlie Jade",
                "Que se serait-il passé si les humains n'avaient pas abusé de la Terre et de ses ressources ? Combien le monde serait-il différent ? Faites un saut dans l'imaginaire et explorez le monde à travers 3 univers parallèles : L'Alphaverse (ce que notre monde pourrait devenir), le Betaverse (notre monde) et le Gammaverse (ce qu'aurait pu être notre monde).Charlie Jade est un détective privé dans un monde futuriste (Alphaverse) dominé par la technologie et les multinationales. Il enquête sur le meurtre d'une jeune femme inconnue... Charlie Jade est une série Sud Africaine, co-produite par le Canada, créée en 2005 par Robert Wertheimer et Chris Roland. Il s'agit d'une série originale par de nombreux aspects, notamment par le traitement visuel de l'image. La série bénéficie de décors naturels magnifiques pour le Gammaverse, sombre et futuriste à la 'Blade Runner' pour l'Alphaverse. Le téléspectateur peut ainsi identifier très facilement l'univers dans lequel se déroule chaque action. La série est tournée à Cape Town (Le Cap - Afrique du Sud)...",
                1,
                20,
                2005,
                "Space"
            );
            error1 = new Serie(
                1,
                "Scrubs",
                "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                9,
                -184,
                2001,
                "ABC (US)"
            );
        }

        [TestCleanup()]
        public void NettoyageDesTests()
        {
            controller = null;
            serie1 = null;
            serie2 = null;
        }



        [TestMethod()]
        public void GetSeries_Ok_ReturnsItems()
        {
            // Act
            var result = controller.GetSeries();
            List<Serie> listValue = result.Result.Value.Where(s => s.Serieid <= 2).ToList();
            // Assert
            CollectionAssert.Contains(listValue, serie1);
            CollectionAssert.Contains(listValue, serie2);
        }



        [TestMethod()]
        public void GetSerie_NotOk_ReturnsNotFound()
        {
            // Act
            var result = controller.GetSerie(1000);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetSerie_Ok_ReturnsRightItem()
        {
            // Act
            var result = controller.GetSerie(1);
            // Assert
            Assert.AreEqual(result.Result, serie1);
        }



        [TestMethod()]
        public void DeleteSerie_NotOk_ReturnsNotFound()
        {
            // Act
            var result = controller.DeleteSerie(0);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void DeleteSerie_Ok_ReturnsRightItem()
        {
            // Act
            var result = controller.DeleteSerie(1);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }



        [TestMethod()]
        public void PostSerie_NotOk_ReturnsAggregateException()
        {
            // Act
            var result = controller.PostSerie(error1);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(AggregateException));
        }

        [TestMethod()]
        public void PostSerie_Ok_ReturnsRightItem()
        {
            // Act
            var result = controller.PostSerie(serie1);
            // Assert
            Assert.AreEqual(result.Result, serie1);
        }



        [TestMethod()]
        public void PutSerie_NotOk_ReturnsAggregateException()
        {
            // Act
            var result = controller.PutSerie(1, error1);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(AggregateException));
        }

        [TestMethod()]
        public void PutSerie_Ok_ReturnsRightItem()
        {
            // Act
            var result = controller.PutSerie(1, serie1);
            // Assert
            Assert.AreEqual((Serie)result.Result, serie1);
        }



    }
}