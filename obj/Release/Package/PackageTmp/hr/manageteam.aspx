<%@ Page Title="Menage Team" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manageteam.aspx.cs" Inherits="WebApplication4.hr.manageteam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="user_name_label" runat="server">
        Manage Team
    </h2>
    <p>
        <asp:button id="backButton" runat="server" text="Wróć do listy" OnClick="goBack_Click"></asp:button>   
        <asp:Button runat="server" ID="edit_button" Text='Edytuj' OnClick="EditButton_Click"></asp:Button><span class="odstep">&nbsp;</span>
    </p>
        <div><asp:HiddenField ID="HiddenFieldTeamName" runat="server" /><asp:HiddenField ID="HiddenFieldMenager" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" CssClass="failureNotification" ValidationGroup="ValidationSummary3"/>
        <div id="hiddendiv" style="float:left;width:250px;" runat="server"><asp:Label ID="addTeamNameLab" runat="server" Text="Nazwa Teamu: " visible="true" style="padding-top:10px"></asp:Label>
        <asp:TextBox ID="addTeamNameTextBox" runat="server" CssClass="textReadOnly" ReadOnly="true"></asp:TextBox><asp:RequiredFieldValidator
            ID="RequiredFieldValidator1" runat="server" ErrorMessage="Dodaj nazwę teamu" ValidationGroup="ValidationSummary3" CssClass="failureNotification" ControlToValidate="addTeamNameTextBox"> * </asp:RequiredFieldValidator>
        </div>
        <div style="float:left;width:210px">
        <asp:Label ID="DropDownMenagerLab" runat="server" Text="Menager: " Visible="true" ></asp:Label>
        <asp:DropDownList ID="DropDownMenager" runat="server" Enabled="false" AppendDataBoundItems="True"  Width="200px" DataTextField="Name" DataValueField="Id">
                <asp:ListItem Text="Wybierz menagera" Value ="-1"></asp:ListItem>
        </asp:DropDownList><asp:RequiredFieldValidator id="Rfv2" runat="server" ControlToValidate="DropDownMenager" 
                   InitialValue="-1"  ErrorMessage="Wybierz menagera" Display="Dynamic" ValidationGroup="ValidationSummary3" CssClass="failureNotification"> * </asp:RequiredFieldValidator>
        </div>
        <div style="clear: both;">
        <asp:button id="ZapiszButton" runat="server" text="Zapisz" Visible="false" 
                onclick="ZapiszButton_Click" ValidationGroup="ValidationSummary3"></asp:button>
        <asp:button id="AnulujButton" runat="server" text="Anuluj" Visible="false" 
                onclick="AnulujButton_Click" CausesValidation="False"></asp:button>
        </div>
    </div>
        <p>
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="670px" >
                      <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="UserName" DataNavigateUrlFormatString="manageuser.aspx?user={0}" Text="Manage" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Imie i nazwisko" HeaderText="Imie i nazwisko" ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Funkcja" HeaderText="Funkcja" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
          </asp:GridView>
    </p>
</asp:Content>
