using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dramazon2.Data.Models;

namespace Dramazon2.Data
{
    public class Dramazon2Repository : IDramazon2Repository
    {
        private Dramazon2Context _ctx;

        public Dramazon2Repository(Dramazon2Context ctx)
        {
            _ctx = ctx;
        }

        public bool LoginCustomer(string username, string password)
        {
            var customer = _ctx.Customers.Where(c => c.Username == username).SingleOrDefault();

            if (customer != null)
            {
                if (customer.Password == password)
                {
                    return true;
                }
            }

            return false;
        }

        public Purchase PurchaseCart(Customer customer)
        {
            Purchase purchase = new Purchase();
            purchase.Products = _ctx.Entry(customer).Collection(c => c.Cart).CurrentValue;
            purchase.Customer = customer;
            purchase.DateOfPurchase = DateTime.Now;

            try
            {
                _ctx.Purchases.Add(purchase);
                _ctx.Entry(customer).Collection(c => c.Cart).CurrentValue.Clear();
                return purchase;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Product> ViewCart(Customer customer)
        {
            return _ctx.Entry(customer).Collection(c => c.Cart).CurrentValue.AsQueryable();
        }

        public bool AddProductToCart(Product product, Customer customer)
        {
            _ctx.Entry(customer).Collection(c => c.Cart).CurrentValue.Add(product);

            return true;
        }

        public bool RemoveProductFromCart(Product product, Customer customer)
        {
            _ctx.Entry(customer).Collection(c => c.Cart).CurrentValue.Remove(product);

            return true;
        }

        public IQueryable<Purchase> GetAllPurchasesByCustomer(Customer customer)
        {
            return _ctx.Purchases.Where(p => p.Customer == customer).AsQueryable();
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _ctx.Customers.AsQueryable();

        }

        public Customer GetCustomerById(int customerId)
        {
            return _ctx.Customers.Where(c => c.Id == customerId).SingleOrDefault();
        }

        public Customer GetCustomerByUsername(string customerUsername)
        {
            return _ctx.Customers.Where(c => c.Username == customerUsername).SingleOrDefault();
        }

        public Customer GetCustomerByEmail(string customerEmail)
        {
            return _ctx.Customers.Where(c => c.Email == customerEmail).SingleOrDefault();
        }

        public Product GetProduct(int productId)
        {
            return _ctx.Products
                    .Include("Tags")
                    .Include("Creator")
                    .Where(p => p.Id == productId)
                    .SingleOrDefault();
        }

        public IQueryable<Product> GetAllProducts()
        {
            return _ctx.Products
                    .Include("Tags")
                    .AsQueryable();
        }

        public IQueryable<Product> GetProductsByDescription(string productDescription)
        {
                return _ctx.Products.Where(p => p.Description.Contains(productDescription)).AsQueryable();
        }

        public IQueryable<Product> GetProductsByTag(int tagId)
        {
            return _ctx.Products.Where(p => p.Tags.Any(t => t.Id == tagId)).AsQueryable();
        }

        public IQueryable<Product> GetProductsByCart(int customerId)
        {
            return _ctx.Customers.Where(c => c.Id == customerId).SingleOrDefault().Cart.AsQueryable();
        }

        public IQueryable<Product> GetProductsByTitle(string productTitle)
        {
            return _ctx.Products.Where(p => p.Title.Contains(productTitle)).AsQueryable();
        }

        public IQueryable<Tag> GetAllTags()
        {
            return _ctx.Tags.AsQueryable();
        }

        public Tag GetTag(int tagId)
        {
            return _ctx.Tags.Where(t => t.Id == tagId).SingleOrDefault();
        }

        public Tag GetTagByName(string tagName)
        {
            return _ctx.Tags.Where(t => t.Name == tagName).SingleOrDefault();
        }

        public Rating Get(int customerId, int productId)
        {
            return _ctx.Ratings.Where(t => t.Customer.Id == customerId && t.Product.Id == productId).SingleOrDefault();
        }

        public IQueryable<Rating> GetAllRatings(int productId) {
            return _ctx.Ratings.Where(p => p.Product.Id == productId).AsQueryable();
        }

        public IQueryable<Rating> GetAllRatingsByCustomer(int customerId)
        {
            return _ctx.Ratings.Where(p => p.Customer.Id == customerId).AsQueryable();
        }

        public bool Insert(Customer customer)
        {
            try
            {
                _ctx.Customers.Add(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Insert(Product product)
        {
            try
            {
                _ctx.Products.Add(product);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Insert(Tag tag)
        {
            try
            {
                _ctx.Tags.Add(tag);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Insert(Rating rating)
        {
            try
            {
                _ctx.Ratings.Add(rating);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Product originalProduct, Product updatedProduct)
        {
            _ctx.Entry(originalProduct).CurrentValues.SetValues(updatedProduct);
            originalProduct.Tags = updatedProduct.Tags;

            return true;
        }

        public bool Update(Customer originalCustomer, Customer updatedCustomer)
        {
            _ctx.Entry(originalCustomer).CurrentValues.SetValues(updatedCustomer);
            originalCustomer.Cart = updatedCustomer.Cart;

            return true;
        }

        public bool Update(Tag originalTag, Tag updatedTag)
        {
            _ctx.Entry(originalTag).CurrentValues.SetValues(updatedTag);

            return true;
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                var entity = _ctx.Customers.Find(id);
                if (entity != null)
                {
                    _ctx.Customers.Remove(entity);
                    return true;
                }
            }
            catch
            { }
            return false;
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var entity = _ctx.Products.Find(id);
                if (entity != null)
                {
                    _ctx.Products.Remove(entity);
                    return true;
                }
            }
            catch
            { }
            return false;
        }

        public bool DeleteTag(int id)
        {
            try
            {
                var entity = _ctx.Tags.Find(id);
                if (entity != null)
                {
                    _ctx.Tags.Remove(entity);
                    return true;
                }
            }
            catch
            { }
            return false;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
