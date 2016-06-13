<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Inbox.aspx.cs" Inherits="Inbox" Title="ActiveWebMessaging&copy;" UICulture="en" Culture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <asp:GridView ID="GridViewInbox" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="None" AutoGenerateColumns="False" 
        Width="100%" AllowPaging="True" OnPageIndexChanging="GridViewInbox_PageIndexChanging" PageSize="9">
        <PagerSettings FirstPageText="&amp;gt;&amp;gt;" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
<asp:BoundField DataField="From" HeaderText="<%$ Resources:Inbox, from %>">
<ItemStyle Width="10%"></ItemStyle>
</asp:BoundField>
<asp:HyperLinkField HeaderText="<%$ Resources:Inbox, subject %>" DataTextField="Subject" DataNavigateUrlFormatString="~/MailShow.aspx?id={0}&type=inbox" DataNavigateUrlFields="Index">
<ItemStyle Width="70%"></ItemStyle>
</asp:HyperLinkField>
<asp:BoundField DataFormatString="{0:mm/dd/yy}" DataField="Date" HeaderText="<%$ Resources:Inbox, date %>">
<ItemStyle Width="20%"></ItemStyle>
</asp:BoundField>
</Columns>
        <RowStyle BackColor="#EFF3FB" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#005099" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    
</asp:Content>

