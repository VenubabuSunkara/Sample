using System.Collections.Generic;
using System.Threading.Tasks;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_DataLayer;
using TransForce.API.TF_InterfaceLayer;

namespace TransForce.API.TF_BusinessLayer
{
    public class DriverConfig
    {
        public async Task<UploadDrivers> UploadDrivers(UploadDrivers model, string UserID)
        {
            IDriverService DriverInfo = new DriverService(model.CompanyID);
            return await DriverInfo.UploadDrivers(model, UserID);
        }
        public async Task<bool> InsertDriver(Driver model, string UserId)
        {
            IDriverService DriverInfo = new DriverService(model.CompanyId);
            return await DriverInfo.InsertDriver(model, model.CompanyId, UserId);
        }

        public Driver EditDriver(int DriverId, int CompanyId)
        {
            IDriverService DriverInfo = new DriverService(CompanyId);
            return DriverInfo.EditDriver(DriverId, CompanyId);
        }
        public async Task<Driver> UpdateDriver(Driver model, string UserId)
        {
            IDriverService DriverInfo = new DriverService(model.CompanyId);
            return await DriverInfo.UpdateDriver(model, model.CompanyId, UserId);
        }

        public async Task<bool> DeleteDriver(int DriverId, int CompanyId, string UserId)
        {
            IDriverService DriverInfo = new DriverService(CompanyId);
            return await DriverInfo.DeleteDriver(DriverId, CompanyId, UserId);
        }
        public async Task<List<Driver>> GetDrivers(int CompanyId)
        {
            IDriverService DriverInfo = new DriverService(CompanyId);
            return await DriverInfo.GetDrivers();
        }
        public async Task<List<Driver>> GetActiveDrivers(int CompanyId)
        {
            IDriverService DriverInfo = new DriverService(CompanyId);
            return await DriverInfo.GetActiveDrivers();
        }

        public async Task<List<Driver>> GetDriversByName(int CompanyId, string Name)
        {
            IDriverService DriverInfo = new DriverService(CompanyId);
            return await DriverInfo.GetDriversByName(Name);
        }
    }
}