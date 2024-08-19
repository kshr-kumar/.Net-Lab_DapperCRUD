using DAL.Entities;
using DAL.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionStrings:DbConnection"];
        }

        public int DeleteProduct(int productId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                var param = new DynamicParameters();
                param.Add("@ProductId", productId);
                var result = con.Execute("DeleteProductById", param, tran, 0, System.Data.CommandType.StoredProcedure);

                if (result > 0)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
                return result;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            string sqlQuery = "Select * from products";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                var products = con.Query<Product>(sqlQuery);
                return products.ToList();
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            string sqlQuery = "Select * from Categories";
            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                var categories = con.Query<Category>(sqlQuery);
                return categories.ToList();
            }
        }

        public Product GetSingleProduct(int productId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                var param  = new DynamicParameters();
                param.Add("@ProductId", productId);
                return con.Query<Product>("usp_getproduct", param, null, true, 0, System.Data.CommandType.StoredProcedure).SingleOrDefault();
            }

        }

        public int InsertProduct(Product Product)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                var param = new DynamicParameters();
                param.Add("@Name", Product.Name);
                param.Add("@UnitPrice", Product.UnitPrice);
                param.Add("@Description", Product.Description);
                param.Add("@CategoryId", Product.CategoryId);

                var result = con.Execute("AddNewProductDetails", param, tran, 0, System.Data.CommandType.StoredProcedure);

                if (result > 0)
                {
                    tran.Commit();
                }
                else
                {
                tran.Rollback(); 
                }
                return result;
            }
        }

        public int UpdateProduct(Product Product)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                var param = new DynamicParameters();
                param.Add("@ProductId", Product.ProductId);
                param.Add("@Name", Product.Name);
                param.Add("@UnitPrice", Product.UnitPrice);
                param.Add("@Description", Product.Description);
                param.Add("@CategoryId", Product.CategoryId);

                var result = con.Execute("UpdateProductDetails", param, tran, 0, System.Data.CommandType.StoredProcedure);

                if (result > 0)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
                return result;
            }
        }
        
    }
}
