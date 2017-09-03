using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TransForce.API.DTOS;
using TransForce.API.Entitys;
using TransForce.API.Models;

namespace TransForce.API.TF_InterfaceLayer
{
    public interface ILocationConfigService
    {
        void CreateLocationChild(int CompanyID, string CreatedBy);
        List<LocationConfigurationDTO> GetLocationConfigData(int CompanyID);
        LocationConfigurationDTO CreateLocationParent(Location_Configuration model);
        LocationConfigurationDTO CreateLocationGrandParent(Location_Configuration model);
        bool DeleteLocationConfig(int LocConfigID, int CompanyID);

        #region Locations, Regions and Area
        List<LocationMetadataDTO> GetLocationDetails(int CompanyID);
        bool AddLocationMeta(LocationMetadata model);
        LocationMetadataDTO UpdateLocationMeta(LocationMetadata model);
        bool DeleteLocationMeta(int LocationMetaID);
        LocationMetadataDTO EditLocationMeta(int LocationMetaID);
        #endregion
        int? GetLocationConfigByCompany(int CompanyID);
        Task<UploadLocation> UploadLocations(UploadLocation model, string Userid);

        Task<bool> AddLocation(Location model, int CompanyId, string UserId);
        Location EditLocation(int LocId, int CompanyId);
        Task<Location> UpdateLocation(Location model, int CompanyId, string UserId);
        Task<bool> DeleteLocation(int LocId, int CompanyId, string UserId);

        Task<List<LocationList>> GetLocations();  //Inspection form
    }
}