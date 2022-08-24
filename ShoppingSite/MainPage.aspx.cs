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
    public partial class MainPage : System.Web.UI.Page
    {
        public string name = "";
        public string email = "";
        public string password = "";
        public int Customer_id = 0;
        public string path = @"C:\Users\ERILEA\source\repos\ShoppingSite\ShoppingSite\ShoppingSite.mdb";
        List<Product> products = new List<Product>();
        List<int> cust_ids = new List<int>();
        public class ProductsInDB
        {
            public string pro_img { get; set; }
            public string pro_name { get; set; }
            public string pro_desc { get; set; }
            public string pro_price { get; set; }
        }
        List<ProductsInDB> pros_db = new List<ProductsInDB>();
        List<ProductsInDB> pros_db2 = new List<ProductsInDB>();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                name = reqCookies["CustomerName"].ToString();
                email = reqCookies["CustomerEmail"].ToString();
                password = reqCookies["CustomerPassword"].ToString();
                Customer_id = int.Parse(reqCookies["CustomerID"].ToString());
                lblCustomerName.Text = name;

                string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
                OleDbConnection dbcon = new OleDbConnection(constr);
                OleDbCommand selectCommand = dbcon.CreateCommand();
                dbcon.Open();
                OleDbDataAdapter ad;
                string select = null;

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

                select = "SELECT * FROM Product";
                ad = new OleDbDataAdapter(select, constr);
                DataTable dataTable = new DataTable();
                ad.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Product newproduct = new Product(int.Parse(dataTable.Rows[i]["ProductID"].ToString()),
                        dataTable.Rows[i]["ProductName"].ToString(),
                        double.Parse(dataTable.Rows[i]["ProductPrice"].ToString()),
                        dataTable.Rows[i]["ProductImage"].ToString(),
                        dataTable.Rows[i]["ProductDescription"].ToString(),
                        dataTable.Rows[i]["ProductBrand"].ToString(),
                        int.Parse(dataTable.Rows[i]["ProductQuantity"].ToString()),
                        dataTable.Rows[i]["ProductCategoryName"].ToString());
                    products.Add(newproduct);

                    if (i % 2 == 0)
                    {
                        ProductsInDB new_one = new ProductsInDB();
                        new_one.pro_img = dataTable.Rows[i]["ProductImage"].ToString();
                        new_one.pro_name = dataTable.Rows[i]["ProductName"].ToString();
                        new_one.pro_desc = dataTable.Rows[i]["ProductDescription"].ToString();
                        new_one.pro_price = dataTable.Rows[i]["ProductPrice"].ToString() + "₺";
                        pros_db.Add(new_one);
                    }
                    else
                    {
                        ProductsInDB new_one = new ProductsInDB();
                        new_one.pro_img = dataTable.Rows[i]["ProductImage"].ToString();
                        new_one.pro_name = dataTable.Rows[i]["ProductName"].ToString();
                        new_one.pro_desc = dataTable.Rows[i]["ProductDescription"].ToString();
                        new_one.pro_price = dataTable.Rows[i]["ProductPrice"].ToString() + "₺";
                        pros_db2.Add(new_one);
                    }
                }
                lvMainPage.DataSource = pros_db;
                lvMainPage.DataBind();
                lvMainPage2.DataSource = pros_db2;
                lvMainPage2.DataBind();
                HttpCookie CartInfo = new HttpCookie("CartInfo");
                CartInfo["id"] = Customer_id.ToString();
                CartInfo.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
                Response.Cookies.Add(CartInfo);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void lvMainPage_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int index = 0;
            string name = "";
            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
            OleDbConnection dbcon = new OleDbConnection(constr);
            OleDbCommand selectCommand = dbcon.CreateCommand();
            dbcon.Open();
            OleDbDataAdapter ad;
            string select = null;
            select = "select * from Product";
            ad = new OleDbDataAdapter(select, constr);
            DataTable dataTable = new DataTable();
            ad.Fill(dataTable);
            if (e.CommandName == "GoToProduct")
            {
                index = e.Item.DataItemIndex;
                for (int i = 0; i < dataTable.Rows.Count; i = i + 2)
                {
                    if (i % 2 == 0 && i / 2 == index)
                    {
                        HttpCookie ProductInfo = new HttpCookie("ProductInfo");
                        ProductInfo["id"] = dataTable.Rows[i]["ProductID"].ToString();
                        ProductInfo["name"] = dataTable.Rows[i]["ProductName"].ToString();
                        ProductInfo["price"] = dataTable.Rows[i]["ProductPrice"].ToString();
                        ProductInfo["img"] = dataTable.Rows[i]["ProductImage"].ToString();
                        ProductInfo["desc"] = dataTable.Rows[i]["ProductDescription"].ToString();
                        ProductInfo["brand"] = dataTable.Rows[i]["ProductBrand"].ToString();
                        ProductInfo["quantity"] = dataTable.Rows[i]["ProductQuantity"].ToString();
                        ProductInfo["category"] = dataTable.Rows[i]["ProductCategoryName"].ToString();
                        ProductInfo["Customerid"] = Customer_id.ToString();
                        ProductInfo.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
                        Response.Cookies.Add(ProductInfo);
                        Response.Redirect("ProductInfo.aspx");
                        break;
                    }
                }
            }
        }
        protected void lvMainPage2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int index = 0;
            string name = "";
            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
            OleDbConnection dbcon = new OleDbConnection(constr);
            OleDbCommand selectCommand = dbcon.CreateCommand();
            dbcon.Open();
            OleDbDataAdapter ad;
            string select = null;
            select = "select * from Product";
            ad = new OleDbDataAdapter(select, constr);
            DataTable dataTable = new DataTable();
            ad.Fill(dataTable);
            int n = 0;
            if (e.CommandName == "GoToProduct2")
            {
                index = e.Item.DataItemIndex;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i % 2 == 1 && i / 2 == index)
                    {
                        //name = dataTable.Rows[i]["ProductName"].ToString();
                        HttpCookie ProductInfo = new HttpCookie("ProductInfo");
                        ProductInfo["id"] = dataTable.Rows[i]["ProductID"].ToString();
                        ProductInfo["name"] = dataTable.Rows[i]["ProductName"].ToString();
                        ProductInfo["price"] = dataTable.Rows[i]["ProductPrice"].ToString();
                        ProductInfo["img"] = dataTable.Rows[i]["ProductImage"].ToString();
                        ProductInfo["desc"] = dataTable.Rows[i]["ProductDescription"].ToString();
                        ProductInfo["brand"] = dataTable.Rows[i]["ProductBrand"].ToString();
                        ProductInfo["quantity"] = dataTable.Rows[i]["ProductQuantity"].ToString();
                        ProductInfo["category"] = dataTable.Rows[i]["ProductCategoryName"].ToString();
                        ProductInfo["Customerid"] = Customer_id.ToString();
                        ProductInfo.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
                        Response.Cookies.Add(ProductInfo);
                        Response.Redirect("ProductInfo.aspx");
                        break;
                    }
                }
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