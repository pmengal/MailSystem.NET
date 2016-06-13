<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MailboxContent.ascx.cs" Inherits="MailboxContent" %>
<table>
	<tr>
		<td colspan="2"><img src="icons/folders_on.gif" align="middle" alt="" />&nbsp;&nbsp;<b><asp:Label id="lFolder" runat="server" /></b><br /><br /></td>
	</tr>
</table>
<asp:Repeater id="dMailbox" runat="server">
	<HeaderTemplate>
	<table width="100%" frame="box" style="border-color:Gray; border-style:solid; border-width:1px;">
	<tr bgcolor="eeeeee">
		<td class="head"><input type="checkbox" id="check" onclick="checkall()" runat="server" /></td>
		<td class="head"><img src="icons/stamp.gif" alt="stamp.gif" /></td>
		<td class="head"><img src="icons/unread.gif" alt="unread.gif" /></td>
		<td class="head"><asp:Label id="lt6" runat="server" /></td>
		<td class="head"><asp:Label id="lt7" runat="server" /></td>
		<td class="head"><asp:Label id="lt8" runat="server" /></td>
		<td class="head"><asp:Label id="lt9" runat="server" /></td>
	</tr>
	</HeaderTemplate>
	<ItemTemplate>
	<tr>
		<td style="white-space:nowrap"><asp:CheckBox id="select" runat="server" /></td>
		<td style="white-space:nowrap"><asp:Image id="fMark" runat="server" /></td>
		<td style="white-space:nowrap"><asp:Image id="fIcon" runat="server" /></td>
		<td style="white-space:nowrap"><asp:LinkButton id="lMFrom" oncommand="MessageTo" runat="server" /></td>
		<td style="width:100%;"><asp:LinkButton id="lMSubject" oncommand="Navigate" runat="server" /></td>
		<td style="white-space:nowrap; text-transform:capitalize"><%# DataBinder.Eval(Container.DataItem,"Date")%></td>
		<td style="white-space:nowrap"><%# DataBinder.Eval(Container.DataItem,"Size")%></td>
	</tr>
	</ItemTemplate>
	<FooterTemplate>
	</table>
	</FooterTemplate>
</asp:Repeater>
<input type="hidden" id="iCurrent" runat="server" />
<input type="hidden" id="iImap" value="false" runat="server" />
<input type="hidden" id="iServer" runat="server" />
<input type="hidden" id="iUsername" runat="server" />
<input type="hidden" id="iMailboxName" runat="server" />