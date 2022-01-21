using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProiectPractica.Controllers;
using ProiectPractica.Models;
using ProiectPractica.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProiectPractica.Test.ControllerTests
{
    public class AnnouncementsControllerTests
    {
        AnnouncementsController _controller;
        Mock<ILogger<AnnouncementsController>> _logger = new Mock<ILogger<AnnouncementsController>>();
        Mock<IAnnouncementsService> _service = new Mock<IAnnouncementsService>();


        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable(); var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s)); return dbSet.Object;
        }
        #region GetUnitTests
        [Fact]
        public void GetTest_WhenNoDataAreReturned()
        {
            //Arrange
            _controller = new AnnouncementsController(_logger.Object, _service.Object);

            //Act
            var result = _controller.Get();

            //Assert
            Assert.IsType<StatusCodeResult>(result);
        }

        [Fact]
        public void GetTest_WhenDataAreReturned()
        {
            //Arrange
            _controller = new AnnouncementsController(_logger.Object, _service.Object);
            var announcement1 = new Announcement { Title = "test", Text = "test" };
            var announcement2 = new Announcement { Title = "test1", Text = "test1" };
            List<Announcement> listSource = new List<Announcement>();
            listSource.Add(announcement1);
            listSource.Add(announcement2);
            var dbSet = GetQueryableMockDbSet(listSource);

            //act
            var announcements = _service.Setup(m => m.Get()).Returns(dbSet);
            var result = _controller.Get();

            //assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Announcement>>(objectResult.Value);
            Assert.Equal(2, model.Count());
        }
        #endregion
        #region PostUnitTests
        [Fact]
        public void PostTest_WhenNoData()
        {
            //Arrange
            _controller = new AnnouncementsController(_logger.Object, _service.Object);
            //Act
            var result = _controller.Post(null);
            //Assert
            var resultStatusCode = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(resultStatusCode.StatusCode, (int)HttpStatusCode.InternalServerError);
        }
        [Fact]
        public void PostTest_WhenSendData()
        {   //Arrange
            _controller = new AnnouncementsController(_logger.Object, _service.Object);
            var announcement1 = new Announcement { Title = "test", Text = "test" };
            var announcementAdded = _service.Setup(m => m.Post(announcement1));


            //Act
            var result = _controller.Post(announcement1);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(Constants.CreateAnnouncement, objectResult.Value);
            Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.Created);
        }
        #endregion
        #region PutUnitTests
        [Fact]
        public void PutTests_WithoutData()
        {
            ///Arrange
            _controller = new AnnouncementsController(_logger.Object, _service.Object);
            //Act
            var result = _controller.Put(null);
            //Assert
            var resultStatusCode = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(resultStatusCode.StatusCode, (int)HttpStatusCode.NotFound);

        }
        [Fact]
        public void PutTests_WithData()
        {
            //Arrange
            _controller = new AnnouncementsController(_logger.Object, _service.Object);
            var announcement1 = new Announcement { Title = "test", Text = "test" };
            var announcementUpdated = _service.Setup(m => m.Put(announcement1));


            //Act
            var result = _controller.Put(announcement1);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(Constants.UpdateAnnouncement, objectResult.Value);

        }
        #endregion
        #region DeleteUnitTests
        [Fact]
        public void DeleteTest_WithoutData()
        {
            ///Arrange
            _controller = new AnnouncementsController(_logger.Object, _service.Object);
            //Act
            var result = _controller.Delete(null);
            //Assert
            var resultStatusCode = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(resultStatusCode.StatusCode, (int)HttpStatusCode.NotFound);
        }
        [Fact]
        public void DeleteTest_WithData()
        {
            //Arrange
            _controller = new AnnouncementsController(_logger.Object, _service.Object);
            var announcement1 = new Announcement { Title = "test", Text = "test" };
            var announcementDeleted = _service.Setup(m => m.Delete(announcement1));


            //Act
            var result = _controller.Delete(announcement1);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(Constants.DeleteAnnouncement, objectResult.Value);
        }
        #endregion
    }
}
