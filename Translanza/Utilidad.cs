using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Translanza
{
    public class Utilidad
    {
        //Hola prueba git
        //2
        public static void SendEmail(MailMessage MailMessageParam, SmtpClient SmtpClientParam)
        {
            try
            {
                SmtpClientParam.Send(MailMessageParam);
                MailMessageParam.Dispose();
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}