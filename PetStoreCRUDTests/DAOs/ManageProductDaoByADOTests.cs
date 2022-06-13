using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thinksoft.crudTutorial.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Thinksoft.crudTutorial.Helper;
using Thinksoft.crudTutorial.EDM;
using System.Transactions;

namespace Thinksoft.crudTutorial.DAOs.Tests
{
    [TestClass()]
    public class ManageProductDaoByADOTests
    {
        private ManageProductDaoByADO ProductDao = default!;

        [TestInitialize()]
        public void Setup()
        {            
        }

        [TestMethod()]
        public void TestProductsIsNotEmpty()
        {
            ProductDao = new ManageProductDaoByADO();
            Assert.IsTrue(ProductDao.GetAllProducts().Count > 0);
        }

        [TestMethod()]
        public void TestIfExistProduct()
        {
            ProductDao = new ManageProductDaoByADO();
            // Test if exist a product which id=1
            Assert.IsNotNull(ProductDao.FindById(1));
        }

        /*
         * Test Create() behavior in Memory Database, ensure not to affect ProductDB.
         */
        [TestMethod()]
        public void TestCretaeProductInMemoryDB()
        {
            int actual;     // actual Product ID.

            Product aProduct = GetProduct();
            ProductDao = ConnectAndCreateToMemoryDB();

            actual = ProductDao.Create(aProduct);
            
            Assert.IsTrue(actual > 0);
        }

        /*
         * Test Update() behavior in Memory Database, ensure not to affect ProductDB.
         */
        [TestMethod()]
        public void TestUpdateProductInMemoryDB()
        {
            int actual;
            int expected = 1;       // 1 = affected 1 row, -1 = no affected row.

            Product aProduct = GetProduct();
            ProductDao = ConnectAndCreateToMemoryDB();
            
            // Insert a product data for update testing.
            int ProductId = ProductDao.Create(aProduct);
            aProduct.Id = ProductId;
            // Update Name for testing.
            aProduct.Name = "Changed Pet Name";
            // Test if update a product is success.
            actual = ProductDao.Update(aProduct);

            Assert.AreEqual(expected, actual);
        }

        /*
         * Test Update() behavior in Memory Database, ensure not to affect ProductDB.
         */
        [TestMethod()]
        public void TestDeleteProductInMemoryDB()
        {
            int actual;
            int expected = 1;       // 1 = affected 1 row, -1 = no affected row.

            Product aProduct = GetProduct();
            ProductDao = ConnectAndCreateToMemoryDB();

            // Insert a product data for update testing.
            int ProductId = ProductDao.Create(aProduct);
            // Test if delete a product is success.
            actual = ProductDao.DeleteById(Convert.ToInt32(ProductId));

            Assert.AreEqual(expected, actual);
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
        private static ManageProductDaoByADO ConnectAndCreateToMemoryDB()
        {
            SqliteConnection DbConnection;
            string ConnStr = DbHelper.GetConnectionString("SqliteMemoryDB");
            DbConnection = new SqliteConnection(ConnStr);
            DbConnection.Open();
            CreateTable(DbConnection);

            return new ManageProductDaoByADO(DbConnection);
        }

        // Create Table In SqliteMemoryDB
        private static void CreateTable(SqliteConnection SqlConnection)
        {

            SqliteCommand SqlCommand;
            string CreateStmt =
            @"  CREATE TABLE IF NOT EXISTS Product
                (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                Name TEXT(36) NOT NULL,
                Price INTEGER NOT NULL,
                Category TEXT(12) )
            ";

            SqlCommand = SqlConnection.CreateCommand();
            SqlCommand.CommandText = CreateStmt;
            SqlCommand.ExecuteNonQuery();
        }
    }
}