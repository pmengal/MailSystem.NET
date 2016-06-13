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

public partial class MasterPage : System.Web.UI.MasterPage
{
    /// <summary>
    /// Master Page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["language"] != null)
            {
                String language = Session["language"].ToString();

                if (language != null && language == "pt")
                {
                    languageDropDown.SelectedIndex = 1;
                }
                else
                {
                    languageDropDown.SelectedIndex = 0;
                }
            }
        }
    }
  
    /// <summary>
    /// Event for change the actual language whit Cookies.
    /// </summary>
    /// <param name="sender">The Sender Object</param>
    /// <param name="e">The Event Arguments</param>
    protected void DropDownLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["language"] = languageDropDown.SelectedValue;
        HttpCookie cookie = new HttpCookie("DropDownLanguage");
        cookie.Value = languageDropDown.SelectedValue;
        Response.SetCookie(cookie);
        Response.Redirect(Request.Url.ToString());
    }
}
