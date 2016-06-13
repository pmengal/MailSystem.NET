<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" Title="ActiveWebMessaging&copy;" UICulture="en" Culture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="Content" runat="server" class="Items">

<asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:Label ID="Label3" runat="server" BorderStyle="None" ForeColor="Red" Text="<%$ Resources:Settings, adviceSettings %>"
        Width="370px"></asp:Label><br />
<asp:Label ID="Label1"  runat="server" Text="<%$ Resources:Settings, accountInformation %>" cssclass="CustomLabel"></asp:Label>
<br />
<br />

<table>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Settings, emailAddress %>"></asp:Label>
        </td>
        <td>        
            <asp:TextBox ID="TextBoxEmailAddress" runat="server"  Width="490px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Mail address is required" Text = "reqired" ControlToValidate = "TextBoxEmailAddress"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelPassword" runat="server" Text="<%$ Resources:Settings, password %>"></asp:Label>        
        </td>
        <td>
            <asp:TextBox ID="TextBoxPassword"  runat="server" Width="490px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate = "TextBoxPassword" runat="server" ErrorMessage="Password is required" Text = "required"></asp:RequiredFieldValidator>        
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Settings, displayName %>"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxDisplayName"   runat="server" Width="490px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate = "TextBoxDisplayName" runat ="server" ErrorMessage="Display Name is required" Text = "required"></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<br />
<asp:Label ID="Label7" runat="server" Text="<%$ Resources:Settings, incomingServerInformation %>" cssclass="CustomLabel"></asp:Label>
<br />
<br />

<table>
    <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Settings, incomingMail %>"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="DropDownListIncomingServer" runat="server">
            <asp:ListItem>POP3</asp:ListItem>
            <asp:ListItem>IMAP</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Settings, incomingServer %>"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxIncomingServer"  runat="server" Width="354px"></asp:TextBox>
        </td>
        <td>
            <asp:CheckBox ID="CheckBoxPortIncoming" runat="server" Text="<%$ Resources:Settings, port %>" Checked="True" OnCheckedChanged="CheckBoxPortIncoming_CheckedChanged" AutoPostBack="True"/>
        </td>
        <td>
            <asp:TextBox ID="TextBoxPortIncoming" runat="server" Width="50px"></asp:TextBox>
        </td>
    </tr>
    </table>
    <asp:CheckBox ID="CheckBoxSecureConnection" runat="server" Text="<%$ Resources:Settings, requiresSecureConnection %>" />
    <table>
    <tr>
        <td width=124>
            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Settings, loginID %>"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxLoginID" runat="server" Width="474px" ></asp:TextBox>
        </td>
    </tr>
    </table>
    <br />
    
    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Settings, outgoingServerInformation %>" cssclass="CustomLabel"></asp:Label>
    <br />
    <br />
    
    <table>
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Settings, outgoingServer %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxOutgoingServer" runat="server"  Width="354px"></asp:TextBox>
            </td>
            <td>
                <asp:CheckBox ID="CheckBoxPortOutgoing" runat="server" Text="<%$ Resources:Settings, port %>" Checked="True" AutoPostBack="True" OnCheckedChanged="CheckBoxPortOutgoing_CheckedChanged" />
            </td>
            <td>
                <asp:TextBox ID="TextBoxPortOutgoing" runat="server"  Width="50px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:CheckBox ID="CheckBoxOutgoingSecure" runat="server"
        Text="<%$ Resources:Settings, serverNeedsSecureConnection %>" /><br />
    <asp:CheckBox ID="CheckBoxOutgoingAuthentication"
        runat="server" Text="<%$ Resources:Settings, serverNeedsAuthentication %>" /><br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;&nbsp;
    <br />
    
    
<asp:Button ID="ButtonOK" runat="server" Text="OK" Width="81px" OnClick="ButtonOK_Click"/>

</div>

</asp:Content>

