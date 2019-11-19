using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
     public interface IProduct
     {
          string Description { get; set; }
          int Id { get; set; }
          double Price { get; set; }
     }

     public class Product : IProduct
     {
          public int Id { get; set; }
          public string Description { get; set; }
          public double Price { get; set; }
     }
}
