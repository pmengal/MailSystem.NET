<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
        xmlDocument.Load(Server.MapPath("WebMessaging.config"));
      //  Application["server\uFFFD"] = xmlDocument.GetElementsByTagName("imapserver\uFFFD")[0].InnerText;
        Application["server\uFFFD"] = xmlDocument.GetElementsByTagName("imapserver")[0].InnerText;
        Application["port\uFFFD"] = System.Convert.ToInt32(xmlDocument.GetElementsByTagName("imapport")[0].InnerText);
        Application["smtpserver\uFFFD"] = xmlDocument.GetElementsByTagName("smtpserver")[0].InnerText;
        Application["defaultlanguage\uFFFD"] = xmlDocument.GetElementsByTagName("defaultlanguage")[0].InnerText;
        Application["startfolder\uFFFD"] = xmlDocument.GetElementsByTagName("startfolder")[0].InnerText;
        Application["writedirectory\uFFFD"] = xmlDocument.GetElementsByTagName("writedirectory")[0].InnerText;
        Application["smtpport\uFFFD"] = System.Convert.ToInt32(xmlDocument.GetElementsByTagName("smtpport")[0].InnerText);

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        if (Application["server\uFFFD"] == null)
        {
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.Load(Server.MapPath("WebMessaging.config"));
            Application["server\uFFFD"] = xmlDocument.GetElementsByTagName("imapserver")[0].InnerText;
            Application["port\uFFFD"] = System.Convert.ToInt32(xmlDocument.GetElementsByTagName("imapport")[0].InnerText);
            Application["smtpserver\uFFFD"] = xmlDocument.GetElementsByTagName("smtpserver")[0].InnerText;
            Application["defaultlanguage\uFFFD"] = xmlDocument.GetElementsByTagName("defaultlanguage")[0].InnerText;
            Application["smtpport\uFFFD"] = System.Convert.ToInt32(xmlDocument.GetElementsByTagName("smtpport")[0].InnerText);
            Application["startfolder\uFFFD"] = xmlDocument.GetElementsByTagName("startfolder")[0].InnerText;
            Application["writedirectory\uFFFD"] = xmlDocument.GetElementsByTagName("writedirectory")[0].InnerText;
        }
    }

    void Session_End(object sender, EventArgs e) 
    {
        if (Session["imapobject\uFFFD"] != null)
            ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).Disconnect();
        string[] sArr = System.IO.Directory.GetFiles(Server.MapPath(Application["writedirectory\uFFFD"].ToString()));
        for (int i = 0; i < sArr.Length; i++)
        {
            string s = sArr[i];
            if (s.IndexOf(Session.SessionID) != -1)
                System.IO.File.Delete(s);
        }
    }
       
</script>
