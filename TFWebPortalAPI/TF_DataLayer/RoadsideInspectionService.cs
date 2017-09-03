using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TransForce.API.App_Start;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_InterfaceLayer;

namespace TransForce.API.TF_DataLayer
{
    public class RoadsideInspectionService : IRoadsideInspectionService
    {
        private readonly TFPMEntities context;
        private readonly bool IsActiveProfile;
        private readonly int CompanyId;
        public RoadsideInspectionService()
        {
            context = new TFPMEntities();
            IsActiveProfile = true;
        }
        public RoadsideInspectionService(int CompanyID)
        {
            context = new Entitys.TFPMEntities();
            IsActiveProfile = context.CustomerProfiles.Any(x => x.CompanyID == CompanyID && x.IsDelete == false);
            CompanyId = CompanyID;
        }
        #region BuikUpload
        private bool CheckDotExists(string DotNumber, int CompanyID)
        {
            if (!string.IsNullOrEmpty(DotNumber))
            {
                return context.CustomerDOTNumbers.Any(x => x.DOTNumber == DotNumber && x.CustomerID == CompanyID && x.IsDelete == false);
            }
            return false;
        }
        public async Task<string> UploadRoadsideInspection(Inspection model, string UserId)
        {
            if (IsActiveProfile)
            {
                var xmlObject = model.xml;
                string DOTNumber = xmlObject.G_DOT_NUMBER.DOT_NUMBER.ToString();
                string Trans_Id = xmlObject.G_DOT_NUMBER.TRANS_ID.ToString();
                if (!context.G_RPT_REG_CP_INSP_ID.Any(x => x.TransID == Trans_Id && x.DOTNumber == DOTNumber && x.CompanyId == model.CompanyId))
                {
                    if (CheckDotExists(DOTNumber, model.CompanyId) && !string.IsNullOrEmpty(Trans_Id))
                    {
                        if (!context.G_RPT_REG_CP_INSP_ID.Any(x => x.DOTNumber == DOTNumber && x.TransID == Trans_Id && x.CompanyId == model.CompanyId))
                            await Insert_G_RPT_REG_CP_INSP_ID(xmlObject.G_RPT_REG_CP_INSP.G_RPT_REG_CP_INSP_ID, UserId, DOTNumber, Trans_Id, model.CompanyId);
                        if (!context.G_RPT_REG_CP_CRASH_ID.Any(x => x.DOTNumber == DOTNumber && x.TransId == Trans_Id && x.CompanyId == model.CompanyId))
                            await InsertCP_CRASH(xmlObject.G_RPT_REG_CP_CRASH.G_RPT_REG_CP_CRASH_ID, UserId, model.CompanyId, DOTNumber, Trans_Id);
                        if (!context.G_VIOL_CAT_TYPE_NAME.Any(x => x.DOTNumber == DOTNumber && x.TransId == Trans_Id && x.CompanyId == model.CompanyId))
                            await InsertCateTypeName(xmlObject.G_VIOL_CAT_TYPE_DESC, DOTNumber, Trans_Id, UserId, model.CompanyId);
                        return "Uploaded Successfully";
                    }
                    return "DOTNumber/Transaction id empty Or DOTNumber not available";
                }
                return "Transaction details already exists.";
            }
            return "Company not active";
        }
        private async Task AddInspection(RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSPG_RPT_REG_CP_INSP_ID inspection, string UserId, string DOTNumber, string Trans_ID, int CompanyId)
        {
            var insp = new Entitys.G_RPT_REG_CP_INSP_ID
            {
                CompanyId = CompanyId,
                DOTNumber = DOTNumber,
                TransID = Trans_ID,
                CreatedBy = UserId,
                ModifiedBy = UserId,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                CARRIER_CITY1 = inspection.CARRIER_CITY1,
                CARRIER_NAME1 = inspection.CARRIER_NAME1,
                CARRIER_STATE1 = inspection.CARRIER_STATE1,
                COMPANY_NUMBER1 = inspection.COMPANY_NUMBER1,
                COMPANY_NUMBER2 = inspection.COMPANY_NUMBER2,
                COMPANY_NUMBER3 = Convert.ToString(inspection.COMPANY_NUMBER3),
                COMPANY_NUMBER4 = Convert.ToString(inspection.COMPANY_NUMBER4),
                COMPANY_NUMBER5 = Convert.ToString(inspection.COMPANY_NUMBER5),
                COMPANY_NUMBER6 = Convert.ToString(inspection.COMPANY_NUMBER6),
                COUNTY_NAME1 = inspection.COUNTY_NAME1,
                DRIVER_DOB = Convert.ToDateTime(inspection.DRIVER_DOB),
                VEH_ST_LICENSE1 = inspection.VEH_ST_LICENSE1,
                VEH_ST_LICENSE2 = inspection.VEH_ST_LICENSE2,
                VEN_ST_LICENSE3 = Convert.ToString(inspection.VEH_ST_LICENSE3),
                VEN_ST_LICENSE4 = Convert.ToString(inspection.VEH_ST_LICENSE4),
                VEH_ST_LICENSE5 = Convert.ToString(inspection.VEH_ST_LICENSE5),
                VEH_ST_LICENSE6 = Convert.ToString(inspection.VEH_ST_LICENSE6),
                DRIVER_FIRST_NAME1 = inspection.DRIVER_FIRST_NAME1,
                DRIVER_LAST_NAME1 = inspection.DRIVER_LAST_NAME,
                DRIVER_LICENSE = inspection.DRIVER_LICENSE,
                DRIVER_LICENSE_STATE = inspection.DRIVER_LICENSE_STATE,
                DRIVER_MI1 = inspection.DRIVER_MI1,
                UNIT_VIN1 = inspection.UNIT_VIN1,
                UNIT_VIN2 = inspection.UNIT_VIN2,
                UNIT_VIN3 = Convert.ToString(inspection.UNIT_VIN3),
                UNIT_VIN4 = Convert.ToString(inspection.UNIT_VIN4),
                UNIT_VIN5 = Convert.ToString(inspection.UNIT_VIN5),
                UNIT_VIN6 = Convert.ToString(inspection.UNIT_VIN6),
                UNIT_NUMBER1 = Convert.ToString(inspection.UNIT_NUMBER1),
                UNIT_NUMBER2 = Convert.ToString(inspection.UNIT_NUMBER2),
                UNIT_NUMBER3 = Convert.ToString(inspection.UNIT_NUMBER3),
                UNIT_NUMBER4 = Convert.ToString(inspection.UNIT_NUMBER4),
                UNIT_NUMBER5 = Convert.ToString(inspection.UNIT_NUMBER5),
                UNIT_NUMBER6 = Convert.ToString(inspection.UNIT_NUMBER6),
                UNIT_TYPE1 = inspection.UNIT_TYPE1,
                UNIT_TYPE2 = inspection.UNIT_TYPE2,
                UNIT_TYPE3 = Convert.ToString(inspection.UNIT_TYPE3),
                UNIT_TYPE4 = Convert.ToString(inspection.UNIT_TYPE4),
                UNIT_TYPE5 = Convert.ToString(inspection.UNIT_TYPE5),
                UNIT_TYPE6 = Convert.ToString(inspection.UNIT_TYPE6),
                TOTAL_OOS = inspection.TOTAL_OOS,
                TOTAL_VIOL = inspection.TOTAL_VIOL,
                INSPECTION_ID = Convert.ToInt32(inspection.INSPECTION_ID),
                INSP_DATE = Convert.ToDateTime(inspection.INSP_DATE),
                INSP_LEVEL_CODE = Convert.ToString(inspection.INSP_LEVEL_CODE),
                INSP_TIME = inspection.INSP_TIME,
                LOCATION1 = inspection.LOCATION1,
                REPORT_NUMBER1 = inspection.REPORT_NUMBER1,
                REPORT_STATE = inspection.REPORT_STATE,
                HM = inspection.HM
            };
            context.G_RPT_REG_CP_INSP_ID.Add(insp);
            await context.SaveChangesAsync();

        }
        private async Task Insert_G_RPT_REG_CP_INSP_ID(RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSPG_RPT_REG_CP_INSP_ID[] inspections, string UserId, string DOTNumber, string Trans_ID, int CompanyId)
        {
            foreach (var inspection in inspections.Where(x => !string.IsNullOrEmpty(Convert.ToString(x.INSPECTION_ID)) && !string.IsNullOrEmpty(x.DRIVER_LICENSE)))
            {
                await AddInspection(inspection, UserId, DOTNumber, Trans_ID, CompanyId);
                List<G_RPT_REG_CP_INSP_VIOL_DETAIL> details = new List<G_RPT_REG_CP_INSP_VIOL_DETAIL>();
                foreach (var inspectionDetails in inspection.LIST_G_RPT_REG_CP_INSP_VIOL_DETAIL.Where(x => !string.IsNullOrEmpty(Convert.ToString(x.INSPECTION_ID1))))
                {
                    G_RPT_REG_CP_INSP_VIOL_DETAIL detail = new G_RPT_REG_CP_INSP_VIOL_DETAIL();
                    detail.CompanyId = CompanyId;
                    detail.CreatedBy = UserId;
                    detail.ModifiedBy = UserId;
                    detail.CreatedOn = DateTime.Now;
                    detail.ModifiedOn = DateTime.Now;
                    detail.DOTNumber = DOTNumber;
                    detail.TransID = Trans_ID;
                    detail.INSPECTION_ID = Convert.ToInt32(inspectionDetails.INSPECTION_ID1);
                    detail.POST_CRASH_FL = inspectionDetails.POST_CRASH_FLAG;
                    detail.VIOL_CAT = inspectionDetails.VIOL_CAT;
                    detail.VIOL_OOS = inspectionDetails.VIOL_OOS;
                    detail.VIOL_UNIT = inspectionDetails.VIOL_UNIT;
                    details.Add(detail);
                }
                if (details.Count > 0)
                {
                    context.G_RPT_REG_CP_INSP_VIOL_DETAIL.AddRange(details);
                    await context.SaveChangesAsync();
                }
            }
        }
        private async Task InsertCateTypeName(RPT_REG_CARRIER_PROFILE_XMLG_VIOL_CAT_TYPE_DESC[] TypeDescs, string DOTNumber, string Trans_ID, string UserId, int CompanyId)
        {
            foreach (var TypeDesc in TypeDescs)
            {
                foreach (var Types in TypeDesc.G_VIOL_CAT_TYPE_NAME)
                {
                    if (!context.G_VIOL_CAT_TYPE_NAME.Any(x => x.INSP_VIOLATION_CATEGORY_NAME1 == Types.INSP_VIOLATION_CATEGORY_NAME1 && x.DOTNumber == DOTNumber))
                    {
                        G_VIOL_CAT_TYPE_NAME typeName = new G_VIOL_CAT_TYPE_NAME();
                        typeName.DOTNumber = DOTNumber;
                        typeName.TransId = Trans_ID;
                        typeName.CreatedBy = UserId;
                        typeName.ModifiedBy = UserId;
                        typeName.CreatedOn = DateTime.Now;
                        typeName.ModifiedOn = DateTime.Now;
                        typeName.SEQ_ORDER1 = TypeDesc.SEQ_ORDER1;
                        typeName.INSP_VIOLATION_CATEGORY_DESC = Types.INSP_VIOLATION_CATEGORY_DESC;
                        typeName.INSP_VIOLATION_CATEGORY_NAME1 = Types.INSP_VIOLATION_CATEGORY_NAME1;
                        typeName.CompanyId = CompanyId;
                        context.G_VIOL_CAT_TYPE_NAME.Add(typeName);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
        private async Task InsertCP_CRASH(RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASHG_RPT_REG_CP_CRASH_ID[] CrashProfs, string UserId, int CompanyId, string DOTNumber, string Trans_Id)
        {

            var Crashs = CrashProfs.Where(x => !string.IsNullOrEmpty(x.DRIVER_LIC_NUMBER))
                .Select(x => new G_RPT_REG_CP_CRASH_ID
                {
                    CompanyId = CompanyId,
                    DOTNumber = DOTNumber,
                    TransId = Trans_Id,
                    CreatedBy = UserId,
                    ModifiedBy = UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    DRIVER_BDAY = Convert.ToDateTime(x.DRIVER_BDAY),
                    DRIVER_FIRST_NAME = x.DRIVER_FIRST_NAME,
                    DRIVER_LAST_NAME = x.DRIVER_LAST_NAME,
                    DRIVER_LIC_NUMBER = x.DRIVER_LIC_NUMBER,
                    DRIVER_LIC_STATE = x.DRIVER_LIC_STATE,
                    DRIVER_MI = x.DRIVER_MI,
                    CITY1 = x.CITY1,
                    CARRIER_CITY = x.CARRIER_CITY,
                    CARRIER_NAME = x.CARRIER_NAME,
                    CARRIER_STATE = x.CARRIER_STATE,
                    COUNTY = x.COUNTY,
                    LOCATION = x.LOCATION,
                    FATALITIES1 = x.FATALITIES1,
                    HAZMAT_FLAG = x.HAZMAT_FLAG,
                    REPORT_DATE = Convert.ToDateTime(x.REPORT_DATE),
                    REPORT_NUMBER = x.REPORT_NUMBER,
                    REPORT_TIME = x.REPORT_TIME,
                    RPT_REG_CP_CRASH_ID = Convert.ToInt32(x.RPT_REG_CP_CRASH_ID),
                    SEQ_OF_EVENT1 = x.SEQ_OF_EVENTS1,
                    SEQ_OF_EVENT2 = x.SEQ_OF_EVENTS2,
                    SEQ_OF_EVENT3 = x.SEQ_OF_EVENTS3,
                    SEQ_OF_EVENT4 = Convert.ToString(x.SEQ_OF_EVENTS4),
                    STATE1 = x.STATE1,
                    INJURIES = x.INJURIES1,
                    VEH_LIC_STATE = x.VEH_LIC_STATE,
                    VEH_LIC_NUMBER = x.VEH_LIC_NUMBER,
                    TOW_AWAY = x.TOW_AWAY
                }).ToList();
            context.G_RPT_REG_CP_CRASH_ID.AddRange(Crashs);
            await context.SaveChangesAsync();
        }
        #endregion


        #region Inspection Manual Entry

        public async Task<List<G_RPT_REG_CP_INSP_ID>> GetInspectionByDate(DateTime InspectionDate)
        {
            if (IsActiveProfile)
            {
                var date = InspectionDate.Date;
                return await context.G_RPT_REG_CP_INSP_ID.Where(x => DbFunctions.TruncateTime(x.INSP_DATE) == date && x.CompanyId == CompanyId).ToListAsync();
            }
            return null;
        }

        public ManualRSI InsertInspectionEntry(ManualRSI Rsi)
        {
            if (IsActiveProfile && CheckDotExists(Rsi.DOTNumber, Rsi.CompanyID))
            {
                var manualRsi = context.ManualRSIs.FirstOrDefault(x => x.CompanyID == Rsi.CompanyID && x.DOTNumber == Rsi.DOTNumber && x.InspectionDate == Rsi.InspectionDate && x.InspectionID == Rsi.InspectionID && x.DriverLicenseNumber == Rsi.DriverLicenseNumber);
                if (manualRsi == null)
                {
                    var MRsi = new ManualRSI
                    {
                        CompanyID = Rsi.CompanyID,
                        Comments = Rsi.Comments,
                        CompanyVehicleNumber = Rsi.CompanyVehicleNumber,
                        CreatedBy = Rsi.CreatedBy,
                        CreatedOn = Rsi.CreatedOn,
                        DisciplineActionIssued = Rsi.DisciplineActionIssued,
                        DOTNumber = Rsi.DOTNumber,
                        DriverFirstName = Rsi.DriverFirstName,
                        DriverLastName = Rsi.DriverLastName,
                        DriverLicenseNumber = Rsi.DriverLicenseNumber,
                        DriverLicenseState = Rsi.DriverLicenseState,
                        DriverLog = Rsi.DriverLog,
                        InspectionDate = Rsi.InspectionDate,
                        InspectionID = Rsi.InspectionID,
                        InspectionReceived = Rsi.InspectionReceived,
                        InspectionState = Rsi.InspectionState,
                        ModifiedBy = Rsi.ModifiedBy,
                        ModifiedOn = Rsi.ModifiedOn,
                        NoticeNumber = Rsi.NoticeNumber,
                        OOS = Rsi.OOS,
                        OtherViolation = Rsi.OtherViolation,
                        RepairWorkOrder = Rsi.RepairWorkOrder,
                        ReportNumber = Rsi.ReportNumber,
                        VehicleLicenseNumber = Rsi.VehicleLicenseNumber,
                        Violations = Rsi.Violations,
                        LocationCode = Rsi.LocationCode,
                        DriverDOB = Rsi.DriverDOB,
                        AdminNotes = Rsi.AdminNotes,
                        INSP_ID = Rsi.INSP_ID
                    };
                    context.ManualRSIs.Add(MRsi);
                    context.SaveChanges();
                    return MRsi;
                }
                else
                {
                    manualRsi.Comments = Rsi.Comments;
                    manualRsi.CompanyVehicleNumber = Rsi.CompanyVehicleNumber;
                    manualRsi.DisciplineActionIssued = Rsi.DisciplineActionIssued;
                    manualRsi.DriverFirstName = Rsi.DriverFirstName;
                    manualRsi.DriverLastName = Rsi.DriverLastName;
                    manualRsi.DriverLog = Rsi.DriverLog;
                    manualRsi.InspectionReceived = Rsi.InspectionReceived;
                    manualRsi.Violations = Rsi.Violations;
                    manualRsi.RepairWorkOrder = Rsi.RepairWorkOrder;
                    manualRsi.OtherViolation = Rsi.OtherViolation;
                    manualRsi.InspectionState = Rsi.InspectionState;
                    manualRsi.InspectionReceived = Rsi.InspectionReceived;
                    manualRsi.LocationCode = Rsi.LocationCode;
                    manualRsi.DriverDOB = Rsi.DriverDOB;
                    manualRsi.AdminNotes = Rsi.AdminNotes;
                    manualRsi.ModifiedBy = Rsi.ModifiedBy;
                    manualRsi.ModifiedOn = Rsi.ModifiedOn;
                    context.SaveChanges();
                }
                return manualRsi;
            }
            return null;
        }

        public async Task<ManualRSI> GetInspectionById(int Inspection_Id)
        {
            if (IsActiveProfile)
            {
                var inspection = await context.G_RPT_REG_CP_INSP_ID.Where(x => x.INSPECTION_ID == Inspection_Id && x.CompanyId == CompanyId).FirstOrDefaultAsync();
                if (inspection.ManualRSIs.Count > 0)
                {
                    return inspection.ManualRSIs.FirstOrDefault();
                }
                else
                {
                    return new ManualRSI
                    {
                        DOTNumber = inspection.DOTNumber,
                        InspectionID = Convert.ToInt32(inspection.INSPECTION_ID),
                        ReportNumber = inspection.REPORT_NUMBER1,
                        InspectionState = inspection.REPORT_STATE,
                        DriverDOB = inspection.DRIVER_DOB,
                        DriverFirstName = inspection.DRIVER_FIRST_NAME1,
                        DriverLastName = inspection.DRIVER_LAST_NAME1,
                        DriverLicenseNumber = inspection.DRIVER_LICENSE,
                        DriverLicenseState = inspection.DRIVER_LICENSE_STATE,
                        LocationCode = inspection.LOCATION1,
                        INSP_ID = inspection.ID
                    };
                }
                //var inspection = context.ManualRSIs.Where(x => x.InspectionID == Inspection_Id && x.CompanyID == CompanyId).FirstOrDefault();
                //if (inspection != null)
                //    return inspection;
                //else
                //    return await context.G_RPT_REG_CP_INSP_ID
                //        .Where(x => x.INSPECTION_ID == Inspection_Id && x.CompanyId == CompanyId)
                //        .Select(y => new ManualRSI
                //        {
                //            DOTNumber = y.DOTNumber,
                //            InspectionID = Convert.ToInt32(y.INSPECTION_ID),
                //            ReportNumber = y.REPORT_NUMBER1,
                //            InspectionState = y.REPORT_STATE,
                //            DriverDOB = y.DRIVER_DOB,
                //            DriverFirstName = y.DRIVER_FIRST_NAME1,
                //            DriverLastName = y.DRIVER_LAST_NAME1,
                //            DriverLicenseNumber = y.DRIVER_LICENSE,
                //            DriverLicenseState = y.DRIVER_LICENSE_STATE,
                //            LocationCode = y.LOCATION1,
                //            INSP_ID = y.ID
                //        }).FirstOrDefaultAsync();
            }
            return null;
        }

        public ManualRSI GetInspectionDetailsByDriverName(string DriverLicence, DateTime InspectinDate, int CompanyId)
        {
            if (IsActiveProfile)
            {
                return context.ManualRSIs.FirstOrDefault(x => x.CompanyID == CompanyId && x.DriverLicenseNumber == DriverLicence && x.InspectionDate == InspectinDate);
            }
            return null;
        }

        public async Task<string> UploadInspectionDocumnets(List<BlobUploadModel> documentData)
        {
            if (IsActiveProfile)
            {
                foreach (var document in documentData)
                {
                    string fileUrl = await AzureProvider.UploadFilestoblobAsync(document.FileData, document.FileName.Split('.')[1], document.FileName);
                    return fileUrl;
                }
            }
            return "";
        }

        public async Task<bool> DeleteDocument(BlobUploadModel documentData)
        {
            if (IsActiveProfile)
            {
                return await AzureProvider.DeleteBlobAsync(documentData.FileUrl);
            }
            return false;
        }
        #endregion
    }
}