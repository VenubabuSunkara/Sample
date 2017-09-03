using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransForce.API.Entitys;
using TransForce.API.Models;

namespace TransForce.API.TF_InterfaceLayer
{
    interface IRoadsideInspectionService
    {
        Task<string> UploadRoadsideInspection(Inspection model, string UserId);
        Task<List<G_RPT_REG_CP_INSP_ID>> GetInspectionByDate(DateTime InspectionDate);
        ManualRSI InsertInspectionEntry(ManualRSI Rsi);
        ManualRSI GetInspectionDetailsByDriverName(string DriverName,DateTime InspectinDate,int CompanyId);
        Task<ManualRSI> GetInspectionById(int Inspection_Id);

        Task<string> UploadInspectionDocumnets(List<BlobUploadModel> documentData);
        Task<bool> DeleteDocument(BlobUploadModel documentData);
    }
}
