<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConfigurationEmailAccount.ascx.cs" Inherits="ConfigurationEmailAccount" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<script  type="text/javascript">

function clean_String(S){
 var digits = "0123456789";
 var temp = "";
 var digit = "";
    for (var i=0; i<S.length; i++){
      digit = S.charAt(i);
      if (digits.indexOf(digit)>=0){temp=temp+digit}
    }
   return temp
}

</script>

<td style="width: 415px; height: 14px">
   &nbsp; &nbsp; &nbsp; &nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
<asp:Label ID="Label1" runat="server" Text="Account Information:" Font-Bold="True" ForeColor="#0000C0"></asp:Label>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
   <asp:Label ID="Label2" runat="server" Text="Email Address: "></asp:Label>&nbsp;
    <asp:TextBox ID="TextBoxEmailAddress" runat="server" Width="354px"></asp:TextBox>
    <asp:Label ID="ErrorEmailAddress" runat="server" ForeColor="Red"></asp:Label><br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp;<asp:Label ID="Label3" runat="server" BackColor="White" BorderColor="White"
        ForeColor="Blue" Text=" abc@xyz.com" Width="99px"></asp:Label>
    <br />
    <br />
    <asp:Label ID="LabelPassword" runat="server" Text="Password: "></asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:TextBox ID="TextBoxPassword" runat="server" Width="354px"></asp:TextBox>
    <asp:Label ID="ErrorPassword" runat="server" ForeColor="Red"></asp:Label><br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="How should your name appear on the emails sent from this account? "></asp:Label><br />
    <asp:Label ID="Label5" runat="server" Text="Display Name:"></asp:Label>&nbsp;
    <asp:TextBox ID="TextBoxDisplayName" runat="server" Width="354px"></asp:TextBox>
    <asp:Label ID="ErrorDispName" runat="server" ForeColor="Red"></asp:Label>&nbsp;<br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label6" runat="server" Text="For example: John Smith"></asp:Label>&nbsp;<br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label7" runat="server" ForeColor="#0000C0" Text="Incoming Server Information:" Font-Bold="True"></asp:Label><br />
    &nbsp;<br />
    <asp:Label ID="Label8" runat="server" Text="Incoming mail"></asp:Label>&nbsp;&nbsp;
    &nbsp;
    <asp:DropDownList ID="DropDownListIncomingServer" runat="server">
        <asp:ListItem>POP3</asp:ListItem>
        <asp:ListItem>IMAP</asp:ListItem>
    </asp:DropDownList>
    
    <br />
    <asp:Label ID="Label10" runat="server" Text="Incoming server: "></asp:Label>
    <asp:TextBox ID="TextBoxIncomingServer" runat="server" Height="21px" Width="354px" CssClass="formtext"></asp:TextBox>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:CheckBox ID="CheckBoxPortIncoming" runat="server" Text="Port:" Checked="True" OnCheckedChanged="CheckBoxPortIncoming_CheckedChanged" AutoPostBack="True" />
    <asp:TextBox ID="TextBoxPortIncoming" runat="server" Height="21px" Width="80px" CssClass="formtext"></asp:TextBox><br />
    </ContentTemplate>
    </asp:UpdatePanel>
    
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
    &nbsp;
    <asp:CheckBox
        ID="CheckBoxSecureConnection" runat="server" Text="    This server requires a secure connection." /><br />
    <br />
    <asp:Label ID="Label11" runat="server" Text="Login ID:"></asp:Label>
    <asp:TextBox ID="TextBoxLoginID" runat="server" Height="22px" Width="354px" CssClass="formtext"></asp:TextBox><br />
    &nbsp;
    &nbsp;&nbsp;<br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; <asp:Label ID="Label12" runat="server"
        Font-Bold="True" ForeColor="#0000C0" Text="Outgoing Server Information:"></asp:Label><br />
    <br />
    <asp:Label ID="Label13" runat="server" Text="Outgoing server:"></asp:Label>
    <asp:TextBox ID="TextBoxOutgoingServer" runat="server" Width="354px" CssClass="formtext"></asp:TextBox>
    <asp:CheckBox ID="CheckBoxPortOutgoing" runat="server" Text="Port:" Checked="True" OnCheckedChanged="CheckBoxPortOutgoing_CheckedChanged" AutoPostBack="True" />
    <asp:TextBox ID="TextBoxPortOutgoing" runat="server" Height="21px" Width="80px" CssClass="formtext"></asp:TextBox><br />
    <br />
    <asp:CheckBox ID="CheckBoxOutgoingSecure" runat="server"
        Text="  My outgoing server needs secure connection (SSL)." /><br />
    <asp:CheckBox ID="CheckBoxOutgoingAuthentication"
        runat="server" Text="     My outgoing server needs authentication." /><br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp;<asp:Button ID="ButtonOK" runat="server" Text="OK" Width="81px" OnClick="ButtonOK_Action" />
    &nbsp;
    <br />



