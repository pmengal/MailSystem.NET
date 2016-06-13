using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for EncryptDescript
/// </summary>
public class EncryptDescript
{
   	public EncryptDescript()
	{		
	}

    public static string CriptDescript(string Texto)
    {
        string crip = string.Empty;
        const int Chave = 1708;
        foreach (char character in Texto)
        {
            int charCode = (int)character;
            char cripChar = (char)(charCode ^ Chave);
            crip += cripChar.ToString();
        }
        return crip;
    }
}
