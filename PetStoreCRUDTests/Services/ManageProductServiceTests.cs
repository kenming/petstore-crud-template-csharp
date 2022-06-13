using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thinksoft.crudTutorial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinksoft.crudTutorial.DAOs;

namespace Thinksoft.crudTutorial.Services.Tests
{
    [TestClass()]
    public class ManageProductServiceTests
    {
        private IManageProductDao ProductDao = default!;

        [TestInitialize()]
        public void Setup()
        {
        }

        [TestMethod()]
        public void TestConnectivityByADO_NET()
        {
            ProductDao = new ManageProductDaoByADO();

            Assert.IsTrue(ProductDao.GetAllProducts().Count > 0);
        }

        [TestMethod()]
        public void TestConnectivityByEFCore()
        {
            ProductDao = new ManageProductDaoByEFCore();

            Assert.IsTrue(ProductDao.GetAllProducts().Count > 0);
        }
    }
}