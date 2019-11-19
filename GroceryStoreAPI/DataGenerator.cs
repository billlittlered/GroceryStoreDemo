using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Models;
using System.IO;
using Newtonsoft.Json.Linq;

namespace GroceryStoreAPI
{
     public class DataGenerator
     {
          public static void Initialize(IServiceProvider serviceProvider)
          {
               using (var context = new GroceryStoreDbContext(
                    serviceProvider.GetRequiredService<DbContextOptions<GroceryStoreDbContext>>()))
               {
                    if (context.Customers.Any())
                         return; //Data already seeded


                    var jObjects = JObject.Parse(File.ReadAllText("database.json"));

                    //Customers
                    JArray customerArray = jObjects.GetValue("customers") as JArray;
                    var customers = customerArray.Select(p => new Customer
                    {
                         Id = (int)p["id"],
                         Name = (string)p["name"]
                    });
                    context.Customers.AddRange(customers);

                    //Orders
                    var orders = new List<Order>();
                    JArray orderArray = jObjects.GetValue("orders") as JArray;
                    context.Orders.AddRange(orderArray.Select(p => p.ToObject<Order>()));


                    //Products
                    var productsArray = jObjects.GetValue("products") as JArray;
                    var products = productsArray.Select(p => new Product
                    {
                         Id = (int)p["id"],
                         Description = (string)p["description"],
                         Price = (double)p["price"]
                    });
                    context.Products.AddRange(products);

                    //"Save" everything in-memory
                    context.SaveChanges();

               }
          }
     }
}
