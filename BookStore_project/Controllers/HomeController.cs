﻿using BookStore_project.Models;
using BookStore_project.Models.Book;
using DataAccess;
using Entity;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;
using Service;
using Service.implementation;
using Service.Implementation;
using System.Collections.Generic;
using System.Diagnostics;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BookStore_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        private IAuthorService _authorService;
        private ICategoryService _categoryService;
        private IPublisherService _publisherService;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBookService bookService, ICategoryService categoryService, IPublisherService publisherService, IAuthorService authorService, ApplicationDbContext context)
        {
            _logger = logger;
            _bookService = bookService;
            _categoryService = categoryService;
            _publisherService = publisherService;
            _authorService = authorService;
            _context = context;
        }

        public IActionResult Index(string SearchText = "", int? page = 1)
        {
            if (SearchText != "" && SearchText != null)
            {
                var model = _bookService.GetBookByNameAndAuthor(SearchText).Select(book => new BookIndexViewModel
                {
                    ID = book.ID,
                    Name = book.Name,
                    PublicDate = book.PublicDate,
                    Amount = book.Amount,
                    Price = book.Price,
                    Image_URL = book.Image_URL,
                    AuthorID = book.AuthorID,
                    CategoryID = book.CategoryID,
                    PublisherID = book.PublisherID,
                }).ToList();

                foreach (var item in model)
                {
                    item.Author = _authorService.GetById(item.AuthorID).Name;
                    item.Category = _categoryService.GetByID(item.CategoryID).Name;
                    item.Publisher = _publisherService.GetById(item.PublisherID).Name;
                }
                int pagesize = 8;
                int pagenumber = (page ?? 1);
                return View(model.ToPagedList(pagenumber, pagesize));
            } 
            else
            {
                var model = _bookService.GetAll().Select(c => new BookIndexViewModel
                {
                    ID = c.ID,
                    Name = c.Name,
                    PublicDate = c.PublicDate,
                    Amount = c.Amount,
                    Price = c.Price,
                    Image_URL = c.Image_URL,
                    AuthorID = c.AuthorID,
                    PublisherID = c.PublisherID,
                    CategoryID = c.CategoryID,
                }).OrderBy(x => x.ID).ToList();

                foreach (var item in model)
                {
                    item.Author = _authorService.GetById(item.AuthorID).Name;
                    item.Category = _categoryService.GetByID(item.CategoryID).Name;
                    item.Publisher = _publisherService.GetById(item.PublisherID).Name;
                }

                int pagesize = 8;
                int pagenumber = (page ?? 1);

                return View(model.ToPagedList(pagenumber, pagesize));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult FilterByCategory(int? page, int categoryid)
        {
            
            if (categoryid.ToString() == null)
            {
                return NotFound();
            }
            var model = _bookService.GetBookByCategoryID(categoryid).Select(book => new BookIndexViewModel
            {
                ID = book.ID,
                Name = book.Name,
                PublicDate = book.PublicDate,
                Amount = book.Amount,
                Price = book.Price,
                Image_URL = book.Image_URL,
                AuthorID = book.AuthorID,
                CategoryID = book.CategoryID,
                PublisherID = book.PublisherID,
            }).ToList();

            foreach (var item in model)
            {
                item.Author = _authorService.GetById(item.AuthorID).Name;
                item.Category = _categoryService.GetByID(item.CategoryID).Name;
                item.Publisher = _publisherService.GetById(item.PublisherID).Name;
            }
            int pagesize = 8;
            int pagenumber = (page ?? 1);
            return View(model.ToPagedList(pagenumber, pagesize));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}