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
    public class CodeSnippetsControllerTests
    {
        CodeSnippetsController _controller;
        Mock<ILogger<CodeSnippetsController>> _logger = new Mock<ILogger<CodeSnippetsController>>();
        Mock<ICodeSnippetsService> _service = new Mock<ICodeSnippetsService>();


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
            _controller = new CodeSnippetsController(_logger.Object, _service.Object);

            //Act
            var result = _controller.Get();

            //Assert
            Assert.IsType<StatusCodeResult>(result);
        }

        [Fact]
        public void GetTest_WhenDataAreReturned()
        {
            //Arrange
            _controller = new CodeSnippetsController(_logger.Object, _service.Object);
            var codeSnippet1 = new CodeSnippet { Title = "test", ContentCode = "test" };
            var codeSnippet2 = new CodeSnippet { Title = "test1", ContentCode = "test1" };
            List<CodeSnippet> listSource = new List<CodeSnippet>();
            listSource.Add(codeSnippet1);
            listSource.Add(codeSnippet2);
            var dbSet = GetQueryableMockDbSet(listSource);

            //act
            var codeSnippets = _service.Setup(m => m.Get()).Returns(dbSet);
            var result = _controller.Get();

            //assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CodeSnippet>>(objectResult.Value);
            Assert.Equal(2, model.Count());
        }
        #endregion
        #region PostUnitTests
        [Fact]
        public void PostTest_WhenNoData()
        {
            //Arrange
            _controller = new CodeSnippetsController(_logger.Object, _service.Object);
            //Act
            var result = _controller.Post(null);
            //Assert
            var resultStatusCode = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(resultStatusCode.StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public void PostTest_WhenSendData()
        {   //Arrange
            _controller = new CodeSnippetsController(_logger.Object, _service.Object);
            var codeSnippet1 = new CodeSnippet { Title = "test", ContentCode = "test" };
            var codeSnippetAdded = _service.Setup(m => m.Post(codeSnippet1));


            //Act
            var result = _controller.Post(codeSnippet1);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(Constants.CreateCodeSnippet, objectResult.Value);
            Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.Created);
        }
        #endregion

        #region PutUnitTests
        [Fact]
        public void PutTests_WithoutData()
        {
            //Arrange
            _controller = new CodeSnippetsController(_logger.Object, _service.Object);

            //Act
            var result = _controller.Put(null);

            //Assert
            var objectResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.NotFound);

        }
        [Fact]
        public void PutTests_WithData()
        {
            //Arrange
            _controller = new CodeSnippetsController(_logger.Object, _service.Object);
            var codeSnippet1 = new CodeSnippet { Title = "test", ContentCode = "test" };
            var codeSnippetUpdated = _service.Setup(m => m.Put(codeSnippet1));

            //Act
            var result = _controller.Put(codeSnippet1);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(Constants.UpdateCodeSnippet, objectResult.Value);

        }
        #endregion
        #region DeleteUnitTests
        [Fact]
        public void DeleteTest_WithoutData()
        {
            //Arrange
            _controller = new CodeSnippetsController(_logger.Object, _service.Object);

            //Act
            var result = _controller.Delete(null);

            //Assert
            var objectResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public void DeleteTest_WithData()
        {
            //Arrange
            _controller = new CodeSnippetsController(_logger.Object, _service.Object);
            var codeSnippet1 = new CodeSnippet { Title = "test", ContentCode = "test" };
            var codeSnippetDeleted = _service.Setup(m => m.Delete(codeSnippet1));

            //Act
            var result = _controller.Delete(codeSnippet1);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(Constants.DeleteCodeSnippet, objectResult.Value);
        }
        #endregion
    }
}
