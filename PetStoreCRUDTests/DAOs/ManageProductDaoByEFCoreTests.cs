using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinksoft.crudTutorial.DAOs;
using Thinksoft.crudTutorial.EDM;
using Thinksoft.crudTutorial.Helper;

namespace Thinksoft.crudTutorial.DAOs.Tests
{
    [TestClass()]
    public class ManageProductDaoByEFCoreTests
    {
        private ManageProductDaoByEFCore ProductDao = default!;

        [TestInitialize()]
        public void Setup()
        {            
        }

        [TestMethod()]
        public void TestProductsIsNotEmpty()
        {
            ProductDao = new ManageProductDaoByEFCore();
            Assert.IsTrue(ProductDao.GetAllProducts().Count > 0);
        }

        [TestMethod()]
        public void TestIfExistProduct()
        {
            ProductDao = new ManageProductDaoByEFCore();
            // Test if exist a product which id=1
            Assert.IsNotNull(ProductDao.FindById(1));
        }

        [TestMethod()]
        public void TestCretaeProductIsSuccess()
        {
            int actual;     // actual Product ID.

            PetStoreDBContext DbContext = new();
            ProductDao = new ManageProductDaoByEFCore(DbContext);

            Product aProduct = GetProduct();
            /*
             * To ensure that the test write data can be restored to its unwritten state, 
             * it is necessary to control the DBContext transaction to rollback.
             */
            using (var DbContextTransaction = DbContext.Database.BeginTransaction())
            {
                actual = ProductDao.Create(aProduct);
                DbContextTransaction.Rollback();
            }

            Assert.IsTrue(actual > 0);
        }

        /*
         * Test Create() behavior in Memory Database, ensure not to affect ProductDB.
         */
        [TestMethod()]
        public void TestCreateProductInMemoryDB()
        {
            int actual;     // actual Product ID.
            
            ProductDao = new ManageProductDaoByEFCore(CreateInMemoryDbContext());
            Product aProduct = GetProduct();
            actual = ProductDao.Create(aProduct);

            Assert.IsTrue(actual > 0);
        }

        /*
         * Test Update() behavior in Memory Database.
         */
        [TestMethod()]
        public void TestUpdateProductInMemoryDB()
        {
            int actual;
            int expected = 1;       // 1 = affected 1 row, -1 = no affected row.

            ProductDao = new ManageProductDaoByEFCore(CreateInMemoryDbContext());
            Product aProduct = GetProduct();
            // Insert a product data for update testing.
            ProductDao.Create(aProduct);
            // Update Name for testing.
            aProduct.Name = "Changed Pet Name";

            // Test if update a product is success.
            actual = ProductDao.Update(aProduct);

            Assert.AreEqual(actual, expected);
        }

        /*
         * Test Update() behavior in Memory Database.
         */
        [TestMethod()]
        public void TestDeleteProductInMemoryDB()
        {
            int actual;
            int expected = 1;       // 1 = affected 1 row, -1 = no affected row.

            ProductDao = new ManageProductDaoByEFCore(CreateInMemoryDbContext());
            Product aProduct = GetProduct();
            // Insert a product data for delete testing.
            ProductDao.Create(aProduct);            

            // Test if delete a product is success.
            actual = ProductDao.DeleteById(Convert.ToInt32(aProduct.Id));

            Assert.AreEqual(actual, expected);
        }

        // provide test data.
        private static Product GetProduct()
        {
            return new Product
            {
                Name = "TestPet",
                Price = 8888,
                Category = "TestCategory"
            };
        }


        public static PetStoreDBContext CreateInMemoryDbContext()
        {
            var connection = new SqliteConnection(DbHelper.GetConnectionString("SqliteMemoryDB"));
            connection.Open();

            var option = new DbContextOptionsBuilder<PetStoreDBContext>().UseSqlite(connection).Options;
            PetStoreDBContext DbContext = new(option);

            if (DbContext != null)
            {
                DbContext.Database.EnsureDeleted();
                DbContext.Database.EnsureCreated();
            }

            return DbContext;
        }
    }
}