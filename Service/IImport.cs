using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public  interface IImport
    {
        Task CreateAsSync(Import import);
        Task DeleteAsSync(Import import);
        IEnumerable<Import> GetAll();
        Import GetById(int id);
        Task UpdateAsSnc(Import import);
    }
}
