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
/// Summary description for Message
/// </summary>
[System.Xml.Serialization.XmlType("WebMailMessage")]
public class WebMailMessage
{
    private bool _answered;
    private byte[] _data;
    private System.DateTime _date;
    private bool _forwarded;
    private ActiveUp.Net.Mail.Address _from;
    private int _id;
    private string _mailbox;
    private bool _marked;
    private ActiveUp.Net.Mail.Message _parsed;
    private bool _read;
    private int _size;
    private string _subject;
    private int _userId;

    [System.Xml.Serialization.XmlAttribute("answered")]
    public bool Answered
    {
        get
        {
            return _answered;
        }
        set
        {
            _answered = value;
        }
    }

    public byte[] Data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("date")]
    public System.DateTime Date
    {
        get
        {
            return _date;
        }
        set
        {
            _date = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("forwarded")]
    public bool Forwarded
    {
        get
        {
            return _forwarded;
        }
        set
        {
            _forwarded = value;
        }
    }

    public ActiveUp.Net.Mail.Address From
    {
        get
        {
            return _from;
        }
        set
        {
            _from = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("id")]
    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("mailbox")]
    public string Mailbox
    {
        get
        {
            return _mailbox;
        }
        set
        {
            _mailbox = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("marked")]
    public bool Marked
    {
        get
        {
            return _marked;
        }
        set
        {
            _marked = value;
        }
    }

    public ActiveUp.Net.Mail.Message Parsed
    {
        get
        {
            return _parsed;
        }
        set
        {
            _parsed = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("read")]
    public bool Read
    {
        get
        {
            return _read;
        }
        set
        {
            _read = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("size")]
    public int Size
    {
        get
        {
            return _size;
        }
        set
        {
            _size = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("subject")]
    public string Subject
    {
        get
        {
            return _subject;
        }
        set
        {
            _subject = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("userid")]
    public int UserId
    {
        get
        {
            return _userId;
        }
        set
        {
            _userId = value;
        }
    }

    public WebMailMessage()
    {
        _from = new ActiveUp.Net.Mail.Address();
    }

    public void Save(string filePath)
    {
        // trial
    }

} // class Message

