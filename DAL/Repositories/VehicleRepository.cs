using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class VehicleRepository
    {
        private readonly EVRentalDBContext _context;

        public VehicleRepository()
        {
            _context = new EVRentalDBContext();
        }
        public List<Vehicle> GetAll()
        {
            return _context.Vehicles.ToList();
        }

        public void Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void Update(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }

        public void Delete(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();
        }

        public List<Vehicle> Search(string keyword)
        {
            return _context.Vehicles
                .Where(v => v.LicensePlate.Contains(keyword) || v.Model.Contains(keyword))
                .ToList();
        }
    }
}