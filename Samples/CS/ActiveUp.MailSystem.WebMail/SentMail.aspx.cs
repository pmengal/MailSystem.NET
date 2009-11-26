using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ActiveUp.Net.Mail;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;

public partial class SentMail : System.Web.UI.Page
{
    /// <summary>
    /// SentMail load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!File.Exists(Constants.ACCOUNT_FILE_NAME_SETTINGS)) Response.Redirect("~/Settings.aspx?permit=false");

        if(!Page.IsPostBack) {
            if (Facade.GetInstance().AccSettings != null)
            {
                this.loadSentMails();
            }
            else
            {
            Response.Redirect("~/Settings.aspx");
            }
        }
    }

    protected override void InitializeCulture()
    {
        String lang = string.Empty;
        HttpCookie cookie = Request.Cookies["DropDownLanguage"];

        if (cookie != null && cookie.Value != null)
        {
            lang = cookie.Value;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
        }
        base.InitializeCulture();
    }

    private void loadSentMails()
    {
        GridViewSentMail.DataSource = Facade.GetInstance().GetSentMailList();
        GridViewSentMail.DataBind();
    }
    

    protected void GridViewInbox_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Facade facade = Facade.GetInstance();
        GridViewSentMail.DataSource = facade.GetSentMailList();
        GridViewSentMail.PageIndex = e.NewPageIndex;
        GridViewSentMail.DataBind(); 
    }
}
