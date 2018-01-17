<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="myaccount.aspx.cs" Inherits="WebApplication4.all.myaccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="user_name_label" runat="server">
        Your Account
    </h2>
    <asp:HiddenField ID="HiddenFielduserid" runat="server" />
    <asp:Label ID="info_label" runat="server" Text="" Visible="false" CssClass="failureNotification" style="display: block;margin-top: 13px;"></asp:Label>
    <p>
        <asp:Button runat="server" ID="ButtonResetPassword" Text='Change Password' OnClick="reset_Click"></asp:Button><span class="odstep">&nbsp;</span>
    </p>
    <p>
        <asp:Table ID="Table1" runat="server" Width="80%">
        <asp:TableRow>
        <asp:TableCell HorizontalAlign="Center"></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell HorizontalAlign="Center">Poprzedni rok</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Obecny rok</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Dodatkowe dni</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Dni wykorzystane</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Dostępne</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">W tym dni na żądanie</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Następny rok</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="Labelpr" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="Labelor" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="Labeldd" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="Labelwyk" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center" ID="suncell" runat="server"><asp:label ID="Labelsum" runat="server" Font-Bold="True" Font-Size="Large"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="Labelnz" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="Labelnr" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        </asp:TableRow>
        </asp:Table>
    </p>
    <p>
        <asp:Label ID="ImieLabel" runat="server" Text="Imię: "></asp:Label>
        <br /><asp:TextBox ID="ImieTextBox" runat="server" CssClass="textReadOnly" ReadOnly="true"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="NazwiskoLabel" runat="server" Text="Nazwisko: "></asp:Label>
        <br /><asp:TextBox ID="NazwiskoTextBox" runat="server" CssClass="textReadOnly" ReadOnly="true"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label_email" runat="server" Text="E-mail adress: "></asp:Label>
        <br /><asp:TextBox ID="TextBox_email" runat="server" CssClass="textReadOnly" ReadOnly="true"></asp:TextBox>
    </p>
    <div class="funkcje">
    <fieldset class="login">
                    <legend>Funkcje</legend>
                    <p>
                        <asp:CheckBox ID="Pracownik" Enabled="false" runat="server"/>
                        <asp:Label ID="PracownikLabel" runat="server" AssociatedControlID="Pracownik" CssClass="inline">Pracownik</asp:Label><span class="odstep">&nbsp;</span>
                        <asp:CheckBox ID="PracownikHR" Enabled="false" runat="server"/>
                        <asp:Label ID="PracownikHRLabel" runat="server" AssociatedControlID="PracownikHR" CssClass="inline">Pracownik kadr</asp:Label><span class="odstep">&nbsp;</span>
                        <asp:CheckBox ID="PracownikMenager" Enabled="false" runat="server"/>
                        <asp:Label ID="PracownikMenagerLabel" runat="server" AssociatedControlID="PracownikMenager" CssClass="inline">Menager</asp:Label><span class="odstep">&nbsp;</span>
                        <asp:CheckBox ID="PracownikAdmin" Enabled="false" runat="server"/>
                        <asp:Label ID="PracownikAdminLabel" runat="server" AssociatedControlID="PracownikAdmin" CssClass="inline">Admin</asp:Label>
                    </p>
    </fieldset>
    </div>
    <p>
        <asp:Label ID="PeselLabel" runat="server" Text="Pesel: "></asp:Label>
        <br /><asp:TextBox ID="PeselTextBox" runat="server" CssClass="textReadOnly" ReadOnly="true"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="DataurodzeniaLabel" runat="server" Text="Data urodzenia: "></asp:Label>
        <br /><asp:TextBox ID="Dataurodzenia" CssClass="textReadOnly" ReadOnly="true" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="DataZatrudnieniaLabel" runat="server" Text="Data zatrudnienia: "></asp:Label>
        <br /><asp:TextBox ID="DataZatrudnienia" CssClass="textReadOnly" ReadOnly="true" runat="server"></asp:TextBox>    
    </p>
    <p>
          <asp:Label ID="TeamLabel" runat="server" Text="Team: "></asp:Label>
        <br /><asp:TextBox ID="TeamTextBox" CssClass="textReadOnly" ReadOnly="true" runat="server"></asp:TextBox> 
    </p>
        <div style="float:left; width:260px; height:55px;"><asp:Label ID="DropDownList2label" runat="server" Text="Ilość dni urlopowych w roku: " Visible="True"></asp:Label><br />
        <asp:TextBox ID="DropDownList2TextBox" CssClass="textReadOnly" ReadOnly="true" runat="server"></asp:TextBox>
        </div>
        <div style="float:left; width:360px; height:55px;"><asp:Label ID="uzyska26label" runat="server" Text="Data uzyskania 26 dni urlopowych: " Visible="false"></asp:Label><br />
        <asp:TextBox ID="uzyska26TextBox" Visible="false" CssClass="textReadOnly" ReadOnly="true" runat="server"></asp:TextBox>
        </div>
</asp:Content>
