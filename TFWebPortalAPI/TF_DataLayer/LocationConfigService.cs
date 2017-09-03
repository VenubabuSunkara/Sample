using System;
using System.Collections.Generic;
using System.Linq;
using TransForce.API.App_Start;
using TransForce.API.Entitys;
using TransForce.API.TF_InterfaceLayer;
using TransForce.API.Models;
using System.Threading.Tasks;
using TransForce.API.DTOS;
using System.Data.Entity;
namespace TransForce.API.TF_DataLayer
{
    public class LocationConfigService : ILocationConfigService
    {
        private readonly TFPMEntities context;
        private readonly bool IsActiveProfile;
        private readonly int CompanyId;
        public LocationConfigService()
        {
            context = new TFPMEntities();
            IsActiveProfile = true;
        }
        public LocationConfigService(int CompanyID)
        {
            context = new Entitys.TFPMEntities();
            IsActiveProfile = context.CustomerProfiles.Any(x => x.CompanyID == CompanyID && x.IsDelete == false);
            CompanyId = CompanyID;
        }
        /// <summary>
        /// Get LocationConfig by ComapnyID
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public List<LocationConfigurationDTO> GetLocationConfigData(int CompanyID)
        {
            if (IsActiveProfile)
            {
                return context.Location_Configuration.Where(x => x.CompanyID.Equals(CompanyID))
                    .Select(x => new LocationConfigurationDTO
                    {
                        LocConfigID = x.LocConfigID,
                        Key = x.Key,
                        Value = x.Value,
                        CompanyID = x.CompanyID,
                        Relation = x.Relation,
                        CreatedOn = x.CreatedOn,
                        ModifiedOn = x.ModifiedOn,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy
                    }).ToList();
            }
            return null;
        }

