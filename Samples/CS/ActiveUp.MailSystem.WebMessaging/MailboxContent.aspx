<%@ Page Language="C#" MasterPageFile="~/Deafalt.master" AutoEventWireup="true" CodeFile="MailboxContent.aspx.cs" Inherits="MailboxContent" Title="Untitled Page" %>
<%@ Register TagPrefix="AU" Src="~/MailboxContent.ascx" TagName="MailboxContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AU:MailboxContent id="content" runat="server" />
</asp:Content>

