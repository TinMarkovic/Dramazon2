using Dramazon2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dramazon2.Data
{
    public interface IDramazon2Repository
    {
        IQueryable<Product> GetAllProducts();
        IQueryable<Product> GetProductsByTag(Tag productTag);
        IQueryable<Product> GetProductsByTitle(string productTitle);
        IQueryable<Product> GetProductsByDescription(string productDescription);
        Product GetProduct(int productId);

        IQueryable<Tag> GetAllTags();
        Tag GetTag(int tagId);
        Tag GetTagByName(string tagName);

        Customer GetCustomerById(int customerId);
        Customer GetCustomerByUsername(string customerUsername);
        bool LoginCustomer(string username, string password);

        bool AddProductToCart(Product product, Customer customer);
        bool RemoveProductFromCart(Product product, Customer customer);
        bool PurchaseCart(Customer customer);
        IQueryable<Product> ViewCart(Customer customer);

        IQueryable<Purchase> GetAllPurchasesByCustomer(Customer customer);

        bool Insert(Tag tag);
        bool Update(Tag originalTag, Tag updatedTag);
        bool DeleteTag(int id);

        bool Insert(Customer customer);
        bool Update(Customer originalCustomer, Customer updatedCustomer);
        bool DeleteCustomer(int id);

        bool Insert(Product product);
        bool Update(Product originalProduct, Product updatedProduct);
        bool DeleteProduct(int id);

        bool SaveAll();
    }
}
