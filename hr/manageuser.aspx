<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="manageuser.aspx.cs" Inherits="WebApplication4.hr.manageuser" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <h2 id="user_name_label" runat="server">
        Manage Users
    </h2>
    <asp:HiddenField ID="HiddenFielduserid" runat="server" />
    <asp:Label ID="info_label" runat="server" Text="" Visible="false" CssClass="failureNotification"></asp:Label>
    <p>
        <asp:button id="backButton" runat="server" text="Wróć do listy" OnClick="goBack_Click"></asp:button>   
        <asp:Button runat="server" ID="emploee_button" Text='Edytuj' OnClick="edytuj_Click"></asp:Button><span class="odstep">&nbsp;</span>
        <asp:Button runat="server" ID="show_holidays" Text='Lista urlopów' OnClick="show_holidays_click" ></asp:Button>
        <asp:Button runat="server" ID="ButtonResetPassword" Text='Reset Password' OnClick="reset_Click"></asp:Button><span class="odstep">&nbsp;</span>
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification" ValidationGroup="UserValidationGroup"/>
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
        <asp:RequiredFieldValidator ID="ImieRequired" runat="server" ControlToValidate="ImieTextBox" 
                                     CssClass="failureNotification" ErrorMessage="Podaj imie pracownika." ToolTip="To pole jest wymagane" 
                                     ValidationGroup="UserValidationGroup">*</asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Label ID="NazwiskoLabel" runat="server" Text="Nazwisko: "></asp:Label>
        <br /><asp:TextBox ID="NazwiskoTextBox" runat="server" CssClass="textReadOnly" ReadOnly="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NazwiskoRequired" runat="server" ControlToValidate="NazwiskoTextBox" 
                                     CssClass="failureNotification" ErrorMessage="Podaj nazwisko pracownika." ToolTip="To pole jest wymagane" 
                                     ValidationGroup="UserValidationGroup">*</asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Label ID="Label_email" runat="server" Text="E-mail adress: "></asp:Label>
        <br /><asp:TextBox ID="TextBox_email" runat="server" CssClass="textReadOnly" ReadOnly="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="mailRequired" runat="server" ControlToValidate="TextBox_email" 
                                     CssClass="failureNotification" ErrorMessage="Podaj adres e-mail." ToolTip="To pole jest wymagane" 
                                     ValidationGroup="UserValidationGroup">*</asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator
            ID="RegularExpressionValidator1" runat="server"  
            ErrorMessage="Niepoprawny format adresu e-mail" 
            ValidationGroup="UserValidationGroup" CssClass="failureNotification" 
            ControlToValidate="TextBox_email" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator>
    </p>
    <p>
                        <asp:CheckBox ID="CheckBoxZablokowany" Enabled="false" runat="server"/>
                        <asp:Label ID="LabelZablokowany" runat="server" AssociatedControlID="CheckBoxZablokowany" CssClass="inline">Zablokowany</asp:Label><span class="odstep">&nbsp;</span>
                        <asp:button id="ButtonOdblokuj" runat="server" text="Odblokuj użytkownika" 
                            CssClass="inline" Visible="false" onclick="ButtonOdblokuj_Click"></asp:button>
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
        <asp:RegularExpressionValidator
            ID="PeselExt" runat="server"  
            ErrorMessage="Pesel musi składać się z 11 liczb" 
            ValidationGroup="UserValidationGroup" CssClass="failureNotification" 
            ControlToValidate="PeselTextBox" ValidationExpression="\d{11}">!</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="PeselRequired" runat="server" ControlToValidate="PeselTextBox" 
                                     CssClass="failureNotification" ErrorMessage="Podaj numer PESEL." ToolTip="To pole jest wymagane" 
                                     ValidationGroup="UserValidationGroup">*</asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Label ID="DataurodzeniaLabel" runat="server" Text="Data urodzenia: "></asp:Label>
        <br /><asp:TextBox ID="Dataurodzenia" CssClass="textReadOnly" ReadOnly="true" runat="server"></asp:TextBox><asp:Image ID="imgCal" runat="server" Visible="false" ImageUrl="~/images/calendar.png" style="margin-left: 5px; cursor: pointer"/>
