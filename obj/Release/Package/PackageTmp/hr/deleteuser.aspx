<%@ Page Title="Delete User" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
CodeBehind="deleteuser.aspx.cs" Inherits="WebApplication4.hr.deleteuser" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2 id="user_name_label" runat="server">
        Delete Users
    </h2>
    <br />
    <asp:Label ID="label_text2" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Button ID="delete_btn" runat="server" Text="Tak, usuń użytkownika" 
        onclick="delete_btn_Click" />
    &nbsp;&nbsp;
    <asp:Button ID="anuluj_btn" runat="server" Text="Anuluj" onclick="anuluj_btn_Click" />
</asp:Content>