using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class Emoticons : ActiveUp.MailSystem.ToolBase
{

    private const string KEY = "HTB_EMOTICONS";

    private string _imagesDir;

    public string ImagesDir
    {
        get
        {
            return _imagesDir;
        }
        set
        {
            _imagesDir = value;
        }
    }

    public Emoticons()
    {
        _imagesDir = System.String.Empty;
        this.ToolTip = "Emoticons\uFFFD";
//        this.ImaIIconOff = "emoticons_off.gif\uFFFD";
//        IconOver = "emoticons_over.gif\uFFFD";
        this.ClientScriptKey = "HTB_EMOTICONS\uFFFD";
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        //this.ClientSideClick = "showEmoticons('\uFFFD" + ParentEditor.ClientID + "', '\uFFFD" + Label + "');\uFFFD";
        this.ClientScriptBlock = "\r\n                <script language='javascript'>\r\n                function showEmoticons(id, caption)\r\n                {\r\n                    var str = ''\r\n                    str += '<table border=\\'0\\' cellspacing=\\'2\\' cellpadding=\\'2\\'>';\r\n                    str += '<tr>' + getEmoticon(id, 'smile.gif', 'Smile');\r\n                    str += getEmoticon(id, 'sad.gif', 'Sad');\r\n                    str += getEmoticon(id, 'shocked.gif', 'Shocked');\r\n                    str += getEmoticon(id, 'biggrin.gif', 'Big Grin') + '</tr>';\r\n                    str += '<tr>' + getEmoticon(id, 'tongue.gif', 'Tongue');\r\n                    str += getEmoticon(id, 'wink.gif', 'Wink');\r\n                    str += getEmoticon(id, 'angry.gif', 'Angry');\r\n                    str += getEmoticon(id, 'blush.gif', 'Blush') + '</tr>';\r\n                    str += '<tr>' + getEmoticon(id, 'crazy.gif', 'Crazy') ;\r\n                    str += getEmoticon(id, 'cry.gif', 'Cry');\r\n                    str += getEmoticon(id, 'doze.gif', 'Doze');\r\n                    str += getEmoticon(id, 'hehe.gif', 'Hehe') + '</tr>';\r\n                    str += '<tr>' + getEmoticon(id, 'laugh.gif', 'Laugh');\r\n                    str += getEmoticon(id, 'plain.gif', 'Plain');\r\n                    str += getEmoticon(id, 'rolleyes.gif', 'Roll Eyes');\r\n                    str += getEmoticon(id, 'satisfied.gif', 'Satisfied') + '</tr>';\r\n                    str += '</table>';\r\n                    showPopup(id, caption, str);\r\n                }\r\n \r\n                function getEmoticon(id, image, label)\r\n                {\r\n                    var str = '';\r\n                    str += '<td ' + getTDRollOver() + '>';\r\n                    str += '<a href=\"javascript:parent.insertImage(\\'' + id + '\\', \\'$IMAGESDIR$' + image + '\\', 0, null, \\'' + label + '\\');\">';\r\n                    str += '<img src=\\'$IMAGESDIR$' + image + '\\' alt=\\'' + label + '\\' border=\\'0\\'>';\r\n                    str += '</a></td>'\r\n                    return str;\r\n                }\r\n\t\t\t\tfunction insertImage(id, loc, bwidth, bcol, alt, align, hspace, vspace, width, height)\r\n\t\t\t\t{\r\n\t\t\t\t\tif (loc.length > 0)\r\n\t\t\t\t\t{\r\n\t\t\t\t\t\tvar image = '<img';\r\n\t\t\t\t\t\tif (loc.length > 0)\r\n\t\t\t\t\t\t\timage += ' src=\"' + loc + '\"';\r\n\t\t\t\t\t\tif (height && height.length > 0)\r\n\t\t\t\t\t\t\timage += ' height=\"' + height + '\"';\r\n\t\t\t\t\t\tif (width && width.length > 0)\r\n\t\t\t\t\t\t\timage += ' width=\"' + width + '\"';\r\n\t\t\t\t\t\tif (bwidth && bwidth.length > 0 && parseInt(bwidth) > 0)\r\n\t\t\t\t\t\t\timage += ' border=\"' + bwidth + '\"';\r\n\t\t\t\t\t\tif (bcol && bcol.length > 0)\r\n\t\t\t\t\t\t\timage += ' style=\"border-color:' + bcol + '\"';\r\n\t\t\t\t\t\tif (alt && alt.length > 0)\r\n\t\t\t\t\t\t\timage += ' alt=\"' + alt + '\"';\r\n\t\t\t\t\t\tif (hspace && hspace.length > 0 && parseInt(hspace) > 0)\r\n\t\t\t\t\t\t\timage += ' hspace=\"' + hspace + '\"';\r\n\t\t\t\t\t\tif (vspace && vspace.length > 0 && parseInt(vspace) > 0)\r\n\t\t\t\t\t\t\timage += ' vspace=\"' + vspace + '\"';\r\n\t\t\t\t\t\tif (align && align.length > 0)\r\n\t\t\t\t\t\t\timage += ' align=\"' + align + '\"';\r\n\t\t\t\t\t\timage += '>';\r\n\t\t\t\t\t\r\n\t\t\t\t\t\tsetSnippet(id, image);\r\n\t\t\t\t\t}\r\n\t\t\t\t\telse\r\n\t\t\t\t\t\thidePopup(id);\r\n\t\t\t\t}\r\n                </script>\r\n            \uFFFD";
        this.ClientScriptBlock = this.ClientScriptBlock.Replace("$IMAGESDIR$\uFFFD", this.ImagesDir);
        base.OnPreRender(e);
    }

} // class Emoticons


