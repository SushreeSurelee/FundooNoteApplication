using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQ = new MessageQueue();
        
        public void sendData2Queue(string token)
        {
            messageQ.Path = @".\private$\Token"; //Setting the QueuPath where we want to store the messages.
            if (!MessageQueue.Exists(messageQ.Path))
            {
                MessageQueue.Create(messageQ.Path);
            }
            messageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQ.ReceiveCompleted += MessageQ_ReceiveCompleted; //implimenting both Delegate and event.
            messageQ.BeginReceive();
            messageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQ.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string subject = "FundooNote Application Reset Link";
                string body = token;
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sushreesahu430@gmail.com", "twrjvnypeszncypj"),
                    EnableSsl = true,
                };
                smtp.Send("sushreesahu430@gmail.com", "sushreesahu430@gmail.com", subject, body);
                messageQ.BeginReceive();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
