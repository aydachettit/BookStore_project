﻿using BookStore_project.Controllers;

namespace BookStore_project.Models.Bill
{
    public class BillProcessViewModel
    {
       public int id { get; set; }
       public DateTime Date { get; set; }
       public string? Customer_ID { get; set; }
       public int Total { get; set; }
       public string? status { get; set; }
    }
}
