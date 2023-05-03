using BookStore_project.Models.ProductDetail;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace BookStore_project.Controllers
{
    public class ProductDetailController : Controller
    {

        private ApplicationDbContext _context;
        private IProductDetailService _productDetailService;
        private IAuthorService _authorService;

        public ProductDetailController(ApplicationDbContext context, IProductDetailService productDetailService, IAuthorService authorService)
        {
            _context = context;
            _productDetailService = productDetailService;
            _authorService = authorService;
        }
        public IActionResult Index(int productID=1,int authorID =1)
        {
            var productDetail = _productDetailService.GetByID(productID);
            var author = _authorService.GetById(authorID);

            if (author == null)
                return NotFound();
            var model = new ProductDetailViewModel
            {
                ID = productDetail.ID,
                ProductTitle = productDetail.ProductTitle,
                ProductDescription = productDetail.ProductDescription,
                ProductPrice = productDetail.ProductPrice,
                AuthorName = author.Name,
                
            };

            return View(model);
        }
    }
}
