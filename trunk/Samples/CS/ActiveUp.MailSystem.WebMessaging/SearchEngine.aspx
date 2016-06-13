<%@ Page Language="C#" MasterPageFile="~/Deafalt.master" AutoEventWireup="true" CodeFile="SearchEngine.aspx.cs" Inherits="SearchEngine" Title="Untitled Page" %>
<%@ Register TagPrefix="AU" Src="~/SearchEngine.ascx" TagName="SearchEngine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AU:SearchEngine id="search" runat="server" />
</asp:Content>

