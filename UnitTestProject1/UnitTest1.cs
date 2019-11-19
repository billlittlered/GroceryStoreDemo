using GroceryStoreAPI;
using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestProject1
{
     [TestClass]
     public class UnitTest1
     {
          GroceryStoreDbContext _db;
          IGroceryStoreService _service;

          [TestInitialize]
          [DeploymentItem(@"database.json", @"bin\Debug\netcoreapp2.1")]
          public void Init()
          {
               Random r = new Random();
               var databaseName = $"db_Test{r.Next(1, 100000000)}";
               var options = new DbContextOptionsBuilder<GroceryStoreDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;


               _db = new GroceryStoreDbContext(options);
               _service = new GroceryStoreService(_db);

               var jObjects = JObject.Parse(File.ReadAllText("database.json"));

               //Customers
               JArray customerArray = jObjects.GetValue("customers") as JArray;
               var customers = customerArray.Select(p => new Customer
               {
                    Id = (int)p["id"],
                    Name = (string)p["name"]
               });
               _db.Customers.AddRange(customers);

               //Orders
               var orders = new List<Order>();
               JArray orderArray = jObjects.GetValue("orders") as JArray;
               _db.Orders.AddRange(orderArray.Select(p => p.ToObject<Order>()));


               //Products
               var productsArray = jObjects.GetValue("products") as JArray;
               var products = productsArray.Select(p => new Product
               {
                    Id = (int)p["id"],
                    Description = (string)p["description"],
                    Price = (double)p["price"]
               });
               _db.Products.AddRange(products);

               //"Save" everything in-memory
               _db.SaveChanges();
          }

          [TestCleanup]
          public void Cleaningup()
          {
               _db.Dispose();
          }

          [TestMethod]
          public void ListAllCustomers()
          {
               var allCustomers = _service.GetAllCustomers();
               Assert.IsTrue(allCustomers.Count > 0);
          }

          [TestMethod]
          public void GetOneCustomer()
          {
               var customerId = 3;
               var firstCustomer = _service.GetCustomerById(customerId);
               Assert.IsNotNull(firstCustomer);  
          }

          [TestMethod]
          public void GetOneCustomer_InvalidId()
          {
               var customerId = 3333;
               var firstCustomer = _service.GetCustomerById(customerId);
               Assert.IsNull(firstCustomer);
          }

          [TestMethod]
          public void ListAllOrders()
          {
               var allOrders = _service.GetAllOrders();
               Assert.IsTrue(allOrders.Count > 0);
          }

          [TestMethod]
          public void ListOrdersForGivenDate()
          {
               var datePlaced = DateTime.MinValue;
               var ordersPlaced = _service.GetOrdersByDate(datePlaced);
               Assert.IsTrue(ordersPlaced.Count() > 0);
          }

          [TestMethod]
          public void ListOrdersForGivenDate_InvalidDate()
          {
               var datePlaced = DateTime.Now.AddDays(-5);
               var ordersPlaced = _service.GetOrdersByDate(datePlaced);
               Assert.IsFalse(ordersPlaced.Count() > 0);
          }

          [TestMethod]
          public void GetOneOrder()
          {
               var orderId = 1;
               var oneOrder = _service.GetOrderById(orderId);
               Assert.IsNotNull(oneOrder);
          }

          [TestMethod]
          public void ListProducts()
          {
               var products = _service.GetAllProducts();
               Assert.IsTrue(products.Count > 0);
          }

          [TestMethod]
          public void GettingAProduct()
          {
               var productId = 2;
               var product = _service.GetProductById(productId);
               Assert.IsNotNull(product);
          }

          [TestMethod]
          public void GetCustomerOrder()
          {
               var customerId = 1;
               var customerOrders = _service.GetCustomerOrder(customerId);
               Assert.IsTrue(customerOrders.Count() > 0);
          }

          [TestMethod]
          public void GetCustomerOrder_InvalidCustomer()
          {
               var customerId = 11111;
               var customerOrders = _service.GetCustomerOrder(customerId);
               Assert.IsNull(customerOrders);
          }

          [TestMethod]
          public void SaveCustomer()
          {
               var testCustomer = new Customer { Id = 5, Name = "Norman" };
               var isSuccessful = _service.SaveCustomer(testCustomer);
               Assert.IsTrue(isSuccessful);
          }

          [TestMethod]
          public void SaveProduct()
          {
               var testProduct = new Product { Id = 8, Description = "Wipes", Price = 5.75 };
               var isSuccessful = _service.SaveProduct(testProduct);
               Assert.IsTrue(isSuccessful);
          }
     }
}
