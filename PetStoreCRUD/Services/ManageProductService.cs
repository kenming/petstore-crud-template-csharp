using Thinksoft.crudTutorial.DAOs;
using Thinksoft.crudTutorial.EDM;

namespace Thinksoft.crudTutorial.Services
{
    public class ManageProductService
    {
        private readonly IManageProductDao ProductDao;

        // Constructor
        public ManageProductService()
        {
            // default : use EF Core to connect to database.
            ProductDao = new ManageProductDaoByEFCore();
            //ProductDao = new ManageProductDaoByADO();
        }

        // use Constructor Injection
        public ManageProductService(IManageProductDao Dao)
        {
            if (Dao != null)
                ProductDao = Dao;
            else
                ProductDao = new ManageProductDaoByEFCore();
                //ProductDao = new ManageProductDaoByADO();
        }

        /*
         * <returns>Product entity data model of list.</returns>
         */
        public List<Product> ListAllProduct()
        {
            return ProductDao.GetAllProducts();
        }

        /*
         * <returns>Product entity data model</returns>
         */
        public Product Get(int ProductId)
        {
            return ProductDao.FindById(ProductId);
        }

        /*
         * <parm name="aProduct">Product entity data model</parm>
         * <returns>Product ID</returns>
         */
        public int Add(Product aProduct)
        {
            return ProductDao.Create(aProduct);
        }

        /*
         * <parm name="aProduct">Product entity data model</parm>
         * <returns>Updated row count.</returns>
         */
        public int Update(Product aProduct)
        {
            return ProductDao.Update(aProduct);
        }

        /*
         * <returns>Deleted row count.</returns>
         */
        public int Delete(int ProductId)
        {
            return ProductDao.DeleteById(ProductId);
        }
    }
}
