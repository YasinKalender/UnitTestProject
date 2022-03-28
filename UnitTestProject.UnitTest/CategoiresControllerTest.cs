using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject.UI.Data;

namespace UnitTestProject.UnitTest
{
    public class CategoiresControllerTest
    {
        protected DbContextOptions<ProjectContext> _dbContextOptions { get; private set; }

        public void SetDbContextOptions(DbContextOptions<ProjectContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        //public void SeedData()
        //{
        //    using (var dbContext = new ProjectContext(_dbContextOptions))
        //    {
        //        dbContext.Database.EnsureDeleted();
        //        dbContext.Database.EnsureCreated();

        //    }


        //}
    }
}
