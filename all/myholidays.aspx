<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="myholidays.aspx.cs" Inherits="WebApplication4.all.myholidays" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="../Scripts/JScript1.js"></script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <h2>
        My Holidays
    </h2>
    <p><asp:button id="addButton" runat="server" text="Dodaj urlop" onclick="addButton_Click" style="margin-bottom:10px;"></asp:button></p>
    <asp:Label ID="info_label" runat="server" Visible="False" 
        CssClass="failureNotification"></asp:Label>
    <div>
    
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" CssClass="failureNotification" ValidationGroup="ValidationSummary2"/>    <div id="hiddendiv" style="float:left;width:120px;height:0px" runat="server">
                <asp:Label ID="OdLab" runat="server" Text="Od: " Visible="false"></asp:Label>
        <br /><asp:TextBox ID="OdTextBox" CssClass="textEntry2" runat="server" ReadOnly="true" Visible="false" Width="80px"></asp:TextBox><asp:Image ID="imgCal" Visible="false" runat="server" ImageUrl="~/images/calendar.png" style="margin-left: 5px; cursor: pointer"/>
<ajaxtoolkit:CalendarExtender id="ctrlCalendar" TargetControlID="OdTextBox" Format="yyyy-MM-dd" PopupButtonID="imgCal" runat="server" PopupPosition="BottomRight">
</ajaxtoolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="OdRequired" runat="server" ControlToValidate="OdTextBox" 
                                     CssClass="failureNotification" ErrorMessage="Chose date" ToolTip="This field is required" 
                                     ValidationGroup="ValidationSummary2">*</asp:RequiredFieldValidator></div>
        <div id="hiddendiv2" style="float:left;width:125px; height:0px" runat="server">
                                                     <asp:Label ID="DoLab" runat="server" Text="Do: " Visible="false"></asp:Label>
        <br /><asp:TextBox ID="DoTextBox" CssClass="textEntry2" ReadOnly="true" runat="server" Visible="false" Width="80px"></asp:TextBox><asp:Image ID="imgCal2" Visible="false" runat="server" ImageUrl="~/images/calendar.png" style="margin-left: 5px; cursor: pointer"/>
<ajaxtoolkit:CalendarExtender id="ctrlCalendar2" TargetControlID="DoTextBox" Format="yyyy-MM-dd" PopupButtonID="imgCal2" runat="server" PopupPosition="BottomRight">
</ajaxtoolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="DoRequired" runat="server" ControlToValidate="DoTextBox" 
                                     CssClass="failureNotification" ErrorMessage="Chose date" ToolTip="This field is required" 
                                     ValidationGroup="ValidationSummary2">*</asp:RequiredFieldValidator>
                                                     <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                                         ErrorMessage="Chose proper date" 
                                                         CssClass="failureNotification" ControlToValidate="DoTextBox" 
                                                         OnServerValidate="DateValidate" ValidationGroup="ValidationSummary2">* </asp:CustomValidator>
        </div>
        <div style="float:left;width:210px">
        <asp:Label ID="DropDownHolidayLab" runat="server" Text="Rodzaj: " Visible="false"></asp:Label>
        <asp:DropDownList ID="DropDownHoliday" runat="server" Enabled="true" AppendDataBoundItems="True" Visible="false" Width="200px" DataTextField="Name" DataValueField="Id">
                <asp:ListItem Text="Wybierz rodzaj" Value ="-1"></asp:ListItem>
        </asp:DropDownList><asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownHoliday" 
                   InitialValue="-1"  ErrorMessage="Wybierz rodzaj urlopu" Display="Dynamic" ValidationGroup="ValidationSummary2" CssClass="failureNotification" Font-Strikeout="True"> * </asp:RequiredFieldValidator>
        </div>
        <div style="float:left;width:210px">
        <asp:Label ID="DropDownMenagerLab" runat="server" Text="Menager: " Visible="false"></asp:Label>
        <asp:DropDownList ID="DropDownMenager" runat="server" Enabled="true" AppendDataBoundItems="True" Visible="false" Width="200px" DataTextField="Name" DataValueField="Id">
                <asp:ListItem Text="Wybierz menagera" Value ="-1"></asp:ListItem>
        </asp:DropDownList>
        </div><asp:RequiredFieldValidator id="Rfv2" runat="server" ControlToValidate="DropDownMenager" 
                   InitialValue="-1"  ErrorMessage="Wybierz menagera" Display="Dynamic" ValidationGroup="ValidationSummary2" CssClass="failureNotification"> * </asp:RequiredFieldValidator>
        <div style="clear: both;">
        <asp:button id="ZapiszButton" runat="server" text="Zapisz" Visible="false" 
                onclick="ZapiszButton_Click" ValidationGroup="ValidationSummary2"></asp:button>
        <asp:button id="AnulujButton" runat="server" text="Anuluj" Visible="false" 
                onclick="AnulujButton_Click" CausesValidation="False"></asp:button>
        </div>

    </div>
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
        <asp:TableCell HorizontalAlign="Center" ID="suncell" runat="server"><asp:label ID="Labelsum" runat="server" Font-Bold="True" Font-Size="Larger"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="Labelnz" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        <asp:TableCell HorizontalAlign="Center"><asp:label ID="Labelnr" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></asp:TableCell>
        </asp:TableRow>
        </asp:Table>
    <p>
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="900px" >
                      <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="manageholiday.aspx?holidayid={0}" Text="Manage" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"/>
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
<h3 class="divbeckground" runat="server" id="h1">Kalendarz urlopów</h3>
    <div class="monthbackground" runat="server" id="bigDiv">
    <div id="m0" runat="server" class="monthbackground"></div>
    <div id="m1" runat="server" class="monthbackground"></div>
    <div id="m2" runat="server" class="monthbackground"></div>
    <div id="m3" runat="server" class="monthbackground"></div>
    <div id="m4" runat="server" class="monthbackground"></div>
    <div id="m5" runat="server" class="monthbackground"></div>
    <div id="m6" runat="server" class="monthbackground"></div>
    <div id="m7" runat="server" class="monthbackground"></div>
    <div id="m8" runat="server" class="monthbackground"></div>
    <div id="m9" runat="server" class="monthbackground"></div>
    <div id="m10" runat="server" class="monthbackground"></div>
    <div id="m11" runat="server" class="monthbackground"></div>
    <div id="m12" runat="server" class="monthbackground"></div>
    <div id="m13" runat="server" class="monthbackground"></div>
    <div id="m14" runat="server" class="monthbackground"></div>
    </div>
</asp:Content>
