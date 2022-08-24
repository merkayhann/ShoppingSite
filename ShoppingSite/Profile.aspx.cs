using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace ShoppingSite
{
    public partial class Profile : System.Web.UI.Page
    {
        public string Customer_name = "";
        public string email = "";
        public string password = "";
        public string Customer_address = "";
        public string Customer_phone = "";
        public int Customer_id = 0;
        public string path = @"C:\Users\ERILEA\source\repos\ShoppingSite\ShoppingSite\ShoppingSite.mdb";
        public class Customers
        {
            public string cus_name { get; set; }
            public string cus_email { get; set; }
            public string cus_pas { get; set; }
            public string cus_address { get; set; }
            public string cus_phone { get; set; }
            public int cus_id { get; set; }
        }
        List<Customers> custs = new List<Customers>();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            Customer_name = reqCookies["CustomerName"].ToString();
            email = reqCookies["CustomerEmail"].ToString();
            password = reqCookies["CustomerPassword"].ToString();
            Customer_id = int.Parse(reqCookies["CustomerID"].ToString());
            lblCustomerName.Text = Customer_name;

            if (!IsPostBack)
            {
                string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
                OleDbConnection dbcon = new OleDbConnection(constr);
                OleDbCommand selectCommand = dbcon.CreateCommand();
                dbcon.Open();
                OleDbDataAdapter ad;

                string select2 = null;
                select2 = "SELECT * FROM Admin";
                ad = new OleDbDataAdapter(select2, constr);
                DataTable dataTable2 = new DataTable();
                ad.Fill(dataTable2);

                for (int i = 0; i < dataTable2.Rows.Count; i++)
                {
                    if (Customer_id.ToString() == dataTable2.Rows[i]["CustomerID"].ToString())
                        lkbtnAdmin.Visible = true;
                }

                string select = null;
                select = "SELECT * FROM Customer";
                ad = new OleDbDataAdapter(select, constr);
                DataTable dataTable = new DataTable();
                ad.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Customers new_cust = new Customers();
                    new_cust.cus_name = dataTable.Rows[i]["CustomerName"].ToString();
                    new_cust.cus_email = dataTable.Rows[i]["CustomerEmail"].ToString();
                    new_cust.cus_pas = dataTable.Rows[i]["CustomerPassword"].ToString();
                    new_cust.cus_address = dataTable.Rows[i]["CustomerAddress"].ToString();
                    new_cust.cus_phone = dataTable.Rows[i]["CustomerPhoneNumber"].ToString();
                    new_cust.cus_id = Convert.ToInt32(dataTable.Rows[i]["CustomerID"].ToString());
                    custs.Add(new_cust);
                }
                foreach (var cus in custs)
                {
                    if (cus.cus_email == email)
                    {
                        Customer_address = cus.cus_address;
                        txtAddress.Text = Customer_address;
                        Customer_phone = cus.cus_phone;
                        txtPhone.Text = Customer_phone;
                        txtUserName.Text = Customer_name;
                        txtEmail.Text = email;
                    }
                }
            }
        }

        protected void lkbtnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }

        protected void lkbtnCart_Click(object sender, EventArgs e)
        {
            HttpCookie CartInfo2 = new HttpCookie("CartInfo2");
            CartInfo2["id"] = Customer_id.ToString();
            Response.Redirect("Cart.aspx");
        }

        protected void imgBrand_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            lblUserName.Text = "Old password:";
            lblEmail.Text = "New password:";
            lblPhoneNumber.Text = "New password again:";
            lblAddress.Visible = false;
            txtAddress.Visible = false;
            btnChangePassword.Visible = false;
            btnUpdateInfos.Visible = false;
            txtUserName.Enabled = true;
            txtAddress.Enabled = true;
            txtPhone.Enabled = true;
            btnSaveChanges.Visible = true;
            txtUserName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtEmail.Enabled = true;
            txtUserName.TextMode = TextBoxMode.Password;
            txtEmail.TextMode = TextBoxMode.Password;
            txtPhone.TextMode = TextBoxMode.Password;

            HttpCookie btnDec = new HttpCookie("btnDec");
            btnDec["val"] = "2"; //1 for change infos, 2 for change password in Profile page
            btnDec.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
            Response.Cookies.Add(btnDec);
        }

        protected void btnUpdateInfos_Click(object sender, EventArgs e)
        {
            txtUserName.Enabled = true;
            txtAddress.Enabled = true;
            txtPhone.Enabled = true;
            btnSaveChanges.Visible = true;
            btnUpdateInfos.Visible = false;
            btnChangePassword.Visible = false;

            HttpCookie btnDec = new HttpCookie("btnDec");
            btnDec["val"] = "1"; //1 for change infos, 2 for change password in Profile page
            btnDec.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
            Response.Cookies.Add(btnDec);
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            HttpCookie reqCookies = Request.Cookies["btnDec"];
            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
            OleDbConnection dbcon = new OleDbConnection(constr);
            OleDbCommand selectCommand = dbcon.CreateCommand();
            dbcon.Open();
            OleDbDataAdapter ad;
            string select = null;
            select = "SELECT * FROM Customer";
            ad = new OleDbDataAdapter(select, constr);
            DataTable dataTable = new DataTable();
            ad.Fill(dataTable);

            if (reqCookies["val"].ToString() == "1")
            {
                string oldName = "";
                oldName = Customer_name;
                Customer_name = txtUserName.Text;
                Customer_phone = txtPhone.Text;
                Customer_address = txtAddress.Text;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (Customer_id == Convert.ToInt32(dataTable.Rows[i]["CustomerID"].ToString()))
                    {
                        OleDbCommand cmd = dbcon.CreateCommand();
                        cmd.CommandText = "update Customer set CustomerName='" + Customer_name + "', CustomerPhoneNumber='" + Customer_phone +
                            "', CustomerAddress='" + Customer_address + "' where CustomerID=" + Customer_id;
                        cmd.Connection = dbcon;
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
                        for (int j = 0; j < dataTable.Rows.Count; j++)
                        {
                            for (int pr = 0; pr < custs.Count; pr++)
                            {
                                if (custs[pr].cus_id == Convert.ToInt32(dataTable.Rows[j]["CustomerID"].ToString()))
                                    custs.RemoveAt(pr);
                            }
                        }
                        break;
                    }
                }
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["CustomerName"] = Customer_name;
                userInfo["CustomerEmail"] = email;
                userInfo["CustomerPassword"] = password;
                userInfo["CustomerID"] = Customer_id.ToString();
                userInfo.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
                Response.Cookies.Add(userInfo);

                txtUserName.Enabled = false;
                txtAddress.Enabled = false;
                txtPhone.Enabled = false;
                btnSaveChanges.Visible = false;
                btnUpdateInfos.Visible = true;
                btnChangePassword.Visible = true;
                Response.Redirect("Profile.aspx");
            }
            else if (reqCookies["val"].ToString() == "2")
            {
                string oldpass = "";
                string newpass = "";
                string newpass2 = "";
                oldpass = txtUserName.Text;
                newpass = txtEmail.Text;
                newpass2 = txtPhone.Text;

                if (oldpass != password)
                    lblerr.Text = "Wrong old password!";
                else if (newpass == "" || newpass2 == "")
                    lblerr.Text = "New password cannot remain empty!";
                else if (newpass != newpass2)
                    lblerr.Text = "Passwords don't match!";
                else
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (Customer_id == Convert.ToInt32(dataTable.Rows[i]["CustomerID"].ToString()))
                        {
                            OleDbCommand cmd = dbcon.CreateCommand();
                            cmd.CommandText = "update Customer set CustomerPassword='" + newpass + "' where CustomerID=" + Customer_id;
                            cmd.Connection = dbcon;
                            cmd.ExecuteNonQuery();
                            dbcon.Close();
                            Customer_name = dataTable.Rows[i]["CustomerName"].ToString();
                            Customer_phone = dataTable.Rows[i]["CustomerPhoneNumber"].ToString();
                            for (int j = 0; j < dataTable.Rows.Count; j++)
                            {
                                for (int pr = 0; pr < custs.Count; pr++)
                                {
                                    if (custs[pr].cus_id == Convert.ToInt32(dataTable.Rows[j]["CustomerID"].ToString()))
                                        custs.RemoveAt(pr);
                                }
                            }
                            break;
                        }
                    }
                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["CustomerName"] = Customer_name;
                    userInfo["CustomerEmail"] = email;
                    userInfo["CustomerPassword"] = newpass;
                    userInfo["CustomerID"] = Customer_id.ToString();
                    userInfo.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
                    Response.Cookies.Add(userInfo);

                    lblUserName.Text = "Username:";
                    lblEmail.Text = "e-mail:";
                    lblPhoneNumber.Text = "Phone Number:";
                    txtAddress.Visible = true;
                    lblAddress.Visible = true;
                    txtUserName.Enabled = false;
                    txtAddress.Enabled = false;
                    txtPhone.Enabled = false;
                    txtEmail.Enabled = false;
                    btnSaveChanges.Visible = false;
                    btnUpdateInfos.Visible = true;
                    btnChangePassword.Visible = true;
                    lblerr.Text = "";
                    txtUserName.TextMode = TextBoxMode.SingleLine;
                    txtEmail.TextMode = TextBoxMode.SingleLine;
                    txtPhone.TextMode = TextBoxMode.SingleLine;

                    txtUserName.Text = Customer_name;
                    txtEmail.Text = email;
                    txtPhone.Text = Customer_phone;
                    Response.Redirect("Profile.aspx");
                }
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            reqCookies.Values.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void lkbtnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }
    }
}