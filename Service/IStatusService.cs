using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public interface IStatusService {
        Task CreateAsSync(Status newStatus);
        Task UpdateAsSync(Status updateStatus);
        Task DeleteAsSync(Status deleteStatus);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Status GetByID(int id);
        IEnumerable<Status> GetAll();
    }
}