﻿using System.ComponentModel.DataAnnotations;

namespace BookStore_project.Models.Publisher
{
    public class PublisherIndexViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
    }
}
