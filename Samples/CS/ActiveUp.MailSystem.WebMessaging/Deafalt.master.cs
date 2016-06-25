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


public partial class Deafalt : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["imapobject\uFFFD"] == null) || (Page.User == null) || !Page.User.Identity.IsAuthenticated)
        {
            if (!IsPostBack)
            {
                LoadLanguages();
                SetWebmailLanguage(null, null);
            }
           
        }
        else if (!IsPostBack)
        {
            LoadLanguages();
            SetWebmailLanguage(null, null);
            
        }
    }

    private void LoadLanguages()
    {
        
    }



    public void SetWebmailLanguage(object sender, System.EventArgs e)
    {
        try
        {
            if (Session["language\uFFFD"] != null)
                //  TODO: review the commented line, it was replaced
                // Session["language\uFFFD"] = Language.Get(Server.MapPath("languages/\uFFFD"), dLanguages.SelectedItem.Value);
                Session["language\uFFFD"] = Language.Get(Server.MapPath("languages/"), dLanguages.SelectedItem.Value);
            else
            {
                if (dLanguages.SelectedItem != null)
                {
                    //  TODO: review the commented line, it was replaced
                    //Session.Add("language\uFFFD", Language.Get(Server.MapPath("languages/\uFFFD"), dLanguages.SelectedItem.Value));
                    Session.Add("language\uFFFD", Language.Get(Server.MapPath("languages/"), dLanguages.SelectedItem.Value));
                }
                else
                {
                    Session.Add("language\uFFFD", Language.Get(Server.MapPath("languages/"), "en"));
                }

            }
            string selectedLanguage;
            if (dLanguages.SelectedItem == null)
            {
                /// default language
                selectedLanguage = "en";
            }
            else
            {
                selectedLanguage = dLanguages.SelectedItem.Value;
            }
            System.Web.HttpCookie httpCookie = new System.Web.HttpCookie("language\uFFFD", selectedLanguage);
            System.DateTime dateTime = System.DateTime.Now;
            httpCookie.Expires = dateTime.AddMonths(2);
            Response.Cookies.Add(httpCookie);
            for (int i = 0; i < Controls.Count; i++)
            {
                if ((Controls[i].GetType() == typeof(System.Web.UI.WebControls.Label)) && Controls[i].ID.StartsWith("lt\uFFFD"))
                    ((System.Web.UI.WebControls.Label)Controls[i]).Text = ((Language)Session["language\uFFFD"]).Words[System.Convert.ToInt32(Controls[i].ID.Replace("lt\uFFFD", "\uFFFD"))].ToString();
            }
           

        }
        catch (System.Exception e1)
        {
            Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[89].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
        }
    }
}
