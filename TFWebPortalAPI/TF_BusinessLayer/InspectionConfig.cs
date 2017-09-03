using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_DataLayer;
using TransForce.API.TF_InterfaceLayer;

namespace TransForce.API.TF_BusinessLayer
{
    public class InspectionConfig
    {
        public async Task<string> UploadRoadsideInspection(Inspection model, string UserID)
        {
            IRoadsideInspectionService InspectionInfo = new RoadsideInspectionService(model.CompanyId);
            return await InspectionInfo.UploadRoadsideInspection(model, UserID);
        }
        public async Task<List<G_RPT_REG_CP_INSP_ID>> GetInspectionByDate(int CompanyId, DateTime InspectionDate)
        {
            IRoadsideInspectionService InspectionInfo = new RoadsideInspectionService(CompanyId);
            return await InspectionInfo.GetInspectionByDate(InspectionDate);
        }

        public async Task<ManualRSI> GetInspectionById(int CompanyId, int Inspection_Id)
        {
            IRoadsideInspectionService InspectionInfo = new RoadsideInspectionService(CompanyId);
            return await InspectionInfo.GetInspectionById(Inspection_Id);
        }
        public ManualRSI GetInspectionDetailsByDriverName(string DriverLicence, DateTime InspectinDate, int CompanyId)
        {
            IRoadsideInspectionService InspectionInfo = new RoadsideInspectionService(CompanyId);
            return InspectionInfo.GetInspectionDetailsByDriverName(DriverLicence, InspectinDate, CompanyId);
        }

        public ManualRSI InsertInspectionEntry(ManualRSI Rsi)
        {
            IRoadsideInspectionService InspectionInfo = new RoadsideInspectionService(Rsi.CompanyID);
            return InspectionInfo.InsertInspectionEntry(Rsi);
        }
        public async Task<string> UploadInspectionDocumnets(List<BlobUploadModel> documentData, int CompanyId)
        {
            IRoadsideInspectionService inspection = new RoadsideInspectionService(CompanyId);
            return await inspection.UploadInspectionDocumnets(documentData);
        }
        public async Task<bool> DeleteDocument(BlobUploadModel documentData)
        {
            IRoadsideInspectionService inspection = new RoadsideInspectionService(documentData.CompanyId);
            return await inspection.DeleteDocument(documentData);
        }
    }
}