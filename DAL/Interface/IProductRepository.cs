using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IProductRepository
    {
        Product GetSingleProduct(int productId); 
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Category> GetCategories(); 
        int InsertProduct(Product Product); 
        int UpdateProduct(Product Product); 
        int DeleteProduct(int productId);
    }
}
