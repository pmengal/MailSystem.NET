using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for SettingManager
/// </summary>
public class SettingManager
{
	public SettingManager()
	{
		
	}     

    public static AccountSettings.AccountInfo getArrayAccountInfo()
    {
        return AccountSettings.Load("AccountSettings").Acc_Info;
    }
}
