using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
     public interface IOrder
     {
          int CustomerId { get; set; }
          int Id { get; set; }
          List<Item> Items { get; set; }
     }

     public class Order : IOrder
     {
          public int Id { get; set; }
          public int CustomerId { get; set; }
          public List<Item> Items { get; set; }

          public virtual DateTime DatePlaced { get; set; }
     }
}
