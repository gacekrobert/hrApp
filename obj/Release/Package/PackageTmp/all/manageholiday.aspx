<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manageholiday.aspx.cs" Inherits="WebApplication4.all.manageholiday" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="../Scripts/JScript1.js"></script>
        <p style="margin-bottom: 20px;">
        <asp:Button ID="Button1" runat="server" Text="Wróć do listy" OnClick="goBack_Click" />
        </p>
        <asp:label runat="server" id="label_status" style="font-size:x-large;padding: 8px 30px 8px 30px;"></asp:label>
        <h3 runat="server" id="h1" style="margin-top: 25px;"><b>Zarządzaj urlopem:</b></h3>
        <p style="margin-bottom: 20px;">
        <asp:Button ID="butt_delete" runat="server" Text="Usuń urlop" OnClick="delete_Click" />
        <asp:Button ID="butt_app_men" runat="server" Text="Zatwierdź urlop [Menager]" OnClick="apprMenager_Click" />
        <asp:Button ID="butt_app_hr" runat="server" Text="Zatwierdź urlop [HR]" OnClick="apprHR_Click" />
        <asp:Button ID="butt_rej_men" runat="server" Text="Odrzuć urlop [Menager]" OnClick="rejectMenager_Click" />
        <asp:Button ID="butt_rej_hr" runat="server" Text="Odrzuć urlop [HR]" OnClick="rejectHR_Click" />
        </p>
        <h3 class="divbeckground" runat="server" id="h_details"></h3>
        <div class="monthbackground monthbackground2">
        <asp:Table ID="Table2" runat="server" Width="95%" Height="100%">
        <asp:TableRow Height="20px">
        <asp:TableCell HorizontalAlign="Center">ID</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Imię i nazwisko</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Od:</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Do:</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Menager</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Ilość dni</asp:TableCell>
        <asp:TableCell HorizontalAlign="Center">Ilość dni (+ weekendy i święta)</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Height="20px">
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="l_id" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="l_name" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="l_od" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="l_do" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="l_menager" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="l_ilość_dni" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="l_losc_dni_all" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        <h3 runat="server" id="h3"><b>Historia urlopu</b></h3>
            <p>
          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="900px" >
                      <Columns>
                <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-Width="20px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Akcja" HeaderText="Akcja" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Osoba" HeaderText="Osoba" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
          </Columns>
          </asp:GridView>
            </p>
        </div>
        <h3 class="divbeckground" runat="server" id="h_userdays"></h3>
        <div class="monthbackground monthbackground2">
        <asp:Table ID="Table3" runat="server" Width="80%">
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
        <h3 runat="server" id="h2"><b>Ostatnie urlopy</b></h3>
            <p>
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="900px" >
                      <Columns>
                <asp:BoundField DataField="From" HeaderText="Od" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="To" HeaderText="Do" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Rodzaj" HeaderText="Rodzaj" ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ManagerId" HeaderText="Manager" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:BoundField DataField="Ilosc_dni" HeaderText="Ilość dni" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
          </asp:GridView>
            </p>
        </div>
        <h3 class="divbeckground" runat="server" id="h1_kalendarz">Kalendarz urlopów</h3>
        <div class="monthbackground monthbackground_hide" runat="server" id="bigDiv">
            <div id="l0" runat="server" class="divbeckground2"></div>
            <div id="m0" runat="server" class="monthbackground"></div><br />
            <div id="l1" runat="server" class="divbeckground2"></div>
            <div id="m1" runat="server" class="monthbackground"></div><br />
            <div id="l2" runat="server" class="divbeckground2"></div>
            <div id="m2" runat="server" class="monthbackground"></div><br />
        </div>
</asp:Content>
