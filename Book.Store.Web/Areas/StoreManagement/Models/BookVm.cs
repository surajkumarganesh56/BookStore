﻿namespace Book.Store.Web.Areas.StoreManagement.Models
{
    public class BookVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CreatedDate { get; set; }
    }
}
