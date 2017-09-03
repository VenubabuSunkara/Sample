using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Xml.Serialization;

namespace TransForce.API.App_Start
{
    public static class CommonFunctions
    {
        public static string Trimstring(string Value)
        {
            return string.IsNullOrEmpty(Value) ? Value : Value.Trim();
        }
        public static bool validateEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return false;
                }
                var address = new MailAddress(email);
                return true;
            }
            catch (FormatException ex)
            {
                return false;
            }
        }
        public static bool validateSEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return true;
                }
                var address = new MailAddress(email);
                return true;
            }
            catch (FormatException ex)
            {
                return false;
            }
        }
        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            try
            {
                StreamReader xmlStream = new StreamReader(XmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                return returnObject;
            }
            return returnObject;
        }
    }
}