<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopNavigation.ascx.cs" Inherits="TopNavigation" %>
<table class="head" <%--style="border-collapse:collapse;width:100%;"--%>>
	<tr>		
		<td class="menu" style="vertical-align:middle;"><asp:LinkButton id="lMove" CssClass="linkmenu" oncommand="Move" runat="server" />&nbsp;<asp:DropDownList id="dBoxes" style="width:150px;" runat="server" /></td>
		<td class="menu"><asp:LinkButton id="lCompose" CssClass="linkmenu" oncommand="Compose" runat="server" OnClick="lCompose_Click" /></td>
		<td class="menu"><asp:LinkButton id="lReply" CssClass="linkmenu" oncommand="Reply" runat="server" OnClick="lReply_Click" /></td>
		<td class="menu"><asp:LinkButton id="lForward" CssClass="linkmenu" oncommand="Forward" runat="server" /></td>
		<td class="menu"><asp:LinkButton id="lDelete" CssClass="linkmenu" oncommand="Delete" runat="server" /></td>
		<td class="menu"><asp:LinkButton id="lMarkAsUnread" CssClass="linkmenu" oncommand="MarkAsUnread" runat="server" /></td>
		<td class="menu"><asp:LinkButton id="lMark" CssClass="linkmenu"  oncommand="Mark" runat="server" /></td>
		<td class="menu"><asp:LinkButton id="lZip" CssClass="linkmenu" oncommand="Zip" runat="server" OnClick="lZip_Click" >Config</asp:LinkButton></td>
		<td class="menu"><asp:LinkButton id="lFolders" CssClass="linkmenu" oncommand="Folders" runat="server" OnClick="lFolders_Click" /></td>
		<td class="menu"><asp:LinkButton id="lSearch" CssClass="linkmenu" oncommand="Search" runat="server" /></td>
		<td class="menu"><asp:LinkButton id="lLogout" CssClass="linkmenu" onCommand="LogOut" runat="server" /></td>
	</tr>
</table>
<table style="width:100%;">
	<tr style="height:16;">
		<td colspan="11" class="underhead" style="width:100%;height:16px;">&nbsp;</td>
	</tr>
</table>