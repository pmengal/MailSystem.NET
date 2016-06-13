using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class Language
{

    private string _code;
    private string _name;
    private System.Collections.ArrayList _words;

    public string Code
    {
        get
        {
            return _code;
        }
        set
        {
            _code = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public System.Collections.ArrayList Words
    {
        get
        {
            return _words;
        }
        set
        {
            _words = value;
        }
    }

    public Language()
    {
        _words = new System.Collections.ArrayList();
    }

    public static Language Get(string basePath, string code)
    {
        Language language1 = new Language();
        System.Collections.Specialized.ListDictionary listDictionary = new System.Collections.Specialized.ListDictionary();
        System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
        xmlDocument.Load(basePath + code + ".xml");
        language1.Code = code;
        language1.Name = xmlDocument.DocumentElement.Attributes["name"].Value;
        foreach (System.Xml.XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
        {
            language1.Words.Add(xmlNode.InnerText);
        }
        return language1;
    }

    public static LanguageCollection GetLanguages(string basePath)
    {
        // trial
        return null;
    }

} // class Language
