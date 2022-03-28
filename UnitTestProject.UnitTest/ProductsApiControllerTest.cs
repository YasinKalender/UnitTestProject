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
using Xunit;

namespace UnitTestProject.UnitTest
{
    public class ProductsApiControllerTest
    {
        private Mock<IRepository<Product>> _mock;
        private ProductsApiController _productsApiController;
        private List<Product> _products;

        public ProductsApiControllerTest()
        {
            _mock = new Mock<IRepository<Product>>();
            _productsApiController = new ProductsApiController(_mock.Object);
            _products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Product1", Stock = 100, Price = 1000 },

        };
        }

        [Fact]
        public void ProductsApiGetAllTest()
        {
            _mock.Setup(i => i.GetAll()).Returns(new List<Product>());
            var result = _productsApiController.GetProducts();

            Assert.IsType<OkObjectResult>(result);


        }

        [Theory]
        [InlineData(1)]
        public void ProductApiGetSuccessTest(int Id)
        {
            var product = _products.First(i => i.Id == Id);
            _mock.Setup(i => i.GetByid(product.Id)).Returns(product);
            var result = _productsApiController.GetProduct(product.Id);

            Assert.IsType<OkObjectResult>(result);



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

        }

        [Theory]
        [InlineData(1)]
        public void ProductApiCreateSuccessTest(int Id)
        {
            var product = _products.FirstOrDefault(i => i.Id==Id);

            _mock.Setup(i => i.Add(product));

            var viewResult = _productsApiController.PostProduct(product);

           var result =  Assert.IsType<CreatedAtActionResult>(viewResult);
            _mock.Verify(i => i.Add(product), Times.Once);

            Assert.Equal("GetProduct", result.ActionName);


        }

    }
}
