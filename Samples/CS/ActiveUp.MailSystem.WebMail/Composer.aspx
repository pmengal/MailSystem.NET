<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Composer.aspx.cs" Inherits="Composer" Title="ActiveWebMessaging&copy;" UICulture="en" Culture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="Content" runat="server" class="Items">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <br />
    <br />
        <asp:Label ID="LabelCompose"  runat="server" Text="<%$ Resources:Composer, composeMailMessage %>" cssclass="CustomLabel"></asp:Label>
    <br />
    <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text="Label"></asp:Label>
    <asp:Label ID="Label5" runat="server" Font-Size="Medium" Text="<%$ Resources:Composer, messageSentConfirm %>"></asp:Label><br />

    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Composer, to %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ToTextBox" runat="server" Width="600px" CssClass="formtext"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ErrorMessage="<%$ Resources:Composer, toFieldRequired %>" Text="<%$ Resources:Composer, required %>" ControlToValidate = "ToTextBox"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Composer, cc %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CCTextBox" runat="server" Width="600px" CssClass="formtext"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Composer, subject %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="SubjectTextBox" runat="server" Width="600px" CssClass="formtext"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Composer, attachment %>"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
    </table>
    
    <br />
    
    <asp:TextBox ID="BodyTextBox" runat="server" Height="200px" Width="699px" CssClass="formtext" TextMode="MultiLine"></asp:TextBox>
    
    <br /><br />
    
    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Composer, send %>" OnClick="Button1_Click" />

</div>

</asp:Content>