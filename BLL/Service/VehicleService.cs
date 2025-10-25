using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Entities;
using DAL.Repositories;
using System.Collections.Generic;

namespace BLL.Services
{
    public class VehicleService
    {
        private readonly VehicleRepository _repository;

        public VehicleService()
        {
            _repository = new VehicleRepository();
        }

        public List<Vehicle> GetVehicles()
        {
            return _repository.GetAll();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _repository.Add(vehicle);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _repository.Update(vehicle);
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            _repository.Delete(vehicle);
        }

        public List<Vehicle> SearchVehicles(string keyword)
        {
            return _repository.Search(keyword);
        }
    }
}