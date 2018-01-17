using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;

namespace holidays
{
    public class PolaczenieSQL
    {
        public static user find_user(string userid, bool justString = true, bool all_data = false)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT UserName, Imie, Nazwisko, team, pesel, data_urodz, data_zatrudnienia, dniurlopowe, kiedy26, dni_p_rok, dni_o_rok, dni_n_rok, dni_wyk, dni_ekstra, dni_nz, dni_wyk_nrok, passtemp FROM aspnet_Users Where UserName = @user";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@user", SqlDbType.VarChar, 50);
            command.Parameters["@user"].Value = userid;
                sdr2 = command.ExecuteReader();
                if (sdr2.HasRows == true)
                {
                    holidays.user u = new holidays.user();
                    while (sdr2.Read())
                    {                        
                        u.imie = sdr2["Imie"].ToString();
                        u.nazwisko = sdr2["Nazwisko"].ToString();
                        u.userid = sdr2["UserName"].ToString();
                        u.teamid = sdr2["team"].ToString();
                        if (justString) break;
                        u.ilosc_obecny = Convert.ToInt16(sdr2["dni_o_rok"].ToString());
                        u.ilosc_poprzedni = Convert.ToInt16(sdr2["dni_p_rok"].ToString());
                        u.ilosc_nastepny = Convert.ToInt16(sdr2["dni_n_rok"].ToString());
                        u.ilosc_nz = Convert.ToInt16(sdr2["dni_nz"].ToString());
                        u.ilosc_wyk = Convert.ToInt16(sdr2["dni_wyk"].ToString());
                        u.ilosc_wyk_next = Convert.ToInt16(sdr2["dni_wyk_nrok"].ToString());
                        u.ilosc_dodatkowy = Convert.ToInt16(sdr2["dni_ekstra"].ToString());
                        if (all_data)
                        {
                            u.pesel = sdr2["pesel"].ToString();
                            holidays.team t = find_team(u.teamid.ToString());
                            u.team = t.name.ToString();
                            u.menager = t.menager.ToString();
                            u.data_urodzenia = sdr2["data_urodz"].ToString();
                            u.data_zatrudnienia = sdr2["data_zatrudnienia"].ToString();
                            u.dniurlopowe = sdr2["dniurlopowe"].ToString();
                            u.kiedy26 = sdr2["kiedy26"].ToString();
                            u.passtemp = sdr2["passtemp"].ToString();
                        }
                    }
                    sdr2.Close();
                    conn.Close();
                    return u;
                }
                else return null;
        }

        public static string find_menager_by_user(string userid)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT UserName, aspnet_Users.Team, Teams.Id, Teams.Menager  FROM aspnet_Users LEFT JOIN Teams ON aspnet_Users.Team=Teams.Id Where UserName = @userid";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@userid", SqlDbType.VarChar, 50);
            command.Parameters["@userid"].Value = userid;
            sdr2 = command.ExecuteReader();
            string menagerid = "";

            if (sdr2.HasRows == true)
            {
                
                while (sdr2.Read())
                {
                    menagerid = sdr2["Menager"].ToString();
                }
                sdr2.Close();
                conn.Close();

                if (menagerid == userid) 
                {
                    menagerid = "gacek_mgr_hr";
                }

                return menagerid;
            }
            else return null;
        }

        public static team find_team(string teamid)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT Id, Name, Menager FROM Teams Where Id = @teamid";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@teamid", SqlDbType.VarChar, 50);
            command.Parameters["@teamid"].Value = teamid;
            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                holidays.team t = new holidays.team();
                while (sdr2.Read())
                {
                    t.teamid = sdr2["Id"].ToString();
                    t.name = sdr2["Name"].ToString();
                    t.menager = sdr2["Menager"].ToString();
                }
                sdr2.Close();
                conn.Close();
                return t;
            }
            else return null;
        }

        public static List<string> find_teams_bymenager(string menagerid)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT Id, Name, Menager FROM Teams Where Menager = @menager";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@menager", SqlDbType.VarChar, 50);
            command.Parameters["@menager"].Value = menagerid;
            sdr2 = command.ExecuteReader();
            List<string> teams = new List<string>();

            if (sdr2.HasRows == true)
            {
                //holidays.team t = new holidays.team();
                
                string teamname; 
                while (sdr2.Read())
                {
                    teamname = sdr2["Id"].ToString();
                    teams.Add(teamname);
                }
                sdr2.Close();
                conn.Close();
                return teams;
            }
            else return null;
        }

        public static holiday find_holiday(string holidayid, bool listadni = false)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT Holidays.Id, Od, Do, Rodzaj, Holidays.Status, Holidays.UserId, MenagerId, OtherMenagerId, aspnet_Users.Imie, aspnet_Users.Nazwisko, HolidaysType.HolidayType_Name, days_count, StatusText, days_count_with_all, days_count_this, days_count_next FROM Holidays LEFT JOIN aspnet_Users ON aspnet_Users.UserName=Holidays.MenagerId LEFT JOIN HolidaysType ON Holidays.Rodzaj=HolidaysType.Id LEFT JOIN HolidayStatus ON Holidays.Status=HolidayStatus.Id Where Holidays.Id = @holidayid";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@holidayid", SqlDbType.VarChar, 50);
            command.Parameters["@holidayid"].Value = holidayid;
            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                holiday h = new holiday();
                while (sdr2.Read())
                {
                    h = new holiday(sdr2["Od"].ToString(), sdr2["Do"].ToString(), sdr2["MenagerId"].ToString(), sdr2["OtherMenagerId"].ToString(), sdr2["Status"].ToString(), sdr2["StatusText"].ToString(), sdr2["Rodzaj"].ToString(), sdr2["HolidayType_Name"].ToString(), sdr2["UserId"].ToString(), sdr2["days_count"].ToString(), sdr2["days_count_this"].ToString(), sdr2["days_count_next"].ToString(), sdr2["days_count_with_all"].ToString(), sdr2["Id"].ToString(), listadni);
                    
                    //string strOd = sdr2["Od"].ToString();
                    //string strDo = sdr2["Do"].ToString();
                    //DateTime myTime;
                    //if (strOd != string.Empty)
                    //{
                    //    myTime = DateTime.Parse(strOd);
                    //    h.date_od = myTime;
                    //}
                    //if (strDo != string.Empty)
                    //{
                    //    myTime = DateTime.Parse(strDo);
                    //    h.date_do = myTime;
                    //}
                    //h.menager = sdr2["MenagerId"].ToString();
                    //h.status = sdr2["StatusText"].ToString();
                    //h.rodzaj = sdr2["HolidayType_Name"].ToString();
                    //h.userid = sdr2["UserId"].ToString();
                    //h.holidayid = sdr2["Id"].ToString();
                    //h.holiday_dyas_all = Convert.ToInt16(sdr2["days_count"].ToString());
                }
                sdr2.Close();
                conn.Close();
                return h;
            }
            else return null;
        }

        public static List<string> find_holiday_byuser(string userid)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT Holidays.Id, Holidays.UserId FROM Holidays Where holidays.UserId = @userid ORDER BY Holidays.Id DESC";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@userid", SqlDbType.VarChar, 50);
            command.Parameters["@userid"].Value = userid;
            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                List<string> holidaysID = new List<string>();
                while (sdr2.Read())
                {
                    holidaysID.Add(sdr2["Id"].ToString());
                }
                sdr2.Close();
                conn.Close();
                return holidaysID;
            }
            else return null;
        }

        public static List<string> find_holiday_days_status_byuser(string userid, string odstring, string tostring)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT Holidays.Id, Holidays.UserId, Holidays.Status, Holidays.Od, Holidays.Do, Holidays.Rodzaj, Holidays.all_days_id FROM Holidays Where ((holidays.UserId = @userid) AND ((Holidays.Od BETWEEN '" + odstring + "' AND '" + tostring + "') OR (Holidays.Do BETWEEN '" + odstring + "' AND '" + tostring + "')));";
            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@userid", SqlDbType.VarChar, 50);
            command.Parameters["@userid"].Value = userid;
            sdr2 = command.ExecuteReader();

            List<string> matrix2 = new List<string>();

            if (sdr2.HasRows == true)
            {
                while (sdr2.Read())
                {
                    matrix2.Add(sdr2["Id"].ToString() + "," + sdr2["Status"].ToString() + "," + sdr2["Rodzaj"].ToString() + "," + sdr2["all_days_id"].ToString() + "," + sdr2["UserId"].ToString());
                }
                sdr2.Close();
                conn.Close();
                return matrix2;
            }
            else return null;
        }

        public static List<string> find_holiday_bystatus(int status, string whereteam, string whereuser)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery;

            if (!String.Equals(whereteam, "all"))
            {
                sqlquery = "SELECT Holidays.Id, Holidays.Status, aspnet_Users.team, aspnet_Users.UserId, Holidays.UserId FROM Holidays LEFT JOIN aspnet_Users ON aspnet_Users.UserName=Holidays.UserId Where (holidays.Status = @ststusid AND aspnet_Users.team = @teamid)";
            }
            else if (!String.Equals(whereuser, "all"))
            {
                sqlquery = "SELECT Holidays.Id, Holidays.Status, Holidays.UserId FROM Holidays Where (holidays.Status = @ststusid AND Holidays.UserId = @userid)";
            }
            else 
            {
                sqlquery = "SELECT Holidays.Id, Holidays.Status FROM Holidays Where holidays.Status = @ststusid";
            }
            
            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            if (!String.Equals(whereteam, "all"))
            {
                command.Parameters.Add("@teamid", SqlDbType.VarChar, 50);
                command.Parameters["@teamid"].Value = whereteam;
            }

            if (!String.Equals(whereuser, "all"))
            {
                command.Parameters.Add("@userid", SqlDbType.VarChar, 50);
                command.Parameters["@userid"].Value = whereuser;
            }

            command.Parameters.Add("@ststusid", SqlDbType.VarChar, 50);
            command.Parameters["@ststusid"].Value = status;
            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                List<string> holidaysID = new List<string>();
                while (sdr2.Read())
                {
                    holidaysID.Add(sdr2["Id"].ToString());
                }
                sdr2.Close();
                conn.Close();
                return holidaysID;
            }
            else return null;
        }

        public static List<string> find_swieta_list()
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT holiday_days.Id, holiday_days.date FROM holiday_days";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;
            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                List<string> holidaysDate = new List<string>();
                while (sdr2.Read())
                {
                    holidaysDate.Add(sdr2["date"].ToString());
                }
                sdr2.Close();
                conn.Close();
                return holidaysDate;
            }
            else return null;
        }

        public static List<string> find_swieta_list_short()
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT holiday_days.Id, holiday_days.stringdateid, holiday_days.name FROM holiday_days";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;
            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                List<string> holidaysDate = new List<string>();
                while (sdr2.Read())
                {
                    holidaysDate.Add(sdr2["stringdateid"].ToString());
                    holidaysDate.Add(sdr2["name"].ToString());
                }
                sdr2.Close();
                conn.Close();
                return holidaysDate;
            }
            else return null;
        }

        public static DataTable find_swieta(int year)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();
            string odstring = year +"-01-01";
            string tostring = year +"-12-31";
            string sqlquery = "SELECT holiday_days.Id, holiday_days.date, holiday_days.name FROM holiday_days WHERE holiday_days.date BETWEEN '" + odstring + "' AND '" + tostring + "' ORDER BY holiday_days.date ASC;";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            DataTable td = new DataTable();
            td.Columns.Add("Date");
            td.Columns.Add("Name");
            td.Columns.Add("id");
            DataRow dr = null;

            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                DateTime myTime;
                while (sdr2.Read())
                {
                    dr = td.NewRow();
                    myTime = DateTime.Parse(sdr2["date"].ToString());
                    string aaa = myTime.ToString("yyyy-MM-dd");
                    string bbb = sdr2["name"].ToString();
                    string ccc = sdr2["Id"].ToString();
                    dr[0] = aaa;
                    dr[1] = bbb;
                    dr[2] = ccc;
                    td.Rows.Add(dr);
                }
                sdr2.Close();
                conn.Close();
                return td;
            }
            else return null;
        }

        public static DataTable find_HistoryHR(string from, string to, string operation)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();
            string sqlshortstring = "";
            switch (operation) 
            {
                case "team":
                    sqlshortstring = "ExtraDaysHistory.Akcja = 'Teams actions'";
                    break;
                case "functions":
                    sqlshortstring = "ExtraDaysHistory.Akcja = 'Function change'";
                    break;
                case "newyear":
                    sqlshortstring = "ExtraDaysHistory.Akcja = 'New Year calculations'";
                    break;
                default:
                    sqlshortstring = "ExtraDaysHistory.Akcja = 'Addtional days' OR ExtraDaysHistory.Akcja = 'Go from 20 to 26 holidays days' OR  ExtraDaysHistory.Akcja = 'Team change' OR  ExtraDaysHistory.Akcja = 'New user'";
                    break;
            }

            string sqlquery = "SELECT ExtraDaysHistory.Id, ExtraDaysHistory.Pracownik, ExtraDaysHistory.PracownikHR, ExtraDaysHistory.IleDni, ExtraDaysHistory.Reason, ExtraDaysHistory.Data, ExtraDaysHistory.Akcja FROM ExtraDaysHistory WHERE (ExtraDaysHistory.Data BETWEEN '" + from + "' AND '" + to + "' AND (" + sqlshortstring + ")) ORDER BY ExtraDaysHistory.Data ASC;";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            DataTable td = new DataTable();
            td.Columns.Add("Pracownik");
            td.Columns.Add("Akcja");
            td.Columns.Add("IleDni");
            td.Columns.Add("PracownikHR");
            td.Columns.Add("Data");
            td.Columns.Add("Reason");
            td.Columns.Add("id");
            DataRow dr = null;

            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                DateTime myTime;
                user u = new user();
                user uhr = new user();
                while (sdr2.Read())
                {
                    dr = td.NewRow();
                    myTime = DateTime.Parse(sdr2["Data"].ToString());
                    string aaa = myTime.ToString("yyyy-MM-dd");
                    u = PolaczenieSQL.find_user(sdr2["Pracownik"].ToString());
                    if (u != null)
                    {
                        dr[0] = u.ToString();
                    }
                    else 
                    {
                        dr[0] = sdr2["Pracownik"].ToString();
                    }
                    dr[1] = sdr2["Akcja"].ToString();
                    dr[2] = sdr2["IleDni"].ToString();
                    uhr = PolaczenieSQL.find_user(sdr2["PracownikHR"].ToString());
                    if (uhr != null)
                    {
                        dr[3] = uhr.ToString();
                    }
                    else
                    {
                        dr[3] = sdr2["PracownikHR"].ToString();
                    }
                    dr[4] = aaa;
                    dr[5] = sdr2["Reason"].ToString();
                    dr[6] = sdr2["Id"].ToString();
                    td.Rows.Add(dr);
                }
                sdr2.Close();
                conn.Close();
                return td;
            }
            else return null;
        }

        public static string[] find_holiday_days_byuser(string userid)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT UserName, dni_p_rok, dni_o_rok, dni_n_rok, dni_wyk, dni_ekstra, dni_nz, dni_wyk_nrok FROM aspnet_Users Where UserName = @userid";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@userid", SqlDbType.VarChar, 50);
            command.Parameters["@userid"].Value = userid;
            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                string[] ArrayDays = new string[11];
                while (sdr2.Read())
                {
                    ArrayDays[0] = sdr2["dni_p_rok"].ToString();
                    ArrayDays[1] = sdr2["dni_o_rok"].ToString();
                    ArrayDays[2] = sdr2["dni_n_rok"].ToString();
                    ArrayDays[3] = sdr2["dni_wyk"].ToString();
                    ArrayDays[4] = sdr2["dni_wyk_nrok"].ToString();
                    ArrayDays[5] = sdr2["dni_ekstra"].ToString();
                    ArrayDays[6] = sdr2["dni_nz"].ToString();
                    ArrayDays[7] = sdr2["dni_wyk"].ToString();
                    ArrayDays[8] = sdr2["dni_o_rok"].ToString();
                    ArrayDays[9] = Convert.ToString(Convert.ToInt16(ArrayDays[2]) - Convert.ToInt16(ArrayDays[4]));
                    ArrayDays[10] = Convert.ToString(Convert.ToInt16(ArrayDays[0]) + Convert.ToInt16(ArrayDays[1]) + Convert.ToInt16(ArrayDays[5]) - Convert.ToInt16(ArrayDays[3]));
                    
                }
                sdr2.Close();
                conn.Close();
                return ArrayDays;
            }
            else return null;
        }

        public static List<string> find_holiday_bymenager(int status, string meanger_id)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery;

             sqlquery = "SELECT Holidays.Id, Holidays.Status, aspnet_Users.team, aspnet_Users.UserId, Holidays.UserId FROM Holidays LEFT JOIN aspnet_Users ON aspnet_Users.UserName=Holidays.UserId Where (holidays.Status = @ststusid AND (Holidays.MenagerId = @menagerid OR Holidays.OtherMenagerId = @menagerid))";


            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;


            command.Parameters.Add("@ststusid", SqlDbType.VarChar, 50);
            command.Parameters.Add("@menagerid", SqlDbType.VarChar, 50);
            command.Parameters["@ststusid"].Value = status;
            command.Parameters["@menagerid"].Value = meanger_id;
            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                List<string> holidaysID = new List<string>();
                while (sdr2.Read())
                {
                    holidaysID.Add(sdr2["Id"].ToString());
                }
                sdr2.Close();
                conn.Close();
                return holidaysID;
            }
            else return null;
        }
        
        public static GridView fill_holidays_by_menager_status(GridView gv1, int status, string meanger_id)
        {

            List<string> holiday_list = holidays.PolaczenieSQL.find_holiday_bymenager(status, meanger_id);
            List<string> holiday_list2 = null;
            if (status == 4)
            {
                holiday_list2 = holidays.PolaczenieSQL.find_holiday_bymenager(5, meanger_id);
            }

            DataTable td = new DataTable();
            td.Columns.Add("From");
            td.Columns.Add("To");
            td.Columns.Add("Pracownik");
            td.Columns.Add("ManagerId");
            td.Columns.Add("Rodzaj");
            td.Columns.Add("Id");
            td.Columns.Add("Ilosc_dni");
            DataRow dr = null;

            DateTime myTime;
            if (holiday_list != null)
            {
                foreach (string holidayID in holiday_list)
                {
                    holidays.holiday h = holidays.PolaczenieSQL.find_holiday(holidayID);
                    dr = td.NewRow();
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_od.ToString());
                        dr[0] = myTime.ToString("dd-MM-yyyy");
                    }
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_do.ToString());
                        dr[1] = myTime.ToString("dd-MM-yyyy");
                    }
                    holidays.user p = holidays.PolaczenieSQL.find_user(h.userid.ToString());
                    dr[2] = p.ToString();
                    holidays.user m = holidays.PolaczenieSQL.find_user(h.menager.ToString());
                    if (m != null)
                    {
                        dr[3] = m.ToString();
                    }
                    else
                    {
                        dr[3] = "User deleted";
                    }
                    dr[4] = h.rodzaj.ToString();
                    dr[5] = h.holidayid.ToString();
                    dr[6] = h.holiday_dyas_all.ToString();
                    td.Rows.Add(dr);
                }
            }
            if (holiday_list2 != null)
            {
                foreach (string holidayID in holiday_list2)
                {
                    holidays.holiday h = holidays.PolaczenieSQL.find_holiday(holidayID);
                    dr = td.NewRow();
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_od.ToString());
                        dr[0] = myTime.ToString("dd-MM-yyyy");
                    }
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_do.ToString());
                        dr[1] = myTime.ToString("dd-MM-yyyy");
                    }
                    holidays.user p = holidays.PolaczenieSQL.find_user(h.userid.ToString());
                    dr[2] = p.ToString();
                    holidays.user m = holidays.PolaczenieSQL.find_user(h.menager.ToString());
                    if (m != null)
                    {
                        dr[3] = m.ToString();
                    }
                    else
                    {
                        dr[3] = "User deleted";
                    }
                    dr[4] = h.rodzaj.ToString();
                    dr[5] = h.holidayid.ToString();
                    dr[6] = h.holiday_dyas_all.ToString();
                    td.Rows.Add(dr);
                }
            }


            gv1.DataSource = td;
            gv1.DataBind();
            holidays.PageMetods.holiday_status_color(gv1);

            return gv1;
        }

        public static GridView fill_holidays_by_status(GridView gv1, int status, string team_id = "all", string user_id = "all") 
        {

            List<string> holiday_list = holidays.PolaczenieSQL.find_holiday_bystatus(status, team_id, user_id);
            List<string> holiday_list2 = null;
            if (status == 4)
            {
                holiday_list2 = holidays.PolaczenieSQL.find_holiday_bystatus(5, team_id, user_id);
            }

            DataTable td = new DataTable();
            td.Columns.Add("From");
            td.Columns.Add("To");
            td.Columns.Add("Pracownik");
            td.Columns.Add("ManagerId");
            td.Columns.Add("Rodzaj");
            td.Columns.Add("Id");
            td.Columns.Add("Ilosc_dni");
            DataRow dr = null;

            DateTime myTime;
            if (holiday_list != null)
            {
                foreach (string holidayID in holiday_list)
                {
                    holidays.holiday h = holidays.PolaczenieSQL.find_holiday(holidayID);
                    dr = td.NewRow();
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_od.ToString());
                        dr[0] = myTime.ToString("dd-MM-yyyy");
                    }
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_do.ToString());
                        dr[1] = myTime.ToString("dd-MM-yyyy");
                    }
                    holidays.user p = holidays.PolaczenieSQL.find_user(h.userid.ToString());
                    dr[2] = p.ToString();
                    holidays.user m = holidays.PolaczenieSQL.find_user(h.menager.ToString());
                    if (m != null)
                    {
                        dr[3] = m.ToString();
                    }
                    else
                    {
                        dr[3] = "User deleted";
                    }
                    dr[4] = h.rodzaj.ToString();
                    dr[5] = h.holidayid.ToString();
                    dr[6] = h.holiday_dyas_all.ToString();
                    td.Rows.Add(dr);
                }
            }
            if (holiday_list2 != null)
            {
                foreach (string holidayID in holiday_list2)
                {
                    holidays.holiday h = holidays.PolaczenieSQL.find_holiday(holidayID);
                    dr = td.NewRow();
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_od.ToString());
                        dr[0] = myTime.ToString("dd-MM-yyyy");
                    }
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_do.ToString());
                        dr[1] = myTime.ToString("dd-MM-yyyy");
                    }
                    holidays.user p = holidays.PolaczenieSQL.find_user(h.userid.ToString());
                    dr[2] = p.ToString();
                    holidays.user m = holidays.PolaczenieSQL.find_user(h.menager.ToString());
                    if (m != null)
                    {
                        dr[3] = m.ToString();
                    }
                    else
                    {
                        dr[3] = "User deleted";
                    }
                    dr[4] = h.rodzaj.ToString();
                    dr[5] = h.holidayid.ToString();
                    dr[6] = h.holiday_dyas_all.ToString();
                    td.Rows.Add(dr);
                }
            }


            gv1.DataSource = td;
            gv1.DataBind();
            holidays.PageMetods.holiday_status_color(gv1);

            return gv1;
        }

        public static GridView fill_holidays_by_user(GridView gv1, string user_id, int max = 15)
        {

            List<string> holiday_list = holidays.PolaczenieSQL.find_holiday_byuser(user_id);

            DataTable td = new DataTable();
            td.Columns.Add("From");
            td.Columns.Add("To");
            td.Columns.Add("Rodzaj");
            td.Columns.Add("ManagerId");
            td.Columns.Add("Status");
            td.Columns.Add("Id");
            td.Columns.Add("Ilosc_dni");
            DataRow dr = null;

            DateTime myTime;
            if (holiday_list != null)
            {
                foreach (string holidayID in holiday_list)
                {
                    holidays.holiday h = holidays.PolaczenieSQL.find_holiday(holidayID);
                    dr = td.NewRow();
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_od.ToString());
                        dr[0] = myTime.ToString("yyyy-MM-dd");
                        if (h.statusid == "3" && (myTime <= DateTime.Today))
                        {
                            h.wykorzystany();
                            h.status = "Wykonany";
                        }
                    }
                    if (h.date_od != null)
                    {
                        myTime = DateTime.Parse(h.date_do.ToString());
                        dr[1] = myTime.ToString("yyyy-MM-dd");
                    }
                    dr[2] = h.rodzaj.ToString();
                    holidays.user m = holidays.PolaczenieSQL.find_user(h.menager.ToString());
                    if (m != null)
                    {
                        dr[3] = m.ToString();
                    }
                    else
                    {
                        dr[3] = "User deleted";
                    }
                    dr[4] = h.status.ToString();
                    dr[5] = h.holidayid.ToString();
                    dr[6] = h.holiday_dyas_all.ToString();
                    td.Rows.Add(dr);

                    if (td.Rows.Count >= max) { break; }
                }
            }

            gv1.DataSource = td;
            gv1.DataBind();
            holidays.PageMetods.holiday_status_color(gv1);

            return gv1;
        }

        public static GridView fill_holiday_history(GridView gv2, string holiday_id)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT history_holiday_id, history_time, history_person_id, history_text  FROM HolidayHistory Where history_holiday_id = @holidayid";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@holidayid", SqlDbType.VarChar, 50);
            command.Parameters["@holidayid"].Value = holiday_id;
            sdr2 = command.ExecuteReader();

            DataTable td = new DataTable();
            td.Columns.Add("Data");
            td.Columns.Add("Akcja");
            td.Columns.Add("Osoba");
            DataRow dr = null;

            DateTime myTime;

            if (sdr2.HasRows == true)
            {
                while (sdr2.Read())
                {
                    dr = td.NewRow();
                    if (sdr2["history_time"].ToString() != null)
                    {
                        myTime = DateTime.Parse(sdr2["history_time"].ToString());
                        dr[0] = myTime.ToString();
                    }
                    dr[1] = sdr2["history_text"].ToString();
                    if (sdr2["history_person_id"].ToString() != "System")
                    {
                        holidays.user u = holidays.PolaczenieSQL.find_user(sdr2["history_person_id"].ToString());
                        dr[2] = u.ToString();
                    }
                    else 
                    {
                        dr[2] = "System";
                    }
                    td.Rows.Add(dr);
                }
            }
            
            gv2.DataSource = td;
            gv2.DataBind();
            return gv2;

        }

        public static DropDownList list_of_holiday_names(DropDownList ddl){

            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            ddl.Items.Clear();
                ListItem firstitem2 = new ListItem();
                firstitem2.Value = "-1";
                firstitem2.Text = "Wybierz rodzaj urlopu";
                DropDownList newddl = ddl;
                newddl.Items.Add(firstitem2);
                string HolidayType = "";
                string sqlquery2 = "SELECT Id, HolidayType_Name, HolidayType_Short_name FROM HolidaysType";

                SqlCommand command2 = new SqlCommand(sqlquery2, conn);
                SqlDataReader sdr3;
                sdr3 = command2.ExecuteReader();
                while (sdr3.Read())
                {
                    ListItem nextitem = new ListItem();
                    HolidayType = sdr3["HolidayType_Name"].ToString() + " (" + sdr3["HolidayType_Short_name"].ToString() + ")";
                    nextitem.Value = sdr3["Id"].ToString();
                    nextitem.Text = HolidayType;
                    newddl.Items.Add(nextitem);
                    nextitem = null;
                }
                sdr3.Close();
                return newddl;
        }

        public static DropDownList list_of_menagers_names(DropDownList ddl)
        {
            string[] menagers;

            ddl.Items.Clear();
            ListItem firstitem2 = new ListItem();
            firstitem2.Value = "-1";
            firstitem2.Text = "Wybierz menagera";
            DropDownList newddl = ddl;
            newddl.Items.Add(firstitem2);

            menagers = Roles.GetUsersInRole("menager");

            foreach (string menager in menagers)
            {
                ListItem nextitem = new ListItem();
                holidays.user men = holidays.PolaczenieSQL.find_user(menager);
                nextitem.Value = men.userid.ToString();
                nextitem.Text = men.ToString();
                ddl.Items.Add(nextitem);
                nextitem = null;
            }
            return newddl;
        }

        public static DropDownList list_of_users_names(DropDownList ddl, string user_id = "none")
        {
            string[] emploees;

            ddl.Items.Clear();
            ListItem firstitem2 = new ListItem();
            firstitem2.Value = "-1";
            firstitem2.Text = "AAAAAAAWszyscy pracownicy";
            DropDownList newddl = ddl;
            newddl.Items.Add(firstitem2);

            emploees = Roles.GetUsersInRole("emploee");

            foreach (string emploee in emploees)
            {
                ListItem nextitem = new ListItem();
                holidays.user men = holidays.PolaczenieSQL.find_user(emploee);
                nextitem.Value = men.userid.ToString();
                nextitem.Text = men.ToString();
                ddl.Items.Add(nextitem);
                nextitem = null;
            }
            PageMetods.SortDDL(ref newddl);
            for (int ii = 0; ii < newddl.Items.Count; ii++)
            {
                if (newddl.Items[ii].Text == "AAAAAAAWszyscy pracownicy")
                {
                    newddl.Items[ii].Text = "Wszyscy pracownicy";
                }
            }
            

            if (!String.Equals(user_id, "none"))
            {
                newddl.SelectedValue = user_id;
            }

            return newddl;
        }

        public static DropDownList list_of_usersid(DropDownList ddl, string user_id = "none")
        {
            string[] emploees;

            ddl.Items.Clear();
            ListItem firstitem2 = new ListItem();
            firstitem2.Value = "-1";
            firstitem2.Text = "AAAAAAASelect user";
            DropDownList newddl = ddl;
            newddl.Items.Add(firstitem2);

            emploees = Roles.GetUsersInRole("emploee");

            foreach (string emploee in emploees)
            {
                ListItem nextitem = new ListItem();
                holidays.user men = holidays.PolaczenieSQL.find_user(emploee);
                nextitem.Value = men.userid.ToString();
                nextitem.Text = men.userid.ToString();
                ddl.Items.Add(nextitem);
                nextitem = null;
            }
            PageMetods.SortDDL(ref newddl);
            for (int ii = 0; ii < newddl.Items.Count; ii++)
            {
                if (newddl.Items[ii].Text == "AAAAAAASelect user")
                {
                    newddl.Items[ii].Text = "Select user";
                }
            }


            if (!String.Equals(user_id, "none"))
            {
                newddl.SelectedValue = user_id;
            }

            return newddl;
        }

        public static DropDownList list_of_teams_names(DropDownList ddl, string team_id = "none")
        {

            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            ddl.Items.Clear();
            ListItem firstitem2 = new ListItem();
            firstitem2.Value = "-1";
            firstitem2.Text = "Wszystkie teamy";
            DropDownList newddl = ddl;
            newddl.Items.Add(firstitem2);
            string team_name = "";
            string sqlquery2 = "SELECT Teams.Id, Teams.Name FROM Teams ORDER BY Name ASC";

            SqlCommand command2 = new SqlCommand(sqlquery2, conn);
            SqlDataReader sdr3;
            sdr3 = command2.ExecuteReader();
            while (sdr3.Read())
            {
                ListItem nextitem = new ListItem();
                team_name = sdr3["Name"].ToString();
                nextitem.Value = sdr3["Id"].ToString();
                nextitem.Text = team_name;
                newddl.Items.Add(nextitem);
                nextitem = null;
            }
            sdr3.Close();

            if(!String.Equals(team_id,"none"))
            {
                newddl.SelectedValue = team_id;
            }

            return newddl;

        }

        public static Repeater list_of_holiday_ststus(Repeater rep1)
        {

            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery2 = "SELECT HolidayStatus.Id, HolidayStatus.StatusText FROM HolidayStatus ORDER BY StatusText ASC";

            SqlCommand command2 = new SqlCommand(sqlquery2, conn);
            SqlDataReader sdr3;
            sdr3 = command2.ExecuteReader();
            if (sdr3.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("val", typeof(string));

                while(sdr3.Read())
                {
                    dt.Rows.Add(sdr3["StatusText"].ToString());
                }

                rep1.DataSource = dt;
                rep1.DataBind();
            }
            sdr3.Close();

            return rep1;

        }

        public static void insertholiday(holiday h, user u)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Holidays (Od, Do, Rodzaj, Status, UserId, MenagerId, OtherMenagerId, days_count, days_count_this, days_count_next, days_count_with_all, all_days_id) VALUES (@od, @do , @rodzaj, @status, @userid, @menagerid, @othermenagerid, @days_count, @days_count_this, @days_count_next, @days_count_with_all, @all_days_id)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@status", SqlDbType.Int, 1).Value = 1;
                cmd.Parameters.Add("@menagerid", SqlDbType.VarChar, 50).Value = h.menager.ToString();
                cmd.Parameters.Add("@othermenagerid", SqlDbType.VarChar, 50).Value = u.menager.ToString();
                cmd.Parameters.Add("@rodzaj", SqlDbType.Int, 3).Value = h.rodzaj.ToString();
                cmd.Parameters.Add("@userid", SqlDbType.VarChar, 50).Value = h.userid.ToString();
                cmd.Parameters.Add("@od", SqlDbType.Date, 50).Value = h.date_od;
                cmd.Parameters.Add("@do", SqlDbType.Date, 50).Value = h.date_do;
                cmd.Parameters.Add("@days_count", SqlDbType.Int, 3).Value = h.holiday_dyas_all.ToString();
                cmd.Parameters.Add("@days_count_this", SqlDbType.Int, 3).Value = h.holiday_dyas.ToString();
                cmd.Parameters.Add("@days_count_next", SqlDbType.Int, 3).Value = h.holiday_dyas_next.ToString();
                cmd.Parameters.Add("@days_count_with_all", SqlDbType.Int, 3).Value = h.holiday_dyas_ciag.ToString();
                cmd.Parameters.Add("@all_days_id", SqlDbType.VarChar).Value = h.wszystkiedni_status.ToString();
                               
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Cancel();

                if (String.Equals(h.rodzaj, "9") || String.Equals(h.rodzaj, "10")) 
                {
                    string sql2 = "UPDATE aspnet_Users SET dni_wyk = @dni_wyk, dni_wyk_nrok = @dni_wyk_nrok, dni_nz = @dni_nz Where UserName = @user";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);

                    cmd2.Parameters.Add("@dni_wyk_nrok", SqlDbType.Int, 2).Value = u.ilosc_wyk_next + h.holiday_dyas_next;
                    cmd2.Parameters.Add("@dni_nz", SqlDbType.Int, 2).Value = u.ilosc_nz - h.holiday_dyas_nz;
                    cmd2.Parameters.Add("@dni_wyk", SqlDbType.Int, 2).Value = u.ilosc_wyk + h.holiday_dyas;
                    cmd2.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = u.userid;

                    cmd2.CommandType = CommandType.Text;
                    cmd2.ExecuteNonQuery();
                }

                string sql3 = "SELECT * FROM Holidays WHERE Id = (SELECT MAX(Id) FROM Holidays)";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                SqlDataReader srd3;

                cmd3.CommandType = CommandType.Text;
                srd3 = cmd3.ExecuteReader();

                string holidayID = "0";

                while (srd3.Read())
                {
                    holidayID = srd3["Id"].ToString();
                }
                
                conn.Close();

                h.holidayid = holidayID;
                h.addHistory("Utworzenie wniosku urlopowego");
                mailSender.SendMailAddHoliday(h.holidayid, h.userid, h.menager);
                
            }
        }

        public static void insertswieto(string date_sw, string name_sw)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO holiday_days (date, name, stringdateid) VALUES (@date, @name, @stringdateid)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                DateTime myDate = DateTime.Parse(date_sw);
                cmd.Parameters.Add("@date", SqlDbType.Date, 50).Value = myDate.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name_sw;
                cmd.Parameters.Add("@stringdateid", SqlDbType.VarChar, 10).Value = myDate.Year.ToString() + "_" + myDate.DayOfYear.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Clone();
                conn.Close();

            }
        }

        public static void usunswieto(string id_sw)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql = "DELETE FROM holiday_days Where Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = id_sw;
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Cancel();

            }
        }

        public static string[] print_history_pdf(string history_id)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                string[] textData = new string[7];
                conn.Open();
                string sqlquery2 = "SELECT ExtraDaysHistory.Id, ExtraDaysHistory.Pracownik, ExtraDaysHistory.PracownikHR, ExtraDaysHistory.IleDni, ExtraDaysHistory.Reason, ExtraDaysHistory.Data, ExtraDaysHistory.Akcja FROM ExtraDaysHistory WHERE ExtraDaysHistory.Id = @id";

                SqlCommand command2 = new SqlCommand(sqlquery2, conn);
                command2.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = history_id;
                SqlDataReader sdr3;
                sdr3 = command2.ExecuteReader();
                if (sdr3.HasRows)
                {
                    while (sdr3.Read())
                    {
                        textData[0] = sdr3["Id"].ToString();
                        textData[1] = PolaczenieSQL.find_user(sdr3["Pracownik"].ToString()) + " [" + sdr3["Pracownik"].ToString() + "]";
                        textData[2] = PolaczenieSQL.find_user(sdr3["PracownikHR"].ToString()) + " [" + sdr3["PracownikHR"].ToString() + "]";
                        textData[3] = sdr3["IleDni"].ToString();
                        textData[4] = sdr3["Reason"].ToString();
                        textData[5] = sdr3["Data"].ToString();
                        textData[6] = sdr3["Akcja"].ToString();
                    }
                }
                sdr3.Close();

                return textData;

            }
        }

        public static List<string> find_team_members_byteamid(string teamid)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            conn.Open();

            string sqlquery = "SELECT UserName, team FROM aspnet_Users Where team = @teamid";

            SqlCommand command = new SqlCommand(sqlquery, conn);
            SqlDataReader sdr2;

            command.Parameters.Add("@teamid", SqlDbType.VarChar, 50);
            command.Parameters["@teamid"].Value = teamid;
            sdr2 = command.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                    List<string> team_members = new List<string>();
                    while (sdr2.Read())
                    {
                        team_members.Add(sdr2["UserName"].ToString());
                    }
                    sdr2.Close();
                    conn.Close();
                    return team_members;
            }
            else return null;
        }

        public static void addhistory(string holidayid, string history_id, string user_id, DateTime time_now)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO HolidayHistory (history_holiday_id, history_text, history_person_id, history_time) VALUES (@holidayid, @text, @personid, @time)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                //DateTime myDate = DateTime.Parse(date_sw);
                cmd.Parameters.Add("@holidayid", SqlDbType.VarChar, 6).Value = holidayid;
                cmd.Parameters.Add("@text", SqlDbType.VarChar, 50).Value = history_id;
                cmd.Parameters.Add("@personid", SqlDbType.VarChar, 50).Value = user_id;
                cmd.Parameters.Add("@time", SqlDbType.DateTime, 21).Value = time_now;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Clone();
                conn.Close();

            }
        }

        public static void update_dni_urlopowe(string user_id, int update_dni_wyk, int update_dni_nz, int update_dni_wyk_nrok, int update_dni_ekstra)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql2 = "UPDATE aspnet_Users SET dni_wyk = @dni_wyk, dni_wyk_nrok = @dni_wyk_nrok, dni_nz = @dni_nz, dni_ekstra = @dni_ekstra Where UserName = @user";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                cmd2.Parameters.Add("@dni_wyk_nrok", SqlDbType.Int, 2).Value = update_dni_wyk_nrok;
                cmd2.Parameters.Add("@dni_nz", SqlDbType.Int, 2).Value = update_dni_nz;
                cmd2.Parameters.Add("@dni_wyk", SqlDbType.Int, 2).Value = update_dni_wyk;
                cmd2.Parameters.Add("@dni_ekstra", SqlDbType.Int, 2).Value = update_dni_ekstra;
                cmd2.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = user_id;

                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
                conn.Close();

            }
        }

        public static void updateHolidayStatus(string holidayid, int status_id)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql2 = "UPDATE Holidays SET Status = @Status Where Id = @id";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                cmd2.Parameters.Add("@Status", SqlDbType.Int, 2).Value = status_id;
                cmd2.Parameters.Add("@id", SqlDbType.Int, 2).Value = holidayid;

                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
                conn.Close();

            }
        }

        public static void update20to26(List<string> userlist, string userHRid, string toNumber)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql2 = "";
                if (toNumber == "2")
                {
                    sql2 = "UPDATE aspnet_Users SET dniurlopowe = '2', kiedy26 = NULL, dni_n_rok = 26, dni_o_rok = 26  Where UserName = @id";
                }
                else
                {
                    sql2 = "UPDATE aspnet_Users SET dniurlopowe = '1', dni_n_rok = 20, dni_o_rok = 20  Where UserName = @id";
                }
                
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.Add("@id", SqlDbType.NChar);
                cmd2.CommandType = CommandType.Text;

                foreach (string userid in userlist) 
                {
                    cmd2.Parameters["@id"].Value = userid;
                    cmd2.ExecuteNonQuery();
                }
                cmd2.Cancel();
                conn.Close();

            }
        }

        public static void addextraday(List<string> userlist, int addnumber)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql2 = "UPDATE aspnet_Users SET dni_ekstra = @ekstradni Where UserName = @id";

                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                cmd2.Parameters.Add("@id", SqlDbType.NChar);
                cmd2.Parameters.Add("@ekstradni", SqlDbType.Int, 2);

                cmd2.CommandType = CommandType.Text;

                int noweExtradni;
                
                foreach (string userid in userlist) 
                {
                    user u = PolaczenieSQL.find_user(userid, false);
                    noweExtradni = u.ilosc_dodatkowy + addnumber;
                    cmd2.Parameters["@id"].Value = userid;
                    cmd2.Parameters["@ekstradni"].Value = noweExtradni;
                    cmd2.ExecuteNonQuery();
                }
                cmd2.Cancel();
                conn.Close();

            }
        }

        public static void addHRhistory(List<string> userlist, int addnumber, string reasonText, string hruserid, string akcja)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql3 = "INSERT INTO ExtraDaysHistory (Pracownik, PracownikHR, IleDni, Reason, Data, Akcja) VALUES (@pracownik, @pracownikHR, @ileDni, @reason, @data, @akcja)";

                SqlCommand cmd3 = new SqlCommand(sql3, conn);

                cmd3.Parameters.Add("@pracownik", SqlDbType.NChar);
                cmd3.Parameters.Add("@pracownikHR", SqlDbType.NChar);
                cmd3.Parameters.Add("@ileDni", SqlDbType.Int);
                cmd3.Parameters.Add("@reason", SqlDbType.NChar);
                cmd3.Parameters.Add("@data", SqlDbType.Date);
                cmd3.Parameters.Add("@akcja", SqlDbType.NChar);

                cmd3.CommandType = CommandType.Text;

                DateTime now = DateTime.Now;

                cmd3.Parameters["@pracownikHR"].Value = hruserid;
                cmd3.Parameters["@ileDni"].Value = addnumber;
                cmd3.Parameters["@reason"].Value = reasonText;
                cmd3.Parameters["@data"].Value = now;
                cmd3.Parameters["@akcja"].Value = akcja;

                foreach (string userid in userlist)
                {
                    cmd3.Parameters["@pracownik"].Value = userid;
                    cmd3.ExecuteNonQuery();
                }
                cmd3.Cancel();
                conn.Close();

            }
        }

        public static void updateNewyear()
        {

            MembershipUserCollection users;
            users = Membership.GetAllUsers();

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql2 = "UPDATE aspnet_Users SET dni_p_rok = @dni_p_rok, dni_o_rok = @dni_o_rok, dni_wyk = @dni_wyk, dni_ekstra = 0, dni_nz = 4, dni_wyk_nrok = 0 Where UserName = @user";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.Add("@user", SqlDbType.NChar);
                cmd2.Parameters.Add("@dni_p_rok", SqlDbType.Int, 2);
                cmd2.Parameters.Add("@dni_o_rok", SqlDbType.Int, 2);
                cmd2.Parameters.Add("@dni_wyk", SqlDbType.Int, 2);

                cmd2.CommandType = CommandType.Text;

                int poprzedni_rok_old;
                int obecny_rok_old;
                int nastepny_rok_old;
                int wykorzystane_old;
                int wykorzystane_nastepny_next_old;
                int ekstra_old;
                int poprzedni_rok_new;
                int obecny_rok_new;
                int wykorzystane_new;

                foreach (MembershipUser userid in users)
                {
                    if (Roles.IsUserInRole(userid.UserName, "emploee"))
                    {
                        user u = PolaczenieSQL.find_user(userid.UserName, false);
                        poprzedni_rok_old = u.ilosc_poprzedni;
                        obecny_rok_old = u.ilosc_obecny;
                        nastepny_rok_old = u.ilosc_nastepny;
                        wykorzystane_old = u.ilosc_wyk;
                        wykorzystane_nastepny_next_old = u.ilosc_wyk_next;
                        ekstra_old = u.ilosc_dodatkowy;

                        poprzedni_rok_new = poprzedni_rok_old + obecny_rok_old + ekstra_old - wykorzystane_old;
                        obecny_rok_new = nastepny_rok_old;
                        wykorzystane_new = wykorzystane_nastepny_next_old;

                        cmd2.Parameters["@user"].Value = userid.UserName;
                        cmd2.Parameters["@dni_p_rok"].Value = poprzedni_rok_new;
                        cmd2.Parameters["@dni_o_rok"].Value = obecny_rok_new;
                        cmd2.Parameters["@dni_wyk"].Value = wykorzystane_new;
                        cmd2.ExecuteNonQuery();
                    }
                                        
                }
                cmd2.Cancel();
                conn.Close();
            }

            
        }

        public static string calculationdate()
        {
            string actionDate = "";

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();

                string sql3 = "SELECT Data, Id, Akcja FROM ExtraDaysHistory WHERE Id = (SELECT MAX(Id) FROM ExtraDaysHistory Where Akcja = 'New Year calculations')";
                //"SELECT Data, Id, Akcja FROM ExtraDaysHistory WHERE Id = (
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                SqlDataReader srd3;

                cmd3.CommandType = CommandType.Text;
                srd3 = cmd3.ExecuteReader();

                while (srd3.Read())
                {
                    actionDate = srd3["Data"].ToString();
                    string actionID = srd3["Id"].ToString();
                }

                cmd3.Cancel();
                conn.Close();

            }
            return actionDate;
        }

        public static void updatepasstemp(string is_pass_temp, string userid)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql2 = "";
                if (is_pass_temp == "yes")
                {
                    sql2 = "UPDATE aspnet_Users SET passtemp = '1' Where UserName = @id";
                }
                else
                {
                    sql2 = "UPDATE aspnet_Users SET passtemp = '0' Where UserName = @id";
                }

                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.Add("@id", SqlDbType.NChar);
                cmd2.CommandType = CommandType.Text;

                    cmd2.Parameters["@id"].Value = userid;
                    cmd2.ExecuteNonQuery();

                cmd2.Cancel();
                conn.Close();

            }
        }
    }

    public static class PageMetods
    {      
        public static void holiday_status_color(GridView datagrindview)
        {
            Color backcolor;
            int col_num = 0;
            bool found = false;

            for (int ii = 0; ii < datagrindview.Columns.Count; ii++) 
            {
                if (datagrindview.Columns[ii].ToString() == "Status") 
                {
                    col_num = ii;
                    found = true;
                    break;
                }
            }

            if (!found) { return; }

            for (int i = 0; i < datagrindview.Rows.Count; i++)
            {
                string celltext = datagrindview.Rows[i].Cells[col_num].Text;
                switch (celltext)
                {
                    case "Czeka na zatwierdzenie przez menagera":
                        backcolor = Color.LightYellow;
                        break;
                    case "Czeka na zatwierdzenie przez dział HR":
                        backcolor = Color.LightYellow;
                        break;
                    case "Zatwierdzony":
                        backcolor = Color.LightGreen;
                        break;
                    case "Wykonany":
                        backcolor = Color.DarkSeaGreen;
                        break;
                    case "Odrzucony przez menagera":
                        backcolor = Color.LightPink;
                        break;
                    case "Odrzucony przez HR":
                        backcolor = Color.LightPink;
                        break;
                    case "Usunięty":
                        backcolor = Color.LightGray;
                        break;
                    default:
                        backcolor = Color.Transparent;
                        break;
                }
                datagrindview.Rows[i].Cells[col_num].BackColor = backcolor;
            }
        }

        public static int licz_dni_pierwszego_roku(DateTime datazatrudnienia, double ma26)
        {
            DateTime dt1 = new DateTime(DateTime.Today.Year, 12, 31);
            DateTime dt2 = datazatrudnienia;
            double days = dt1.Subtract(dt2).TotalDays;
            double monyths = Math.Round((days / 30), 2);
            days = Math.Round((monyths * ma26));
            int h_days = Convert.ToInt16(days);
            return h_days;
        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static void SortDDL(ref DropDownList objDDL)
        {
            ArrayList textList = new ArrayList();
            ArrayList valueList = new ArrayList();


            foreach (ListItem li in objDDL.Items)
            {
                textList.Add(li.Text);
            }

            textList.Sort();


            foreach (object item in textList)
            {
                string value = objDDL.Items.FindByText(item.ToString()).Value;
                valueList.Add(value);
            }
            objDDL.Items.Clear();

            for (int i = 0; i < textList.Count; i++)
            {
                ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
                objDDL.Items.Add(objItem);
            }
        }

        public static HtmlControl month_calendar_by_holidayid(HtmlControl thisDiv, DateTime calendar_begin, DateTime calendar_end, string userid, bool teamyn = false, string holidayid = "0", bool menageryn = false)
        {

            List<string> personsid = new List<string>();
            List<string> personsToString = new List<string>();
            List<string> wszystkieUrlopy = new List<string>();

            string[] miesiace = new string[] { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
            double montsbetween = Math.Round(((calendar_end - calendar_begin).TotalDays / 29), 0);
            List<string> swieta_date = PolaczenieSQL.find_swieta_list_short();
            List<string> wszytskiedni;

            user u = PolaczenieSQL.find_user(userid);

            if (!menageryn)
            {
                personsid.Add(userid);
                personsToString.Add(u.ToString());
                wszytskiedni = PolaczenieSQL.find_holiday_days_status_byuser(u.userid, calendar_begin.ToShortDateString(), calendar_end.ToShortDateString());
                if (wszytskiedni != null)
                {
                    foreach (string urlop in wszytskiedni)
                    {
                        wszystkieUrlopy.Add(urlop);
                    }
                }
            }

            if (teamyn)
            {
                if (!menageryn)
                {
                    team t = new team(u.teamid);
                    for (int ii = 0; ii < t.team_member.Count; ii++)
                    {
                        if (!String.Equals(personsid[0], t.team_member[ii]))
                        {
                            u = PolaczenieSQL.find_user(t.team_member[ii]);
                            personsid.Add(t.team_member[ii]);
                            personsToString.Add(u.ToString());
                            wszytskiedni = PolaczenieSQL.find_holiday_days_status_byuser(u.userid, calendar_begin.ToShortDateString(), calendar_end.ToShortDateString());

                            if (wszytskiedni != null)
                            {
                                foreach (string urlop in wszytskiedni)
                                {
                                    wszystkieUrlopy.Add(urlop);
                                }
                            }
                        }
                    }
                }
                else
                {
                    List<string> teams = PolaczenieSQL.find_teams_bymenager(u.userid);
                    if (teams != null)
                    {
                        team t;
                        foreach (string team in teams)
                        {
                            t = new team(team);
                            for (int ii = 0; ii < t.team_member.Count; ii++)
                            {
                                u = PolaczenieSQL.find_user(t.team_member[ii]);
                                personsid.Add(t.team_member[ii]);
                                personsToString.Add(u.ToString());
                                wszytskiedni = PolaczenieSQL.find_holiday_days_status_byuser(u.userid, calendar_begin.ToShortDateString(), calendar_end.ToShortDateString());
                                if (wszytskiedni != null)
                                {
                                    foreach (string urlop in wszytskiedni)
                                    {
                                        wszystkieUrlopy.Add(urlop);
                                    }
                                }
                            }
                        }
                    }
                }
            }


            for (int i = 0; i < montsbetween + 1 ;i++)
            {
                DateTime datamonth = calendar_begin.AddMonths(i);
                int year = datamonth.Year;
                int month = datamonth.Month;
                int days_count;
                days_count = DateTime.DaysInMonth(year, month);
                Table tb = new Table();
                for (int p = 0; p < personsid.Count; p++)
                {
                    //u = PolaczenieSQL.find_user(personsid[p]);
                    //wszytskiedni = PolaczenieSQL.find_holiday_days_status_byuser(u.userid, calendar_begin.ToShortDateString(), calendar_end.ToShortDateString());
                    //string wszytskiedni = PolaczenieSQL.find_holiday_days_status_byuser(userid);
                    TableRow tr = new TableRow();
                    tb.Rows.Add(tr);
                    TableCell tCell = new TableCell();
                        tr.Cells.Add(tCell);
                        if (teamyn)
                        {
                            tCell.Text = personsToString[p];
                        }
                        else
                        {
                            tCell.Text = miesiace[month - 1] + " " + year.ToString();
                        }
                        tCell.CssClass = "cellnormal_name";

                    for (int iii = 1; iii <= days_count; iii++)
                    {
                        tCell = new TableCell();
                        tr.Cells.Add(tCell);
                        tCell.Text = iii.ToString();
                        tCell.CssClass = "cellnormal";
                        DateTime thisday = new DateTime(year, month, iii);
                        tCell.ID = personsid[p] + "_" + Convert.ToString(year) + "_" + thisday.DayOfYear.ToString();

                        if (thisday.DayOfWeek == DayOfWeek.Sunday || thisday.DayOfWeek == DayOfWeek.Saturday)
                        {
                            tCell.BackColor = Color.FromArgb(255, 180, 180);
                        }

                        if (swieta_date.Contains(thisday.Year.ToString() + "_" + thisday.DayOfYear.ToString()))
                        {
                            tCell.BackColor = Color.Coral;
                            string findstring = thisday.Year.ToString() + "_" + thisday.DayOfYear.ToString();
                            int takeList = swieta_date.IndexOf(findstring);
                            tCell.ToolTip = swieta_date[takeList + 1];
                        }

                    }

                }
                if (teamyn)
                {
                    string str_id2 = "l" + i.ToString();
                    HtmlControl divl = (HtmlControl)thisDiv.FindControl(str_id2);
                    divl.Controls.Add(new LiteralControl(miesiace[month - 1] + " " + year.ToString()));
                }

                string str_id = "m" + i.ToString();
                HtmlControl divm = (HtmlControl)thisDiv.FindControl(str_id);
                divm.Controls.Add(tb);

                if (datamonth == calendar_end) {break; }
            }

            if (wszystkieUrlopy != null)
            {
                string findstring;
                foreach (string holidaystring in wszystkieUrlopy)
                {
                    string[] words = holidaystring.Split(',');
                    string[] holidays_ids = words[3].Split(';');
                    foreach (string ids in holidays_ids)
                    {
                        if (words[1] == "4" || words[1] == "5" || words[1] == "7") { break; }
                        findstring = words[4] + "_" + ids;
                        TableCell myControl1 = (TableCell)thisDiv.FindControl(findstring);
                        if (myControl1 != null)
                        {

                            if (words[0] == holidayid)
                            {
                                myControl1.Font.Bold = true;
                            }

                            if (myControl1.BackColor.IsEmpty)
                            {
                                switch (words[1])
                                {
                                    case "1":
                                        myControl1.BackColor = Color.Yellow;
                                        myControl1.ToolTip = "Status: Waiting for approval";
                                        break;
                                    case "2":
                                        myControl1.BackColor = Color.Yellow;
                                        myControl1.ToolTip = "Status: Waiting for approval";
                                        break;
                                    case "3":
                                        myControl1.BackColor = Color.LightGreen;
                                        myControl1.ToolTip = "Status: Approved";
                                        break;
                                    case "6":
                                        myControl1.BackColor = Color.DarkSeaGreen;
                                        myControl1.ToolTip = "Status: Done";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            
                            switch (words[2])
                            {
                                case "9":
                                    myControl1.Text = "W";
                                    myControl1.ToolTip = myControl1.ToolTip + " ; Urlop wypoczynkowy";
                                    break;
                                case "10":
                                    myControl1.Text = "NŻ";
                                    myControl1.ToolTip = myControl1.ToolTip + " ; Na żądanie";
                                    break;
                                case "11":
                                    myControl1.Text = "B";
                                    myControl1.ToolTip = myControl1.ToolTip + " ; Urlop bezpłatny";
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                }
            }

            return thisDiv;
        }
    }

    public class holiday
    {
        public DateTime date_od;
        public DateTime date_do;
        public string menager;
        public string othermenager;
        public string status;
        public string statusid;
        public string rodzaj;
        public string rodzajid;
        public string userid;
        public string holidayid;
        public int holiday_dyas_ciag;
        public int holiday_dyas;
        public int holiday_dyas_next;
        public int holiday_dyas_all;
        public int holiday_dyas_nz;
        public string wszystkiedni_status;

        public holiday()
        {
        }

        public holiday(string odtext, string dotext, string menagertext, string ststustextid, string ststustext, string rodzajtext, string useridtext, string holidayidtext = "0", bool listadni = false)
        {

            DateTime myTime;

            if (odtext != string.Empty)
            {
                myTime = DateTime.Parse(odtext);
                this.date_od = myTime;
                //this.date_od = DateTime.ParseExact(odtext, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            if (dotext != string.Empty)
            {
                myTime = DateTime.Parse(dotext);
                this.date_do = myTime;
                //this.date_do = DateTime.ParseExact(dotext, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            this.menager = menagertext;
            this.status = ststustext;
            this.statusid = ststustextid;
            this.rodzaj = rodzajtext;
            this.userid = useridtext;
            this.holidayid = holidayidtext;

            DateTime dt1 = date_od;
            DateTime dt2 = date_do;
            TimeSpan days = dt1.Subtract(dt2);

            this.holiday_dyas_ciag = Convert.ToInt16(dt2.Subtract(dt1).TotalDays) + 1;
            int int_holiday_dyas_all = this.holiday_dyas_ciag;
            int day_count = 0;
            int day_count_next = 0;
            List<string> swieta_date = PolaczenieSQL.find_swieta_list();
            Boolean isholiday;
            string daysstring = "";

            List<List<string>> matrix = new List<List<string>>();

            foreach (DateTime day in PageMetods.EachDay(dt1, dt2)) 
            {
                isholiday = false;

                if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday)
                {isholiday = true; }

                foreach (string swieto in swieta_date)
                {
                    if(String.Equals(day.ToString(),swieto))
                    {
                        isholiday = true;}
                }

                if (!isholiday)
                {
                    if (day.Year == DateTime.Today.Year)
                    { day_count = day_count + 1; }
                    else if (day.Year == DateTime.Today.Year + 1)
                    { day_count_next = day_count_next + 1; }
                }
                else 
                {
                    int_holiday_dyas_all = int_holiday_dyas_all - 1;
                }

                daysstring = daysstring + day.Year.ToString() + "_" + day.DayOfYear.ToString() + ";";

                //if (listadni)
                //    if (ststustextid == "1" || ststustextid == "2" || ststustextid == "3" || ststustextid == "6")
                //    {
                //        {
                //            List<string> track = new List<string>();
                //            track.Add(day.ToShortDateString());
                //            track.Add(this.statusid.ToString());
                //            track.Add(this.holidayid.ToString());
                //            matrix.Add(track);
                //        }
                //    }
               
            }

            this.wszystkiedni_status = daysstring;
            this.holiday_dyas = day_count;
            this.holiday_dyas_next = day_count_next;
            this.holiday_dyas_all = int_holiday_dyas_all;

            int dyas_nz = 0;

            if (String.Equals("10",rodzajtext))
            {
                dyas_nz = day_count;
            }
            this.holiday_dyas_nz = dyas_nz;
        }

        public holiday(string odtext, string dotext, string menagertext, string othermenagertext, string ststustextid, string ststustext, string rodzajid, string rodzajtext, string useridtext, string days_count, string days_count_this, string days_count_next, string days_count_with_all, string holidayidtext = "0", bool listadni = false)
        {
            DateTime myTime;

            if (odtext != string.Empty)
            {
                myTime = DateTime.Parse(odtext);
                this.date_od = myTime;
                //this.date_od = DateTime.ParseExact(odtext, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            if (dotext != string.Empty)
            {
                myTime = DateTime.Parse(dotext);
                this.date_do = myTime;
                //this.date_do = DateTime.ParseExact(dotext, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            this.menager = menagertext;
            this.othermenager = othermenagertext;
            this.status = ststustext;
            this.statusid = ststustextid;
            this.rodzaj = rodzajtext;
            this.rodzajid = rodzajid;
            this.userid = useridtext;
            this.holidayid = holidayidtext;
            this.holiday_dyas_ciag = Convert.ToInt16(days_count_with_all);
            this.holiday_dyas_all = Convert.ToInt16(days_count);
            this.holiday_dyas = Convert.ToInt16(days_count_this);
            this.holiday_dyas_next = Convert.ToInt16(days_count_next);

            this.holiday_dyas_nz = 0;

            if (this.rodzajid == "10") 
            {
                this.holiday_dyas_nz = this.holiday_dyas_all;
            }

        }

        public void addHistory(string history_id, string user_id = "none")
        {
            DateTime time_now = DateTime.Now;
            if (user_id == "none")
            {
                PolaczenieSQL.addhistory(this.holidayid, history_id, this.userid, time_now);
            }else
            {
                PolaczenieSQL.addhistory(this.holidayid, history_id, user_id, time_now);
            }

        }

        public void updateStatus(int status_id)
        {
            PolaczenieSQL.updateHolidayStatus(this.holidayid, status_id);
        }

        public void rejectHR(string by_user_id)
        {
            this.addHistory("Urlop odrzucony przez pracownika HR", by_user_id);
            this.updateStatus(5);

            if (String.Equals(this.rodzajid, "9") || String.Equals(this.rodzajid, "10")) 
            {
                user u = PolaczenieSQL.find_user(this.userid,false);
                u.update_dni_urlopowe(-this.holiday_dyas, this.holiday_dyas_nz, -this.holiday_dyas_next);
            }
            mailSender.SendMailrejectHR(this.holidayid, by_user_id, this.menager, this.userid);
        }

        public void rejectMenager(string by_user_id)
        {
            this.addHistory("Urlop odrzucony przez menagera", by_user_id);
            this.updateStatus(4);

            if (String.Equals(this.rodzajid, "9") || String.Equals(this.rodzajid, "10"))
            {
                user u = PolaczenieSQL.find_user(this.userid,false);
                u.update_dni_urlopowe(-this.holiday_dyas, this.holiday_dyas_nz, -this.holiday_dyas_next);
            }
            mailSender.SendMailrejectMenager(this.holidayid, by_user_id, this.menager, this.userid);
        }

        public void deleted(string by_user_id)
        {
            this.addHistory("Urlop usunięty przez pracownika", by_user_id);
            this.updateStatus(7);

            if (String.Equals(this.rodzajid, "9") || String.Equals(this.rodzajid, "10"))
            {
                user u = PolaczenieSQL.find_user(this.userid,false);
                u.update_dni_urlopowe(-this.holiday_dyas, this.holiday_dyas_nz, -this.holiday_dyas_next);
            }
        }

        public void deletedHR(string by_user_id)
        {
            this.addHistory("Urlop usunięty przez pracownika HR", by_user_id);
            this.updateStatus(7);

            if (String.Equals(this.rodzajid, "9") || String.Equals(this.rodzajid, "10"))
            {
                user u = PolaczenieSQL.find_user(this.userid,false);
                u.update_dni_urlopowe(-this.holiday_dyas, this.holiday_dyas_nz, -this.holiday_dyas_next);
            }
            mailSender.SendMaildeletedHR(this.holidayid, by_user_id, this.menager, this.userid);
        }

        public void appruveHR(string by_user_id)
        {
            this.addHistory("Urlop zatwierdzony przez pracownika HR", by_user_id);
            this.addHistory("Urlop czeka na wykorzystanie go przez pracownika", "System");
            this.updateStatus(3);
            mailSender.SendMailappruveHR(this.holidayid, by_user_id, this.menager, this.userid);
        }

        public void appruveMenager(string by_user_id)
        {
            this.addHistory("Urlop zatwierdzony przez menagera", by_user_id);
            this.updateStatus(2);
            mailSender.SendMailappruveMenager(this.holidayid, this.userid);
        }

        public void wykorzystany()
        {
            this.addHistory("Urlop wykorzystany przez pracownika", "System");
            this.updateStatus(6);
        }

    }

    public class user
    {
        public String imie;
        public String nazwisko;
        public String userid;
        public String pesel;
        public String teamid;
        public String team;
        public String menagerid;
        public String menager;
        public String data_urodzenia;
        public String dniurlopowe;
        public String data_zatrudnienia;
        public String kiedy26;
        public String passtemp;
        public int ilosc_poprzedni;
        public int ilosc_obecny;
        public int ilosc_nastepny;
        public int ilosc_dodatkowy;
        public int ilosc_nz;
        public int ilosc_wyk;
        public int ilosc_wyk_next;

        public user() { }

        public user(string user_id)
        {
            this.userid = user_id;
            user u = PolaczenieSQL.find_user(this.userid);
        }

        public void update_dni_urlopowe(int dni_wyk = 0, int dni_nz = 0, int dni_wyk_nrok = 0, int dni_ekstra = 0)
        {
            int update_dni_wyk = this.ilosc_wyk + dni_wyk;
            int update_dni_nz = this.ilosc_nz + dni_nz;
            int update_dni_wyk_nrok = this.ilosc_wyk_next + dni_wyk_nrok;
            int update_dni_ekstra = this.ilosc_dodatkowy + dni_ekstra;

            PolaczenieSQL.update_dni_urlopowe(this.userid,update_dni_wyk, update_dni_nz, update_dni_wyk_nrok, update_dni_ekstra);
        }

        public override string ToString()
        {
            return this.imie + " " + this.nazwisko;
        }
    }

    public class team
    {
        public String name;
        public String menager;
        public String teamid;
        public List<string> team_member;

        public team(){}

        public team(string teamid) 
        {
            this.teamid = teamid;
            team t = PolaczenieSQL.find_team(teamid);
            this.menager = t.menager.ToString();
            this.name = t.name.ToString();
            this.team_member = PolaczenieSQL.find_team_members_byteamid(teamid);
        }

    }

    public class menager : user
    {
        public String team_owned;

        public menager() { }

        public menager(team t)
        {
            this.team_owned = t.name;
        }

    }

    public class mailSender
    {
        public static void SendMail(string to, string subject, string body)
        {
            MailMessage mailMsg = new MailMessage();
            //mailMsg.To.Add(to);
            mailMsg.To.Add("gacekrobert@gmail.com");
            // From
            MailAddress mailAddress = new MailAddress("noreply@hrapp.hostingasp.pl");
            mailMsg.From = mailAddress;

            // Subject and Body
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = true;
            // Init SmtpClient and send on port 587 in my case. (Usual=port25)
            SmtpClient smtpClient = new SmtpClient("smtp.webio.pl");
            System.Net.NetworkCredential credentials =
               new System.Net.NetworkCredential("noreply@hrapp.hostingasp.pl", "*");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }

        public static void SendMailPassRestart(string pass, string mail, string name)
        {
            string body = "<h3>Helo " + name + "</h3><br/><h4>Your password has been restarted.</h4><br/>Your new password is: <b>" + pass + "</b><br/>Please log in and change your password now:<br/><a href='https://ssl5.webio.pl/hrapp/Account/ChangePassword.aspx'>Change Password</a>";
            string subject = "Your password has been restarted";
            SendMail(mail, subject, body);
        }

        public static void SendMailAddHoliday(string id, string action_person, string holiday_menager)
        {
            user u = PolaczenieSQL.find_user(action_person);
            user m = PolaczenieSQL.find_user(holiday_menager);
            MembershipUser usr = Membership.GetUser(action_person);
            MembershipUser usrm = Membership.GetUser(holiday_menager);

            string body = "Helo " + u.ToString() + "<br/>Your created new holiday request<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            string subject = "New holiday request";
            SendMail(usr.Email, subject, body);

            body = "Helo " + m.ToString() + "<br/>" + u.ToString() + " created new holiday request<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            subject = "New holiday request from user " + u.ToString();
            SendMail(usrm.Email, subject, body);
        }

        public static void SendMailrejectHR(string id, string action_person, string holiday_menager, string userid)
        {
            user u = PolaczenieSQL.find_user(userid);
            user m = PolaczenieSQL.find_user(holiday_menager);
            user hr = PolaczenieSQL.find_user(action_person);
            MembershipUser usr = Membership.GetUser(userid);
            MembershipUser usrm = Membership.GetUser(holiday_menager);

            string body = "Helo " + u.ToString() + "<br/>Your holiday request has been rejected by HR employee " + hr.ToString() + "<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            string subject = "Your holiday request has been rejcected by HR";
            SendMail(usr.Email, subject, body);

            body = "Helo " + m.ToString() + "<br/>User's " + u.ToString() + " holiday has been rejected by HR employee<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            subject = "User " + u.ToString() + " holiday request has been rejcected by HR";
            SendMail(usrm.Email, subject, body);
        }

        public static void SendMailrejectMenager(string id, string action_person, string holiday_menager, string userid)
        {
            user u = PolaczenieSQL.find_user(userid);
            user m = PolaczenieSQL.find_user(holiday_menager);
            MembershipUser usr = Membership.GetUser(userid);
            MembershipUser usrm = Membership.GetUser(holiday_menager);

            string body = "Helo " + u.ToString() + "<br/>Your holiday request has been rejected by menager " + m.ToString() + "<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            string subject = "Your holiday request has been rejcected by menager";
            SendMail(usr.Email, subject, body);

        }

        public static void SendMaildeletedHR(string id, string action_person, string holiday_menager, string userid)
        {
            user u = PolaczenieSQL.find_user(userid);
            user m = PolaczenieSQL.find_user(holiday_menager);
            user hr = PolaczenieSQL.find_user(action_person);
            MembershipUser usr = Membership.GetUser(userid);
            MembershipUser usrm = Membership.GetUser(holiday_menager);

            string body = "Helo " + u.ToString() + "<br/>Your holiday request has been deleted by HR employee " + hr.ToString() + "<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            string subject = "Your holiday request has been deleted by HR";
            SendMail(usr.Email, subject, body);

            body = "Helo " + m.ToString() + "<br/>User's " + u.ToString() + " holiday has been deleted by HR employee<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            subject = "User " + u.ToString() + " holiday request has been deleted by HR";
            SendMail(usrm.Email, subject, body);
        }

        public static void SendMailappruveMenager(string id, string userid)
        {
            user u = PolaczenieSQL.find_user(userid);
            MembershipUser usr = Membership.GetUser(userid);

            string body = "Helo<br/>Holiday request is waiting for HR approuval<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            string subject = "New holiday request from " + u.ToString();
            SendMail("hrmail@gmail.com", subject, body);

        }

        public static void SendMailappruveHR(string id, string action_person, string holiday_menager, string userid)
        {
            user u = PolaczenieSQL.find_user(userid);
            user m = PolaczenieSQL.find_user(holiday_menager);
            user hr = PolaczenieSQL.find_user(action_person);
            MembershipUser usr = Membership.GetUser(userid);
            MembershipUser usrm = Membership.GetUser(holiday_menager);

            string body = "Helo " + u.ToString() + "<br/>Your holiday request has been approuved by HR employee " + hr.ToString() + "<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            string subject = "Your holiday request has been approuved by HR";
            SendMail(usr.Email, subject, body);

            body = "Helo " + m.ToString() + "<br/>User's " + u.ToString() + " holiday has been approuved by HR employee<br/>See details <a href='https://ssl5.webio.pl/hrapp/all/manageholiday.aspx?holidayid=" + id + "'>Details</a>";
            subject = "User " + u.ToString() + " holiday request has been approuved by HR";
            SendMail(usrm.Email, subject, body);
        }

        public static void SendMailNewAccount(string pass, string mail, string name)
        {

            string body = "Helo " + name + "<br/>Your account in Holiday App has been created.<br/>Please log in and change your temporary password <a href='https://ssl5.webio.pl/hrapp/Account/Login.aspx?ReturnUrl=%2fhrapp%2fall%2fmyaccount.aspx'>here</a><br/>Username: " + name + "<br/>Password: " + pass;
            string subject = "Your account has been created";
            SendMail(mail, subject, body);

        }
    }
}