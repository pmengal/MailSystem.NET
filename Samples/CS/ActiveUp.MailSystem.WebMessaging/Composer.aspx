<%@ Page Language="C#" MasterPageFile="~/Deafalt.master" AutoEventWireup="true" CodeFile="Composer.aspx.cs" Inherits="Composer" Title="Untitled Page" %>
<%@ Register TagPrefix="AU" Src="~/Composer.ascx" TagName="ComposerTag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AU:ComposerTag id="ComposerTag" runat="server" />
</asp:Content>

