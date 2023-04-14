using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IShipmentService
    {
        Task CreateAsSync(Shipment newShipment);
        Task UpdateAsSync(Shipment updateShipment);
        Task DeleteAsSync(Shipment deleteShipment);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Shipment GetByID(int id);
        IEnumerable<Shipment> GetAll();
    }
}
