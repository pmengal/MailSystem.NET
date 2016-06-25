using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

[System.Xml.Serialization.XmlType]
public class Envelope
{

    private bool _answered;
    private System.DateTime _date;
    private string _datestring;
    private bool _forwarded;
    private ActiveUp.Net.Mail.Address _from;
    private int _id;
    private string _mailbox;
    private bool _marked;
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

    [System.Xml.Serialization.XmlAttribute("datestring")]
    public string DateString
    {
        get
        {
            return _datestring;
        }
        set
        {
            _datestring = value;
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

    public Envelope()
    {
        _from = new ActiveUp.Net.Mail.Address();
    }

    public Envelope(Envelope env)
    {
        _from = new ActiveUp.Net.Mail.Address();
        Answered = env.Answered;
        Date = env.Date;
        DateString = env.DateString;
        Forwarded = env.Forwarded;
        From = env.From;
        Id = env.Id;
        Mailbox = env.Mailbox;
        Marked = env.Marked;
        Read = env.Read;
        Size = env.Size;
        Subject = env.Subject;
    }

    public void Save(string filePath)
    {
        // trial
    }

} // class Envelope
