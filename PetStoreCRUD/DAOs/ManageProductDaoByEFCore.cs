using Thinksoft.crudTutorial.EDM;

namespace Thinksoft.crudTutorial.DAOs
{    
    public class ManageProductDaoByEFCore : IManageProductDao
    {
        private readonly PetStoreDBContext DbContext;

        // Default Constructor
        public ManageProductDaoByEFCore()
        {
            DbContext = new();
        }

        // Constructor use Injection.
        public ManageProductDaoByEFCore(PetStoreDBContext PDbContext)
        {
            if (DbContext == null)
                DbContext = PDbContext;
            else
                DbContext = new();
        }
        public int Create(Product product)
        {
            DbContext.Products.Add(product);
            DbContext.SaveChanges();
            int Id = Convert.ToInt32(product.Id);

            return Id;
        }
        public Product? FindById(int Id)
        {
            Product? product = DbContext.Products.Find(Convert.ToInt64(Id));

            return product;
        }
        public List<Product> GetAllProducts() 
        {
            // use LINQ to get all products.
            var ListOfProduct = (from product in DbContext.Products
                           orderby product.Id
                           select product).ToList<Product>();

            return ListOfProduct;
        }
        public int Update(Product product)
        {
            DbContext.Update(product);
            int Result = DbContext.SaveChanges();

            return Result;
        }
        public int DeleteById(int ProductId)
        {
            Product aProduct = DbContext.Products.Where(p => p.Id == ProductId).First();
            DbContext.Remove(aProduct);
            int Result = DbContext.SaveChanges();

            return Result;
        }
    }    
}
