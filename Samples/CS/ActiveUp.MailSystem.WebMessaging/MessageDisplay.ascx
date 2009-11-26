<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageDisplay.ascx.cs" Inherits="MessageDisplay" %>
<table>
	<tr>
		<td colspan="2"><img src="icons/markunread_on.gif" align="absmiddle" alt="" />&nbsp;&nbsp;<b><asp:Label id="lt105" runat="server" /></b><br /><br /></td>
	</tr>
</table>
<table cellspacing="0" cellpadding="0">
	<tr>
		<td valign="top">
<table cellspacing="0" cellpadding="0">
	<tr>
		<td style="width:500px;">
<table style="width:500px;">
	<tr>
		<td><b><asp:Label id="lt6" runat="server" />: </b></td>
		<td><asp:Label id="lFrom" runat="server" /></td>
	</tr>
	<tr>
		<td valign="top"><b><asp:Label id="lt22" runat="server" />: </b></td>
		<td><asp:Label id="lTo" runat="server" /><asp:Label id="lCc" runat="server" />
	<tr>
		<td valign="top"><b><asp:Label id="lt8" runat="server" />: </b></td>
		<td><asp:Label id="lDate" Style="text-transform:capitalize;" runat="server" /></td>
	</tr>
	<tr>
		<td><b><asp:Label id="lt7" runat="server" />: </b></td>
		<td><asp:Image id="fMarked" src="icons/marked.gif"  runat="server" />&nbsp;<asp:Image id="fIcon" runat="server" />&nbsp;<asp:Label id="lSubject" runat="server" /></td>
	</tr>
</table>
<div id="headers" style="visibility:hidden;position:absolute;z-index:-2;">
<table style="width:500px;">
	<asp:Literal id="lHeaders" runat="server" />
</table>
</div>
<span style="cursor:hand;" onclick="ShowHeaders()"><img id="more" src="icons/tree-close-h.gif" alt="Show all headers" /></span>
<table>
	<tr>
		<td id="headersDisplay"></td>
	</tr>
</table>
<table style="width:500px;">
	<tr>
		<td colspan="2"><asp:Repeater id="rAttach" runat="server">
	<HeaderTemplate>
	<table>
	</HeaderTemplate>
	<ItemTemplate>
	<tr>
		<td><%# DataBinder.Eval(Container.DataItem,"col")%></td>
		<td><%# DataBinder.Eval(Container.DataItem,"filename")%></td>
		<td><%# DataBinder.Eval(Container.DataItem,"size")%></td>
	</tr>
	</ItemTemplate>
	<FooterTemplate>
	</table>
	</FooterTemplate>
</asp:Repeater><br /><asp:LinkButton id="lBack"  onmouseover="rollimg('back', 'icons/folders_on.gif'); return true;" onmouseout="rollimg('back', 'icons/folders_off.gif'); return true;" oncommand="Navigate" runat="server" /><asp:LinkButton id="lPrevious" onmouseover="rollimg('previous', 'icons/previous_on.gif'); return true;" onmouseout="rollimg('previous', 'icons/previous_off.gif'); return true;" oncommand="Navigate" runat="server" /><asp:LinkButton id="lNext" onmouseover="rollimg('next', 'icons/next_on.gif'); return true;" onmouseout="rollimg('next', 'icons/next_off.gif'); return true;" oncommand="Navigate" runat="server" /><br />
		</td>
	</tr>
</table>
		</td>
	</tr>
</table>
		</td>
	</tr>
	<tr>
		<td colspan="2"><hr /><br /><asp:Label id="lBody" runat="server" /></td>
	</tr>
</table>
<input type="hidden" id="iCurrent" runat="server" />
<input type="hidden" id="iImap" value="false" runat="server" />
<input type="hidden" id="iServer" runat="server" />
<input type="hidden" id="iUsername" runat="server" />
<input type="hidden" id="iMailboxName" runat="server" />
<asp:Literal id="lCss" runat="server" />