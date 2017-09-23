using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace aashrayhospital.Helper
{
    public static class EmailHelper
    {
        public static string EmailSmtpServer { get; set; }
        public static int EmailSmtPport { get; set; }
        public static bool EmailIsSsl { get; set; }

        public static string EmailFromDisplayName { get; set; }
        public static string EmailFromAddress { get; set; }
        public static string EmailFromPassword { get; set; }
        public static string EmailToDisplayName { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="recipient"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="attachments"></param>
        public static void SendMail(string subject, string message, string recipient, string cc = "", string bcc = "",
            Dictionary<string, string> attachments = null)
        {
            try
            {
                string.Format("EMAIL > TO:{0}, SUBJECT:{1}, MESSAGE:{2}",recipient,subject,message).Log(typeof(EmailHelper));

                // Populate the mail message
                using (var mailMessage = new MailMessage
                {
                    From = new MailAddress(EmailFromAddress, EmailFromDisplayName),
                    Subject = subject,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal,
                    Body = message
                })
                {
                    // set reply to address
                    mailMessage.ReplyToList.Add(mailMessage.From);

                    // Add the alternate body to the message.
                    mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mailMessage.Body,
                        new ContentType("text/html")));

                    if (!string.IsNullOrEmpty(recipient))
                    {
                        foreach (var emailAddress in recipient.Split(','))
                        {
                            mailMessage.To.Add(new MailAddress(emailAddress, EmailToDisplayName));
                        }
                    }

                    if (!string.IsNullOrEmpty(cc))
                    {
                        foreach (var emailAddress in cc.Split(','))
                        {
                            mailMessage.Bcc.Add(new MailAddress(emailAddress));
                        }
                    }

                    if (!string.IsNullOrEmpty(bcc))
                    {
                        foreach (var emailAddress in bcc.Split(','))
                        {
                            mailMessage.Bcc.Add(new MailAddress(emailAddress));
                        }
                    }

                    // Add attachments if required
                    if (attachments != null)
                    {
                        foreach (var key in attachments.Keys)
                        {
                            if (!string.IsNullOrEmpty(attachments[key]) && File.Exists(attachments[key]))
                            {
                                var stream = new FileStream(attachments[key], FileMode.Open);
                                mailMessage.Attachments.Add(new Attachment(stream,
                                    key.Replace("-", "_")
                                        .Replace("\\", string.Empty)
                                        .Replace("/", string.Empty)
                                        .Replace(" ", string.Empty)));
                            }
                        }
                    }

                    using (var smtpServer = new SmtpClient
                    {
                        Host = EmailSmtpServer,
                        Port = EmailSmtPport,
                        EnableSsl = EmailIsSsl,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(EmailFromAddress, EmailFromDisplayName)
                    })
                    {
                        #region ssl check

                        //if (smtpServer.EnableSsl)
                        //{
                        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //}
                        //else
                        //{
                        //    smtpServer.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        //    smtpServer.PickupDirectoryLocation = Setting.EmailPickupDirectory;
                        //}

                        #endregion

                        #region From name set

                        try
                        {
                            smtpServer.Send(mailMessage);
                        }
                        catch (Exception newEx)
                        {
                            newEx.Log(typeof(EmailHelper));
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Log(typeof(EmailHelper));
            }
        }
    }
}