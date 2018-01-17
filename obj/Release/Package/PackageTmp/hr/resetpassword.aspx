<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="resetpassword.aspx.cs" Inherits="WebApplication4.hr.resetpassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <h3>Retrieve Password</h3><br /><br />

  Username: <asp:DropDownList ID="PersonDropDown" AutoPostBack="true" runat="server"></asp:DropDownList><br /><br />
<%--  <asp:Textbox id="UsernameTextBox" Columns="30" runat="server" AutoPostBack="true" />
            <asp:RequiredFieldValidator id="UsernameRequiredValidator" runat="server"
                                        ControlToValidate="UsernameTextBox" ForeColor="red"
                                        Display="Static" ErrorMessage="Required" />--%>
  <asp:Button id="ResetPasswordButton" Text="Reset Password" 
              OnClick="ResetPassword_OnClick" runat="server" Enabled="false" /><br /><br />

  <asp:Label id="Msg" runat="server" ForeColor="maroon" Font-Size="Medium" /><br /><br />
  <asp:Label id="Msg2" runat="server" ForeColor="maroon" Font-Size="Large" />

</asp:Content>
