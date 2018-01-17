<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="deleteteam.aspx.cs" Inherits="WebApplication4.hr.deleteteam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="user_name_label" runat="server">
        Delete Team
    </h2>
    <br />
    <asp:Label ID="label_text2" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Button ID="delete_btn" runat="server" Text="Tak, usuń team" 
        onclick="delete_btn_Click" />
    &nbsp;&nbsp;
    <asp:Button ID="anuluj_btn" runat="server" Text="Anuluj" onclick="anuluj_btn_Click" />
</asp:Content>
