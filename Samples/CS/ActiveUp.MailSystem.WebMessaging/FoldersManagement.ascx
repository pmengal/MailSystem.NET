<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FoldersManagement.ascx.cs" Inherits="FoldersManagement" %>
<table>
	<tr>
		<td colspan="2" style="height: 72px"><img src="icons/folders_on.gif" align="absmiddle" alt="" />&nbsp;&nbsp;<b><asp:Label id="lt104" runat="server" /></b><br /><br /></td>
	</tr>
</table>
<asp:Panel id="pFolders" runat="server">
<asp:LinkButton Text="Add" id="lAddFolders" oncommand="AddFolders" runat="server" /><br /><br />
<asp:Repeater id="rFoldersListing" runat="server">
	<HeaderTemplate>
		<table>
			<tr>
				<td><asp:Label id="lt17" runat="server" /></td>
				<td><asp:Label id="lt18" runat="server" /></td>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr>
	 		<td><img src="icons/de.gif" align="absmiddle" style="width:16px;height:16px;" alt="" />&nbsp;<%# DataBinder.Eval(Container.DataItem,"Mailbox")%></td>
			<td><asp:LinkButton id="lModify" oncommand="ModifyFolders" Text="Modify" runat="server" /> - <asp:LinkButton id="lDelete" oncommand="DeleteFolders" Text="Delete" runat="server" /> - <asp:LinkButton id="lChild" oncommand="CreateChild" Text="Create child" runat="server" /></td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
		</table>
	</FooterTemplate>
</asp:Repeater>
</asp:Panel>

<asp:Panel id="pFoldersForm" Visible="false" runat="server">
<table>
	<tr>
		<td><asp:Label id="lt13" runat="server" /> : </td>
		<td><asp:TextBox id="iMailboxName" class="formtext" Size="40" runat="server" /></td>
	</tr>
	<tr>
		<td><input type="submit" id="iFoldersSubmit" class="formsubmit" value="Save" onserverclick="DoModifyFolders" runat="server" /><input type="hidden" id="iId" runat="server" /></td>
	</tr>
</table>
</asp:Panel>
<asp:Label id="lError" runat="server" />