        /// <summary>
        /// Create Location
        /// </summary>
        /// <param name="CompanyID"></param>
        public void CreateLocationChild(int CompanyID, string CreatedBy)
        {
            if (IsActiveProfile)
            {
                var location = new Location_Configuration
                {
                    CompanyID = CompanyID,
                    Key = "Location",
                    Value = "Location",
                    CreatedOn = DateTime.Now,
                    Relation = 0,
                    CreatedBy = CreatedBy
                };
                context.Location_Configuration.Add(location);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Create Location Parent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LocationConfigurationDTO CreateLocationParent(Location_Configuration model)
        {
            if (IsActiveProfile)
            {
                var locationdata = context.Location_Configuration.Where(o => o.CompanyID.Equals(model.CompanyID) && o.Key.Equals(model.Key))
                    .Select(x => new LocationConfigurationDTO
                    {
                        LocConfigID = x.LocConfigID,
                        Key = x.Key,
                        Value = x.Value,
                        CompanyID = x.CompanyID,
                        Relation = x.Relation,
                        CreatedOn = x.CreatedOn,
                        ModifiedOn = x.ModifiedOn,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy

                    }).FirstOrDefault();
                if (locationdata == null)
                {
                    var location = new Location_Configuration
                    {
                        CompanyID = model.CompanyID,
                        Key = CommonFunctions.Trimstring(model.Key),
                        Value = CommonFunctions.Trimstring(model.Value),
                        CreatedOn = DateTime.Now,
                        CreatedBy = model.CreatedBy,
                        Relation = 1
                    };
                    context.Location_Configuration.Add(location);
                    context.SaveChanges();
                    var locconfig = new LocationConfigurationDTO
                    {
                        LocConfigID = location.LocConfigID,
                        Key = location.Key,
                        Value = location.Value,
                        CompanyID = location.CompanyID,
                        Relation = location.Relation,
                        CreatedOn = location.CreatedOn,
                        ModifiedOn = location.ModifiedOn,
                        CreatedBy = location.CreatedBy,
                        ModifiedBy = location.ModifiedBy
                    };
                    return locconfig;
                }
                return locationdata;
            }
            return null;

        }

        /// <summary>
        /// Create Location GrandParent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LocationConfigurationDTO CreateLocationGrandParent(Location_Configuration model)
        {
            if (IsActiveProfile)
            {
                var locationdata = context.Location_Configuration.Where(o => o.CompanyID.Equals(model.CompanyID) && o.Key.Equals(model.Key))
                   .Select(x => new LocationConfigurationDTO
                   {
                       LocConfigID = x.LocConfigID,
                       Key = x.Key,
                       Value = x.Value,
                       CompanyID = x.CompanyID,
                       Relation = x.Relation,
                       CreatedOn = x.CreatedOn,
                       ModifiedOn = x.ModifiedOn,
                       CreatedBy = x.CreatedBy,
                       ModifiedBy = x.ModifiedBy
                   }).FirstOrDefault();
                if (locationdata == null)
                {
                    var location = new Location_Configuration
                    {
                        CompanyID = model.CompanyID,
                        Key = CommonFunctions.Trimstring(model.Key),
                        Value = CommonFunctions.Trimstring(model.Value),
                        CreatedOn = DateTime.Now,
                        CreatedBy = model.CreatedBy,
                        Relation = 2
                    };
                    context.Location_Configuration.Add(location);
                    context.SaveChanges();
                    var locconfig = new LocationConfigurationDTO
                    {
                        LocConfigID = location.LocConfigID,
                        Key = location.Key,
                        Value = location.Value,
                        CompanyID = location.CompanyID,
                        Relation = location.Relation,
                        CreatedOn = location.CreatedOn,
                        ModifiedOn = location.ModifiedOn,
                        CreatedBy = location.CreatedBy,
                        ModifiedBy = location.ModifiedBy
                    };
                    return locconfig;
                }
                return locationdata;
            }
            return null;

        }

        /// <summary>
        /// Delete LocationConfig
        /// </summary>
        /// <param name="LocConfigID"></param>
        public bool DeleteLocationConfig(int LocConfigID, int CompanyID)
        {
            if (IsActiveProfile)
            {
                var LocationConfigCount = context.Location_Configuration.Where(x => x.CompanyID.Equals(CompanyID)).Count() - 1;
                var ConfigID = context.Location_Configuration.Where(x => x.LocConfigID.Equals(LocConfigID)).FirstOrDefault();
                if (LocationConfigCount > 0 && LocationConfigCount == ConfigID.Relation)
                {
                    context.Location_Configuration.Attach(ConfigID);
                    context.Location_Configuration.Remove(ConfigID);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }


        #region Create/Edit/Delete Locationdetails, Region and Area
        /// <summary>
        /// Get Location Config Details
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<LocationMetadataDTO> GetLocationDetails(int CompanyID)
        {
            if (IsActiveProfile)
            {
                var loactionDetails = context.LocationMetadatas.Where(loc => loc.CustomerID.Equals(CompanyID) && loc.IsDelete == false)
                    .Select(x => new LocationMetadataDTO
                    {
                        LocConfigID = x.LocConfigID,
                        Key = x.Key,
                        Value = x.Value,
                        CustomerID = x.CustomerID,
                        FieldType = x.FieldType,
                        LocId = x.LocId,
                        LocMID = x.LocMID,
                        Location = x.Location,
                        Location_Configuration = x.Location_Configuration,
                        CreatedOn = x.CreatedOn,
                        ModifiedOn = x.ModifiedOn,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy
                    }).ToList();
                return loactionDetails;
            }
            return null;
        }
        /// <summary>
        /// Add New Location
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddLocationMeta(LocationMetadata model)
        {
            if (IsActiveProfile)
            {
                var LocationMeta = context.LocationMetadatas.Any(locmeta => locmeta.FieldType.Equals(model.FieldType)
                && (locmeta.Key.Equals(model.Key) || locmeta.Value.Equals(model.Value))
                && locmeta.CustomerID.Equals(model.CustomerID));
                if (LocationMeta)
                {
                    var LocationMetaData = new LocationMetadata
                    {
                        Key = CommonFunctions.Trimstring(model.Key),
                        Value = CommonFunctions.Trimstring(model.Value),
                        FieldType = model.FieldType,
                        LocConfigID = model.LocConfigID,
                        CustomerID = model.CustomerID,
                        Tags = CommonFunctions.Trimstring(model.Tags),
                        CreatedBy = model.CreatedBy,
                        CreatedOn = model.CreatedOn
                    };
                    context.LocationMetadatas.Add(LocationMetaData);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// Update Location metadata
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public LocationMetadataDTO UpdateLocationMeta(LocationMetadata model)
        {
            if (IsActiveProfile)
            {
                var LocationMeta = context.LocationMetadatas.Where(x => x.LocMID.Equals(model.LocMID)).FirstOrDefault();
                if (LocationMeta != null)
                {
                    LocationMeta.Value = CommonFunctions.Trimstring(model.Value);
                    LocationMeta.Tags = CommonFunctions.Trimstring(model.Tags);
                    LocationMeta.ModifiedBy = model.ModifiedBy;
                    LocationMeta.ModifiedOn = model.ModifiedOn;
                    context.SaveChanges();
                }
                var locatinM = new LocationMetadataDTO
                {
                    LocMID = LocationMeta.LocMID,
                    CustomerID = LocationMeta.CustomerID,
                    LocConfigID = LocationMeta.LocConfigID,
                    Key = LocationMeta.Key,
                    Value = LocationMeta.Value,
                    FieldType = LocationMeta.FieldType,
                    Tags = LocationMeta.Tags,
                    CreatedBy = LocationMeta.CreatedBy,
                    CreatedOn = LocationMeta.CreatedOn,
                    ModifiedBy = LocationMeta.ModifiedBy,
                    ModifiedOn = LocationMeta.ModifiedOn,
                    LocId = LocationMeta.LocId
                };
                return locatinM;
            }
            return null;
        }

        public bool DeleteLocationMeta(int LocationMetaID)
        {
            if (IsActiveProfile)
            {
                var locmeta = context.LocationMetadatas.FirstOrDefault(x => x.LocMID.Equals(LocationMetaID));
                if (locmeta != null)
                {
                    context.LocationMetadatas.Remove(locmeta);
                    return true;
                }
                return false;
            }
            return false;
        }

        public LocationMetadataDTO EditLocationMeta(int LocationMetaID)
        {
            if (IsActiveProfile)
            {
                return context.LocationMetadatas.Where(x => x.LocMID.Equals(LocationMetaID))
                    .Select(x => new LocationMetadataDTO
                    {
                        LocMID = x.LocMID,
                        CustomerID = x.CustomerID,
                        LocConfigID = x.LocConfigID,
                        Key = x.Key,
                        Value = x.Value,
                        FieldType = x.FieldType,
                        Tags = x.Tags,
                        CreatedBy = x.CreatedBy,
                        CreatedOn = x.CreatedOn,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedOn = x.ModifiedOn,
                        LocId = x.LocId
                    }).FirstOrDefault();
            }
            return null;
        }
        #endregion
        public int? GetLocationConfigByCompany(int CompanyID)
        {
            if (IsActiveProfile)
            {
                return context.Location_Configuration.FirstOrDefault(x => x.CompanyID == CompanyID).LocConfigID;
            }
            return null;
        }
        #region Location Upload
        private LocationMetadata SetMeta(int CompanyID, int LocConfigID, string Key, string Value, int FieldType, string Tags, string UserID, DateTime Date, int LocId, bool isActive)
        {
            LocationMetadata meta = new LocationMetadata();
            meta.CustomerID = CompanyID;
            meta.LocConfigID = LocConfigID;
            meta.Key = Key;
            meta.Value = Value;
            meta.FieldType = FieldType;
            meta.Tags = Tags;
            meta.LocId = LocId;
            meta.CreatedBy = UserID;
            meta.CreatedOn = Date;
            meta.ModifiedBy = UserID;
            meta.ModifiedOn = DateTime.Now;
            meta.IsActive = isActive;
            return meta;
        }
        private LocationValidationData ValidateModel(UploadLocation model)
        {
            LocationValidationData data = new LocationValidationData();
            data.UnValidData = new UploadLocation();
            data.ValidData = new UploadLocation();
            data.ValidData.Locationdata = new List<LocationData>();
            data.UnValidData.Locationdata = new List<LocationData>();

            List<LocationData> invalidLocationName = model.Locationdata.Where(x => string.IsNullOrEmpty(x.LocationName))
                                                              .Select(x => new LocationData
                                                              {
                                                                  LocationName = x.LocationName,
                                                                  LocCode = x.LocCode,
                                                                  FirstContactEmail = x.FirstContactEmail,
                                                                  FirstContactName = x.FirstContactName,
                                                                  SecondContactEmail = x.SecondContactEmail,
                                                                  SecondContactName = x.SecondContactName,
                                                                  isActive = x.isActive,
                                                                  ThirdContactEmail = x.ThirdContactEmail,
                                                                  ThirdContactName = x.ThirdContactName,
                                                                  Tags = x.Tags,
                                                                  Remarks = "Location Name is Empty"
                                                              }).ToList();
            model.Locationdata.RemoveAll(x => invalidLocationName.Any(y => y.LocationName == x.LocationName));


            List<LocationData> invalidLocationCode = model.Locationdata.Where(x => string.IsNullOrEmpty(x.LocCode))
                                      .Select(x => new LocationData
                                      {
                                          LocationName = x.LocationName,
                                          LocCode = x.LocCode,
                                          FirstContactEmail = x.FirstContactEmail,
                                          FirstContactName = x.FirstContactName,
                                          SecondContactEmail = x.SecondContactEmail,
                                          SecondContactName = x.SecondContactName,
                                          isActive = x.isActive,
                                          ThirdContactEmail = x.ThirdContactEmail,
                                          ThirdContactName = x.ThirdContactName,
                                          Tags = x.Tags,
                                          Remarks = "Location Code is Empty"
                                      }).ToList();
            model.Locationdata.RemoveAll(x => invalidLocationCode.Any(y => y.LocCode == x.LocCode));

            List<LocationData> invalidFirstContactName = model.Locationdata.Where(x => string.IsNullOrEmpty(x.FirstContactName))
                                         .Select(x => new LocationData
                                         {
                                             LocationName = x.LocationName,
                                             LocCode = x.LocCode,
                                             FirstContactEmail = x.FirstContactEmail,
                                             FirstContactName = x.FirstContactName,
                                             SecondContactEmail = x.SecondContactEmail,
                                             SecondContactName = x.SecondContactName,
                                             isActive = x.isActive,
                                             ThirdContactEmail = x.ThirdContactEmail,
                                             ThirdContactName = x.ThirdContactName,
                                             Tags = x.Tags,
                                             Remarks = "FirstContact Name is Empty"
                                         }).ToList();
            model.Locationdata.RemoveAll(x => invalidFirstContactName.Any(y => y.FirstContactName == x.FirstContactName));

            List<LocationData> invalidFirstContactEmail = model.Locationdata.Where(x => !CommonFunctions.validateEmail(x.FirstContactEmail))
                                      .Select(x => new LocationData
                                      {
                                          LocationName = x.LocationName,
                                          LocCode = x.LocCode,
                                          FirstContactEmail = x.FirstContactEmail,
                                          FirstContactName = x.FirstContactName,
                                          SecondContactEmail = x.SecondContactEmail,
                                          SecondContactName = x.SecondContactName,
                                          isActive = x.isActive,
                                          ThirdContactEmail = x.ThirdContactEmail,
                                          ThirdContactName = x.ThirdContactName,
                                          Tags = x.Tags,
                                          Remarks = "FirstContact Email is Empty or Email format wrong"
                                      }).ToList();
            model.Locationdata.RemoveAll(x => invalidFirstContactEmail.Any(y => y.FirstContactEmail == x.FirstContactEmail));

            List<LocationData> invalidSecondContactEmail = model.Locationdata.Where(x => !CommonFunctions.validateSEmail(x.SecondContactEmail))
                                                 .Select(x => new LocationData
                                                 {
                                                     LocationName = x.LocationName,
                                                     LocCode = x.LocCode,
                                                     FirstContactEmail = x.FirstContactEmail,
                                                     FirstContactName = x.FirstContactName,
                                                     SecondContactEmail = x.SecondContactEmail,
                                                     SecondContactName = x.SecondContactName,
                                                     isActive = x.isActive,
                                                     ThirdContactEmail = x.ThirdContactEmail,
                                                     ThirdContactName = x.ThirdContactName,
                                                     Tags = x.Tags,
                                                     Remarks = "SecondContact Email is Wrongformat"
                                                 }).ToList();
            model.Locationdata.RemoveAll(x => invalidSecondContactEmail.Any(y => y.SecondContactEmail == x.SecondContactEmail));

            List<LocationData> invalidThirdContactEmail = model.Locationdata.Where(x => !CommonFunctions.validateSEmail(x.ThirdContactEmail))
                                                 .Select(x => new LocationData
                                                 {
                                                     LocationName = x.LocationName,
                                                     LocCode = x.LocCode,
                                                     FirstContactEmail = x.FirstContactEmail,
                                                     FirstContactName = x.FirstContactName,
                                                     SecondContactEmail = x.SecondContactEmail,
                                                     SecondContactName = x.SecondContactName,
                                                     isActive = x.isActive,
                                                     ThirdContactEmail = x.ThirdContactEmail,
                                                     ThirdContactName = x.ThirdContactName,
                                                     Tags = x.Tags,
                                                     Remarks = "ThirdContact Email is Wrongformat"
                                                 }).ToList();
            model.Locationdata.RemoveAll(x => invalidThirdContactEmail.Any(y => y.ThirdContactEmail == x.ThirdContactEmail));

            data.ValidData = model;
            data.UnValidData.Locationdata = invalidLocationName;
            data.UnValidData.Locationdata.AddRange(invalidLocationCode);
            data.UnValidData.Locationdata.AddRange(invalidFirstContactName); data.UnValidData.Locationdata.AddRange(invalidFirstContactEmail);
            data.UnValidData.Locationdata.AddRange(invalidSecondContactEmail); data.UnValidData.Locationdata.AddRange(invalidThirdContactEmail);
            return data;
        }
        private async Task insertLocations(TFPMEntities context, LocationData loc, int? LocConfigID, int CompanyID, string Userid, int LocId, bool isActive)
        {
            List<LocationMetadata> metadata = new List<LocationMetadata>();
            metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "LocationName", loc.LocationName, 1, loc.Tags, Userid, DateTime.Now, LocId, isActive));
            metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "FirstContactName", loc.FirstContactName, 3, loc.Tags, Userid, DateTime.Now, LocId, isActive));
            metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "FirstContactEmail", loc.FirstContactEmail, 2, loc.Tags, Userid, DateTime.Now, LocId, isActive));
            if (!string.IsNullOrEmpty(loc.SecondContactName))
            {
                metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "SecondContactName", loc.SecondContactName, 5, loc.Tags, Userid, DateTime.Now, LocId, isActive));
            }
            if (!string.IsNullOrEmpty(loc.SecondContactEmail))
            {
                metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "SecondContactEmail", loc.SecondContactEmail, 4, loc.Tags, Userid, DateTime.Now, LocId, isActive));
            }
            if (!string.IsNullOrEmpty(loc.ThirdContactName))
            {
                metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "ThirdContactName", loc.ThirdContactName, 7, loc.Tags, Userid, DateTime.Now, LocId, isActive));
            }
            if (!string.IsNullOrEmpty(loc.ThirdContactEmail))
            {
                metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "ThirdContactEmail", loc.ThirdContactEmail, 6, loc.Tags, Userid, DateTime.Now, LocId, isActive));
            }
            context.LocationMetadatas.AddRange(metadata);
            await context.SaveChangesAsync();
        }

        public async Task<UploadLocation> UploadLocations(UploadLocation model, string Userid)
        {

            if (IsActiveProfile)
            {
                var locData = ValidateModel(model);
                int? LocConfigID = GetLocationConfigByCompany(model.CompanyID);
                foreach (var loc in locData.ValidData.Locationdata)
                {
                    bool isActive = (loc.isActive.ToLower() == "yes" || loc.isActive.ToLower() == "y") ? true : false;
                    //Insert Location Data
                    var Locationadata = await context.Locations.FirstOrDefaultAsync(locCode => locCode.LocCode == loc.LocCode && locCode.CompanyID == model.CompanyID);
                    if (Locationadata == null)
                    {
                        Locationadata = new Location
                        {
                            LocCode = loc.LocCode,
                            CompanyID = model.CompanyID,
                            ModifiedBy = Userid,
                            ModifiedOn = DateTime.Now,
                            CreatedBy = Userid,
                            CreatedOn = DateTime.Now,
                            IsActive = isActive,
                            IsDelete = false
                        };
                        context.Locations.Add(Locationadata);
                        await context.SaveChangesAsync();
                        //Insert Location Meta data
                        await insertLocations(context, loc, LocConfigID, model.CompanyID, Userid, Locationadata.LocId, isActive);
                    }
                    else
                    {
                        //UpdateLocationMeta Locationmeta if Exists
                        var LocationMeta = Locationadata.LocationMetadatas.Where(x => x.LocId == Locationadata.LocId && x.CustomerID == model.CompanyID).ToList();
                        await UpdateLocationMeta(LocationMeta, Userid, loc, LocConfigID, model.CompanyID, Locationadata.LocId, isActive);
                    }
                }
                return locData.UnValidData;
            }
            else
            {
                return null;
            }
        }
        private async Task UpdateLocationMeta(List<LocationMetadata> LocationMeta, string Userid, LocationData loc, int? LocConfigID, int CompanyID, int LocId, bool isActive)
        {
            List<LocationMetadata> metadata = new List<LocationMetadata>();
            var LocationName = LocationMeta.FirstOrDefault(x => x.Key == "LocationName");

            LocationName.Value = loc.LocationName;
            LocationName.ModifiedBy = Userid;
            LocationName.ModifiedOn = DateTime.Now;
            await context.SaveChangesAsync();
            //
            var FirstContactName = LocationMeta.FirstOrDefault(x => x.Key == "FirstContactName");

            FirstContactName.Value = loc.FirstContactName;
            FirstContactName.ModifiedBy = Userid;
            FirstContactName.ModifiedOn = DateTime.Now;
            await context.SaveChangesAsync();
            //
            var FirstContactEmail = LocationMeta.FirstOrDefault(x => x.Key == "FirstContactEmail");
            FirstContactEmail.Value = loc.FirstContactEmail;
            FirstContactEmail.ModifiedBy = Userid;
            FirstContactEmail.ModifiedOn = DateTime.Now;
            await context.SaveChangesAsync();
            //
            if (!string.IsNullOrEmpty(loc.SecondContactName))
            {
                var SecondContactName = LocationMeta.FirstOrDefault(x => x.Key == "SecondContactName");
                if (SecondContactName == null)
                {
                    metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "SecondContactName", loc.SecondContactName, 5, loc.Tags, Userid, DateTime.Now, LocId, isActive));
                }
                else
                {
                    SecondContactName.Value = loc.SecondContactName;
                    SecondContactName.ModifiedBy = Userid;
                    SecondContactName.ModifiedOn = DateTime.Now;
                    await context.SaveChangesAsync();
                }
            }
            //
            if (!string.IsNullOrEmpty(loc.SecondContactEmail))
            {
                var SecondContactEmail = LocationMeta.FirstOrDefault(x => x.Key == "SecondContactEmail");
                if (SecondContactEmail == null)
                {
                    metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "SecondContactEmail", loc.SecondContactEmail, 4, loc.Tags, Userid, DateTime.Now, LocId, isActive));
                }
                else
                {
                    SecondContactEmail.Value = loc.SecondContactEmail;
                    SecondContactEmail.ModifiedBy = Userid;
                    SecondContactEmail.ModifiedOn = DateTime.Now;
                    await context.SaveChangesAsync();
                }
            }

            //
            if (!string.IsNullOrEmpty(loc.ThirdContactName))
            {
                var ThirdContactName = LocationMeta.FirstOrDefault(x => x.Key == "ThirdContactName");
                if (ThirdContactName == null)
                {
                    metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "ThirdContactName", loc.ThirdContactName, 7, loc.Tags, Userid, DateTime.Now, LocId, isActive));
                }
                else
                {
                    ThirdContactName.Value = loc.ThirdContactName;
                    ThirdContactName.ModifiedBy = Userid;
                    ThirdContactName.ModifiedOn = DateTime.Now;
                    await context.SaveChangesAsync();
                }
            }
            //
            if (!string.IsNullOrEmpty(loc.ThirdContactEmail))
            {
                var ThirdContactEmail = LocationMeta.FirstOrDefault(x => x.Key == "ThirdContactEmail");
                if (ThirdContactEmail == null)
                {
                    metadata.Add(SetMeta(CompanyID, LocConfigID ?? 0, "ThirdContactEmail", loc.ThirdContactEmail, 6, loc.Tags, Userid, DateTime.Now, LocId, isActive));
                }
                else
                {
                    ThirdContactEmail.Value = loc.ThirdContactEmail;
                    ThirdContactEmail.ModifiedBy = Userid;
                    ThirdContactEmail.ModifiedOn = DateTime.Now;
                    await context.SaveChangesAsync();
                }
            }
            context.LocationMetadatas.AddRange(metadata);
            await context.SaveChangesAsync();
        }

        public async Task<bool> AddLocation(Location model, int CompanyId, string UserId)
        {
            if (IsActiveProfile)
            {
                var loc = new Location
                {
                    LocCode = model.LocCode,
                    CompanyID = CompanyId,
                    IsActive = model.IsActive,
                    IsDelete = false,
                    CreatedBy = UserId,
                    ModifiedBy = UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };
                context.Locations.Add(loc);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Location EditLocation(int LocId, int CompanyId)
        {
            if (IsActiveProfile)
            {
                return context.Locations.FirstOrDefault(x => x.LocId == LocId && x.CompanyID == CompanyId);
            }
            return null;
        }

        public async Task<Location> UpdateLocation(Location model, int CompanyId, string UserId)
        {
            if (IsActiveProfile)
            {
                var loc = context.Locations.FirstOrDefault(x => x.LocId == model.LocId && x.CompanyID == CompanyId);
                if (loc != null)
                {
                    loc.IsActive = model.IsActive;
                    loc.ModifiedBy = UserId;
                    loc.ModifiedOn = DateTime.Now;
                    loc.LocCode = model.LocCode;
                    await context.SaveChangesAsync();
                    return loc;
                }
            }
            return null;
        }

        public async Task<bool> DeleteLocation(int LocId, int CompanyId, string UserId)
        {
            if (IsActiveProfile)
            {
                var loc = context.Locations.FirstOrDefault(x => x.LocId == LocId && x.CompanyID == CompanyId);
                if (loc != null)
                {
                    loc.IsActive = false;
                    loc.IsDelete = true;
                    loc.ModifiedBy = UserId;
                    loc.ModifiedOn = DateTime.Now;
                    await context.SaveChangesAsync();
                }
                return false;
            }
            return false;
        }
        public async Task<List<LocationList>> GetLocations()
        {
            if (IsActiveProfile)
            {
                return await context.LocationMetadatas.Where(x => x.CustomerID == CompanyId && x.IsActive == true && x.Key == "LocationName")
                    .Select(y => new LocationList
                    {
                        LocationName = y.Value,
                        LocationCode = y.Location.LocCode
                    }).ToListAsync();
            }
            return null;
        }
        #endregion
    }
}