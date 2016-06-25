<%@ Page Language="C#" MasterPageFile="~/Deafalt.master" AutoEventWireup="true" CodeFile="MessageDisplay.aspx.cs" Inherits="MessageDisplay" Title="Untitled Page" %>
<%@ Register TagPrefix="AU" Src="~/MessageDisplay.ascx" TagName="MessageDisplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AU:MessageDisplay id="display" runat="server" />
</asp:Content>

