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
    public partial class Admin : System.Web.UI.Page
    {
        public string Customer_name = "";
        public string email = "";
        public string password = "";
        public int Customer_id = 0;
        public string path = @"C:\Users\ERILEA\source\repos\ShoppingSite\ShoppingSite\ShoppingSite.mdb";
        public class ProductsInDB
        {
            public string pro_img { get; set; }
            public string pro_name { get; set; }
        }
        List<ProductsInDB> pros = new List<ProductsInDB>();
        List<Product> products = new List<Product>();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie reqCookies2 = Request.Cookies["userInfo"];
            if (reqCookies2 != null)
            {
                Customer_name = reqCookies2["CustomerName"].ToString();
                email = reqCookies2["CustomerEmail"].ToString();
                password = reqCookies2["CustomerPassword"].ToString();
                Customer_id = int.Parse(reqCookies2["CustomerID"].ToString());
                lblCustomerName.Text = Customer_name;

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
                select = "select * from Product";
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

                    ProductsInDB new_one = new ProductsInDB();
                    new_one.pro_img = dataTable.Rows[i]["ProductImage"].ToString();
                    new_one.pro_name = dataTable.Rows[i]["ProductName"].ToString();
                    pros.Add(new_one);
                }
                lvCart.DataSource = pros;
                lvCart.DataBind();
                dbcon.Close();
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void lkbtnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }

        protected void lkbtnCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }

        protected void imgBrand_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }

        protected void lkbtnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void btnAddNewAdmin_Click(object sender, EventArgs e)
        {
            pnlTopTwo.Visible = false;
            pnlProducts.Visible = false;
            pnlAddNewAdmin.Visible = true;
        }
        protected void btnAddNewProduct_Click(object sender, EventArgs e)
        {
            pnlTopTwo.Visible = false;
            pnlProducts.Visible = false;
            pnlAddNewProduct.Visible = true;

            HttpCookie btnDec = new HttpCookie("btnDec");
            btnDec["val"] = "3"; //3 for add new pro, 4 for update existing pro in Admin page
            btnDec.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
            Response.Cookies.Add(btnDec);
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            bool fl = false;
            string newAdName = "";
            string newAdEmail = "";
            string newAdPassword = "";
            int newAdID = 0;
            newAdEmail = txtNewAdminEmail.Text;
            string userPass = "";
            userPass = txtYourPassword.Text;

            if (userPass != password)
                lblAddNewAdminError.Text = "Wrong password!";
            else
            {
                string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
                OleDbConnection dbcon = new OleDbConnection(constr);
                OleDbCommand selectCommand = dbcon.CreateCommand();
                dbcon.Open();
                OleDbDataAdapter ad;

                string select2 = null;
                select2 = "SELECT * FROM Customer";
                ad = new OleDbDataAdapter(select2, constr);
                DataTable dataTable2 = new DataTable();
                ad.Fill(dataTable2);

                for (int i = 0; i < dataTable2.Rows.Count; i++)
                {
                    if (newAdEmail == dataTable2.Rows[i]["CustomerEmail"].ToString())
                    {
                        fl = true;
                        newAdName = dataTable2.Rows[i]["CustomerName"].ToString();
                        newAdPassword = dataTable2.Rows[i]["CustomerPassword"].ToString();
                        newAdID = Convert.ToInt32(dataTable2.Rows[i]["CustomerID"].ToString());
                    }

                }

                if (fl == false)
                    lblAddNewAdminError.Text = "No such user exist!";
                else
                {
                    string query = "Insert into Admin(AdminName, AdminEmail, AdminPassword, CustomerID) values " +
                        "(@AdminName, @AdminEmail, @AdminPassword, @CustomerID)";
                    OleDbConnection dbconn = new OleDbConnection(constr);
                    dbconn.Open();
                    OleDbCommand cmd = dbconn.CreateCommand();
                    cmd = new OleDbCommand(query, dbconn);
                    cmd.Parameters.AddWithValue("@AdminName", newAdName);
                    cmd.Parameters.AddWithValue("@AdminEmail", newAdEmail);
                    cmd.Parameters.AddWithValue("@AdminPassword", newAdPassword);
                    cmd.Parameters.AddWithValue("@CustomerID", newAdID);
                    cmd.ExecuteNonQuery();
                    dbconn.Close();
                    pnlTopTwo.Visible = true;
                    pnlAddNewAdmin.Visible = false;
                    pnlProducts.Visible = true;
                }
            }
        }

        protected void btnSaveChanges2_Click(object sender, EventArgs e)
        {
            HttpCookie reqCookies = Request.Cookies["btnDec"];
            bool fl = false;
            string newProName = "";
            string newProAuthor = "";
            string newProDesc = "";
            string newProCategory = "";
            string newProImgUrl = "";
            int newProPrice = 0;
            int newProQuantity = 0;

            newProName = txtProductName.Text;
            newProAuthor = txtProductBrand.Text;
            newProDesc = txtProductDesc.Text;
            newProCategory = txtProductCategoryName.Text;
            newProImgUrl = txtProductImg.Text;
            newProPrice = Convert.ToInt32(txtProductPrice.Text);
            newProQuantity = Convert.ToInt32(txtProductQuantity.Text);

            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
            OleDbConnection dbcon = new OleDbConnection(constr);
            OleDbCommand selectCommand = dbcon.CreateCommand();
            dbcon.Open();
            OleDbDataAdapter ad;
            string select2 = null;
            select2 = "SELECT * FROM Product";
            ad = new OleDbDataAdapter(select2, constr);
            DataTable dataTable2 = new DataTable();
            ad.Fill(dataTable2);

            if (reqCookies["val"].ToString() == "3")
            {
                for (int i = 0; i < dataTable2.Rows.Count; i++)
                {
                    if (newProName == dataTable2.Rows[i]["ProductName"].ToString())
                        fl = true;
                }
                if (fl == true)
                    lblAddNewProError.Text = "This product already exist!";
                else
                {
                    string query = "Insert into Product(ProductName, ProductPrice, ProductImage, ProductDescription, ProductBrand, " +
                        "ProductQuantity, ProductCategoryName) values (@ProductName, @ProductPrice, @ProductImage, " +
                        "@ProductDescription, @ProductBrand, @ProductQuantity, @ProductCategoryName)";
                    OleDbConnection dbconn = new OleDbConnection(constr);
                    dbconn.Open();
                    OleDbCommand cmd = dbconn.CreateCommand();
                    cmd = new OleDbCommand(query, dbconn);
                    cmd.Parameters.AddWithValue("@ProductName", newProName);
                    cmd.Parameters.AddWithValue("@ProductPrice", newProPrice);
                    cmd.Parameters.AddWithValue("@ProductImage", newProImgUrl);
                    cmd.Parameters.AddWithValue("@ProductDescription", newProDesc);
                    cmd.Parameters.AddWithValue("@ProductBrand", newProAuthor);
                    cmd.Parameters.AddWithValue("@ProductQuantity", newProQuantity);
                    cmd.Parameters.AddWithValue("@ProductCategoryName", newProCategory);
                    cmd.ExecuteNonQuery();
                    dbconn.Close();
                    pnlTopTwo.Visible = true;
                    pnlProducts.Visible = true;
                    pnlAddNewProduct.Visible = false;
                }
            }
            else if (reqCookies["val"].ToString() == "4")
            {
                OleDbCommand cmd = dbcon.CreateCommand();
                cmd.CommandText = "update Product set ProductPrice=" + newProPrice + ", ProductImage='" + newProImgUrl +
                    "', ProductDescription='" + newProDesc + "', ProductBrand='" + newProAuthor + "', ProductQuantity=" +
                    newProQuantity + ", ProductCategoryName='" + newProCategory + "' where ProductName='" + newProName + "'";
                cmd.Connection = dbcon;
                cmd.ExecuteNonQuery();
                dbcon.Close();

                pnlTopTwo.Visible = true;
                pnlProducts.Visible = true;
                pnlAddNewProduct.Visible = false;
                txtProductName.Enabled = true;
            }
            Response.Redirect("Admin.aspx");
        }
        protected void lvCart_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int index = 0;
            string name = "";
            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path;
            OleDbConnection dbcon = new OleDbConnection(constr);
            OleDbCommand selectCommand = dbcon.CreateCommand();
            dbcon.Open();
            OleDbDataAdapter ad;
            string select = null;
            select = "SELECT * FROM Product";
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
                        name = dataTable.Rows[i]["ProductName"].ToString();
                        OleDbCommand cmd = dbcon.CreateCommand();
                        cmd.CommandText = "delete from Product where ProductName='" + name + "'";
                        cmd.Connection = dbcon;
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
                        Response.Redirect("Admin.aspx");
                    }
                }
            }
            else if (e.CommandName == "Updt")
            {
                HttpCookie btnDec = new HttpCookie("btnDec");
                btnDec["val"] = "4"; //3 for add new pro, 4 for update existing pro in Admin page
                btnDec.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
                Response.Cookies.Add(btnDec);

                index = e.Item.DataItemIndex;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        name = dataTable.Rows[i]["ProductName"].ToString();
                        txtProductName.Text = name;
                        txtProductBrand.Text = dataTable.Rows[i]["ProductBrand"].ToString();
                        txtProductCategoryName.Text = dataTable.Rows[i]["ProductCategoryName"].ToString();
                        txtProductImg.Text = dataTable.Rows[i]["ProductImage"].ToString();
                        txtProductQuantity.Text = dataTable.Rows[i]["ProductQuantity"].ToString();
                        txtProductPrice.Text = dataTable.Rows[i]["ProductPrice"].ToString();
                        txtProductDesc.Text = dataTable.Rows[i]["ProductDescription"].ToString();
                        txtProductName.Enabled = false;
                        pnlTopTwo.Visible = false;
                        pnlProducts.Visible = false;
                        pnlAddNewProduct.Visible = true;
                        break;
                    }
                }
            }
            else if (e.CommandName == "Details")
            {
                index = e.Item.DataItemIndex;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        name = dataTable.Rows[i]["ProductName"].ToString();
                        HttpCookie ProductInfo = new HttpCookie("ProductInfo");
                        foreach (var pro in products)
                        {
                            if (pro.ProductName == name)
                            {
                                ProductInfo["id"] = pro.ProductID.ToString();
                                ProductInfo["name"] = pro.ProductName.ToString();
                                ProductInfo["price"] = pro.ProductPrice.ToString();
                                ProductInfo["img"] = pro.ProductImage.ToString();
                                ProductInfo["desc"] = pro.ProductDescription.ToString();
                                ProductInfo["brand"] = pro.ProductBrand.ToString();
                                ProductInfo["quantity"] = pro.ProductQuantity.ToString();
                                ProductInfo["category"] = pro.ProductCategoryName.ToString();
                                ProductInfo["Customerid"] = Customer_id.ToString();
                                ProductInfo.Expires.Add(new TimeSpan(720, 0, 0)); //it expires in 30 days
                                Response.Cookies.Add(ProductInfo);
                            }
                        }
                        Response.Redirect("ProductInfo.aspx");
                        break;
                    }
                }
            }
        }
    }
}