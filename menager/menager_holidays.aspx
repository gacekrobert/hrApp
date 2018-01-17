<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="menager_holidays.aspx.cs" Inherits="WebApplication4.menager.menager_holidays" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="../Scripts/JScript1.js"></script>

    <asp:Table ID="Table1" runat="server">
<%--    <asp:TableRow>
    <asp:TableCell>Pokaż urlopy dla: </asp:TableCell>
    <asp:TableCell><asp:DropDownList ID="TeamDropDown" OnSelectedIndexChanged="TeamDropDown_Change" AutoPostBack="true" runat="server" Visible="false"></asp:DropDownList></asp:TableCell>
    <asp:TableCell>lub</asp:TableCell>
    <asp:TableCell><asp:DropDownList ID="PersonDropDown" OnSelectedIndexChanged="PersonDropDown_Change" AutoPostBack="true" runat="server" Visible="false"></asp:DropDownList></asp:TableCell>
    </asp:TableRow>--%>
    </asp:Table>
    <h3 class="divbeckground" runat="server" id="h_AppMen">Do zatwierdzenia przez menagera</h3>
        <div class="monthbackground">
          <asp:GridView ID="GridViewAppMen" runat="server" AutoGenerateColumns="False" Width="900px">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="menager_holiday.aspx?holidayid={0}" Text="Szczegóły" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="From" HeaderText="Od" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="To" HeaderText="Do" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Pracownik" HeaderText="Pracownik" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ManagerId" HeaderText="Manager" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Rodzaj" HeaderText="Rodzaj" ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:BoundField DataField="Ilosc_dni" HeaderText="Ilość dni" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
          </asp:GridView>
    </div>  
    <h3 class="divbeckground" runat="server" id="h_AppHR">Do zatwierdzenia przez dział HR</h3>
        <div class="monthbackground">
          <asp:GridView ID="GridViewAppHR" runat="server" AutoGenerateColumns="False" Width="900px">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="menager_holiday.aspx?holidayid={0}" Text="Szczegóły" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="From" HeaderText="Od" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="To" HeaderText="Do" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Pracownik" HeaderText="Pracownik" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ManagerId" HeaderText="Manager" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Rodzaj" HeaderText="Rodzaj" ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:BoundField DataField="Ilosc_dni" HeaderText="Ilość dni" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
          </asp:GridView>
    </div> 
    <h3 class="divbeckground" runat="server" id="h_Zat">Urlopy zatwierdzone</h3>
         <div class="monthbackground">
          <asp:GridView ID="GridViewZat" runat="server" AutoGenerateColumns="False" Width="900px" >
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="menager_holiday.aspx?holidayid={0}" Text="Szczegóły" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="From" HeaderText="Od" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="To" HeaderText="Do" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Pracownik" HeaderText="Pracownik" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ManagerId" HeaderText="Manager" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Rodzaj" HeaderText="Rodzaj" ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:BoundField DataField="Ilosc_dni" HeaderText="Ilość dni" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
          </asp:GridView>
    </div>  
    <h3 class="divbeckground" runat="server" id="h_OK">Urlopy wykorzystane</h3>
        <div class="monthbackground monthbackground_hide">
          <asp:GridView ID="GridViewOK" runat="server" AutoGenerateColumns="False" Width="900px" >
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="menager_holiday.aspx?holidayid={0}" Text="Szczegóły" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="From" HeaderText="Od" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="To" HeaderText="Do" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Pracownik" HeaderText="Pracownik" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ManagerId" HeaderText="Manager" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Rodzaj" HeaderText="Rodzaj" ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:BoundField DataField="Ilosc_dni" HeaderText="Ilość dni" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
          </asp:GridView>
    </div>
        <h3 class="divbeckground" runat="server" id="h_Rejec">Urlopy odrzucone</h3>
         <div class="monthbackground monthbackground_hide">
          <asp:GridView ID="GridViewRejec" runat="server" AutoGenerateColumns="False" Width="900px" >
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="menager_holiday.aspx?holidayid={0}" Text="Szczegóły" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="From" HeaderText="Od" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="To" HeaderText="Do" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Pracownik" HeaderText="Pracownik" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ManagerId" HeaderText="Manager" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Rodzaj" HeaderText="Rodzaj" ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:BoundField DataField="Ilosc_dni" HeaderText="Ilość dni" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
          </asp:GridView>
    </div> 
    <h3 class="divbeckground" runat="server" id="h_Delete">Urlopy usunięte</h3>
        <div class="monthbackground monthbackground_hide">
          <asp:GridView ID="GridViewDelete" runat="server" AutoGenerateColumns="False" Width="900px" >
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="menager_holiday.aspx?holidayid={0}" Text="Szczegóły" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="From" HeaderText="Od" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="To" HeaderText="Do" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Pracownik" HeaderText="Pracownik" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ManagerId" HeaderText="Manager" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Rodzaj" HeaderText="Rodzaj" ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:BoundField DataField="Ilosc_dni" HeaderText="Ilość dni" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
          </asp:GridView>
    </div>
            <h3 class="divbeckground" runat="server" id="h1_kalendarz">Kalendarz urlopów [cały team]</h3>
        <div class="monthbackground monthbackground_hide" runat="server" id="bigDiv">
            <div id="l0" runat="server" class="divbeckground2"></div>
            <div id="m0" runat="server" class="monthbackground"></div><br />
            <div id="l1" runat="server" class="divbeckground2"></div>
            <div id="m1" runat="server" class="monthbackground"></div><br />
            <div id="l2" runat="server" class="divbeckground2"></div>
            <div id="m2" runat="server" class="monthbackground"></div><br />
            <div id="l3" runat="server" class="divbeckground2"></div>
            <div id="m3" runat="server" class="monthbackground"></div><br />
            <div id="l4" runat="server" class="divbeckground2"></div>
            <div id="m4" runat="server" class="monthbackground"></div><br />
            <div id="l5" runat="server" class="divbeckground2"></div>
            <div id="m5" runat="server" class="monthbackground"></div><br />
            <div id="l6" runat="server" class="divbeckground2"></div>
            <div id="m6" runat="server" class="monthbackground"></div><br />
            <div id="l7" runat="server" class="divbeckground2"></div>
            <div id="m7" runat="server" class="monthbackground"></div><br />
            <div id="l8" runat="server" class="divbeckground2"></div>
            <div id="m8" runat="server" class="monthbackground"></div><br />
            <div id="l9" runat="server" class="divbeckground2"></div>
            <div id="m9" runat="server" class="monthbackground"></div><br />
            <div id="l10" runat="server" class="divbeckground2"></div>
            <div id="m10" runat="server" class="monthbackground"></div><br />
            <div id="l11" runat="server" class="divbeckground2"></div>
            <div id="m11" runat="server" class="monthbackground"></div><br />
            <div id="l12" runat="server" class="divbeckground2"></div>
            <div id="m12" runat="server" class="monthbackground"></div><br />
            <div id="l13" runat="server" class="divbeckground2"></div>
            <div id="m13" runat="server" class="monthbackground"></div><br />
            <div id="l14" runat="server" class="divbeckground2"></div>
            <div id="m14" runat="server" class="monthbackground"></div><br />
        </div>

</asp:Content>
