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
    public class ProductControllerTest
    {
        private Mock<IRepository<Product>> _mock;
        private ProductsController _productsController;
        private List<Product> products;

        public ProductControllerTest()
        {
            _mock = new Mock<IRepository<Product>>();
            _productsController = new ProductsController(_mock.Object);
            products = new List<Product>() { 
                new Product(){Id=1,Name="Product1",Stock=100,Price=1000},
                new Product(){Id=2,Name="Product2",Stock=100,Price=2000},
                new Product(){Id=3,Name="Product3",Stock=100,Price=3000},
                new Product(){Id=4,Name="Product4",Stock=100,Price=4000},
                new Product(){Id=5,Name="Product4",Stock=100,Price=5000},
                new Product(){Id=6,Name="Product5",Stock=100,Price=6000},
                new Product(){Id=7,Name="Product6",Stock=100,Price=7000},
                new Product(){Id=8,Name="Product7",Stock=100,Price=8000},
                new Product(){Id=9,Name="Product8",Stock=100,Price=9000}};

        }

        [Fact]
        public void ProductsIndexControllerTest()
        {
            var result = _productsController.Index();
            Assert.IsType<ViewResult>(result);

            

        }
        [Fact]
        public void ProductsGetAllTest()
        {
            _mock.Setup(i => i.GetAll()).Returns(products);

            var result = _productsController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var productList = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);

            Assert.Equal<int>(9, productList.Count());


        }

        [Theory]
        [InlineData(1)]

        public void ProductDetailControllerTest(int id)
        {
            _mock.Setup(i => i.GetByid(id)).Returns(products.First(i=>i.Id==id));
            var productDetailsController = _productsController.Details(id);
            Assert.IsType<ViewResult>(productDetailsController);

        }

        [Fact]
        public void ProductIdDetailNullTest()
        {
            var result = _productsController.Details(null);
            var redirect=Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirect.ActionName);


        }
        
        [Fact]
        public void ProductDetailInvalidTest()
        {
           
            var result = _productsController.Details(0);
            var redirect = Assert.IsType<NotFoundResult>(result);

            Assert.Equal<int>(404, redirect.StatusCode);
        }

       [Theory]
       [InlineData(1)]
        public void ProdductDetailSuccessTest(int Id)
        {
            var product = products.FirstOrDefault(i=>i.Id==Id);
            _mock.Setup(i => i.GetByid(Id)).Returns(product);

            var result = _productsController.Details(Id);
            var viewResult = Assert.IsType<ViewResult>(result);

            var resultProduct = Assert.IsAssignableFrom<Product>(viewResult.Model);

            Assert.Equal(product.Id, resultProduct.Id);
            Assert.Equal(product.Name, resultProduct.Name);
            Assert.Equal(product.Price, resultProduct.Price);


        }

        [Fact]
        public void ProductCreatePageTest()
        {
            var actionResult = _productsController.Create();
            var viewResult = Assert.IsType<ViewResult>(actionResult);

        }

        [Fact]
        public void ProductCreateInvalidModelState()
        {
            _productsController.ModelState.AddModelError("Name", "Name alanı zorunlu bir alandır");

            var result = _productsController.Create(products.First());

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsType<Product>(viewResult.Model);

        }

        [Fact]
        public void ProductCreateValidModelStatePageTest()
        {
            var result = _productsController.Create(products.First());

            var redirect=Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirect.ActionName);
           

        }

        [Fact]
        public void ProductCreateTest()
        {
            Product newPeoduct = null;
            _mock.Setup(i => i.Add(It.IsAny<Product>())).Callback<Product>(x => newPeoduct = x);

            var result = _productsController.Create(products.First());

            _mock.Verify(i => i.Add(It.IsAny<Product>()), Times.Once);

            Assert.Equal(products.First().Id, newPeoduct.Id);



        }

        [Fact]
        public void ProductNeverCreateTest()
        {
            _productsController.ModelState.AddModelError("Name", "Name alanı gereklidir");

            var result = _productsController.Create(products.First());

            _mock.Verify(i => i.Add(It.IsAny<Product>()), Times.Never);

        }


        #region  EditGetTest
        [Fact]
        public void ProductEditControllerTest()
        {
            var product = products.FirstOrDefault().Id;
            _mock.Setup(i => i.GetByid(1)).Returns(products.FirstOrDefault());
            var page = _productsController.Edit(product);

            var viewResult = Assert.IsType<ViewResult>(page);
            var result = Assert.IsAssignableFrom<Product>(viewResult.Model);




        }

        [Fact]
        public void ProductEditStatusNullTest()
        {
            var controller = _productsController.Edit(null);
            var result = Assert.IsType<RedirectToActionResult>(controller);
            Assert.Equal("Index", result.ActionName);

        }
        [Theory]
        [InlineData(0)]
        public void ProductEditHasNotIdTest(int Id)
        {
            Product product = null;

            _mock.Setup(i => i.GetByid(Id)).Returns(product);
            var productEditController = _productsController.Edit(Id);

            var viewResult = Assert.IsType<NotFoundResult>(productEditController);
            Assert.Equal<int>(404, viewResult.StatusCode);





        } 
        #endregion


        #region EditPostTest


        [Theory]
        [InlineData(0)]
        public void ProductEditPostNotEqualIdTest(int Id)
        {
            Product product = products.Find(i=>i.Id==5);
           

            _mock.Setup(i => i.GetByid(Id)).Returns(product);
            var editController = _productsController.Edit(Id, product);
            var result = Assert.IsType<NotFoundResult>(editController);

        }

        [Theory]
        [InlineData(1)]
        public void ProductEditModelStateInvalidTest(int Id)
        {
            _productsController.ModelState.AddModelError("", "");

            var result = _productsController.Edit(Id, products.First(i => i.Id == Id));

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<Product>(viewResult.Model);

        }

        [Theory]
        [InlineData(1)]
        public void EditProductModelStateValidTest(int Id)
        {

            var result = _productsController.Edit(Id, products.First(i=>i.Id==Id));
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);


        }

        [Theory]
        [InlineData(1)]
        public void EditProductModelStateValidCorrectTrue(int Id)
        {
            var updateProduct = products.First(i => i.Id == Id);
            _mock.Setup(i => i.Update(updateProduct));
            var result = _productsController.Edit(Id, updateProduct);
            _mock.Verify(i=> i.Update(It.IsAny<Product>()), Times.Once);

        }


        #endregion

        #region DeleteGetTest
        [Fact]
        public void ProductDeleteIdNullTest()
        {

            var viewResult = _productsController.Delete(null);
            Assert.IsType<NotFoundResult>(viewResult);

        }

        [Theory]
        [InlineData(0)]
        public void ProductDeleteIdNotFoundTest(int Id)
        {
            Product product = null;
            _mock.Setup(i => i.GetOne(i => i.Id == Id)).Returns(product);
            var viewResult = _productsController.Delete(Id);

            Assert.IsType<NotFoundResult>(viewResult);


        }

        [Theory]
        [InlineData(1)]
        public void ProductDeleteSuccessTest(int Id)
        {
            var product = products.First(i => i.Id == Id);
            _mock.Setup(i => i.GetByid(Id)).Returns(product);

            var result = _productsController.Delete(product.Id);
            
            var viewResult= Assert.IsType<ViewResult>(result);
            
            Assert.IsAssignableFrom<Product>(viewResult.Model);
        }

        #endregion


        #region DeletePostTest

        [Theory]
        [InlineData(1)]
        public void ProductDeletePostSuccessTest(int Id)
        {
            var product = products.FirstOrDefault(i => i.Id == Id);
            _mock.Setup(i => i.Delete(product));
            
            var viewResult = _productsController.DeleteConfirmed(product.Id);

            _mock.Verify(i => i.Delete(It.IsAny<Product>()), Times.Once);


        }

        [Theory]
        [InlineData(1)]
        public void ProductDeletePostSuccessMethodTest(int Id)
        {
            var product = products.FirstOrDefault(i => i.Id == Id);
            _mock.Setup(i => i.Delete(product));

            var viewResult = _productsController.DeleteConfirmed(product.Id);

            Assert.IsType<RedirectToActionResult>(viewResult);


        }

        [Theory]
        [InlineData(0)]
        public void ProductNullExistProduct(int Id)
        {

            _mock.Setup(i => i.GetByid(Id));
            var result = _productsController.ProductExists(Id);


            Assert.IsType<bool>(result);




        }

        [Theory]
        [InlineData(1)]
        public void ProductHaslExistProduct(int Id)
        {

            _mock.Setup(i => i.GetByid(Id));
            var result = _productsController.ProductExists(Id);


            Assert.IsType<bool>(result);




        }

        #endregion

    }
}
