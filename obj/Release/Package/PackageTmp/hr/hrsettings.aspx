<%@ Page Title="HR Settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hrsettings.aspx.cs" Inherits="WebApplication4.hr.hrsettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type = "text/javascript">
    function SetTarget() {
        document.forms[0].target = "_blank";
    }
</script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <h2>
        Ustawienia
    </h2>
    <asp:Label ID="info_label" runat="server" Visible="False" CssClass="failureNotification" style="padding-top:10px;display:block;"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" CssClass="failureNotification" ValidationGroup="ValidationSummary2"/>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification" ValidationGroup="ValidationSummary1"/>
    <h3><asp:Label ID="LabelYear" runat="server" Font-Bold="True"></asp:Label></h3><br />
    <asp:Repeater ID="RepeaterYears" runat="server" onitemcommand="RepeaterYears_ItemCommand">
              <ItemTemplate>
             <tr>
                <td><asp:LinkButton ID="LinkButtonYears" runat="server" CommandArgument="<%# Container.DataItem %>">   <%# Container.DataItem %>   </asp:LinkButton></td>
             </tr>
          </ItemTemplate></asp:Repeater><br /><br />
    <asp:GridView ID="GridViewSwieta" runat="server" AutoGenerateColumns="False" onrowcommand="GridViewSwieta_RowCommand" AllowSorting="True" Width="500px">
        <Columns>
            <asp:ButtonField ButtonType="Link" Text="Delete" CommandName="usun" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Date" HeaderText="Data" ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Name" HeaderText="Święto" ItemStyle-Width="200px"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="id" Visible="false" />
        </Columns>
    </asp:GridView><br />
    <div style="width:505px;">
    <div id="hiddendiv2" style="float:left;width:170px;" runat="server">
    <asp:Label ID="DoLab" runat="server" Text="Data: " Visible="true"></asp:Label>
    <asp:TextBox ID="DateTextBox" CssClass="textEntry2" ReadOnly="true" runat="server" Visible="true" Width="80px" ></asp:TextBox>
    <asp:Image ID="imgCal2" Visible="true" runat="server" ImageUrl="~/images/calendar.png" style="margin-left: 5px; cursor: pointer"/>
    <ajaxtoolkit:CalendarExtender id="ctrlCalendar2" TargetControlID="DateTextBox" Format="yyyy-MM-dd" PopupButtonID="imgCal2" runat="server" PopupPosition="BottomRight"></ajaxtoolkit:CalendarExtender>
    <asp:RequiredFieldValidator ID="DoRequired" runat="server" ControlToValidate="DateTextBox" CssClass="failureNotification" ErrorMessage="Podaj datę święta" ToolTip="To pole jest wymagane" ValidationGroup="ValidationSummary1">*</asp:RequiredFieldValidator>
    </div>
    <div style="float:left;width:330px">
    <asp:Label ID="DropDownHolidayLab" runat="server" Text="Nazwa święta: " Visible="true"></asp:Label>
    <asp:TextBox ID="NameTextBox" runat="server" Width="200px"></asp:TextBox>
    <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="NameTextBox" ErrorMessage="Dodaj nazwę święta" ValidationGroup="ValidationSummary1" CssClass="failureNotification"> * </asp:RequiredFieldValidator>
    </div></div>
    <div style="clear: both;">
    <asp:button id="DodajButton" runat="server" text="Dodaj święto" Visible="True" onclick="DodajButton_Click" ValidationGroup="ValidationSummary1"></asp:button></div>
    <br />
    <br />
    <h3><b>HR operation history</b></h3><br />
    <div>
    <div id="Div1" style="float:left;width:330px;display:inline-block" runat="server">
        <asp:LinkButton ID="LinkButtonPersonal" runat="server" OnCommand="hrhistory_Click" CommandName="hrhistory" CommandArgument="personal" ValidationGroup="ValidationSummary2" CausesValidation="true" Font-Bold="False">PERSONAL</asp:LinkButton> | <asp:LinkButton runat="server" ID="LinkButtonTeam" OnCommand="hrhistory_Click" CommandName="hrhistory" CommandArgument="team" ValidationGroup="ValidationSummary2" CausesValidation="true">TEAM</asp:LinkButton> | <asp:LinkButton ID="LinkButtonFunctions" runat="server"  OnCommand="hrhistory_Click" CommandName="hrhistory" CommandArgument="functions" ValidationGroup="ValidationSummary2" CausesValidation="true">FUNCTIONS</asp:LinkButton> | <asp:LinkButton ID="LinkButtonNewYear" runat="server"  OnCommand="hrhistory_Click" CommandName="hrhistory" CommandArgument="newyear" ValidationGroup="ValidationSummary2" CausesValidation="true">NEW YEAR ACTIONS</asp:LinkButton>
	</div>
	<div style="float:left;width:155px;display:inline-block" runat="server">
		<asp:Label ID="OdLab" runat="server" Text="From: "></asp:Label>
		<asp:TextBox ID="OdTextBox" CssClass="textEntry2" runat="server" ReadOnly="true" Width="80px"></asp:TextBox><asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" style="margin-left: 5px; cursor: pointer"/>
		<ajaxtoolkit:CalendarExtender id="ctrlCalendar" TargetControlID="OdTextBox" Format="yyyy-MM-dd" PopupButtonID="imgCal" runat="server" PopupPosition="BottomRight">
		</ajaxtoolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="OdTextBox" 
                                     CssClass="failureNotification" ErrorMessage="Chose date" ToolTip="This field is required" 
                                     ValidationGroup="ValidationSummary2">*</asp:RequiredFieldValidator>
	</div>
	<div style="float:left;width:155px;display:inline-block" runat="server">
		<asp:TextBox ID="DoTextBox" CssClass="textEntry2" ReadOnly="true" runat="server" Width="80px"></asp:TextBox><asp:Image ID="imgCal3" runat="server" ImageUrl="~/images/calendar.png" style="margin-left: 5px; cursor: pointer"/>
		<ajaxtoolkit:CalendarExtender id="CalendarExtender1" TargetControlID="DoTextBox" Format="yyyy-MM-dd" PopupButtonID="imgCal3" runat="server" PopupPosition="BottomRight">
		</ajaxtoolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DoTextBox" 
                                     CssClass="failureNotification" ErrorMessage="Chose date" ToolTip="This field is required" 
                                     ValidationGroup="ValidationSummary2">*</asp:RequiredFieldValidator>
		<asp:CustomValidator ID="CustomValidator1" runat="server" 
		ErrorMessage="Chose proper date" 
		CssClass="failureNotification" ControlToValidate="DoTextBox" 
		OnServerValidate="DateValidate" ValidationGroup="ValidationSummary2">* </asp:CustomValidator>
    </div>
    </div>
    <br />
    <div style="clear: both;"><asp:Label ID="LabelOperation" runat="server" Visible="true" Font-Bold="True" Font-Size="Smaller"></asp:Label></div><br />
    <asp:GridView ID="GridViewHistoryHR" runat="server" AutoGenerateColumns="False" onrowcommand="GridViewHistoryHR_RowCommand" OnClientClick="SetTarget();" AllowSorting="True" Width="100%">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Pracownik" HeaderText="Who?" ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Akcja" HeaderText="Action" ItemStyle-Width="200px"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="IleDni" HeaderText="Days Count" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="PracownikHR" HeaderText="HR emploee" ItemStyle-Width="200px"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Data" HeaderText="Date" ItemStyle-Width="80px"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Reason" HeaderText="Reason" ItemStyle-Width="200px"  ItemStyle-HorizontalAlign="Center"/>
            <asp:ButtonField ButtonType="Link" Text="Print" CommandName="print_pdf" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"/>
        </Columns>
    </asp:GridView>
    <div style="clear: both;"><br />
    <h3><b>Operacje noworoczne</b></h3><br /></div>
    <div style="display:inline-block;margin-right: 20px;"><asp:button id="Button1" runat="server" text="Przelicz dni [nowy rok]" Visible="True" onclick="NowyRokButton_Click" ></asp:button></div>
    <div style="display:inline-block; font-weight: 700;"><asp:Label ID="LabelLastCalculation" runat="server" Text="Label"></asp:Label></div>
    <br />
</asp:Content>
