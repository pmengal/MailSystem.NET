using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

[System.Serializable]
public class EnvelopeCollection : System.Collections.CollectionBase
{
    [System.Xml.Serialization.XmlArrayItem("envelope", typeof(Envelope))]
    [System.Xml.Serialization.XmlArray("envelopes")]
    public Envelope this[int index]
    {
        get
        {
            return (Envelope)List[index];
        }
    }

    public EnvelopeCollection()
    {
    }

    public void Add(Envelope env)
    {
        List.Add(env);
    }

    public void Insert(Envelope env, int index)
    {
        List.Insert(index, env);
    }

    public void Save(string filePath)
    {
        // trial
    }

    public static EnvelopeCollection Load(string filePath)
    {
        // trial
        return null;
    }

} // class EnvelopeCollection


