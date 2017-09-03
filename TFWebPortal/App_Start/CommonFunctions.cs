using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TransForce.Web.Models;
using TransForce.Web.Portal.Models;

namespace TransForce.Web.Portal.App_Start
{
    public static class CommonFunctions
    {
        public static ValidationFile ReadExecuteCsvFile(string strFileName)
        {
            ValidationFile validations = new Models.ValidationFile();
            var ValidList = new List<List<LocationData>>();
            using (CsvReader reader = new CsvReader(strFileName, Encoding.Default))
            {
                DataTable csvTable = reader.ReadIntoDataTable();
                var rangePartitioner = Partitioner.Create(0, csvTable.Rows.Count, 1000);
                Parallel.ForEach(rangePartitioner, (range, loopState) =>
                {
                    var Valid = new List<LocationData>();
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        var classObj = new LocationData()
                        {
                            LocationName = Convert.ToString(csvTable.Rows[i]["LocationName"]),
                            LocCode = Convert.ToString(csvTable.Rows[i]["LocCode"]),
                            FirstContactName = Convert.ToString(csvTable.Rows[i]["FirstContactName"]),
                            FirstContactEmail = Convert.ToString(csvTable.Rows[i]["FirstContactEmail"]),
                            isActive = string.IsNullOrEmpty(Convert.ToString(csvTable.Rows[i]["isActive"])) ? "N" : (Convert.ToString(csvTable.Rows[i]["isActive"]).ToLower() == "y" || Convert.ToString(csvTable.Rows[i]["isActive"]).ToLower() == "yes") ? "Y" : "N",
                            SecondContactName = Convert.ToString(csvTable.Rows[i]["SecondContactName"]).Trim(','),
                            SecondContactEmail = Convert.ToString(csvTable.Rows[i]["SecondContactEmail"]).Trim(','),
                            ThirdContactName = Convert.ToString(csvTable.Rows[i]["ThirdContactName"]).Trim(','),
                            ThirdContactEmail = Convert.ToString(csvTable.Rows[i]["ThirdContactEmail"]).Trim(',')
                        };
                        Valid.Add(classObj);
                    }
                    ValidList.Add(Valid);
                });
            }
            validations.ChunkvalidData = ValidList;
            return validations;
        }
        /// <summary>
        /// Read drivers Csv File
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public static UploadDrivers ReadExecuteDriverCsvFile(string strFileName)
        {
            UploadDrivers validations = new UploadDrivers();
            var ValidList = new List<List<DriversModel>>();
            using (CsvReader reader = new CsvReader(strFileName, Encoding.Default))
            {
                DataTable csvTable = reader.ReadIntoDataTable();
                var rangePartitioner = Partitioner.Create(0, csvTable.Rows.Count, 1000);
               
                Parallel.ForEach(rangePartitioner, (range, loopState) =>
                {
                    var Valid = new List<DriversModel>();
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        var classObj = new DriversModel()
                        {
                            DotNumber = Convert.ToString(csvTable.Rows[i]["DotNumber"]),
                            FirstName = Convert.ToString(csvTable.Rows[i]["FirstName"]),
                            LastName = Convert.ToString(csvTable.Rows[i]["LastName"]),
                            LicenseState = Convert.ToString(csvTable.Rows[i]["LicenseState"]),
                            isActive = string.IsNullOrEmpty(Convert.ToString(csvTable.Rows[i]["isActive"])) ? "N" : (Convert.ToString(csvTable.Rows[i]["isActive"]).ToLower() == "y" || Convert.ToString(csvTable.Rows[i]["isActive"]).ToLower() == "yes") ? "Y" : "N",
                            LicenseNumber = Convert.ToString(csvTable.Rows[i]["LicenseNumber"]),
                            DOB = csvTable.Rows[i]["DOB"].ToString().Replace(" 12:00:00 AM", "")
                        };
                        Valid.Add(classObj);
                    }
                    ValidList.Add(Valid);
                });               
            }
            validations.ChunkDriverdata = ValidList;
            return validations;
        }

        /// <summary>
        /// Tab delemited txt file reader for Drivers
        /// </summary>
        /// <param name="docPath"></param>
        /// <returns></returns>
        public static UploadDrivers ReadExecuteDelimitedDriverFile(string docPath)
        {
            UploadDrivers validations = new UploadDrivers();
            var ValidList = new List<DriversModel>();
            bool isHeader = true;
            using (var file = new StreamReader(docPath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (isHeader == false)
                    {
                        var delimiters = new char[] { '\t' };
                        var segments = line.Split(delimiters, StringSplitOptions.None);
                        DriversModel driverData = new DriversModel();
                        driverData.DotNumber = segments[0];
                        driverData.FirstName = segments[1];
                        driverData.LastName = segments[2];
                        driverData.LicenseState = segments[3];
                        driverData.isActive = string.IsNullOrEmpty(segments[4]) ? "N" : Convert.ToString(segments[4]).ToLower().Contains('y') ? "Y" : "N";
                        driverData.LicenseNumber = string.IsNullOrEmpty(segments[5]) ? string.Empty : segments[5];
                        driverData.DOB = string.IsNullOrEmpty(segments[6]) ? string.Empty : segments[6];
                        ValidList.Add(driverData);
                    }
                    else
                    {
                        isHeader = false;
                    }
                }
                file.Close();
            }
            var ValidLists = new List<List<DriversModel>>();
            var rangePartitioner = Partitioner.Create(0, ValidList.Count, 1000);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                var Valid = new List<DriversModel>();
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    var classObj = new DriversModel()
                    {
                        DotNumber = Convert.ToString(ValidList[i].DotNumber),
                        FirstName = Convert.ToString(ValidList[i].FirstName),
                        LastName = Convert.ToString(ValidList[i].LastName),
                        LicenseState = Convert.ToString(ValidList[i].LicenseState),
                        isActive = string.IsNullOrEmpty(Convert.ToString(ValidList[i].isActive)) ? "N" : (Convert.ToString(ValidList[i].isActive).ToLower() == "y" || Convert.ToString(ValidList[i].isActive).ToLower() == "yes") ? "Y" : "N",
                        LicenseNumber = Convert.ToString(ValidList[i].LicenseNumber).Trim(','),
                        DOB = Convert.ToString(ValidList[i].DOB).Trim(',')
                    };
                    Valid.Add(classObj);
                }
                ValidLists.Add(Valid);
            });
            validations.ChunkDriverdata = ValidLists;
            return validations;

        }
        /// <summary>
        /// Tab delemited txt file reader
        /// </summary>
        /// <param name="docPath"></param>
        /// <returns></returns>
        public static ValidationFile ReadExecuteDelimitedFile(string docPath)
        {
            ValidationFile validations = new Models.ValidationFile();
            var ValidList = new List<LocationData>();
            bool isHeader = true;
            using (var file = new StreamReader(docPath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (isHeader == false)
                    {
                        var delimiters = new char[] { '\t' };
                        var segments = line.Split(delimiters, StringSplitOptions.None);
                        LocationData lData = new Models.LocationData();
                        lData.LocationName = segments[0];
                        lData.LocCode = segments[1];
                        lData.FirstContactName = segments[2];
                        lData.FirstContactEmail = segments[3];
                        lData.isActive = string.IsNullOrEmpty(segments[4]) ? "N" : Convert.ToString(segments[4]).ToLower().Contains('y') ? "Y" : "N";
                        lData.SecondContactName = string.IsNullOrEmpty(segments[5]) ? string.Empty : segments[5];
                        lData.SecondContactEmail = string.IsNullOrEmpty(segments[6]) ? string.Empty : segments[6];
                        lData.ThirdContactName = string.IsNullOrEmpty(segments[7]) ? string.Empty : segments[7];
                        lData.ThirdContactEmail = string.IsNullOrEmpty(segments[8]) ? string.Empty : segments[8];
                        ValidList.Add(lData);
                    }
                    else
                    {
                        isHeader = false;
                    }
                }
                file.Close();
            }
            var ValidLists = new List<List<LocationData>>();
            var rangePartitioner = Partitioner.Create(0, ValidList.Count, 1000);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                var Valid = new List<LocationData>();
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    var classObj = new LocationData()
                    {
                        LocationName = Convert.ToString(ValidList[i].LocationName),
                        LocCode = Convert.ToString(ValidList[i].LocCode),
                        FirstContactName = Convert.ToString(ValidList[i].FirstContactName),
                        FirstContactEmail = Convert.ToString(ValidList[i].FirstContactEmail),
                        isActive = string.IsNullOrEmpty(Convert.ToString(ValidList[i].isActive)) ? "N" : (Convert.ToString(ValidList[i].isActive).ToLower() == "y" || Convert.ToString(ValidList[i].isActive).ToLower() == "yes") ? "Y" : "N",
                        SecondContactName = Convert.ToString(ValidList[i].SecondContactName).Trim(','),
                        SecondContactEmail = Convert.ToString(ValidList[i].SecondContactEmail).Trim(','),
                        ThirdContactName = Convert.ToString(ValidList[i].ThirdContactName).Trim(','),
                        ThirdContactEmail = Convert.ToString(ValidList[i].ThirdContactEmail).Trim(',')
                    };
                    Valid.Add(classObj);
                }
                ValidLists.Add(Valid);
            });
            validations.ChunkvalidData = ValidLists;
            return validations;
        }
        public static RPT_REG_CARRIER_PROFILE_XML ReadDOTXmlfile(string strFileName)
        {
            return DeserializeXMLFileToObject<RPT_REG_CARRIER_PROFILE_XML>(strFileName);
        }
        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            StreamReader xmlStream = null;
            try
            {
                xmlStream = new StreamReader(XmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                return returnObject;
            }
            finally
            {
                xmlStream.Dispose();
            }
            return returnObject;
        }
    }
}