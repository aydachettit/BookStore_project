using BookStore_project.Models.Author;
using BookStore_project.Models.Import;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public IActionResult Index()
        {
            var model = _ImportService.GetAll().Select(Import => new ImportIndexViewModel
            {
                id=Import.id,
                date_import=Import.date_import,
                Total=Import.Total
            }).ToList();

            return View(model);
        }
        public IActionResult number()
        {
            var model = new ImportNumberOfProductsViewModel();
           
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(ImportNumberOfProductsViewModel modell)
        {
            var import = new Import();
            import.date_import = DateTime.Today;
            import.Total = 0;
            _ImportService.CreateAsSync(import);
            var model = new ImportCreateViewModel();
            model.import_id = import.id;
            model.numberofproduct = modell.number;
            ViewBag.Books = _importDetailService.getAllBookforImport();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImportCreateViewModel model)
        {
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
                    return RedirectToAction("Index");
              
            }
            return View();
        }
        [HttpGet]
        public IActionResult DeleteIme(int id)
        {
            var import = _ImportService.GetById(id);
            _ImportService.DeleteAsSync(import);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
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
