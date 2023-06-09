﻿using BookStore_project.Models.Author;
using BookStore_project.Models.Import;
using Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using PagedList;
using Service;
using Service.Implementation;

namespace BookStore_project.Controllers
{
    public class ImportController : Controller
    {
        private IImport _ImportService;
        private IBookService _bookService;
        private IImportDetailService _importDetailService;
        private IWebHostEnvironment _hostingEnvironment;
        public ImportController(IImport importservice, IImportDetailService importDetailService, IBookService bookService,IWebHostEnvironment hostingEnvironment)
        {
            _ImportService = importservice;
            _importDetailService = importDetailService;
            _bookService = bookService;
            _hostingEnvironment = hostingEnvironment;
        }
       
        public IActionResult Index(int? page)
        { if (!User.IsInRole("Admin") )
            {
                return RedirectToAction("Index", "Home");
            }
            
            var model = _ImportService.GetAll().Select(Import => new ImportIndexViewModel
            {
                id=Import.id,
                date_import=Import.date_import,
                Total=Import.Total
            }).OrderBy(x=>x.id).ToList();
            int pagesize = 5;
            int pagenumber = (page ?? 1);

            return View(model.ToPagedList(pagenumber, pagesize));
        }
        
        public IActionResult number()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ImportNumberOfProductsViewModel();
           
            return View(model);
        }
        [HttpGet]
        
        public IActionResult Create(ImportNumberOfProductsViewModel modell)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var import = new Import();
            import.date_import = DateTime.Now;
            import.Total = 0;
            _ImportService.CreateAsSync(import);
            var model = new ImportCreateViewModel();
            model.import_id = import.id;
            model.numberofproduct = modell.number;
            model.date_import = import.date_import;
            ViewBag.Books = _importDetailService.getAllBookforImport();
            return View(model);
        }
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImportCreateViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                
                    var total = 0.0;
                    foreach (var detail in model.lod)
                    {
                        total += detail.book_amount * detail.book_price;
                        detail.import_id = model.import_id;
                        var book_id = Convert.ToInt32(detail.book_name);
                        var book = _bookService.GetByID(book_id);
                        book.Amount += detail.book_amount;
                        await _bookService.UpdateAsSync(book);
                        await _importDetailService.CreateAsSync(detail);
                    }
                    var import = _ImportService.GetById(model.import_id);
                    import.Total = total;
                    await _ImportService.UpdateAsSnc(import);
                DateTime dateImport = _ImportService.GetById(model.import_id).date_import;
                string year = Convert.ToString(dateImport.Year);
                string month = Convert.ToString(dateImport.Month);
                string Day = Convert.ToString(dateImport.Day);
                string time = Convert.ToString(dateImport.TimeOfDay.Hours)+ Convert.ToString(dateImport.TimeOfDay.Minutes);
                string Date = year+month+Day+time;
                string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, ("wwwroot/exportpdf/"+Date+".pdf"));
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();
                ///
                Paragraph title = new Paragraph("Import File ID #" + import.id, new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                ///
                document.Add(new Paragraph("IMPORT PRODUCT"));
                document.Add(new Paragraph("Date: " + dateImport.ToShortDateString()));
                PdfPTable table = new PdfPTable(3);
                table.WidthPercentage = 100;
                table.SpacingBefore = 20;
                table.SpacingAfter = 20;
                PdfPCell cell = new PdfPCell(new Phrase("Book Name", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Quantity", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Price$", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);

                foreach(var detail in model.lod)
                {
                    var book_id = Convert.ToInt32(detail.book_name);
                    var book = _bookService.GetByID(book_id);
                    table.AddCell(new PdfPCell(new Phrase(book.Name)));
                    table.AddCell(new PdfPCell(new Phrase((detail.book_amount).ToString())));
                    table.AddCell(new PdfPCell(new Phrase((detail.book_price).ToString())));
                }
                document.Add(table);
                Paragraph totalcell = new Paragraph("Total: " + total.ToString("C"), new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
                totalcell.Alignment = Element.ALIGN_RIGHT;
                document.Add(totalcell);
                document.Close();
                
                return RedirectToAction("Index");
              
            }
            return View();
        }
        [HttpGet]
        
        public IActionResult DeleteIme(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var import = _ImportService.GetById(id);
            _ImportService.DeleteAsSync(import);
            return RedirectToAction("Index");
        }
        [HttpGet]
       
        public IActionResult Delete(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var import =_ImportService.GetById(id);
            var model = new ImportDeleteViewModel();
            model.id = import.id;
            model.date_import = import.date_import;
            model.Total = import.Total;
            return View(model);
        }
        [HttpPost]
        
        public async Task<IActionResult> Delete(ImportDeleteViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var import = _ImportService.GetById(model.id);
                await _ImportService.DeleteAsSync(import);
                return RedirectToAction("Index");
            }
            return View();
        }
       
        public IActionResult Detail(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var import = _ImportService.GetById(id);
            var list_of_detail = _importDetailService.getAllByImportId(id);
            var model = new ImportDetailViewModel();
            model.id = id;
            model.date_import = import.date_import;
            model.Total = import.Total;
            model.lod = list_of_detail;
            foreach(var item in model.lod)
            {
                item.book_name = _bookService.GetByID(Convert.ToInt32(item.book_name)).Name;
            }
            return View(model);
        }
    }
}
