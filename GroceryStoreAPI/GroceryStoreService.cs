using GroceryStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI
{
     public interface IGroceryStoreService
     {
          List<Customer> GetAllCustomers();
          List<Order> GetAllOrders();
          List<Product> GetAllProducts();
          Product GetProductById(int id);
          Customer GetCustomerById(int id);
          List<Item> GetCustomerOrder(int customerId);
          List<Order> GetOrdersByDate(DateTime dtInput);
          Order GetOrderById(int id);
          bool SaveCustomer(Customer customer);
          bool SaveProduct(Product product);
     }

     public class GroceryStoreService : IGroceryStoreService
     {
          private readonly GroceryStoreDbContext _db;
          public GroceryStoreService(GroceryStoreDbContext dbContext)
          {
               _db = dbContext;
          }

          public List<Customer> GetAllCustomers()
          {
               return _db.Customers.ToList();
          }

          public Customer GetCustomerById(int id)
          {
               return _db.Customers.SingleOrDefault(p => p.Id == id);
          }

          public List<Order> GetAllOrders()
          {
               return _db.Orders.ToList();
          }

          public List<Order> GetOrdersByDate(DateTime dtInput)
          {
               return _db.Orders.Where(p => p.DatePlaced == dtInput).ToList();
          }

          public Order GetOrderById(int id)
          {
               return _db.Orders.SingleOrDefault(p => p.Id == id);
          }

          public List<Product> GetAllProducts()
          {
               return _db.Products.ToList();
          }

          public Product GetProductById(int id)
          {
               return _db.Products.SingleOrDefault(p => p.Id == id);
          }

          public List<Item> GetCustomerOrder(int customerId)
          {
               var tmpOrder = _db.Orders.SingleOrDefault(p => p.CustomerId == customerId);
               return tmpOrder?.Items;
          }

          public bool SaveCustomer(Customer customer)
          {
               try
               {
                    _db.Customers.Add(customer);
                    _db.SaveChanges();

                    return true;
               }
               catch (Exception ex)
               {
                    //some logging would occur here, for now just rethrow the original exception
                    throw;
               }
          }

          public bool SaveProduct(Product product)
          {
               try
               {
                    _db.Products.Add(product);
                    _db.SaveChanges();

                    return true;
               }
               catch (Exception ex)
               {
                    //some logging would occur here, for now just rethrow the original exception
                    throw;
               }
          }

     }
}
