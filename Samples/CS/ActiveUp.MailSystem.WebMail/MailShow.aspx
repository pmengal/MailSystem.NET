<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MailShow.aspx.cs" Inherits="MailShow" Title="ActiveWebMessaging&copy;" UICulture="en" Culture="en-US" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="Content" runat="server" class="Items">
<br />
    &nbsp;<table>
        <tr>
            <td>
                <asp:Label id="Label5" runat="server" Text="<%$ Resources:MailShow, from %>"></asp:Label></td>
            <td>
                <asp:Label id="FromLabel" runat="server" Width="606px"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:MailShow, to %>"></asp:Label>
            </td>
            <td>
                &nbsp;<asp:Label id="ToLabel" runat="server" Width="606px"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:MailShow, cc %>"></asp:Label>
            </td>
            <td>
                &nbsp;<asp:Label id="CCLabel" runat="server" Width="606px"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:MailShow, subject %>"></asp:Label>
            </td>
            <td>
                &nbsp;<asp:Label id="SubjectLabel" runat="server" Width="606px"></asp:Label></td>
        </tr>
        <tr>
            <td style="height: 18px">
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:MailShow, attachment %>"></asp:Label>
            </td>
            <td style="height: 18px">
                &nbsp;<asp:Repeater id="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                <Itemtemplate>
                <asp:LinkButton ID="test" CommandArgument='<%#Eval("FileName")%>' runat="server" CommandName="Download" >Download</asp:LinkButton>
                <asp:Label ID="Nome" runat="server" Text='<%#Eval("FileName")%>'></asp:Label>
                </Itemtemplate>                                
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <br />
    
    <span id="MailContent" runat="server" visible="true">
    </span>
    
    
    
    &nbsp;<br /><br />
    <asp:LinkButton id="ForwardButton" runat="server" OnClick="ForwardButton_Click" Text="<%$ Resources:MailShow, forward %>" />
    <asp:LinkButton id="ReplyButton" runat="server" OnClick="ReplyButton_Click" Text="<%$ Resources:MailShow, reply %>" />
    <asp:LinkButton id="ReplyAllButton" runat="server" OnClick="ReplyAllButton_Click" Text="<%$ Resources:MailShow, replyAll %>" /></div>

</asp:Content>

