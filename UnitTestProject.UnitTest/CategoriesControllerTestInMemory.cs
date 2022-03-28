using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class CategoriesControllerTestInMemory:CategoiresControllerTest
    {

        public CategoriesControllerTestInMemory()
        {
            SetDbContextOptions(new DbContextOptionsBuilder<UI.Data.ProjectContext>().UseInMemoryDatabase("UnitTest").Options);
        }

        [Fact]
        public async Task CategoriesModelCreatePostTest()
        {
            using(var context = new ProjectContext(_dbContextOptions))
            {
                var category = context.Categories.First();
                var newCategory = new Category() { Name = "Category Deneme" };

                var controller = new CategoriesController(context);
                var crateCategory = await controller.Create(newCategory);

                var redirect =Assert.IsType<RedirectToActionResult>(crateCategory);
                Assert.Equal("Index",redirect.ActionName);

            }

            using (var context = new ProjectContext(_dbContextOptions))
            {
                var category = context.Categories.FirstOrDefault(i => i.Name == "Category Deneme");

                Assert.Equal("Category Deneme", category.Name);

            }
        }
    }
}