<ajaxtoolkit:CalendarExtender id="ctrlCalendar" Enabled="false" TargetControlID="Dataurodzenia" Format="dd-MM-yyyy" PopupButtonID="imgCal" runat="server" PopupPosition="BottomRight">
</ajaxtoolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="UrodzenieRequired" runat="server" ControlToValidate="Dataurodzenia" 
                                     CssClass="failureNotification" ErrorMessage="Podaj datę urodzenia." ToolTip="To pole jest wymagane" 
                                     ValidationGroup="UserValidationGroup">*</asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Label ID="DataZatrudnieniaLabel" runat="server" Text="Data zatrudnienia: "></asp:Label>
        <br /><asp:TextBox ID="DataZatrudnienia" CssClass="textReadOnly" ReadOnly="true" runat="server"></asp:TextBox><asp:Image ID="imgCal2" Visible="false" runat="server"  ImageUrl="~/images/calendar.png" style="margin-left: 5px; cursor: pointer"/>
<ajaxtoolkit:CalendarExtender id="CalendarExtender1" Enabled="false" TargetControlID="DataZatrudnienia" Format="dd-MM-yyyy" PopupButtonID="imgCal2" runat="server" PopupPosition="BottomRight">
</ajaxtoolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="ZatrudnienieRequired" runat="server" ControlToValidate="DataZatrudnienia" 
                                     CssClass="failureNotification" ValidationGroup="UserValidationGroup" ErrorMessage="Podaj datę zatrudnienia.">*</asp:RequiredFieldValidator>
    
    </p>
    <p>
        <asp:SqlDataSource ID="SqlTeams" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT [Id], [Name] FROM [Teams]" >
        </asp:SqlDataSource>
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlTeams" Enabled="false" DataTextField="Name" DataValueField="Id" Width="200px" AppendDataBoundItems="True">
        <asp:ListItem Text="Wybierz team" Value ="-1"></asp:ListItem>
        </asp:DropDownList><asp:HiddenField ID="HiddenFieldTeam"  runat="server" /><asp:HiddenField ID="HiddenTeamName"  runat="server" />
                       <asp:RequiredFieldValidator id="Rfv1" runat="server" ControlToValidate="DropDownList1" 
                   InitialValue="-1"  ErrorMessage="Wybierz team" Display="Dynamic" ValidationGroup="UserValidationGroup" CssClass="failureNotification">*</asp:RequiredFieldValidator>
    </p>
        <div style="float:left; width:260px; height:55px;"><asp:Label ID="DropDownList2label" runat="server" Text="Ilość dni urlopowych w roku: " Visible="True"></asp:Label><br />
        <asp:DropDownList ID="DropDownList2" runat="server" Enabled="false" 
                DataTextField="Number" DataValueField="Id" Width="200px" 
                AppendDataBoundItems="True" AutoPostBack="true"
                onselectedindexchanged="DropDownList2_SelectedIndexChanged">
        <asp:ListItem Text="Lość dni wolnych w roku" Value ="-1"></asp:ListItem>
        <asp:ListItem Text="20" Value ="1"></asp:ListItem>
        <asp:ListItem Text="26" Value ="2"></asp:ListItem>
        </asp:DropDownList><asp:HiddenField ID="HiddenField20t26"  runat="server" />
        </div><div style="float:left; width:360px; height:55px;"><asp:Label ID="uzyska26label" runat="server" Text="Data uzyskania 26 dni urlopowych: " Visible="false"></asp:Label><br />
        <asp:TextBox ID="uzyska26TextBox" Visible="false" CssClass="textReadOnly" ReadOnly="true" runat="server"></asp:TextBox><asp:Image ID="imgCal3" Visible="false" runat="server"  ImageUrl="~/images/calendar.png" style="margin-left: 5px; cursor: pointer"/>
<ajaxtoolkit:CalendarExtender id="CalendarExtender2" Enabled="false" TargetControlID="uzyska26TextBox" Format="dd-MM-yyyy" PopupButtonID="imgCal3" runat="server" PopupPosition="BottomRight">
</ajaxtoolkit:CalendarExtender>
        </div>
     <p style="clear: both;margin-bottom:18px">
         <asp:Button ID="SaveUserButton" runat="server" Visible="false" Text="Zapisz" onclick="Button1_Click" 
         ValidationGroup="UserValidationGroup"/>    <asp:Button ID="CancelUserButton" 
         runat="server" Visible="false" Text="Anuluj" onclick="anuluj_Click" 
         ValidationGroup="UserValidationGroup" CausesValidation="False"/>
     </p>
</asp:Content>
