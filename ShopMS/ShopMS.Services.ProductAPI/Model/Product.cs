﻿using System.ComponentModel.DataAnnotations;

namespace ShopMS.Services.ProductAPI.Model
{
    public class Product
    {


        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 1000)]
        public double Price { get; set; }

        public string Description { get; set; }

        public string CatagoryName { get; set; }

        public string ImageUrl { get; set; }


    }
}