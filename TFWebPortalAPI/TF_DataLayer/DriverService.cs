using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_InterfaceLayer;

namespace TransForce.API.TF_DataLayer
{
    public class DriverService : IDriverService
    {
        private readonly TFPMEntities context;
        private readonly bool IsActiveProfile;
        private readonly int CompanyId;
        public DriverService()
        {
            context = new TFPMEntities();
            IsActiveProfile = true;
        }
        public DriverService(int CompanyID)
        {
            context = new Entitys.TFPMEntities();
            IsActiveProfile = context.CustomerProfiles.Any(x => x.CompanyID == CompanyID && x.IsDelete == false);
            CompanyId = CompanyID;
        }
        private bool CheckDotExists(string DotNumber, int companyId)
        {
            if (!string.IsNullOrEmpty(DotNumber))
            {
                return context.CustomerDOTNumbers.Any(x => x.DOTNumber == DotNumber && x.CustomerID == companyId && x.IsDelete == false);
            }
            return false;
        }
        private bool CheckDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime dt;
                var formatStrings = new string[] { "M/d/yyyy", "M/d/yy" };
                if (DateTime.TryParseExact(date, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    var todaysDate = DateTime.Today;
                    if (dt < todaysDate)
                        return true;
                    return false;
                }
            }
            return false;
        }
        public async Task<UploadDrivers> UploadDrivers(UploadDrivers model, string Userid)
        {
            if (IsActiveProfile)
            {
                //FirstName Empty
                Regex rgx = new Regex("^[a-zA-Z0-9'-]+$");
                var invalidFirstName = model.DriverData.Where(x => string.IsNullOrEmpty(x.FirstName) || !rgx.IsMatch(x.FirstName))
                     .Select(y => new DriversModel
                     {
                         DOB = y.DOB,
                         DotNumber = y.DotNumber,
                         FirstName = y.FirstName,
                         LastName = y.LastName,
                         isActive = y.isActive,
                         LicenseNumber = y.LicenseNumber,
                         LicenseState = y.LicenseState,
                         Remarks = "FirstName is empty Or invalid format"
                     }).ToList();
                model.DriverData.RemoveAll(x => invalidFirstName.Any(y => y.FirstName == x.FirstName));
                //LastName Empty
                var invalidLastName = model.DriverData.Where(x => string.IsNullOrEmpty(x.LastName) || !rgx.IsMatch(x.LastName))
                   .Select(y => new DriversModel
                   {
                       DOB = y.DOB,
                       DotNumber = y.DotNumber,
                       FirstName = y.FirstName,
                       LastName = y.LastName,
                       isActive = y.isActive,
                       LicenseNumber = y.LicenseNumber,
                       LicenseState = y.LicenseState,
                       Remarks = "LastName is empty Or invalid format"
                   }).ToList();
                model.DriverData.RemoveAll(x => invalidLastName.Any(y => y.LastName == x.LastName));

                //LicenceState Empty               
                var invalidLicenseState = model.DriverData.Where(x => string.IsNullOrEmpty(x.LicenseState) && x.LicenseState.Length == 2)
                  .Select(y => new DriversModel
                  {
                      DOB = y.DOB,
                      DotNumber = y.DotNumber,
                      FirstName = y.FirstName,
                      LastName = y.LastName,
                      isActive = y.isActive,
                      LicenseNumber = y.LicenseNumber,
                      LicenseState = y.LicenseState,
                      Remarks = "License state is empty Or enter state shortname(two digit)"
                  }).ToList();
                model.DriverData.RemoveAll(x => invalidLicenseState.Any(y => y.LicenseState == x.LicenseState));

                //Licence No Empty
                var invalidLicenseNumber = model.DriverData.Where(x => string.IsNullOrEmpty(x.LicenseNumber))
                .Select(y => new DriversModel
                {
                    DOB = y.DOB,
                    DotNumber = y.DotNumber,
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    isActive = y.isActive,
                    LicenseNumber = y.LicenseNumber,
                    LicenseState = y.LicenseState,
                    Remarks = "License number is empty"
                }).ToList();
                model.DriverData.RemoveAll(x => invalidLicenseNumber.Any(y => y.LicenseNumber == x.LicenseNumber));

                //isActive No Empty
                var invalidIsActive = model.DriverData.Where(x => string.IsNullOrEmpty(x.isActive))
               .Select(y => new DriversModel
               {
                   DOB = y.DOB,
                   DotNumber = y.DotNumber,
                   FirstName = y.FirstName,
                   LastName = y.LastName,
                   isActive = y.isActive,
                   LicenseNumber = y.LicenseNumber,
                   LicenseState = y.LicenseState,
                   Remarks = "IsActive is empty"
               }).ToList();
                model.DriverData.RemoveAll(x => invalidIsActive.Any(y => y.isActive == x.isActive));

                //DOB Exists
                var invalidDOB = model.DriverData.Where(x => !CheckDate(x.DOB))
                  .Select(y => new DriversModel
                  {
                      DOB = y.DOB,
                      DotNumber = y.DotNumber,
                      FirstName = y.FirstName,
                      LastName = y.LastName,
                      isActive = y.isActive,
                      LicenseNumber = y.LicenseNumber,
                      LicenseState = y.LicenseState,
                      Remarks = "date of birth is invalid format"
                  }).ToList();
                model.DriverData.RemoveAll(x => invalidDOB.Any(y => y.DOB == x.DOB));

                //Invalid DOT Number
                var invalidDOTnumber = model.DriverData.Where(x => !CheckDotExists(x.DotNumber, model.CompanyID))
                 .Select(y => new DriversModel
                 {
                     DOB = y.DOB,
                     DotNumber = y.DotNumber,
                     FirstName = y.FirstName,
                     LastName = y.LastName,
                     isActive = y.isActive,
                     LicenseNumber = y.LicenseNumber,
                     LicenseState = y.LicenseState,
                     Remarks = "DOTnumber not exist"
                 }).ToList();
                model.DriverData.RemoveAll(x => invalidDOTnumber.Any(y => y.DotNumber == x.DotNumber));


                await InsertDrivers(model, Userid);

                var InvalidModel = invalidFirstName; InvalidModel.AddRange(invalidLastName);
                InvalidModel.AddRange(invalidIsActive); InvalidModel.AddRange(invalidLicenseState);
                InvalidModel.AddRange(invalidLicenseNumber); InvalidModel.AddRange(invalidDOB);
                InvalidModel.AddRange(invalidDOTnumber);
                var InvalidResult = new UploadDrivers();
                InvalidResult.DriverData = new List<DriversModel>();
                InvalidResult.CompanyID = model.CompanyID;
                InvalidResult.DriverData = InvalidModel;
                return InvalidResult;
            }
            else
            {
                return null;
            }
        }
        private async Task InsertDrivers(UploadDrivers model, string Userid)
        {
            var companyId = model.CompanyID;
            foreach (var driver in model.DriverData)
            {
                var Lcount = context.Drivers.Where(m => m.LicenseNumber == driver.LicenseNumber && m.CompanyId == model.CompanyID && m.isDelete == false).FirstOrDefault();
                if (Lcount == null)
                {
                    var drivermodel = new Driver
                    {
                        CompanyId = companyId,
                        FirstName = driver.FirstName,
                        LastName = driver.LastName,
                        DOB = Convert.ToDateTime(driver.DOB).Date,
                        LicenseState = driver.LicenseState,
                        LicenseNumber = driver.LicenseNumber,
                        DOTNumber = driver.DotNumber,
                        CreatedBy = Userid,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = Userid,
                        ModifiedOn = DateTime.Now,
                        isActive = (driver.isActive.ToLower() == "yes" || driver.isActive.ToLower() == "y") ? true : false,
                        isDelete = false
                    };
                    context.Drivers.Add(drivermodel);
                }
                else
                {
                    Lcount.FirstName = driver.FirstName;
                    Lcount.LastName = driver.LastName;
                    Lcount.DOB = Convert.ToDateTime(driver.DOB).Date;
                    Lcount.LicenseState = driver.LicenseState;
                    Lcount.LicenseNumber = driver.LicenseNumber;
                    Lcount.DOTNumber = driver.DotNumber;
                    Lcount.ModifiedBy = Userid;
                    Lcount.ModifiedOn = DateTime.Now;
                    Lcount.isActive = (driver.isActive.ToLower() == "yes" || driver.isActive.ToLower() == "y") ? true : false;
                }
                await context.SaveChangesAsync();
            }
        }
        public async Task<bool> InsertDriver(Driver model, int CompanyId, string UserId)
        {
            if (IsActiveProfile && CheckDotExists(model.DOTNumber, CompanyId))
            {
                if (!context.Drivers.Any(a => a.LicenseNumber == model.LicenseNumber && a.CompanyId == model.CompanyId && a.isDelete == false))
                {
                    var driverinfo = new Driver
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DOB = model.DOB,
                        LicenseNumber = model.LicenseNumber,
                        LicenseState = model.LicenseState,
                        CompanyId = CompanyId,
                        CreatedBy = UserId,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = UserId,
                        ModifiedOn = DateTime.Now,
                        isActive = model.isActive,
                        DOTNumber = model.DOTNumber,
                        isDelete = false
                    };
                    context.Drivers.Add(driverinfo);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public Driver EditDriver(int DriverId, int CompanyId)
        {
            if (IsActiveProfile)
            {
                return context.Drivers.FirstOrDefault(x => x.DriverID == DriverId && x.CompanyId == CompanyId && x.isDelete == false);
            }
            return null;
        }

        public async Task<Driver> UpdateDriver(Driver model, int CompanyId, string UserId)
        {
            if (IsActiveProfile)
            {
                var driver = context.Drivers.FirstOrDefault(x => x.DriverID == model.DriverID && x.CompanyId == CompanyId && x.isDelete == false);
                if (driver != null && CheckDotExists(model.DOTNumber, model.CompanyId))
                {
                    if (driver.LicenseNumber == model.LicenseNumber)
                    {
                        driver.DOB = model.DOB;
                        driver.FirstName = model.FirstName;
                        driver.LastName = model.LastName;
                        driver.LicenseState = model.LicenseState;
                        driver.DOTNumber = model.DOTNumber;
                        driver.isActive = model.isActive;
                        driver.ModifiedBy = UserId;
                        driver.ModifiedOn = DateTime.Now;
                    }
                    else
                    {
                        if (!context.Drivers.Where(o => o.LicenseNumber == model.LicenseNumber && o.CompanyId == CompanyId && o.isDelete == false).Any())
                        {
                            driver.DOB = model.DOB;
                            driver.FirstName = model.FirstName;
                            driver.LastName = model.LastName;
                            driver.LicenseNumber = model.LicenseNumber;
                            driver.LicenseState = model.LicenseState;
                            driver.DOTNumber = model.DOTNumber;
                            driver.isActive = model.isActive;
                            driver.ModifiedBy = UserId;
                            driver.ModifiedOn = DateTime.Now;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    await context.SaveChangesAsync();
                    return driver;
                }
                return null;
            }
            return null;
        }

        public async Task<bool> DeleteDriver(int Driverid, int CompanyId, string UserId)
        {
            if (IsActiveProfile)
            {
                var driver = context.Drivers.FirstOrDefault(x => x.DriverID == Driverid && x.CompanyId == CompanyId && x.isDelete == false);
                if (driver != null)
                {
                    driver.isActive = false;
                    driver.isDelete = true;
                    driver.ModifiedBy = UserId;
                    driver.ModifiedOn = DateTime.Now;
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<List<Driver>> GetDrivers()
        {
            if (IsActiveProfile)
            {
                return await context.Drivers.Where(x => x.CompanyId == CompanyId && x.isDelete == false).ToListAsync();
            }
            return null;
        }
        public async Task<List<Driver>> GetActiveDrivers()
        {
            if (IsActiveProfile)
            {
                return await context.Drivers.Where(x => x.CompanyId == CompanyId && x.isActive == true && x.isDelete == false).ToListAsync();
            }
            return null;
        }

        public async Task<List<Driver>> GetDriversByName(string Name)
        {
            if (IsActiveProfile)
            {
                return await context.Drivers.Where(x => (x.FirstName.StartsWith(Name) || x.LastName.StartsWith(Name)) && x.CompanyId == CompanyId && x.isDelete == false).ToListAsync();
            }
            return null;
        }
    }
}