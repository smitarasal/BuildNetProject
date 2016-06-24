using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QSI.Services.Spec;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services
{
   [QSI.Services.ServiceBehavior]
    public class EmailService : IEmailService
    {

       public EmailReponse EmailBookMark(string bookMarkData, string ToAddress, string FromAddress)
        {

            EmailReponse response = new EmailReponse();
            try
            {
              //  var bookmarkName = JObject.Parse(bookMarkData)["name"];
                var bookmarkName = "Book Mark";
                if (string.IsNullOrEmpty(FromAddress))
                FromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();
                var emailAddress = ConfigurationManager.AppSettings["EmailAddress"].ToString();
                var password = ConfigurationManager.AppSettings["Password"].ToString();
                MailMessage mail = new MailMessage();
                var SMTPServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
                SmtpClient SmtpServer = new SmtpClient(SMTPServer);
                string filename = bookmarkName.ToString()+ ".txt";
                mail.From = new MailAddress(FromAddress);
                mail.To.Add(ToAddress);
                mail.Subject = "BookMark";
                mail.Body = "Book Mark Attachment.";

                System.Net.Mail.Attachment attachment;
                Stream memoryStream = GenerateStreamFromString(bookMarkData);
                attachment = new System.Net.Mail.Attachment(memoryStream, filename);
                mail.Attachments.Add(attachment);
                SmtpServer.Port = Convert.ToInt16(ConfigurationManager.AppSettings["Port"]);
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailAddress, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                ErrorObject obj = new ErrorObject { Message = "Email Sent Successfully", Status = "Success" };
                response.EmailData = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                ErrorObject obj = new ErrorObject { Message = ex.Message, Status = "Failure" };
                response.EmailData = JsonConvert.SerializeObject(obj);
            }
            return response;
        }
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
