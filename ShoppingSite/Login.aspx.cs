using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

namespace ShoppingSite
{
    public partial class Login : System.Web.UI.Page
    {
        public string path = @"C:\Users\ERILEA\source\repos\ShoppingSite\ShoppingSite\ShoppingSite.mdb";
        string email = "";
        string password = "";
        string name = "";
        string idd = "";
        int id = 0;
        string pas2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lkbtnSignUp_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
            pnlSignUp.Visible = true;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            HttpCookie userInfo = new HttpCookie("userInfo");
            if (txtLoginEmail.Text == "")
                lblLoginError.Text = "E-mail cannot remain empty!";
            else if (txtLoginPassword.Text == "")
                lblLoginError.Text = "Password cannot remain empty!";
            else
            {
                email = txtLoginEmail.Text;
                password = txtLoginPassword.Text;

                string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
                OleDbConnection dbcon = new OleDbConnection(constr);
                OleDbCommand selectCommand = dbcon.CreateCommand();
                dbcon.Open();
                OleDbDataAdapter ad;
                string select = null;
                select = "select * from Customer";
                ad = new OleDbDataAdapter(select, constr);
                DataTable dataTable = new DataTable();
                ad.Fill(dataTable);

                bool fl = false;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (email == dataTable.Rows[i]["CustomerEmail"].ToString())
                    {
                        name = dataTable.Rows[i]["CustomerName"].ToString();
                        pas2 = dataTable.Rows[i]["CustomerPassword"].ToString();
                        idd = dataTable.Rows[i]["CustomerID"].ToString();
                        id = Convert.ToInt32(idd);
                        fl = true;
                    }
                }
                dbcon.Close();
                if (fl == false)
                    lblLoginError.Text = "No such user exists! Please sign up to login.";
                else
                {
                    if (password != pas2)
                        lblLoginError.Text = "Wrong password!";
                    else
                    {
                        userInfo["CustomerName"] = name;
                        userInfo["CustomerEmail"] = email;
                        userInfo["CustomerPassword"] = password;
                        userInfo["CustomerID"] = id.ToString();
                        userInfo.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
                        Response.Cookies.Add(userInfo);
                        Response.Redirect("MainPage.aspx");
                    }
                }
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txtSignUpName.Text == "")
                lblSignUpError.Text = "Name cannot remain empty!";
            else if (txtSignUpEmail.Text == "")
                lblSignUpError.Text = "E-mail cannot remain empty!";
            else if (txtSignUpPassword.Text == "")
                lblSignUpError.Text = "Password cannot remain empty!";
            else if (txtSignUpPassword2.Text == "")
                lblSignUpError.Text = "Passwords don't match!";
            else
            {
                email = txtSignUpEmail.Text;
                password = txtSignUpPassword.Text;
                name = txtSignUpName.Text;
                string password2 = "";
                password2 = txtSignUpPassword2.Text;

                string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
                OleDbConnection dbcon = new OleDbConnection(constr);
                OleDbCommand selectCommand = dbcon.CreateCommand();
                dbcon.Open();
                OleDbDataAdapter ad;
                string select = null;
                select = "select * from Customer";
                ad = new OleDbDataAdapter(select, constr);
                DataTable dataTable = new DataTable();
                ad.Fill(dataTable);

                bool fl = false;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (email == dataTable.Rows[i]["CustomerEmail"].ToString())
                    {
                        lblSignUpError.Text = "This user already exists!";
                        fl = true;
                    }
                }
                if (fl == false)
                {
                    if (password != password2)
                        lblSignUpError.Text = "Passwords don't match!";
                    else
                    {
                        OleDbCommand cmd = dbcon.CreateCommand();
                        cmd.CommandText = "Insert into Customer(CustomerName,CustomerEmail,CustomerPassword)Values('" + name + "','" + email + "','" + password + "')";
                        cmd.Connection = dbcon;
                        cmd.ExecuteNonQuery();
                        id = dataTable.Rows.Count + 1;
                    }
                    dbcon.Close();
                    txtLoginEmail.Text = "";
                    txtLoginPassword.Text = "";
                    lblLoginError.Text = "";
                    pnlSignUp.Visible = false;
                    pnlLogin.Visible = true;
                }
            }
        }
    }
}