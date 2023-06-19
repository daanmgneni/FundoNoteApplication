using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Models
{
    public class MSMQModel
    {
        MessageQueue msgQueue = new MessageQueue();
       

    public void sendData2Queue(string Token)
        {
            msgQueue.Path = @".\private$\OTP";
            if (!MessageQueue.Exists(msgQueue.Path))
            {
                MessageQueue.Create(msgQueue.Path);
            }
            msgQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            msgQueue.ReceiveCompleted += msgQueue_ReceiveCompleted;
            msgQueue.Send(Token);
            msgQueue.BeginReceive();
            msgQueue.Close();
        }
       public void msgQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = msgQueue.EndReceive(e.AsyncResult);
                string data = msg.Body.ToString();
                string subject = "Fundo Note Reset Link";
                string body = data;
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("Azarhulkgo@gmail.com", "gtbrecwcqpjefdhu"),
                    EnableSsl = true
                };
                smtp.Send("Azarhulkgo@gmail.com", "Azarhulkgo@gmail.com", subject, body);
                msgQueue.BeginReceive();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
