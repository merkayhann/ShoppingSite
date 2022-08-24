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
    public partial class Cart : System.Web.UI.Page
    {
        public string CustomerName = "";
        public string email = "";
        public string password = "";
        public int Customer_id = 0;
        public string path = @"C:\Users\ERILEA\source\repos\ShoppingSite\ShoppingSite\ShoppingSite.mdb";
        public string name = "";
        public int total_total_price = 0;
        public int rem_quan = 0;
        public class ProductsInCart
        {
            public string pro_img { get; set; }
            public string pro_name { get; set; }
            public string pro_quantity { get; set; }
            public string pro_price { get; set; }
            public string pro_total_price { get; set; }
        }
        List<ProductsInCart> pros = new List<ProductsInCart>();
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

                HttpCookie reqCookies = Request.Cookies["CartInfo"];
                Customer_id = int.Parse(reqCookies["id"].ToString());
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
                select = "select * from Cart";
                ad = new OleDbDataAdapter(select, constr);
                DataTable dataTable = new DataTable();
                ad.Fill(dataTable);

                bool fl = false;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (Customer_id == Convert.ToInt32(dataTable.Rows[i]["CustomerID"].ToString()))
                    {
                        fl = true;
                        ProductsInCart new_pro = new ProductsInCart();
                        new_pro.pro_img = dataTable.Rows[i]["OrderCImage"].ToString();
                        new_pro.pro_name = dataTable.Rows[i]["OrderCName"].ToString();
                        new_pro.pro_quantity = dataTable.Rows[i]["OrderCQuantity"].ToString();
                        new_pro.pro_price = dataTable.Rows[i]["OrderCPrice"].ToString();
                        new_pro.pro_total_price = dataTable.Rows[i]["OrderCTotalPrice"].ToString();
                        total_total_price += Convert.ToInt32(dataTable.Rows[i]["OrderCTotalPrice"].ToString());
                        pros.Add(new_pro);
                    }
                }
                if (fl == false)
                {
                    lblCartWarning.Text = "The cart is empty!";
                    pnlCartNotEmpty.Visible = false;
                    lvCart.Visible = false;
                    btnGoBackToShop.Visible = true;
                }
                else
                {
                    lvCart.DataSource = pros;
                    lvCart.DataBind();
                    lblTotalPrice0.Text = total_total_price.ToString();
                }
                dbcon.Close();
            }
            else
                Response.Redirect("Login.aspx");
        }
        protected void lvCart_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int index = 0;
            int quan = 0;
            int prc = 0;
            int tot = 0;
            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
            OleDbConnection dbcon = new OleDbConnection(constr);
            OleDbCommand selectCommand = dbcon.CreateCommand();
            dbcon.Open();
            OleDbDataAdapter ad;
            string select = null;
            select = "select * from Cart where CustomerID=" + Customer_id;
            ad = new OleDbDataAdapter(select, constr);
            DataTable dataTable = new DataTable();
            ad.Fill(dataTable);

            if (e.CommandName == "Remove")
            {
                index = e.Item.DataItemIndex;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        name = dataTable.Rows[i]["OrderCName"].ToString();
                        for (int j = 0; j < dataTable.Rows.Count; j++)
                        {
                            for (int pr = 0; pr < pros.Count; pr++)
                            {
                                if (pros[pr].pro_name == dataTable.Rows[j]["OrderCName"].ToString() && Customer_id == Convert.ToInt32(dataTable.Rows[j]["CustomerID"].ToString()))
                                {
                                    total_total_price -= Convert.ToInt32(pros.ElementAt(pr).pro_total_price.ToString());
                                    pros.RemoveAt(pr);
                                }
                            }
                        }
                        OleDbCommand cmd = dbcon.CreateCommand();
                        cmd.CommandText = "delete from Cart where CustomerID=" + Customer_id + " and OrderCName='" + name + "'";
                        cmd.Connection = dbcon;
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
                        break;
                    }
                }
                lvCart.DataSource = pros;
                lvCart.DataBind();
                lblTotalPrice0.Text = total_total_price.ToString();
                Response.Redirect("Cart.aspx");
            }
            else if (e.CommandName == "Minus")
            {
                index = e.Item.DataItemIndex;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        name = dataTable.Rows[i]["OrderCName"].ToString();
                        quan = Convert.ToInt32(dataTable.Rows[i]["OrderCQuantity"].ToString());
                        if (quan != 1)
                        {
                            quan--;
                            prc = Convert.ToInt32(dataTable.Rows[i]["OrderCPrice"].ToString());
                            tot = quan * prc;
                            total_total_price -= prc;
                            OleDbCommand cmd = dbcon.CreateCommand();
                            cmd.CommandText = "update Cart set OrderCQuantity=" + quan + ", OrderCTotalPrice=" + tot + " where CustomerID=" + Customer_id + " and OrderCName='" + name + "'";
                            cmd.Connection = dbcon;
                            cmd.ExecuteNonQuery();
                            dbcon.Close();
                            break;
                        }
                    }
                }
                lblTotalPrice0.Text = total_total_price.ToString();
                Response.Redirect("Cart.aspx");
            }
            else if (e.CommandName == "Plus")
            {
                HttpCookie reqCookies = Request.Cookies["ProductInfo"];
                rem_quan = int.Parse(reqCookies["quantity"].ToString());
                index = e.Item.DataItemIndex;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        name = dataTable.Rows[i]["OrderCName"].ToString();
                        quan = Convert.ToInt32(dataTable.Rows[i]["OrderCQuantity"].ToString());

                        if (quan < rem_quan)
                        {
                            quan++;
                            prc = Convert.ToInt32(dataTable.Rows[i]["OrderCPrice"].ToString());
                            tot = quan * prc;
                            total_total_price += prc;
                            OleDbCommand cmd = dbcon.CreateCommand();
                            cmd.CommandText = "update Cart set OrderCQuantity=" + quan + ", OrderCTotalPrice=" + tot + " where CustomerID=" + Customer_id + " and OrderCName='" + name + "'";
                            cmd.Connection = dbcon;
                            cmd.ExecuteNonQuery();
                            dbcon.Close();
                            break;
                        }
                        else
                            lblCartWarning.Text = "The quantity cannot be greater than the remaining amount!";
                    }
                }
                lblTotalPrice0.Text = total_total_price.ToString();
                Response.Redirect("Cart.aspx");
            }
            else
            {
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

        protected void btnPurchase_Click(object sender, EventArgs e)
        {
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

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (Customer_id == Convert.ToInt32(dataTable.Rows[i]["CustomerID"].ToString()))
                {
                    for (int j = 0; j < dataTable.Rows.Count; j++)
                    {
                        for (int pr = 0; pr < pros.Count; pr++)
                        {
                            if (pros[pr].pro_name == dataTable.Rows[j]["OrderCName"].ToString() && Customer_id == Convert.ToInt32(dataTable.Rows[j]["CustomerID"].ToString()))
                            {
                                pros.RemoveAt(pr);
                            }
                        }
                    }
                    lblCartWarning.Text = "You successfully purchased all the items in your cart!";
                    OleDbCommand cmd = dbcon.CreateCommand();
                    cmd.CommandText = "delete * from Cart where CustomerID=" + Customer_id.ToString();
                    cmd.Connection = dbcon;
                    cmd.ExecuteNonQuery();
                    dbcon.Close();
                    break;
                }
            }
            lvCart.DataSource = pros;
            lvCart.DataBind();
            pnlCartNotEmpty.Visible = false;
            lvCart.Visible = false;
            btnGoBackToShop.Visible = true;
        }

        protected void btnGoBackToShop_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx");
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