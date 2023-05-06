using Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IImportDetailService
    {
        Task CreateAsSync(ImportDetail detail);
        Book searchBookByName (string title);
        List<ImportDetail> getAllByImportId(int id);
        IEnumerable<SelectListItem> getAllBookforImport();
    }
}
