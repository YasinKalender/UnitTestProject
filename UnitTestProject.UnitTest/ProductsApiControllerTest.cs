using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject.UI.Contollers;
using UnitTestProject.UI.Data;
using UnitTestProject.UI.Entities;
using UnitTestProject.UI.TestHelpers;
using Xunit;

namespace UnitTestProject.UnitTest
{
    public class ProductsApiControllerTest
    {
        private Mock<IRepository<Product>> _mock;
        private ProductsApiController _productsApiController;
        private List<Product> _products;
        private TestHelper _testHelper;

        public ProductsApiControllerTest()
        {
            _mock = new Mock<IRepository<Product>>();
            _productsApiController = new ProductsApiController(_mock.Object);
            _products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Product1", Stock = 100, Price = 1000 },

        };
            _testHelper = new TestHelper();
        }

        [Fact]
        public void ProductsApiGetAllTest()
        {
            _mock.Setup(i => i.GetAll()).Returns(new List<Product>());
            var result = _productsApiController.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);


        }

        [Theory]
        [InlineData(1)]
        public void ProductApiGetSuccessTest(int Id)
        {
            var product = _products.First(i => i.Id == Id);
            var produts = _mock.Setup(i => i.GetByid(product.Id)).Returns(product);
            var result = _productsApiController.GetProduct(product.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var okResultValue = Assert.IsAssignableFrom<Product>(okResult.Value);

            var okResultValueCount = Assert.IsType<int>(_products.Count());
            Assert.Equal<int>(1, okResultValueCount);
            Assert.Equal<int>(okResultValue.Id, Id);




        }

        [Fact]
        public void ProductApiGetNullTest()
        {
            Product product = null;
            _mock.Setup(i => i.GetByid(0)).Returns(product);
            var viewResult = _productsApiController.GetProduct(0);

            Assert.IsType<NotFoundResult>(viewResult);
           

        }

        [Theory]
        [InlineData(2)]
        public void ProductApiPutNotEqualIdTest(int Id)
        {
            var product = _productsApiController.PutProduct(Id, _products.FirstOrDefault());

            Assert.IsType<BadRequestResult>(product);


        }
        [Theory]
        [InlineData(1)]
        public void ProductApiPutSuccessTest(int Id)
        {
            var product = _products.FirstOrDefault(i => i.Id == Id);
            _mock.Setup(i => i.Update(product));
            var result = _productsApiController.PutProduct(Id, product);

           
            _mock.Verify(i=>i.Update(product),Times.Once);
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public void ProductApiCreateSuccessTest()
        {
            var product = _products.First();

            _mock.Setup(i => i.Add(product));

            var viewResult = _productsApiController.PostProduct(product);

           var result =  Assert.IsType<CreatedAtActionResult>(viewResult);
            _mock.Verify(i => i.Add(product), Times.Once);

            Assert.Equal("GetProduct", result.ActionName);


        }

        [Fact]
        public void ProductApiDeleteNullTest()
        {
            Product product = null;
            _mock.Setup(i => i.GetByid(0)).Returns(product);
            var result = _productsApiController.DeleteProduct(0);
            Assert.IsType<NotFoundResult>(result.Result);


        }

        [Theory]
        [InlineData(1)]
        public void ProductApiDeleteSuccessTest(int Id)
        {
            var product = _products.FirstOrDefault(i => i.Id == Id);
            _mock.Setup(i => i.GetByid(product.Id)).Returns(product);
            _mock.Setup(i => i.Delete(product));
            var result = _productsApiController.DeleteProduct(product.Id);

            _mock.Verify(i => i.Delete(product),Times.Once);
            Assert.IsType<NoContentResult>(result.Result);

        }

        [Theory]
        [InlineData(1,2,2)]
        public void TestMultiplicationTest(int a,int b,int total)
        {
            var result = _productsApiController.TestMultiplication(a, b);
            Assert.IsType<int>(result);
            var resultExpected = _testHelper.Multiplication(a, b);
            Assert.Equal(resultExpected, total);
           
            


        }

    }
}
