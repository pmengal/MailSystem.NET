using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ActiveUp.Net.Mail;

/// <summary>
/// This controller is used for send
/// mail messages for SMTP protocol
/// </summary>
/// 
public class SmtpController
{
    #region Constructor

	public SmtpController()
	{
    }

    #endregion

    #region Method

    /// <summary>
    /// Method used for send a message.
    /// </summary>
    /// <param name="message">The message will be send.</param>  
    /// <param name="acc">The account information.</param>       
    public void SendMail(Message message, AccountSettings acc)
    {
        AccountSettings.AccountInfo arrayAcc_info = acc.Acc_Info;

        try
        {
            if (arrayAcc_info != null)
            {
                message.From.Email = arrayAcc_info.EmailAddress;
                string outgoing = arrayAcc_info.OutgoingServer;
                int smtp_Port = arrayAcc_info.PortOutgoingServer;
                string email = EncryptDescript.CriptDescript(arrayAcc_info.EmailAddress);
                string password = EncryptDescript.CriptDescript(arrayAcc_info.Password);

                bool ssl = arrayAcc_info.IsOutgoingSecureConnection;
                bool port = arrayAcc_info.PortOutgoingChecked;

                if (ssl)
                {
                    if (port)
                    {
                        ActiveUp.Net.Mail.SmtpClient.SendSsl(message, outgoing, smtp_Port, email, password, ActiveUp.Net.Mail.SaslMechanism.Login);
                    }
                    else
                    {
                        ActiveUp.Net.Mail.SmtpClient.SendSsl(message, outgoing, email, password, ActiveUp.Net.Mail.SaslMechanism.Login);
                    }
                }
                else
                {
                    if (port)
                    {
                        ActiveUp.Net.Mail.SmtpClient.Send(message, outgoing, smtp_Port, email, password, ActiveUp.Net.Mail.SaslMechanism.Login);
                    }
                    else
                    {
                        ActiveUp.Net.Mail.SmtpClient.SendSsl(message, outgoing, email, password, ActiveUp.Net.Mail.SaslMechanism.Login);
                    }
                }
                this.storeMessageSent(message);
            }
        }
        catch (Exception)
        {

        }
    }


    private void storeMessageSent(Message msg)
    {
        //msg.StoreToFile();
    }

    #endregion
}
