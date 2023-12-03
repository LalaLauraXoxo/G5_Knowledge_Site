using ASI.Basecode.Services.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    public async Task SendResetPasswordEmail(string email, string resetLink)
    {
        var smtpServer = "smtp.gmail.com";
        var smtpPort = 587;
        var smtpUsername = "knowbodysmtp@gmail.com";
        var smtpPassword = "aspn szij yosl vgxo";

        
        using (var client = new SmtpClient(smtpServer, smtpPort))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUsername);
            mailMessage.To.Add(email);
            mailMessage.Subject = "KnowBody Site: Password Reset";
            mailMessage.Body = $"Please reset your password using this link: {resetLink}";

            await client.SendMailAsync(mailMessage);
        }
       
    }
}
