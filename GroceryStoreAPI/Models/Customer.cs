using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
     public interface ICustomer
     {
          int Id { get; set; }
          string Name { get; set; }
     }

     public class Customer : ICustomer
     {
          public int Id { get; set; }
          public string Name { get; set; }
     }
}
