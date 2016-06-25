<%@ Page Language="C#" MasterPageFile="~/Deafalt.master" AutoEventWireup="true" CodeFile="FoldersManagement.aspx.cs" Inherits="FoldersManagement" Title="Untitled Page" %>
<%@ Register TagPrefix="AU" Src="~/FoldersManagement.ascx" TagName="FoldersManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AU:FoldersManagement id="folders" Visible="True" runat="server" />
</asp:Content>

