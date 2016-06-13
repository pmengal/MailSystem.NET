<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchEngine.ascx.cs" Inherits="SearchEngine" %>
<%@ Register TagPrefix="AU" Namespace="ActiveUp.WebControls" Assembly="ActiveUp.WebControls" %>
<table>
	<tr>
		<td colspan="2"><img src="icons/search_on.gif" align="middle" alt="" />&nbsp;&nbsp;<b><asp:Label id="lt102" runat="server" /></b><br /><br /></td>
	</tr>	
	<tr>
		<td colspan="2"><asp:Label id="lt69" runat="server" /> : <asp:TextBox id="iQuick" CssClass="formtext" runat="server" />&nbsp;&nbsp;<input type="submit" id="iSubmitQuick" value="Search" class="formsubmit" onserverclick="SearchQuick" runat="server" /></td>
	</tr>
	<tr>
		<td colspan="2"><hr /></td>
	</tr>
	<tr>
		<td colspan="2"><asp:Label id="lt101" runat="server" /> : </td>
	</tr>
	<tr>
		<td colspan="2"><asp:Label id="lt46" runat="server" />&nbsp;* <asp:DropDownList id="dBoxes" style="vertical-align:middle;" runat="server" /> : </td>
	</tr>
	<tr>
		<td><asp:Label id="lt6" runat="server" /> : </td>
		<td><asp:TextBox id="iFrom" CssClass="formtext" runat="server" /></td>
	</tr>
	<tr>
		<td><asp:Label id="lt22" runat="server" /> : </td>
		<td><asp:TextBox id="iTo" CssClass="formtext" runat="server" /></td>
	</tr>
	<tr>
		<td><asp:Label id="lt23" runat="server" /> : </td>
		<td><asp:TextBox id="iCc" CssClass="formtext" runat="server" /></td>
	</tr>
	<tr>
		<td><asp:Label id="lt47" runat="server" /> </td>
		<td><table><tr><td><asp:CheckBox id="cBefore" runat="server" /><asp:Label id="lt50" runat="server" /></td><td><AU:Calendar id="iBefore" Format="month;-;day;-;year" runat="server" /></td></tr></table><table><tr><td><asp:CheckBox id="cOn" runat="server" /><asp:Label id="lt51" runat="server" /></td><td><AU:Calendar id="iOn" Format="month;-;day;-;year" runat="server" /></td></tr></table><table><tr><td><asp:CheckBox id="cAfter" runat="server" /><asp:Label id="lt52" runat="server" /></td><td><AU:Calendar id="iAfter" Format="month;-;day;-;year" runat="server" /></td></tr></table></td>
	</tr>
	<tr>
		<td><asp:Label id="lt48" runat="server" /> : </td>
		<td><asp:TextBox id="iSubject" CssClass="formtext" runat="server" /></td>
	</tr>
	<tr>
		<td><asp:Label id="lt49" runat="server" /> : </td>
		<td><asp:TextBox id="iBody" CssClass="formtext" runat="server" /></td>
	</tr>
	<tr>
		<td><input type="submit" id="iSubmit" class="formsubmit" value="Search" onserverclick="Search" runat="server" /></td><td></td>
	</tr>
	<tr>
		<td>*&nbsp;<asp:Label id="lt67" Style="font-size:9px;" runat="server" />.<br /><br /></td>
	</tr>
</table>