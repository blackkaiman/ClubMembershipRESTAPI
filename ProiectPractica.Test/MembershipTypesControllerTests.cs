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
    public class MembershipTypesControllerTests
    {
        MembershipTypesController _controller;
        Mock<ILogger<MembershipTypesController>> _logger = new Mock<ILogger<MembershipTypesController>>();
        Mock<IMembershipTypesService> _service = new Mock<IMembershipTypesService>();


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
            _controller = new MembershipTypesController(_logger.Object, _service.Object);

            //Act
            var result = _controller.Get();

            //Assert
            Assert.IsType<StatusCodeResult>(result);
        }
        [Fact]
        public void GetTest_WhenDataAreReturned()
        {
            //Arrange
            _controller = new MembershipTypesController(_logger.Object, _service.Object);
            var membershipType1 = new MembershipType { Name = "test" };
            var membershipType2 = new MembershipType { Name = "test1" };
            List<MembershipType> listSource = new List<MembershipType>();
            listSource.Add(membershipType1);
            listSource.Add(membershipType2);
            var dbSet = GetQueryableMockDbSet(listSource);

            //act
            var membershipTypes = _service.Setup(m => m.Get()).Returns(dbSet);
            var result = _controller.Get();

            //assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<MembershipType>>(objectResult.Value);
            Assert.Equal(2, model.Count());
        }
        #endregion
        #region PostUnitTests
        [Fact]
        public void PostTest_WhenNoData()
        {
            //Arrange
            _controller = new MembershipTypesController(_logger.Object, _service.Object);
            //Act
            var result = _controller.Post(null);
            //Assert
            var resultStatusCode = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(resultStatusCode.StatusCode, (int)HttpStatusCode.InternalServerError);
        }
        [Fact]
        public void PostTest_WhenSendData()
        {   //Arrange
            _controller = new MembershipTypesController(_logger.Object, _service.Object);
            var membershipType1 = new MembershipType { Name = "test" };
            var membershipTypeAdded = _service.Setup(m => m.Post(membershipType1));


            //Act
            var result = _controller.Post(membershipType1);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(Constants.CreateMembershipType, objectResult.Value);
            Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.Created);
        }
        #endregion
        #region PutUnitTests
        [Fact]
        public void PutTests_WithoutData()
        {
            ///Arrange
            _controller = new MembershipTypesController(_logger.Object, _service.Object);
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
            _controller = new MembershipTypesController(_logger.Object, _service.Object);
            var membershipType1 = new MembershipType { Name = "test" };
            var membershipTypeUpdated = _service.Setup(m => m.Put(membershipType1));


            //Act
            var result = _controller.Put(membershipType1);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(Constants.UpdateMembershipType, objectResult.Value);

        }
        #endregion
        #region DeleteUnitTests
        [Fact]
        public void DeleteTest_WithoutData()
        {
            ///Arrange
            _controller = new MembershipTypesController(_logger.Object, _service.Object);
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
            _controller = new MembershipTypesController(_logger.Object, _service.Object);
            var membershipType1 = new MembershipType { Name = "test" };
            var membershipTypeDeleted = _service.Setup(m => m.Delete(membershipType1));


            //Act
            var result = _controller.Delete(membershipType1);
            
            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(Constants.DeleteMembershipType, objectResult.Value);
        }
        #endregion
    }
}
