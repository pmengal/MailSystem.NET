<%@Page Language="C#" Inherits="_Default" CodeFile="~/Default2.aspx.cs" %>
<%@ Register TagPrefix="AU" Src="~/MailboxContent.ascx" TagName="MailboxContent" %>
<%@ Register TagPrefix="AU" Src="~/FoldersTree.ascx" TagName="FoldersTree" %>
<%@ Register TagPrefix="AU" Src="~/TopNavigation.ascx" TagName="TopNavigation" %>
<%@ Register TagPrefix="AU" Src="~/MessageDisplay.ascx" TagName="MessageDisplay" %>
<%@ Register TagPrefix="AU" Src="~/Composer.ascx" TagName="Composer" %>
<%@ Register TagPrefix="AU" Src="~/FoldersManagement.ascx" TagName="FoldersManagement" %>
<%@ Register TagPrefix="AU" Src="~/SearchEngine.ascx" TagName="SearchEngine" %>
<%@ Register TagPrefix="AU" Src="~/LoginForm.ascx" TagName="LoginForm"  %>
<%@ Register TagPrefix="AU" Src="~/ConfigurationEmailAccount.ascx" TagName="ConfigurationEmailAccount"  %>
<html>

<head>
<title>Active WebMessaging - BETA 1</title>
<link rel="stylesheet" media="screen" type="text/css" href="WebMessaging.css" />

<script src="/CheckAll.js" type="text/javascript"></script>
<script src="/ErrorDialog.js" type="text/javascript"></script>
<script src="/ImagesHandler.js" type="text/javascript"></script>
<script src="/Headers.js" type="text/javascript"></script>
<script src="/PopUp.js" type="text/javascript"></script>
<script src="/CheckForm.js" type="text/javascript"></script>
</head>

<body style="margin-left:0; margin-top:0; height:100%;">
<div visible="false" style="position:absolute; z-index:2;top:-130px;left:-350px;filter:Alpha(Opacity=0);" id="ErrorDialog">
<table width="350" style="border-width:1px;border-style:solid;border-color:#cccccc;" cellspacing="0" cellpadding="3">
	<tr>
		<td style="text-align:center;background-image:url(icons/bk-head.gif);"><img src="icons/error.gif" alt="Error" /></td>
		<td id="ErrorMessage" style="text-align:center;background-image:url(icons/bk-head.gif);font-family:verdana;font-weight:bold;font-size:10px;"></td>
	</tr>
</table>
</div>
<form id="Form1" enctype="multipart/form-data" runat="server" action="Default.aspx">
<table style="border-collapse:collapse; width:100%;height:100%;" cellpadding="0" cellspacing="0">
	<tr>
		<td style="vertical-align:top;">
<table style="border-collapse:collapse; width:100%;" cellpadding="0" cellspacing="0">
	<tr>
		<td colspan="2" style="vertical-align:top; height: 58px;"><AU:TopNavigation id="topnav" runat="server" /></td>
	</tr>
	<tr>
		<td style="white-space:nowrap;width:15%;vertical-align:top;"><AU:FoldersTree id="tree" runat="server" /></td>
		<td style="vertical-align:top;"><AU:LoginForm id="login" Visible="False" runat="server" /><AU:ConfigurationEmailAccount id="config" Visible="True" runat="server" /><AU:SearchEngine id="search" Visible="False" runat="server" /><AU:MailboxContent id="content" Visible="False" runat="server" /><AU:MessageDisplay id="display" Visible="False" runat="server" /><AU:FoldersManagement id="folders" Visible="False" runat="server" /><AU:Composer id="composer" Visible="False" runat="server" /></td>
	</tr>
</table>
		</td>
	</tr>
	<tr>
		<td style="vertical-align:bottom">
		</td>
	</tr>
</table>
</form>
</body>
</html>