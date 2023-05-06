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
        Task CreateAsAsync(Shipment newShipment);
        Task UpdateAsAsync(Shipment updateShipment);
        Task DeleteAsAsync(Shipment deleteShipment);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Shipment GetByID(int id);
        IEnumerable<Shipment> GetAll();

        IEnumerable<Shipment> GetShipmentByBillID(int BillID);
        IEnumerable<Shipment> GetShipmentByCustomerName(string CustomerName);
        IEnumerable<Shipment> GetShipmentByStatusID(int StatusID);

    }
}
