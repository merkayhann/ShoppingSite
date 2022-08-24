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
    public partial class ProductInfo : System.Web.UI.Page
    {
        public string CustomerName = "";
        public string email = "";
        public string password = "";
        public int Customer_id = 0;
        public string path = @"C:\Users\ERILEA\source\repos\ShoppingSite\ShoppingSite\ShoppingSite.mdb";
        public string name = "";
        public string imgpath = "";
        public int price = 0;
        public int rem_quantity = 0;
        public int quantity = 0;
        public int total_price = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie reqCookies2 = Request.Cookies["userInfo"];
            if (reqCookies2 != null)
            {
                CustomerName = reqCookies2["CustomerName"].ToString();
                email = reqCookies2["CustomerEmail"].ToString();
                password = reqCookies2["CustomerPassword"].ToString();
                Customer_id = int.Parse(reqCookies2["CustomerID"].ToString());
                lblCustomerName.Text = CustomerName;

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

                HttpCookie reqCookies = Request.Cookies["ProductInfo"];
                lblPName0.Text = reqCookies["name"].ToString();
                name = reqCookies["name"].ToString();
                lblPPrice.Text = reqCookies["price"].ToString() + "₺";
                price = int.Parse(reqCookies["price"].ToString());
                imgPro.ImageUrl = reqCookies["img"].ToString();
                imgpath = reqCookies["img"].ToString();
                lblPDesc0.Text = reqCookies["desc"].ToString();
                lblPAuthor0.Text = reqCookies["brand"].ToString();
                lblPCategory0.Text = reqCookies["category"].ToString();
                lblPQuantity0.Text = reqCookies["quantity"].ToString();
                rem_quantity = int.Parse(reqCookies["quantity"].ToString());
            }
            else
                Response.Redirect("Login.aspx");
        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            quantity = Convert.ToInt32(txtQuantity.Text);
            if (quantity <= 0)
                lblQuantityWarning.Text = "Quantity cannot be lower than 1!";
            else
            {
                total_price = quantity * price;
                string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
                OleDbConnection dbcon = new OleDbConnection(constr);
                OleDbCommand selectCommand = dbcon.CreateCommand();
                dbcon.Open();
                OleDbDataAdapter ad;
                string select = null;
                select = "select * from Cart";
                ad = new OleDbDataAdapter(select, constr);
                DataTable dataTable = new DataTable();
                ad.Fill(dataTable);

                int fl = 0;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (name == dataTable.Rows[i]["OrderCName"].ToString() && Customer_id == Convert.ToInt32(dataTable.Rows[i]["CustomerID"].ToString()))
                    {
                        lblQuantityWarning.Text = "The item is already in the cart!";
                        fl = 1;
                    }
                }
                dbcon.Close();
                if (fl == 0)
                {
                    if (quantity <= rem_quantity)
                    {
                        string query = "Insert into Cart(CustomerID, OrderCImage, OrderCName, OrderCQuantity, OrderCPrice, OrderCTotalPrice) values (@CustomerID, @OrderCImage, @OrderCName, @OrderCQuantity, @OrderCPrice, @OrderCTotalPrice)";
                        OleDbConnection dbconn = new OleDbConnection(constr);
                        dbconn.Open();
                        OleDbCommand cmd = dbconn.CreateCommand();
                        cmd = new OleDbCommand(query, dbconn);
                        cmd.Parameters.AddWithValue("@CustomerID", Customer_id);
                        cmd.Parameters.AddWithValue("@OrderCImage", imgpath);
                        cmd.Parameters.AddWithValue("@OrderCName", name);
                        cmd.Parameters.AddWithValue("@OrderCQuantity", quantity);
                        cmd.Parameters.AddWithValue("@OrderCPrice", price);
                        cmd.Parameters.AddWithValue("@OrderCTotalPrice", total_price);
                        cmd.ExecuteNonQuery();
                        dbconn.Close();
                        lblQuantityWarning.Text = " ";
                    }
                    else
                        lblQuantityWarning.Text = "Quantity cannot be more than the remaining amount of items!";
                }
            }
        }

        protected void btnGoBackToShop_Click(object sender, EventArgs e)
        {
            HttpCookie reqCookies = Request.Cookies["ProductInfo"];
            reqCookies.Values.Clear();
            Response.Redirect("MainPage.aspx");
        }

        protected void btnGoToCart_Click(object sender, EventArgs e)
        {
            HttpCookie CartInfo2 = new HttpCookie("CartInfo2");
            CartInfo2["id"] = Customer_id.ToString();
            Response.Redirect("Cart.aspx");
        }

        protected void btnPlus_Click(object sender, EventArgs e)
        {
            int nr = 0;
            nr = Convert.ToInt32(txtQuantity.Text);
            if (nr >= rem_quantity)
            {
                lblQuantityWarning.Text = "Quantity cannot be more than the remaining amount of items!";
                nr = rem_quantity;
            }

            else
            {
                nr++;
                txtQuantity.Text = nr.ToString();
            }

        }

        protected void btnMinus_Click(object sender, EventArgs e)
        {
            int nr = 0;
            nr = Convert.ToInt32(txtQuantity.Text);
            if (nr == 1)
                txtQuantity.Text = nr.ToString();
            else
            {
                nr--;
                txtQuantity.Text = nr.ToString();
            }
        }

        protected void imgBrand_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }

        protected void lkbtnCart_Click(object sender, EventArgs e)
        {
            HttpCookie CartInfo2 = new HttpCookie("CartInfo2");
            CartInfo2["id"] = Customer_id.ToString();
            Response.Redirect("Cart.aspx");
        }

        protected void lkbtnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }

        protected void lkbtnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }
    }
}