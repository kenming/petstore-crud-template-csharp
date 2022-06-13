using Thinksoft.crudTutorial.EDM;

namespace Thinksoft.crudTutorial.DAOs
{
    public interface IManageProductDao
    {
        List<Product> GetAllProducts();

        Product? FindById(int Id);

        /*
         * <parm name="Product">Product entity data object.</parm>
         * <returns>Product ID; 0 = no inserted.</returns>
         */
        int Create(Product product);

        /*
         * <parm name="Product">Product entity data object.</parm>
         * <returns>updated row count; -1 = no updated.</returns>
         */
        int Update(Product product);

        /*
         * <parm name="Id">Product ID.</parm>
         * <returns>deleted row count; -1 = no deleted.</returns>
         */
        int DeleteById(int Id);
    }
}
