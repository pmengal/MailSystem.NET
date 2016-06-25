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




public partial class DownloadZip : System.Web.UI.Page
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        System.DateTime dateTime = System.DateTime.Now;
        string s1 = "Message_Backup_\uFFFD" + dateTime.ToString("ddMMyy_HHmmss\uFFFD") + ".zip\uFFFD";
        System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
        ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipOutputStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(memoryStream);
        ActiveUp.Net.Mail.Mailbox mailbox = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(Request.QueryString["b\uFFFD"]);
        char[] chArr = new char[] { ',' };
        string[] sArr = Request.QueryString["m\uFFFD"].Split(chArr);
        for (int i = 0; i < sArr.Length; i++)
        {
            string s2 = sArr[i];
            byte[] bArr = mailbox.Fetch.Message(System.Convert.ToInt32(s2));
            ActiveUp.Net.Mail.Header header = ActiveUp.Net.Mail.Parser.ParseHeader(bArr);
            ICSharpCode.SharpZipLib.Zip.ZipEntry zipEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(header.Subject + ".eml\uFFFD");
            zipOutputStream.PutNextEntry(zipEntry);
            zipOutputStream.SetLevel(9);
            zipOutputStream.Write(bArr, 0, bArr.Length);
            zipOutputStream.CloseEntry();
        }
        zipOutputStream.Finish();
        Response.AddHeader("Content-Disposition\uFFFD", "attachment; filename=\uFFFD" + s1);
        Response.ContentType = "application/zip\uFFFD";
        Response.BinaryWrite(memoryStream.GetBuffer());
        zipOutputStream.Close();
    }

}
