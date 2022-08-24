<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="ShoppingSite.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FictionLand</title>
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
        .numeric_button
        {
            color:yellow;     
        }
        .current_page
        {
            color:#0e2a99;    
            font-weight:bold;      
        }
        .parent {
          background-color:#faeabb; margin-left:13%; 
          margin-right:13%; margin-top:2%;
        }
        .child {
            background-color:#faeabb;
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
            <div class="child" style="float:left">
                <asp:Panel ID="pnlMainProducts1" runat="server">
                    <asp:ListView ID="lvMainPage" runat="server" OnItemCommand="lvMainPage_ItemCommand"
                        style="display:table">                    
                        <ItemTemplate>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lkbtnImage" runat="server" CommandName="GoToProduct" height="200px" width="200px">
                                        <asp:Image ID="imgPro" runat="server" height="200px" width="200px" ImageUrl='<%# string.Format("~/{0}",Eval("pro_img"))%>'/></asp:LinkButton>
                                    <br /><br /><br /><br />
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:LinkButton ID="lkbtnProName" runat="server" CommandName="GoToProduct" Font-Size="18pt" Font-Underline="false" ForeColor="#51294b" Text='<%# Eval("pro_name")%>'></asp:LinkButton>
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lkbtnProDesc" runat="server" CommandName="GoToProduct" Font-Size="12pt" Font-Underline="false" style="color:black" Text='<%# Eval("pro_desc")%>'></asp:LinkButton>
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblProPrice" runat="server" BackColor="Orange" Font-Size="23pt" style="color:black" Text='<%# Eval("pro_price")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table id="Table1" runat="server">
                                <tr id="ItemPlaceholder" runat="server"></tr>                            
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <div id="selected">
                                <%# Eval("pro_name") %>
                            </div>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </asp:Panel>
            </div>
            <div class="child" style="float:right">
                <asp:Panel ID="pnlMainProducts2" runat="server">
                    <asp:ListView ID="lvMainPage2" runat="server" OnItemCommand="lvMainPage2_ItemCommand"
                        style="display:table">                    
                        <ItemTemplate>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lkbtnImage2" runat="server" CommandName="GoToProduct2" height="200px" width="200px">
                                        <asp:Image ID="imgPro" runat="server" height="200px" width="200px" ImageUrl='<%# string.Format("~/{0}",Eval("pro_img"))%>'/></asp:LinkButton>
                                    <br /><br /><br /><br />
                                </td>
                                <td>&nbsp;&nbsp;
                                    <asp:LinkButton ID="lkbtnProName2" runat="server" CommandName="GoToProduct2" Font-Size="18pt" Font-Underline="false" ForeColor="#51294b" Text='<%# Eval("pro_name")%>'></asp:LinkButton>
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lkbtnProDesc2" runat="server" CommandName="GoToProduct2" Font-Size="12pt" Font-Underline="false" style="color:black" Text='<%# Eval("pro_desc")%>'></asp:LinkButton>
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblProPrice2" runat="server" BackColor="Orange" Font-Size="23pt" style="color:black" Text='<%# Eval("pro_price")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table id="Table1" runat="server">
                                <tr id="ItemPlaceholder" runat="server"></tr>                            
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <div id="selected">
                                <%# Eval("pro_name") %>
                            </div>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </asp:Panel>
            </div>
        </div>       
    </form>
</body>
</html>
