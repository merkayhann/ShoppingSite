<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ShoppingSite.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Cart</title>
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
        .TableCSS   
        {   border-color: #abcdef; background-color:#abcdef;   
            width: 850px;   
        }   
        .TableHeader   
        {   border-color: #abcdef; background-color:#abcdef;   
            color:#0066FF; font-size:22px;      
        }   
        .TableData   
        {  
            border-color: #abcdef; background-color:#abcdef;  
            color:#FFFF99; font-size:20px; height:65px;
        }
        .labels{
            font-size:13pt; color:#51294b; margin-left:50px;
        }
        .buttons{
            background-color:#66FF66; border-radius:15px; height:40px; width:220px; border-color:#66FF66;
            font-family:Arial; font-size:20px; color:#51294b; margin-left:150px;
        }
        .parent {
          background-color:#abcdef; margin-left:20%; 
          margin-right:25%; margin-top:40px;
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
           <asp:ListView ID="lvCart" runat="server" OnItemCommand="lvCart_ItemCommand">
             <LayoutTemplate>   
                <table id="Table1" runat="server" class="TableCSS">   
                   <tr id="Tr1" runat="server" class="TableHeader" style="text-align:center">   
                      <td id="Td1" runat="server">Product</td>   
                      <td id="Td2" runat="server">Name</td>   
                       <td id="Td3" runat="server">Quantity</td>   
                       <td id="Td4" runat="server">Unit Price</td>   
                       <td id="Td5" runat="server">Total</td>   
                    </tr>   
                    <tr id="ItemPlaceholder" runat="server"> </tr>   
                </table>   
             </LayoutTemplate>   
             <ItemTemplate>
                <tr class="TableData">   
                   <td style="text-align:center">
                      <asp:Image ID="Image1" runat="server" height="70px" width="70px" ImageUrl='<%# string.Format("~/{0}",Eval("pro_img"))%>'>   
                      </asp:Image>   
                   </td>   
                        <td style="text-align:center"> 
                           <asp:Label  ID="lblProName" runat="server" Text='<%# Eval("pro_name")%>'></asp:Label>
                           <br />
                           <asp:LinkButton runat="server" ID="lkbtnRemove" Font-Underline="false" Text="Remove" Font-Size="10pt" ForeColor="#0066FF" CommandName="Remove"/>
                        </td>  
                        <td style="text-align:center">
                           <asp:LinkButton runat="server" ID="lkbtnMinus" Font-Underline="false" Text="-" Font-Size="15pt" ForeColor="#0066FF" CommandName="Minus"/>
                           <asp:Label  ID="Label3" runat="server" Text='<%# Eval("pro_quantity")%>'></asp:Label>
                           <asp:LinkButton runat="server" ID="lkbtnPlus" Font-Underline="false" Text="+" Font-Size="12pt" ForeColor="#0066FF" CommandName="Plus"/>
                        </td>  
                        <td style="text-align:center"> 
                           <asp:Label  ID="Label4" runat="server" Text='<%# Eval("pro_price")%>'></asp:Label>
                           <asp:Label  ID="Label42" runat="server" Text="₺"></asp:Label> 
                        </td>  
                        <td style="text-align:center"> 
                           <asp:Label  ID="Label5" runat="server" Text='<%# Eval("pro_total_price")%>'></asp:Label>
                           <asp:Label  ID="Label1" runat="server" Text="₺"></asp:Label>
                        </td>   
                 </tr>                   
              </ItemTemplate> 
              <SelectedItemTemplate>
                 <div id="selected"><%# Eval("pro_name") %></div>
              </SelectedItemTemplate>
          </asp:ListView>
        </div>
           <asp:Panel ID="pnlCartNotEmpty" runat="server" style="text-align:center; margin-left:20%; margin-right:25%; background-color:#abcdef" >
             <asp:Label ID="lblTotalPrice" runat="server" Font-Size="23pt" ForeColor="#FFFFcc" Text="Total Price: "></asp:Label>
             <asp:Label ID="lblTotalPrice0" runat="server" Font-Size="23pt" ForeColor="#FFFFcc" Text="--"></asp:Label>
             <asp:Label ID="lblTotalPrice1" runat="server" Font-Size="23pt" ForeColor="#FFFFcc" Text="₺"></asp:Label>
             <br /><br />
             <asp:Button ID="btnPurchase" runat="server" class="buttons" OnClick="btnPurchase_Click" Text="Purchase" style="margin-left:0" />
            <br /><br /><br />
          </asp:Panel>
        <div style="text-align:center">
            <asp:Label ID="lblCartWarning" runat="server" Font-Size="15pt" ForeColor="#51294b"></asp:Label>
            <br /><br /><br />
            <asp:Button ID="btnGoBackToShop" runat="server" Text="Go Back To The Shop" CssClass="buttons" 
                style="margin-left:0" OnClick="btnGoBackToShop_Click" Visible="False" />
        </div>        
    </form>
</body>
</html>
