using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ProductDetailService : IProductDetailService
    {
        private ApplicationDbContext _context;
        public ProductDetailService(ApplicationDbContext context)
        {
            _context= context;

        }
        public ProductDetail GetByID(int id)
        {
            return _context.ProductDetail.Where(pd => pd.ID == id).FirstOrDefault();
        }
    }
}
