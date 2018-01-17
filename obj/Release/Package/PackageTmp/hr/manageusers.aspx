<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="manageusers.aspx.cs" Inherits="WebApplication4.manageusers" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <h2>
        Manage Users
    </h2>
    <p>
    <asp:Button runat="server" ID="buttonAdduser" Text='Add user' OnClick="buttonAdduser_click" ></asp:Button>
    </p>
    <p>
        <asp:LinkButton runat="server" ID="unregister_button" Text='Niezarejestrowani' 
                                CommandName='unregister' onclick="unregister_Click"></asp:LinkButton> | <asp:LinkButton runat="server" ID="emploee_button" Text='Pracownicy' CommandName='emploee' onclick="administrators_Click"></asp:LinkButton> | <asp:LinkButton runat="server" ID="menager_button" Text='Menagerowie' CommandName='menager' onclick="administrators_Click"></asp:LinkButton> | <asp:LinkButton runat="server" ID="administrators_button" Text='Administratorzy' 
                                CommandName='Administrators' onclick="administrators_Click"></asp:LinkButton> | <asp:LinkButton runat="server" ID="hremploee_button" Text='Pracownicy kadr' 
                                CommandName='hr_emploee' onclick="administrators_Click"></asp:LinkButton>
    </p>
    <asp:Label ID="info_label" runat="server" Text="" Visible="false" CssClass="failureNotification" Font-Bold="False"></asp:Label><br />
    <asp:Label ID="users_name_label2" runat="server" Font-Bold="true"></asp:Label>
    <p>
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="800px" AllowSorting="True">
                      <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="UserName" DataNavigateUrlFormatString="deleteuser.aspx?deleteuser={0}" Text="Usuń" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Center"/>
                <asp:HyperLinkField DataNavigateUrlFields="UserName" DataNavigateUrlFormatString="manageuser.aspx?user={0}" Text="Manage" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:TemplateField HeaderText="Select" ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="chkRow" runat="server"/>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Imie i nazwisko" HeaderText="Imie i nazwisko" ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Team" HeaderText="Team" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Podstawa urlopowa" HeaderText="Podstawa urlopowa" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Kiedy 26" HeaderText="Kiedy 26" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
          </asp:GridView>
    </p>
        <table style="width:635px;margin-top: -11px;" id="tabele1" runat="server">
            <tr>
                <td style="width: 70px;text-align: center;padding-right: 8px;"><asp:LinkButton runat="server" onclick="Invert_Click">Invert selection</asp:LinkButton></td>
                <td style="width: 15px;text-align: right;padding-right: 8px;"><i class="fa fa-arrow-circle-o-up fa-2x"></i></td>
                <td><asp:Button ID="Button1" runat="server" Text="Change to 26 days" onclick="from20t026_Click"/></td>
                <td style="text-align: center;width: 28px;">|</td>
                <td style="width: 525px;"><div style="display: inline-block;width: 51px;top: 6px;position: relative;"><asp:TextBox ID="TextBoxAdddays" runat="server" style="width: 23px;vertical-align: top;margin-top: 5px;text-align: center;" ReadOnly="True" Text="0"></asp:TextBox>&nbsp; <div style="display: inline-block;width: 0px;margin-left: -5px;"><asp:LinkButton runat="server" onclick="Plus_Click"><i class="fa fa-plus-square fa-fw"></i></asp:LinkButton><asp:LinkButton ID="LinkButton1" runat="server" onclick="Minus_Click"><i class="fa fa-minus-square fa-fw" style="position: relative;top: -4px;"></i></asp:LinkButton></div></div><div style="display: inline-block;position: relative;top: -11px;padding-right: 11px;"><asp:TextBox ID="TextBoxReason" placeholder="Reason?" runat="server" MaxLength="250"  style="width: 150px;vertical-align: top;margin-top: 5px;padding-left: 4px;"></asp:TextBox></div><div style="display: inline-block;position: relative;top: -5px;"><asp:Button ID="Button2" runat="server" Text="Add extra days" onclick="addextraday_Click"/></div></td>
            </tr>
        </table>
</asp:Content>
