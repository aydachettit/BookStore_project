using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ShipmentService : IShipmentService
    {
        public ApplicationDbContext _context { get; set; }
        public ShipmentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsAsync(Shipment newShipment)
        {
            _context.Shipments.Add(newShipment);
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsAsync(Shipment deleteShipment)
        {
           _context.Shipments.Remove(deleteShipment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByID(int id)
        {
            var Shipment = GetByID(id);
            _context.Shipments.Remove(Shipment);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Shipment> GetAll()
        {
            return _context.Shipments.ToList();
        }

        public Shipment GetByID(int id)
        {
            return _context.Shipments.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task UpdateAsAsync(Shipment updateShipment)
        {
            _context.Shipments.Update(updateShipment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByID(int id)
        {
            var Shipment = GetByID(id);
            _context.Shipments.Update(Shipment);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Shipment> GetShipmentByBillID(int BillID)
        {
            return _context.Shipments.Where(x => x.BillID == BillID).ToList();
        }

        public IEnumerable<Shipment> GetShipmentByStatusID(int StatusID)
        {
            return _context.Shipments.Where(x => x.Shipment_Status_ID == StatusID).ToList();
        }

        public IEnumerable<Shipment> GetShipmentByCustomerName(string CustomerName)
        {
            return _context.Shipments.Where(x => x.CustomerName == CustomerName).ToList();
        }
    }
}
