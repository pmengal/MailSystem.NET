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
using System.ComponentModel;
using ActiveUp.Net.Mail;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Globalization;

public partial class Inbox : System.Web.UI.Page
{
    /// <summary>
    /// Inbox page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            Facade facade = Facade.GetInstance();

            //if (Facade.GetInstance().AccSettings == null) Response.Redirect("~/Settings.aspx");
            if (!File.Exists(Constants.ACCOUNT_FILE_NAME_SETTINGS)) Response.Redirect("~/Settings.aspx?permit=false");

            if (facade.ChangeImcoming || !PermitMakesRetrives)
            {
                facade.ChangeImcoming = false;
                PermitMakesRetrives = true;
                facade.Connect();
                GridViewInbox.DataSource = facade.Retrieves();
                GridViewInbox.DataBind();
            }
            else
            {
                GridViewInbox.DataSource = facade.getListHeaders();
                GridViewInbox.DataBind();
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

    /// <summary>
    /// Event fired when the actual GridView page is changed.
    /// </summary>
    /// <param name="sender">The sender object</param>
    /// <param name="e">The page event arguments</param>
    protected void GridViewInbox_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Facade facade = Facade.GetInstance();
        GridViewInbox.DataSource = facade.getListHeaders();
        GridViewInbox.PageIndex = e.NewPageIndex;
        GridViewInbox.DataBind();    
    }
    
    public bool PermitMakesRetrives
    {
        get
        {
            if (Session["PermitMakesRetrives"] == null)
            {
                return false;
            }
            else
            {
                return ((bool)Session["PermitMakesRetrives"]);
            }
        }
        set { Session["PermitMakesRetrives"] = value; }
    }
}
