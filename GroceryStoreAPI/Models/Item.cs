using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
     public interface IItem
     {
          int ID { get; set; }
          int ProductId { get; set; }
          int Quantity { get; set; }
     }

     public class Item : IItem
     {
          public int ID { get; set; }
          public int ProductId { get; set; }
          public int Quantity { get; set; }
     }
}
