﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Translanza
{
    public class Utilidad
    {
        public static void SendEmail(MailMessage MailMessageParam, SmtpClient SmtpClientParam)
        {
            try
            {
                //Error
                SmtpClientParam.Send(MailMessageParam);
                //error3
                MailMessageParam.Dispose();
                //Error2
            }
            catch (Exception ex)
            {
                //carturar
                //llllllll
                //dfsdfsdfdsfjsdfskdfkjdsfn
            }

            //77777sjhasdjasd

            //777asjasdjads
        }

    }
}