<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductInfo.aspx.cs" Inherits="ShoppingSite.ProductInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Information</title>
        <style type="text/css">        
        .header{
            background-color:#ffffcc; height:85px;
            margin:0; 
        }
        .header_bottom{
            background-color:#361818; height:60px;
            margin:0;
        }
        .footer{
            background-color:#ffffcc; height:45px;
            margin:0; margin-bottom:0;
        }
        .labels{
            font-size:13pt; color:#51294b; margin-left:50px;
        }
        .buttons{
            background-color:#66FF66; border-radius:15px; height:40px; width:250px; border-color:#66FF66;
            font-family:Arial; font-size:20px; color:#51294b; margin-left:150px;
        }
        .parent {
          background-color:#abcdef; margin-left:15%; 
          margin-right:15%; margin-top:40px;
        }
        .child {
            background-color:#abcdef;
        }
    </style>
</head>
<body style="background-color:#faeabb; margin:0">
    <form id="form1" runat="server">
        <div class="header">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="imgBrand" ImageUrl="Images/banner.jpg" runat="server" Height="77px" Width="356px" OnClick="imgBrand_Click" /> 
        </div>
        <div class="header_bottom">
            <asp:Label ID="lblMainWelcome" runat="server" Text="Welcome" style="color:#ffffcc; font-size: 20px; padding-left:35px"></asp:Label>
            <br />
            <asp:Label ID="lblCustomerName" runat="server" Text="-" style="color:#ffffcc; font-size: 20px; padding-left:35px"></asp:Label>
            <asp:LinkButton ID="lkbtnProfile" runat="server" style="color:#ffffcc; font-size: 25px; padding-left:25%"
                Font-Underline="False" OnClick="lkbtnProfile_Click" >Profile</asp:LinkButton>
            <asp:LinkButton ID="lkbtnCart" runat="server" style="color:#ffffcc; font-size: 25px; padding-left:30%"
                Font-Underline="False" OnClick="lkbtnCart_Click" >My Cart</asp:LinkButton>
            <asp:LinkButton ID="lkbtnAdmin" runat="server" style="color:#ffffcc; font-size: 25px; padding-left:16%"
                Font-Underline="False" OnClick="lkbtnAdmin_Click" Visible="false">Admin</asp:LinkButton>
        </div>
        <div class="parent">
            <div class="child" style="float:left; width: 550px;">
                <asp:ImageButton ID="imgPro" runat="server" Height="253px" Width="231px" style="margin-left:50px; margin-top:50px"/>
                <br /><br />
                <asp:Label ID="lblPName" runat="server" Text="Name: " CssClass="labels"></asp:Label>
                <asp:Label ID="lblPName0" runat="server" Text="--" style="font-size:13pt; color:yellow;"></asp:Label>
                <br />
                <asp:Label ID="lblPDesc" runat="server" Text="Description: " CssClass="labels"></asp:Label>
                <asp:Label ID="lblPDesc0" runat="server" Text="--" style="font-size:13pt; color:yellow;"></asp:Label>
                <br />
                <asp:Label ID="lblPAuthor" runat="server" Text="Author: " CssClass="labels"></asp:Label>
                <asp:Label ID="lblPAuthor0" runat="server" Text="--" style="font-size:13pt; color:yellow;"></asp:Label>
                <br />
                <asp:Label ID="lblPCategory" runat="server" Text="Category: " CssClass="labels"></asp:Label>
                <asp:Label ID="lblPCategory0" runat="server" Text="--" style="font-size:13pt; color:yellow;"></asp:Label>
                <br />
                <asp:Label ID="lblPQuantity" runat="server" Text="Remaining Amount: " CssClass="labels"></asp:Label>
                <asp:Label ID="lblPQuantity0" runat="server" Text="--" style="font-size:13pt; color:yellow;"></asp:Label>
                <br /> <br /><br />               
            </div>
            <div class="child" style="float:right; margin-right:30px; margin-top:50px; width: 500px;">
                <asp:Label ID="lblPPrice" runat="server" Text="--" CssClass="labels" BackColor="Orange" Height="50px" 
                    Font-Size="36pt" style="margin-left:120px"></asp:Label>
                <br /><br /><br /><br />
                <asp:Button ID="btnMinus" runat="server" BackColor="#3399FF" ForeColor="#FFFF99" Height="30px" 
                OnClick="btnMinus_Click" Text="-" Width="30px" style="margin-left:100px"/>
                &nbsp;&nbsp;<asp:TextBox ID="txtQuantity" runat="server" Height="25px" Width="40px">1</asp:TextBox>
                &nbsp;&nbsp;<asp:Button ID="btnPlus" runat="server" BackColor="#3399FF" ForeColor="#FFFF99" Height="30px" OnClick="btnPlus_Click" Text="+" Width="30px" />
                <br />          
                <asp:Label ID="lblQuantityWarning" runat="server" Font-Size="13pt" ForeColor="#FFFF99" Text="" style="margin-left:80px"></asp:Label>
                <br /><br />
                <asp:Button ID="btnAddToCart" runat="server" Text="Add To Cart" CssClass="buttons" OnClick="btnAddToCart_Click" style="margin-left:40px"/>
                <br /><br />
                <asp:Button ID="btnGoToCart" runat="server" Text="Go To Cart" CssClass="buttons" OnClick="btnGoToCart_Click" style="margin-left:40px" />
                <br /><br />
                <asp:Button ID="btnGoBackToShopping" runat="server" Text="Go Back To Shop" CssClass="buttons" OnClick="btnGoBackToShop_Click" style="margin-left:40px" />
                <br /><br /><br /><br />
            </div>
            <div style="clear: both;"></div>
            
        </div>
    </form>
</body>
</html>
