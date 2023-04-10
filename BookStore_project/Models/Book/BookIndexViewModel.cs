﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore_project.Models.Book
{
    public class BookIndexViewModel
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public DateOnly PublicDate { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public string? Image_URL { get; set; }

        public int AuthorID { get; set; }
        public int CategoryID { get; set; }

        public int PublisherID { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
    }
}
