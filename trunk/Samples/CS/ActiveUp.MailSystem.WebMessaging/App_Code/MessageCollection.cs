using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class WebMailMessageCollection : System.Collections.CollectionBase
{

    public WebMailMessage this[int index]
    {
        get
        {
            return (WebMailMessage)List[index];
        }
    }

    public WebMailMessageCollection()
    {
    }

    public void Add(WebMailMessage msg)
    {
        List.Add(msg);
    }

    public void Save(string filePath)
    {
        // trial
    }

} // class MessageCollection

