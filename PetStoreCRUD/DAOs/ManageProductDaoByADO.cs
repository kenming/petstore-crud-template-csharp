using Microsoft.Data.Sqlite;
using System.Transactions;
using Thinksoft.crudTutorial.EDM;
using Thinksoft.crudTutorial.Helper;

namespace Thinksoft.crudTutorial.DAOs
{
    public class ManageProductDaoByADO : IManageProductDao
    {
        private SqliteConnection? DbConnetion;

        // Default Constructor
        public ManageProductDaoByADO()
        {
            DbConnetion = GetConnectionAndOpen();
        }

        // Constructor use Injection.
        public ManageProductDaoByADO(SqliteConnection? dbConnetion)
        {
            if (DbConnetion == null)
                DbConnetion = dbConnetion;
            else
                DbConnetion = GetConnectionAndOpen();
        }

        // Destructor : Close Database Connection
        ~ManageProductDaoByADO()
        { 
            DbConnetion.Close();
        }

        public int Create(Product product)
        {
            string Stmt = "INSERT INTO Product (Name, Price, Category) " +
                          "VALUES (@Name, @Price, @Category);" +
                          "SELECT last_insert_rowid()";

            SqliteCommand SqlCommand;
            SqlCommand = new SqliteCommand(Stmt, DbConnetion);

            SqlCommand.Parameters.AddWithValue("@Name", product.Name);
            SqlCommand.Parameters.AddWithValue("@Price", Convert.ToInt64(product.Price));
            SqlCommand.Parameters.AddWithValue("@Category", product.Category);

            int ProductId = Convert.ToInt32(SqlCommand.ExecuteScalar());

            return ProductId;
        }

        public Product? FindById(int Id)
        {
            Product product;
            string Stmt = "SELECT * FROM Product WHERE ID = @Id";

            var SqlCommand = DbConnetion.CreateCommand();
            SqlCommand.CommandText = Stmt;
            SqlCommand.Parameters.AddWithValue("@Id", Id);

            // Transfer a product record to Product Entity.
            product = TransferToProduct(SqlCommand);

            return product;
        }

        public List<Product> GetAllProducts()
        {            
            string Stmt = "SELECT * FROM Product";                        
            var SqlCommand = DbConnetion.CreateCommand();
            SqlCommand.CommandText = Stmt;

            // Transfer Product resultset to List<Product>
            List<Product>? Products = TransferToProductList(SqlCommand);
            
            return Products;
        }

        public int Update(Product product)
        {
            string Stmt = "UPDATE Product SET "+
                          "Name = @Name, Price = @Price, Category = @Category " +
                          "WHERE Id = @Id";

            SqliteCommand SqlCommand;
            SqlCommand = new SqliteCommand(Stmt, DbConnetion);

            SqlCommand.Parameters.AddWithValue("@Name", product.Name);
            SqlCommand.Parameters.AddWithValue("@Price", Convert.ToInt64(product.Price));
            SqlCommand.Parameters.AddWithValue("@Category", product.Category);
            SqlCommand.Parameters.AddWithValue("@Id", product.Id);

            int RowCount = SqlCommand.ExecuteNonQuery();

            return RowCount;
        }

        public int DeleteById(int ProductId)
        {
            string Stmt = "DELETE FROM Product " +
                          "WHERE Id = @Id";

            SqliteCommand SqlCommand;
            SqlCommand = new SqliteCommand(Stmt, DbConnetion);            
            SqlCommand.Parameters.AddWithValue("@Id", ProductId);

            int RowCount = SqlCommand.ExecuteNonQuery();

            return RowCount;
        }

        /*
         * Transfer Result Set -> List<Product>
         */
        private static List<Product> TransferToProductList(SqliteCommand SqlCommand)
        {
            List<Product> Products = new();

            try
            {
                using (var SqlReader = SqlCommand.ExecuteReader())
                {
                    while (SqlReader.Read())
                    {
                        Products.Add(
                            ConvertToProduct(SqlReader)
                        );
                    }
                    SqlReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No Record : " + ex.Message);
            }

            return Products;
        }

        /*
         * Convert Sql Data Type to C# DataType
         */
        private static Product ConvertToProduct(SqliteDataReader SqlReader)
        {
            return new Product
            {
                Id = Convert.ToInt32(SqlReader["Id"]),
                Name = SqlReader["Name"].ToString(),
                Category = SqlReader["Category"].ToString(),
                Price = Convert.ToInt32(SqlReader["Price"])
            };
        }

        /*
         * Transfer Single Result to Product
         */
        private static Product TransferToProduct(SqliteCommand SqlCommand)
        {
            Product product=default!;

            try
            {
                SqliteDataReader SqlReader = SqlCommand.ExecuteReader();
                if (SqlReader.Read())
                    product = ConvertToProduct(SqlReader);
                else
                    Console.WriteLine("No Record");
                SqlReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No Record : " + ex.Message);
            }

            return product;
        }

        // Get a SQLite Connection
        private SqliteConnection GetConnectionAndOpen()
        {
            if (DbConnetion == null)
                DbConnetion = new SqliteConnection(DbHelper.GetConnectionString("SqliteDB"));
            DbConnetion.Open();

            return DbConnetion;
        }                
    }
}
