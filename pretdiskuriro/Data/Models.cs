using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(450)]
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime isConfirmedEmail { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [AllowNull]
        public Category Category { get; set; } // 11

        [Required]
        public List<DailyPrice> Prices { get; set; } // 1m

        public List<Market> Markets { get; set; } // mm

    }

    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        //FK IMPORTANT FOR 1M
        // have to be explicit!!
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class DailyPrice
    {
        public int Id { get; set; }

        [Required]
        public float Price { get; set; }
        public DateTime EndDate { get; set; }


        public Product Product { get; set; } // Foreign key
        public int ProductId{ get; set; }   
    }

    // Furnizori
    public class Market
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; }

    }

}
