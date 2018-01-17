<%@ Page Title="Menage teams" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="menageteams.aspx.cs" Inherits="WebApplication4.hr.menageteams" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>
        Manage Teams
    </h2>
    <p><asp:button id="addButton" runat="server" text="Dodaj Team" onclick="addButton_Click" style="margin-bottom:10px;"></asp:button></p>
    <asp:Label ID="info_label" runat="server" Visible="False" 
        CssClass="failureNotification"></asp:Label>
    <div>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" CssClass="failureNotification" ValidationGroup="ValidationSummary2"/>
        <div id="hiddendiv" style="float:left;width:250px;height:0px" runat="server"><asp:Label ID="addTeamNameLab" runat="server" Text="Nazwa Teamu: " visible="false" style="padding-top:10px"></asp:Label>
        <asp:TextBox ID="addTeamNameTextBox" runat="server" visible="false" CssClass="textEntry2"></asp:TextBox><asp:RequiredFieldValidator
            ID="RequiredFieldValidator1" runat="server" ErrorMessage="Dodaj nazwę teamu" ValidationGroup="ValidationSummary2" CssClass="failureNotification" ControlToValidate="addTeamNameTextBox"> * </asp:RequiredFieldValidator>
        </div>
        <div style="float:left;width:210px">
        <asp:Label ID="DropDownMenagerLab" runat="server" Text="Menager: " Visible="false"></asp:Label>
        <asp:DropDownList ID="DropDownMenager" runat="server" Enabled="true" AppendDataBoundItems="True" Visible="false" Width="200px" DataTextField="Name" DataValueField="Id">
                <asp:ListItem Text="Wybierz menagera" Value ="-1"></asp:ListItem>
        </asp:DropDownList><asp:RequiredFieldValidator id="Rfv2" runat="server" ControlToValidate="DropDownMenager" 
                   InitialValue="-1"  ErrorMessage="Wybierz menagera" Display="Dynamic" ValidationGroup="ValidationSummary2" CssClass="failureNotification"> * </asp:RequiredFieldValidator>
        </div>
        <div style="clear: both;">
        <asp:button id="ZapiszButton" runat="server" text="Zapisz" Visible="false" 
                onclick="ZapiszButton_Click" ValidationGroup="ValidationSummary2"></asp:button>
        <asp:button id="AnulujButton" runat="server" text="Anuluj" Visible="false" 
                onclick="AnulujButton_Click" CausesValidation="False"></asp:button>
        </div>

    </div>

    <p>
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="600px" >
                      <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id,TeamName" DataNavigateUrlFormatString="deleteteam.aspx?deleteteamid={0}&deleteteamName={1}" Text="Usuń" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Center"/>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="manageteam.aspx?teamid={0}" Text="Manage" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="TeamName" HeaderText="Team Name" ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Menager" HeaderText="Menager" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Count" HeaderText="Liczba pracowników" ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" Visible="false"/>
            </Columns>
          </asp:GridView>
    </p>
</asp:Content>
