using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI
{
     public class GroceryStoreDbContext: DbContext
     {
          public GroceryStoreDbContext(DbContextOptions<GroceryStoreDbContext> options): base(options)
          {

          }

          public DbSet<Customer> Customers { get; set; }
          public DbSet<Item> Items { get; set; }
          public DbSet<Order> Orders { get; set; }
          public DbSet<Product> Products { get; set; }
     }
}
