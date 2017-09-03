using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using TransForce.API.Models;
using System.Web.Security;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;

namespace TransForce.API
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSMTPasync(message);
            // Plug in your emailservice service here to send an email.
            //  return Task.FromResult(0);
        }
        private async Task configSMTPasync(IdentityMessage message)
        {
            // Plug in your email  here to send an email.
            //var credentialUserName = ConfigurationManager.AppSettings["credentialUserName"].ToString();
            var sentFrom = ConfigurationManager.AppSettings["sentFrom"].ToString();
            //var pwd = ConfigurationManager.AppSettings["pwd"].ToString();
            //var host = ConfigurationManager.AppSettings["host"].ToString();
            //var port = ConfigurationManager.AppSettings["port"].ToString();
            // Configure the client:
            // System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(host);//("mail.ourdomain.com");

            //client.Port = Convert.ToInt32(port);
            //client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteCertificateValidationCallback);
            //// Creatte the credentials:
            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(credentialUserName, pwd);
            //client.EnableSsl = true;
            //client.Credentials = credentials;
            //Override callback


            //// Create the message:
            //var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);
            //mail.Subject = message.Subject;
            //mail.Body = message.Body;

            //await client.SendMailAsync(mail);

            String APIKey = "cdf78c4606e923f9836fc48d59e88c5a";
            String SecretKey = "ffe759f5079678bc8d341c4a56e1bcce";
            String From = sentFrom;
            String To = message.Destination;

            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(From);

            msg.To.Add(new MailAddress(To));

            msg.Subject = message.Subject;
            msg.Body = message.Body;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("in.mailjet.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(APIKey, SecretKey);

            await client.SendMailAsync(msg);
        }
        static public bool RemoteCertificateValidationCallback(Object sender,
                              X509Certificate certificate,
                              X509Chain chain,
                              SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(RoleStore<IdentityRole> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }
}
