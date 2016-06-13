<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginForm.ascx.cs" Inherits="LoginForm" %>
<table>
	<tr>
		<td colspan="2"><img id="imgLogout" runat="server" src="icons/logout_on.gif" align="absmiddle" alt="" visible="true" />&nbsp;&nbsp;<b><asp:Label id="lt106" runat="server" /></b><br /><br /></td>		
	</tr>
	<tr>
		<td><asp:Label id="lt97" runat="server" /> : </td>
		<td><asp:TextBox CssClass="formtext" style="width:150px;" id="iLogin" runat="server" /></td>
	</tr>
	<tr>
		<td><asp:Label id="lt98" runat="server" /> : </td>
		<td><asp:TextBox CssClass="
		" style="width:150px;" id="iPassword" TextMode="Password" runat="server" /></td>
	</tr>
	<tr>
		<td><input type="submit" class="formsubmit" id="iLoginButton" value="Login" onserverclick="Login" runat="server" /></td>
	</tr>
</table>