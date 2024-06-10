using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDExampleDOTNET
{
    public class EmailNotification
    {
        private IEmailSender _IEmailSender;
        //private SmtpClient _SmtpClient;

        //public EmailNotification()
        public EmailNotification(IEmailSender emailSender)
        {
            _IEmailSender = emailSender;
            //_SmtpClient = new SmtpClient();
        }

        public void Send(string emailAddress, string message)
        {
            _IEmailSender.Send(emailAddress, message);
        }
    }

    public interface IEmailSender 
    {
        void Send(string emailAddress, string message);
    }

    class SmtpClient : IEmailSender
    {
        void IEmailSender.Send(string emailAddress, string message)
        {
            throw new NotImplementedException();
        }
    }
}
