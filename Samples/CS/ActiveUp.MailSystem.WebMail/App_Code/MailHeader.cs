using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ActiveUp.Net.Mail;

/// <summary>
/// Summary description for MailHeader
/// </summary>
public class MailHeader
{
    #region Attributes

    /// <summary>
    /// Attributes for describe a header
    /// </summary>
    public string _from;
    private string _subject;
    public DateTime _date;
    private string _index;
    private string _to;

    #endregion

    #region Properties

    /// <summary>
    /// Represents the from header.
    /// </summary>
    public string From
    {
        get { return _from; }
        set { _from = value; }
    }

    public string To
    {
        get { return _to; }
        set { _to = value; }
    } 

    /// <summary>
    /// Represents the Subject
    /// </summary>
    public string Subject
    {
        get { return _subject; }
        set { _subject = value; }
    }

    /// <summary>
    /// Represents the index, a identifier used for gets the Message of this header 
    /// </summary>
    public string Index
    {
        get { return _index; }
        set { _index = value; }
    }

    /// <summary>
    /// Represents the Subject
    /// </summary>
    public DateTime Date
    {
        get { return _date; }
        set { _date = value; }
    }

    /// <summary>
    /// Represents the Subject
    /// </summary>
	public MailHeader()
	{
        _index = "";
    }

    #endregion

    #region Methods

    /// <summary>
    /// Fill the attributes
    /// </summary>
    /// <param name="header">The header object</param>
    public void FillHeader(Header header) {
    
        this._subject = header.Subject;
        this._from = header.From.Email;
        this._date = header.Date;
    }

    

    #endregion

    public void FillHeader(Message msg, string name)
    {
        this._subject = msg.Subject;
        this._from = String.Empty;
        this._date = msg.Date;

        int i = 0;
        foreach (Address a in msg.To)
        {
            i++;
            _to += a.Email;
            if (msg.To.Count != i) _to += ", ";
        }


        this.Index = System.IO.Path.GetFileName(name);
    }
}
