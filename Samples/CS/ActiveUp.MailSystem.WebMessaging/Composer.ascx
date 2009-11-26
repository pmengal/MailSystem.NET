<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Composer.ascx.cs" Inherits="Composer" %>
<asp:Label ID="Label1" runat="server" Text="To:"></asp:Label>
<asp:TextBox ID="TextBox1" runat="server" Width="470px" CssClass="formtext"></asp:TextBox><br />
<asp:Label ID="Label2" runat="server" Text="CC:"></asp:Label>
<asp:TextBox ID="TextBox2" runat="server" Width="465px" CssClass="formtext"></asp:TextBox>
<br />
<asp:Label ID="Label3" runat="server" Text="Subject:"></asp:Label>
<asp:TextBox ID="TextBox3" runat="server" Width="441px" CssClass="formtext"></asp:TextBox>&nbsp;<br />
<asp:Label ID="Label4" runat="server" Text="Attachments:"></asp:Label><br />
<asp:FileUpload ID="FileUpload1" runat="server" /><br />
<br />
<asp:TextBox ID="TextBox4" runat="server" Height="381px" Width="529px" CssClass="formtext" TextMode="MultiLine"></asp:TextBox><br />
<br />
<asp:Button ID="Button1" runat="server" Text="Send" />
