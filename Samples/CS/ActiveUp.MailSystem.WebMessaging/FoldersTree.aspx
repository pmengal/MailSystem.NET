<%@ Page Language="C#" MasterPageFile="~/Deafalt.master" AutoEventWireup="true" CodeFile="FoldersTree.aspx.cs" Inherits="FoldersTree" Title="Untitled Page" %>
<%@ Register TagPrefix="AU" Src="~/FoldersTree.ascx" TagName="FoldersTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AU:FoldersTree id="tree" runat="server" />
</asp:Content>