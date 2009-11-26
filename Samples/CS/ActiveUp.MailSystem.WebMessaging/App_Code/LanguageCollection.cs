using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class LanguageCollection : System.Collections.CollectionBase
{

    public Language this[int index]
    {
        get
        {
            return (Language)List[index];
        }
    }

    public LanguageCollection()
    {
    }

    public void Add(Language language)
    {
        // trial
    }

} // class LanguageCollection

