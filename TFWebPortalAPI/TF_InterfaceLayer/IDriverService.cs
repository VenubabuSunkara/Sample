using System.Collections.Generic;
using System.Threading.Tasks;
using TransForce.API.Entitys;
using TransForce.API.Models;

namespace TransForce.API.TF_InterfaceLayer
{
    public interface IDriverService
    {
        Task<UploadDrivers> UploadDrivers(UploadDrivers model, string Userid);

        Task<bool> InsertDriver(Driver model, int CompanyId, string UserId);
        Driver EditDriver(int DriverId, int CompanyId);
        Task<Driver> UpdateDriver(Driver model, int CompanyId, string UserId);
        Task<bool> DeleteDriver(int Driverid, int CompanyId, string UserId);

        Task<List<Driver>> GetDrivers();
        Task<List<Driver>> GetActiveDrivers();

        Task<List<Driver>> GetDriversByName(string Name);
    }
}