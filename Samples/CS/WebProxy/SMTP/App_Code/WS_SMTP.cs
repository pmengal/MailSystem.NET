using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using ActiveUp.Net.Mail;
using ActiveUp.Net.Security;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WS_SMTP : System.Web.Services.WebService
{
    public WS_SMTP()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region State Maintaining calls to set up the vales in steps
    /// <summary>
    /// Save the Sender Address in the session
    /// </summary>
    /// <param name="strFrom">Email address of the user sending the email</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean SetFrom(string strFrom)
    {
        System.Diagnostics.Debug.WriteLine(strFrom);
        return SetFromWithName(strFrom, String.Empty);
    }

    /// <summary>
    /// Save the Sender Address and Name in the session
    /// </summary>
    /// <param name="strFrom">Email address of the user sending the email</param>
    /// <param name="strFromName">Name of the user sending the email</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean SetFromWithName(string strFrom, string strFromName)
    {
        try
        {
            Session["From"] = strFrom;
            Session["FromName"] = strFromName;
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Add the email address to the list of To addresses
    /// </summary>
    /// <param name="strTo">Email address of the To recipient</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean AddToAddress(string strTo)
    {
        System.Diagnostics.Debug.WriteLine(Session.Contents);
        return AddToAddressWithName(strTo, String.Empty);
    }

    /// <summary>
    /// Add the email address and name to the list of To addresses
    /// </summary>
    /// <param name="strTo">Email address of the To recipient</param>
    /// <param name="strToName">Name of the To recipient</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean AddToAddressWithName(string strTo, string strToName)
    {
        try
        {
            if (Session["To"] != null && Session["To"] as AddressCollection != null)
            {
                ((AddressCollection)Session["To"]).Add(new Address(strTo, strToName));
            }
            else
            {
                Session["To"] = new AddressCollection();
                ((AddressCollection)Session["To"]).Add(new Address(strTo, strToName));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Add the email address and name to the list of CC addresses
    /// </summary>
    /// <param name="strCC">Email address of the CC recipient</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean AddCCAddress(string strCC)
    {
        return AddCCAddressWithName(strCC, String.Empty);
    }

    /// <summary>
    /// Add the email address and name to the list of CC addresses
    /// </summary>
    /// <param name="strCC">Email address of the CC recipient</param>
    /// <param name="strCCName">Name of the CC recipient</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean AddCCAddressWithName(string strCC, string strCCName)
    {
        try
        {
            if (Session["CC"] != null && Session["CC"] as AddressCollection != null)
            {
                ((AddressCollection)Session["CC"]).Add(new Address(strCC, strCCName));
            }
            else
            {
                Session["CC"] = new AddressCollection();
                ((AddressCollection)Session["CC"]).Add(new Address(strCC, strCCName));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Add the email address and name to the list of BCC addresses
    /// </summary>
    /// <param name="strBCC">Email address of the BCC recipient</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean AddBCCAddress(string strBCC)
    {
        return AddBCCAddressWithName(strBCC, String.Empty);
    }

    /// <summary>
    /// Add the email address and name to the list of BCC addresses
    /// </summary>
    /// <param name="strBCC">Email address of the BCC recipient</param>
    /// <param name="strBCCName">Name of the BCC recipient</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean AddBCCAddressWithName(string strBCC, string strBCCName)
    {
        try
        {
            if (Session["BCC"] != null && Session["BCC"] as AddressCollection != null)
            {
                ((AddressCollection)Session["BCC"]).Add(new Address(strBCC, strBCCName));
            }
            else
            {
                Session["BCC"] = new AddressCollection();
                ((AddressCollection)Session["BCC"]).Add(new Address(strBCC, strBCCName));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Set the port of the server used for SMTP
    /// </summary>
    /// <param name="iPort">Port number to use for sending email</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean SetPort(int iPort)
    {
        try
        {
            Session["Port"] = iPort;
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Add the SMTP server to the list of the Server to try
    /// </summary>
    /// <param name="host">Name or IP address of the SMTP server</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean AddHost(string host)
    {
        try
        {
            if (Session["Hosts"] != null && Session["Hosts"] as ServerCollection != null)
            {
                ((ServerCollection)Session["Hosts"]).Add(host);
            }
            else
            {
                Session["Hosts"] = new ServerCollection();
                ((ServerCollection)Session["Hosts"]).Add(host);
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Add the SMTP server to the list of the Server to try
    /// </summary>
    /// <param name="host">Name or IP address of the SMTP server</param>
    /// <param name="port">Port of the server to be used</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean AddHostWithPort(string host, int port)
    {
        try
        {
            if (Session["Hosts"] != null && Session["Hosts"] as ServerCollection != null)
            {
                ((ServerCollection)Session["Hosts"]).Add(host, port);
            }
            else
            {
                Session["Hosts"] = new AddressCollection();
                ((ServerCollection)Session["Hosts"]).Add(host, port);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Set the username and password to be used for authentication with the SMTP server
    /// </summary>
    /// <param name="strUser">Sender's username for the SMTP server</param>
    /// <param name="strPassword">Sender's password for the SMTP server</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean SetUser(String strUser, String strPassword)
    {
        try
        {
            Session["User"] = strUser;
            Session["Password"] = strPassword;
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Set the subject of the email
    /// </summary>
    /// <param name="strSubject">Subject of the email</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean SetSubject(String strSubject)
    {
        try
        {
            Session["Subject"] = strSubject;
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Set the HTML content of the Body of the Email
    /// </summary>
    /// <param name="strBodyHTML">HTML for the body of the email</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean SetstrBodyHTML(String strBodyHTML)
    {
        try
        {
            Session["BodyHTML"] = strBodyHTML;
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Set the Text content of the Body of the Email
    /// </summary>
    /// <param name="strBodyText">Text for the body of the email</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean SetBodyText(String strBodyText)
    {
        try
        {
            Session["BodyText"] = strBodyText;
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    /// <summary>
    /// Send mail using the specified Protocol and Security settings
    /// </summary>
    /// <param name="UseSSL">Set to true to use SSL</param>
    /// <param name="UseMD5">Set to true to use MD5 method, otherwise uses the Login method</param>
    /// <param name="protocol">Protocol to be used for communication <see cref="System.Security.Authentication.SslProtocols"/>System.Security.Authentication.SslProtocols</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public Boolean SendSessionedMail(Boolean UseSSL, Boolean UseMD5, System.Security.Authentication.SslProtocols protocol)
    {
        Message message = new Message();

        ServerCollection Hosts = null;
        int Port=0;
        string UserName=string.Empty;
        string Password=string.Empty;
        bool PORTExist = false;

        try
        {
            if (Session["From"] != null)
            {
                message.From.Email = Session["From"].ToString();
            }
            else
            {
                return false;
                //throw new Exception("From Email is a mandatory information");
            }
            if (Session["FromName"] != null)
            {
                message.From.Name = Session["FromName"].ToString();
            }
            else
            {
                return false;
                //throw new Exception("From Name is a mandatory information");
            }

            if (Session["To"] != null)
            {
                message.To.AddRange((AddressCollection)Session["To"]);
            }
            else
            {
                return false;
                //throw new Exception("Email to is a mandatory information");
            }
            if (Session["CC"] != null)
            {
                message.Cc.AddRange((AddressCollection)Session["CC"]);
            }


            if (Session["BCC"] != null)
            {
                message.Bcc.AddRange((AddressCollection)Session["BCC"]);
            }

            if (Session["Subject"] != null)
            {
                message.Subject = Session["Subject"].ToString();
            }
            else
            {
                //May not be needed
                //throw new Exception("Subject to is a mandatory information");
            }
            if (Session["BodyText"] != null)
            {
                message.BodyText.Text = Session["BodyText"].ToString();
            }

            if (Session["BodyHTML"] != null)
            {
                message.BodyHtml.Text = Session["BodyHTML"].ToString();
            }


            if (Session["Hosts"] != null)
            {
                Hosts = (ServerCollection)Session["Hosts"];
            }

            if (Session["Port"] != null)
            {
                Port = (int)Session["Port"];
                PORTExist = true;
            }

            if (Session["User"] != null)
            {
                UserName = (string)Session["User"];
            }

            if (Session["Password"] != null)
            {
                Password = (string)Session["Password"];
            }

            //TODO - Need to find a way to manage multiple attachemnts
            // Session is not a solution as it will generate too much network traffic
            // Probably need to store it locally and associate with Session ID

            if (UserName.Length > 0 && Password.Length > 0 && Hosts.Count > 0 && UseSSL)
            {
                //Use SSL to send mail
                SmtpClient smtpClient = new SmtpClient();
                SslHandShake handShake = new SslHandShake(Hosts[0].Host, protocol);
                handShake.ServerCertificateValidationCallback = MyServerCertificateValidationCallback;
                message.Send(Hosts[0].Host, UserName, Password, UseMD5 ? SaslMechanism.CramMd5 : SaslMechanism.Login);
                return true;
            }
            else if (UserName.Length > 0 && Password.Length > 0 && Hosts.Count > 0 && PORTExist)
            {
                //Use Authentication for sending mail
                message.Send(Hosts[0].Host, Port, UserName, Password, UseMD5 ? SaslMechanism.CramMd5 : SaslMechanism.Login);
                return true;
            }
            else if ((UserName.Trim().Length == 0 || Password.Length == 0) && Hosts.Count > 0 && PORTExist)
            {
                //Use specified port for sending mail
                message.Send(Hosts[0].Host, Port);
                return true;
            }
            else if ((UserName.Trim().Length == 0 || Password.Length == 0) && Hosts.Count > 0 && !PORTExist)
            {
                //Send without authentication
                message.Send(Hosts);
                return true;
            }
            else
            {
                //Send directly without specifying a server
                message.DirectSend();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            message = null;
        }
    }

    static bool MyServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        // Check if there are any errors.
        bool ok = errors.Equals(SslPolicyErrors.None);

        //this.AddLogEntry(string.Format("SSL Authentication was {0} successful", (ok ? "" : "NOT ")));

        // We decide to allow communication with the server even if it isn't authenticated (not recommended).
        return true;
    }

    /// <summary>
    /// Print the Session contents to the Debug output 
    /// window (for testing the contents of the session)
    /// </summary>
    public void Debugger()
    {
        for (int i = 0; i < Session.Count; i++)
        {
            System.Diagnostics.Debug.WriteLine(Session[i]);
            System.Diagnostics.Debug.WriteLine("---------------------");
        }
    }

    #region OverLoaded Stateless Methods
    [WebMethod]
    public Boolean SendMail01
        (String strFromEmail, String strFromName,
        String strTos, String strCCTos, String strBCCTos,
        String strSubject, String strBodyText)
    {
        return SendMail(strFromEmail, strFromName,
        new string[] { strTos }, new string[] { strCCTos }, new string[] { strBCCTos },
        strSubject, strBodyText, String.Empty,
        String.Empty, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail02
        (String strFromEmail, String strFromName,
        String strTos, String strCCTos, String strBCCTos,
        String strSubject, String strBodyText,
        String strHost)
    {
        return SendMail(strFromEmail, strFromName,
        new string[] { strTos }, new string[] { strCCTos }, new string[] { strBCCTos },
        strSubject, strBodyText, String.Empty,
        strHost, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail03
        (String strFromEmail, String strFromName,
        String strTos, String[] strCCTos, String strBCCTos,
        String strSubject, String strBodyText,
        String strHost)
    {
        return SendMail(strFromEmail, strFromName,
        new string[] { strTos }, strCCTos, new string[] { strBCCTos },
        strSubject, strBodyText, String.Empty,
        strHost, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail04
        (String strFromEmail, String strFromName,
        String strTos, String strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText,
        String strHost)
    {
        return SendMail(strFromEmail, strFromName,
        new string[] { strTos }, new string[] { strCCTos }, strBCCTos,
        strSubject, strBodyText, String.Empty,
        strHost, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail05
        (String strFromEmail, String strFromName,
        String strTos, String[] strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText,
        String strHost)
    {
        return SendMail(strFromEmail, strFromName,
        new string[] { strTos }, strCCTos, strBCCTos,
        strSubject, strBodyText, String.Empty,
        strHost, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail06
        (String strFromEmail, String strFromName,
        String[] strTos, String[] strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText,
        String strHost)
    {
        return SendMail(strFromEmail, strFromName,
        strTos, strCCTos, strBCCTos,
        strSubject, strBodyText, String.Empty,
        strHost, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail07
        (String strFromEmail, String strFromName,
        String[] strTos, String strCCTos, String strBCCTos,
        String strSubject, String strBodyText,
        String strHost)
    {
        return SendMail(strFromEmail, strFromName,
        strTos, new string[] { strCCTos }, new string[] { strBCCTos },
        strSubject, strBodyText, String.Empty,
        strHost, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail08
        (String strFromEmail, String strFromName,
        String[] strTos, String[] strCCTos, String strBCCTos,
        String strSubject, String strBodyText,
        String strHost)
    {
        return SendMail(strFromEmail, strFromName,
        strTos, strCCTos, new string[] { strBCCTos },
        strSubject, strBodyText, String.Empty,
        strHost, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail09
        (String strFromEmail, String strFromName,
        String[] strTos, String strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText,
        String strHost)
    {
        return SendMail(strFromEmail, strFromName,
        strTos, new string[] { strCCTos }, strBCCTos,
        strSubject, strBodyText, String.Empty,
        strHost, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail10
        (String strFromEmail, String strFromName,
        String[] strTos, String[] strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText, String strBodyHTML,
        String strHost)
    {
        return SendMail(strFromEmail, strFromName,
        strTos, strCCTos, strBCCTos,
        strSubject, strBodyText, strBodyHTML,
        strHost, 0, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail11
        (String strFromEmail, String strFromName,
        String[] strTos, String[] strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText, String strBodyHTML,
        String strHost, int iPort)
    {
        return SendMail(strFromEmail, strFromName,
        strTos, strCCTos, strBCCTos,
        strSubject, strBodyText, strBodyHTML,
        strHost, iPort, String.Empty, String.Empty,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail12
        (String strFromEmail, String strFromName,
        String[] strTos, String[] strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText, String strBodyHTML,
        String strHost, int iPort, String strUserName, String strUserPassword)
    {
        return SendMail(strFromEmail, strFromName,
        strTos, strCCTos, strBCCTos,
        strSubject, strBodyText, strBodyHTML,
        strHost, iPort, strUserName, strUserPassword,
        true, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail13
        (String strFromEmail, String strFromName,
        String[] strTos, String[] strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText, String strBodyHTML,
        String strHost, int iPort, String strUserName, String strUserPassword,
        Boolean bUseSSL)
    {
        return SendMail(strFromEmail, strFromName,
        strTos, strCCTos, strBCCTos,
        strSubject, strBodyText, strBodyHTML,
        strHost, iPort, strUserName, strUserPassword,
        bUseSSL, false, null, null);
    }

    [WebMethod]
    public Boolean SendMail14
        (String strFromEmail, String strFromName,
        String[] strTos, String[] strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText, String strBodyHTML,
        String strHost, int iPort, String strUserName, String strUserPassword,
        Boolean bUseSSL, Boolean bUseMD5)
    {
        return SendMail(strFromEmail, strFromName,
        strTos, strCCTos, strBCCTos,
        strSubject, strBodyText, strBodyHTML,
        strHost, iPort, strUserName, strUserPassword,
        bUseSSL, bUseMD5, null, null);
    }

    [WebMethod]
    public Boolean SendMail
        (String strFromEmail, String strFromName,
        String[] strTos, String[] strCCTos, String[] strBCCTos,
        String strSubject, String strBodyText, String strBodyHTML,
        String strHost, int iPort, String strUserName, String strUserPassword,
        Boolean bUseSSL, Boolean bUseMD5, byte[][] baAttachment, String[] strFileName)
    {
        Message message = new Message();
        try
        {
            message.From.Email = strFromEmail;
            message.From.Name = strFromName;

            if (strTos != null)
            {
                foreach (string strTo in strTos)
                {
                    message.To.Add(new Address(strTo));
                }
            }
            if (strCCTos != null)
            {
                foreach (string strCCTo in strCCTos)
                {
                    message.Cc.Add(new Address(strCCTo));
                }
            }
            if (strTos != null)
            {
                foreach (string strBCCTo in strBCCTos)
                {
                    message.Bcc.Add(new Address(strBCCTo));
                }
            }

            if (strBodyHTML.Length > 0)
            {
                message.BodyHtml.Text = strBodyHTML;
            }
            message.BodyText.Text = strBodyText;

            //Check for Arrays to be of equal length
            //if (baAttachment != null && strFileName != null)
            //{
            //    if (baAttachment.GetLength(0) == strFileName.GetLength(0))
            //    {
            //        //add all the attachments
            //        for (int i = 0; i < baAttachment.GetLength(0); i++)
            //            message.Attachments.Add(new MimePart(baAttachment[i], strFileName[i]));
            //    }
            //}
            //else
            //{
            //    return false;
            //}

            if (strHost.Length > 0)
            {
                //Check if a custom port is to be used
                if ((int)Session["Port"] > 0)
                {
                    //Check if SSL is to be used
                    if (bUseSSL)
                    {
                        message.Send(strHost, iPort, strUserName, strUserPassword, bUseMD5 ? SaslMechanism.CramMd5 : SaslMechanism.Login);
                    }
                    else
                    {
                        message.Send(strHost, iPort);
                    }
                }
                else
                {
                    //Check if SSL is to be used
                    if (bUseSSL)
                    {
                        message.Send(strHost, strUserName, strUserPassword, bUseMD5 ? SaslMechanism.CramMd5 : SaslMechanism.Login);
                    }
                    else
                    {
                        message.Send(strHost);
                    }
                }
            }
            else
            {
                message.DirectSend();
            }
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }

    }
    #endregion

}
