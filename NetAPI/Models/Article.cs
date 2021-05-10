﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NetAPI.Models
{
    public class Article
    {
        public int Id { get; set; }
        public int IdCategorie { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Domaine { get; set; }
        public string ImgUrl { get; set; }
        public string Volume { get; set; }
        public DateTime CreateDate { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public bool isActive { get; set; }
        public bool commandeAuto { get; set; }
        public Provider Provider { get; set; }
        public Categorie Categorie { get; set; }
        public ICollection<OrderArticle> OrderArticles { get; set; }
        public Stock Stock { get; set; }
        public Admin Admin { get; set; }
    }
}
