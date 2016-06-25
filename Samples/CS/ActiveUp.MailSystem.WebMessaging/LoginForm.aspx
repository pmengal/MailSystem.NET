<%@ Page Language="C#" MasterPageFile="~/Deafalt.master" AutoEventWireup="true" CodeFile="LoginForm.aspx.cs" Inherits="LoginForm" Title="Untitled Page" %>
<%@ Register TagPrefix="AU" Src="~/LoginForm.ascx" TagName="LoginForm"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AU:LoginForm id="login" runat="server" />
</asp:Content>

