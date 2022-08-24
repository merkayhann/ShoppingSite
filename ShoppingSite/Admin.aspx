<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ShoppingSite.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Page</title>
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
            font-size:13pt; color:#51294b;
        }
        .buttons{
            background-color:#66FF66; border-radius:15px; height:40px; width:220px; border-color:#66FF66;
            font-family:Arial; font-size:20px; color:#51294b;
        }
        .parent {
          background-color:#abcdef; margin-left:20%; 
          margin-right:25%; margin-top:40px;
        }
        .child {
            background-color:#abcdef;
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
        .numeric_button
        {
            background-color:#384B69;
            color:#FFFFFF;    
            font-family:Arial;
            font-weight:bold;    
            padding:2px;  
            border:none;  
        }
        .current_page
        {
            background-color:#09151F;
            color:#FFFFFF;    
            font-family:Arial;
            font-weight:bold;    
            padding:2px;    
        }
        .next_button
        {
            background-color:#1F3548;
            color:#FFFFFF;    
            font-family:Arial;
            font-weight:bold;    
            padding:2px;    
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
        <asp:Panel ID="pnlTopTwo" runat="server">
            <asp:Button ID="btnAddNewAdmin" runat="server" Text="Add New Admin" CssClass="buttons" style="margin-top:2%;
                margin-left:20%" OnClick="btnAddNewAdmin_Click" />
            <asp:Button ID="btnAddNewProduct" runat="server" Text="Add New Product" CssClass="buttons" style="margin-top:2%; 
                margin-left:30%" OnClick="btnAddNewProduct_Click"/>
        </asp:Panel>        
        <asp:Panel ID="pnlAddNewAdmin" runat="server" style="text-align:center; margin-top:9%" Visible="false">
            <asp:Label ID="lblNewAdminEmail" runat="server" Text="New admin's e-mail:" CssClass="labels"></asp:Label>
            <br />
            <asp:TextBox ID="txtNewAdminEmail" runat="server" Height="32px" Width="300px"></asp:TextBox>
            <br /><br />
            <asp:Label ID="lblYourPassword" runat="server" Text="Your password:" CssClass="labels"></asp:Label>
            <br />
            <asp:TextBox ID="txtYourPassword" runat="server" Height="32px" Width="300px" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="lblAddNewAdminError" runat="server" Text="" CssClass="labels"></asp:Label>
            <br /><br />
            <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" class="buttons" OnClick="btnSaveChanges_Click"/>
        </asp:Panel>
        <asp:Panel ID="pnlAddNewProduct" runat="server" style="margin-left:25%; margin-top:7%" Visible="false">
            <asp:Label ID="lblProductName" runat="server" Text="Product Name:" CssClass="labels"></asp:Label>
            <asp:Label ID="lblProductImg" runat="server" Text="Product Image URL:" CssClass="labels" style="margin-left:25%"></asp:Label>
            <br />
            <asp:TextBox ID="txtProductName" runat="server" Height="32px" Width="300px"></asp:TextBox>
            <asp:TextBox ID="txtProductImg" runat="server" Height="32px" Width="300px" style="margin-left:9%"></asp:TextBox>
            <br /><br />           
            <asp:Label ID="lblProductBrand" runat="server" Text="Product Author:" CssClass="labels"></asp:Label>
            &nbsp;&nbsp;<asp:Label ID="lblProductQuantity" runat="server" CssClass="labels" style="margin-left:24%" Text="Product Quantity:"></asp:Label>
            <br />
            <asp:TextBox ID="txtProductBrand" runat="server" Height="32px" Width="300px"></asp:TextBox>
            <asp:TextBox ID="txtProductQuantity" runat="server" Height="32px" style="margin-left:9%" Width="300px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblProductCategoryName" runat="server" CssClass="labels" Text="Product Category:"></asp:Label>
            &nbsp;<asp:Label ID="lblProductPrice" runat="server" Text="Product Price:" CssClass="labels" style="margin-left:23%"></asp:Label>
            <br />
            <asp:TextBox ID="txtProductCategoryName" runat="server" Height="32px" Width="300px"></asp:TextBox>
            <asp:TextBox ID="txtProductPrice" runat="server" Height="32px" Width="300px" style="margin-left:9%"></asp:TextBox>         
            <br /><br />
            <asp:Label ID="lblProductDesc" runat="server" Text="Product Description:" CssClass="labels"></asp:Label>
            <br />
            <asp:TextBox ID="txtProductDesc" runat="server" Height="150px" Width="300px" TextMode="MultiLine"></asp:TextBox>            
            <br />
            <asp:Label ID="lblAddNewProError" runat="server" Text="" CssClass="labels"></asp:Label>
            <br /><br />
            <asp:Button ID="btnSaveChanges2" runat="server" Text="Save Changes" class="buttons" OnClick="btnSaveChanges2_Click" style="margin-left:40%"/>
        </asp:Panel>
        <asp:Panel ID="pnlProducts" runat="server" style="text-align:center">
            <div class="parent">
                <asp:ListView ID="lvCart" runat="server" OnItemCommand="lvCart_ItemCommand" >
                 <LayoutTemplate>   
                    <table id="Table1" runat="server" class="TableCSS">   
                       <tr id="Tr1" runat="server" class="TableHeader" style="text-align:center">   
                          <td id="Td1" runat="server">Product</td>   
                          <td id="Td2" runat="server">Name</td>   
                           <td id="Td3" runat="server">Update</td>   
                           <td id="Td4" runat="server">Remove</td>   
                           <td id="Td5" runat="server">Details</td>   
                        </tr>   
                        <tr id="ItemPlaceholder" runat="server"> </tr>   
                    </table>   
                 </LayoutTemplate>   
                 <ItemTemplate>
                    <tr class="TableData">   
                       <td style="text-align:center">
                          <asp:Image ID="Image1" runat="server" height="90px" width="90px" 
                              ImageUrl='<%# string.Format("~/{0}",Eval("pro_img"))%>'></asp:Image>   
                       </td>   
                       <td style="text-align:center"> 
                           <asp:Label  ID="lblProName" runat="server" style="font-size:14pt" Text='<%# Eval("pro_name")%>'></asp:Label>
                       </td> 
                       <td style="text-align:center"> 
                           <asp:LinkButton ID="lkbtnDetails" runat="server" Text="Details" style="color:#3d760d; font-size:14pt" 
                                    CommandName="Details" />
                       </td>
                       <td style="text-align:center">
                           <asp:LinkButton ID="lkbtnUpdate" runat="server" Text="Update" style="color:#bb1e1e; font-size:14pt" 
                                    CommandName="Updt" />
                       </td>  
                       <td style="text-align:center"> 
                           <asp:LinkButton ID="lkbtnRemove" runat="server" Text="Remove" style="color:#0066FF; font-size:14pt" 
                                    CommandName="Remove" />
                       </td>                            
                     </tr>   
                  </ItemTemplate> 
                  <SelectedItemTemplate>
                     <div id="selected"><%# Eval("pro_name") %></div>
                  </SelectedItemTemplate>                   
          </asp:ListView>
            </div>
        </asp:Panel>                
    </form>
</body>
</html>
