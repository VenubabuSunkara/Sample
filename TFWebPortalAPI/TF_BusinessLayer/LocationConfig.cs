using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransForce.API.TF_DataLayer;
using TransForce.API.TF_InterfaceLayer;
using TransForce.API.Entitys;
using TransForce.API.Models;
using System.Threading.Tasks;
using TransForce.API.DTOS;

namespace TransForce.API.TF_BusinessLayer
{
    public class LocationConfig
    {
        public void CreateLocationChild(int CompanyID, string UserID)
        {
            ILocationConfigService loc = new LocationConfigService(CompanyID);
            loc.CreateLocationChild(CompanyID, UserID);
        }
        public List<LocationConfigurationDTO> GetLocationConfigByCompanyID(int CompanyID)
        {
            ILocationConfigService loc = new LocationConfigService(CompanyID);
            return loc.GetLocationConfigData(CompanyID);
        }

        public LocationConfigurationDTO CreateLocationForParent(Location_Configuration model)
        {
            ILocationConfigService loc = new LocationConfigService(Convert.ToInt32(model.CompanyID));
            return loc.CreateLocationParent(model);
        }

        public LocationConfigurationDTO CreateLocationForGrandParent(Location_Configuration model)
        {
            ILocationConfigService loc = new LocationConfigService(Convert.ToInt32(model.CompanyID));
            return loc.CreateLocationGrandParent(model);
        }
        public bool DeletingLocationConfig(int LocConfigID, int CompanyID)
        {
            ILocationConfigService loc = new LocationConfigService(CompanyID);
            return loc.DeleteLocationConfig(LocConfigID, CompanyID);
        }
        #region Get Locationdetails, Region and Area
        public List<LocationMetadataDTO> GetLocationDetails(int CompanyID)
        {
            ILocationConfigService loc = new LocationConfigService(CompanyID);
            return loc.GetLocationDetails(CompanyID);
        }
        public bool AddLocationMeta(LocationMetadata model)
        {
            ILocationConfigService loc = new LocationConfigService(model.CustomerID);
            return loc.AddLocationMeta(model);
        }
        public LocationMetadataDTO UpdateLocationMeta(LocationMetadata model)
        {
            ILocationConfigService loc = new LocationConfigService(model.CustomerID);
            return loc.UpdateLocationMeta(model);
        }
        #endregion

        public int? GetLocationConfigByCompany(int CompanyID)
        {
            ILocationConfigService loc = new LocationConfigService(CompanyID);
            return loc.GetLocationConfigByCompany(CompanyID);
        }
        public async Task<UploadLocation> UploadLocations(UploadLocation model, string Userid)
        {
            ILocationConfigService loc = new LocationConfigService(model.CompanyID);
            return await loc.UploadLocations(model, Userid);
        }


        public async Task<bool> AddLocation(Location model, string UserId)
        {
            ILocationConfigService loc = new LocationConfigService(model.CompanyID);
            return await loc.AddLocation(model, model.CompanyID, UserId);
        }

        public Location EditLocation(int LocId, int CompanyId)
        {
            ILocationConfigService loc = new LocationConfigService(CompanyId);
            return loc.EditLocation(LocId, CompanyId);
        }

        public async Task<Location> UpdateLocation(Location model, string UserId)
        {
            ILocationConfigService loc = new LocationConfigService(model.CompanyID);
            return await loc.UpdateLocation(model, model.CompanyID, UserId);
        }

        public async Task<bool> DeleteLocation(int LocId, int CompanyID, string UserId)
        {
            ILocationConfigService loc = new LocationConfigService(CompanyID);
            return await loc.DeleteLocation(LocId, CompanyID, UserId);
        }
        public async Task<List<LocationList>> GetLocations(int CompanyId)
        {
            ILocationConfigService locationInfo = new LocationConfigService(CompanyId);
            return await locationInfo.GetLocations();
        }
    }
}