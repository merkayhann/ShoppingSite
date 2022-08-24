using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingSite
{
    public class Product
    {
        public int ProductID;
        public string ProductName;
        public double ProductPrice;
        public string ProductImage;
        public string ProductDescription;
        public string ProductBrand;
        public int ProductQuantity;
        public string ProductCategoryName;
        public Product(int ProductID, string ProductName, double ProductPrice,
        string ProductImage, string ProductDescription, string ProductBrand,
        int ProductQuantity, string ProductCategoryName)
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
            this.ProductImage = ProductImage;
            this.ProductDescription = ProductDescription;
            this.ProductBrand = ProductBrand;
            this.ProductQuantity = ProductQuantity;
            this.ProductCategoryName = ProductCategoryName;
        }
    }
}