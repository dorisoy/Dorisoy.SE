using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace DCMS.SE.Services
{
    public class MailService
    {
        DatabaseConnection _conn;
        ApplicationDbContext _context;
        public MailService(ApplicationDbContext context , DatabaseConnection conn)
        {
            _conn = conn;
            _context = context;
        }
        public void SendMail(Mailbox area)
        {
            using (MailMessage mm = new MailMessage(area.Email, area.ToEmail))
            {
                mm.Subject = area.Subject;
                mm.Body = area.Body;
                //if (fuAttachment.HasFile)
                //{
                //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                //}
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(area.Email, area.Password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
            //using (MailMessage mm = new MailMessage(area.Email, area.ToEmail))
            //    {
            //        mm.Subject = area.Subject;
            //        mm.Body = area.Body;
            //        string fileName = string.Empty;
            //        //if (area.attachment.Length > 0)
            //        //{
            //           // fileName = Path.GetFileName(area.attachment.FileName);
            //           // mm.Attachments.Add(new Attachment(area.attachment.OpenReadStream(), fileName));
            //        //}
            //        mm.IsBodyHtml = false;
            //        using (SmtpClient smtp = new SmtpClient())
            //        {
            //            smtp.Host = "smtp.gmail.com";
            //            smtp.EnableSsl = true;
            //            NetworkCredential NetworkCred = new NetworkCredential(area.Email, area.Password);
            //        smtp.EnableSsl = true;
            //        smtp.UseDefaultCredentials = true;
            //            smtp.Credentials = NetworkCred;
            //            smtp.Port = 587;
            //            smtp.Send(mm);
            //            area.FileName = fileName;
            //            area.CompanyId = 1;
            //            area.AddedDate = DateTime.Now;
            //            _context.Mailbox.Add(area);
            //            _context.SaveChanges();
            //        }
            //    }
        }
        public MailboxView GetSmsPurchasemaster(long PurchaseMasterId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@PurchaseMasterId", PurchaseMasterId);
                var result = sqlcon.Query<MailboxView>("PurchaseSendGmail", para, null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
        }
    }
}
