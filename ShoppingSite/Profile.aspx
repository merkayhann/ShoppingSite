<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ShoppingSite.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profile</title>
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
            background-color:#66FF66; border-radius:20px; height:40px; width:300px; border-color:#66FF66;
            font-family:Arial; font-size:18px; color:#51294b;
        }
        .parent {
          margin-left:15%; 
          margin-top:5%;
        }
        .child {
            
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
            <asp:Panel ID="pnlProfile" runat="server">
                <div class="child" style="float:left">
                <asp:Label ID="lblUserName" runat="server" Text="Username:" CssClass="labels"></asp:Label>
                <br />
                <asp:TextBox ID="txtUserName" runat="server" Width="300px" Height="32px" Enabled="False"></asp:TextBox>
                <br /> <br />
                <asp:Label ID="lblEmail" runat="server" Text="e-mail:" CssClass="labels"></asp:Label>
                <br />
                <asp:TextBox ID="txtEmail" runat="server" Width="300px" Height="32px" Enabled="False"></asp:TextBox>
                <br /> <br />
                <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number:" CssClass="labels"></asp:Label>
                <br />
                <asp:TextBox ID="txtPhone" runat="server" Width="300px" Height="32px" Enabled="False"></asp:TextBox>
                <br /> <br />
                <asp:Label ID="lblAddress" runat="server" Text="Address:" CssClass="labels"></asp:Label>
                <br />
                <asp:TextBox ID="txtAddress" runat="server" Width="300px" Height="150px" Enabled="False" TextMode="MultiLine"></asp:TextBox>
                <br /> <br />
                <asp:Label ID="lblerr" runat="server" Text="" CssClass="labels"></asp:Label>
            </div>           
            <div class="child" style="float:right; margin-right:45%; margin-left:10%; margin-top:7%">
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="buttons" OnClick="btnChangePassword_Click"/>
                <br /><br />
                <asp:Button ID="btnUpdateInfos" runat="server" Text="Change Informations" CssClass="buttons" OnClick="btnUpdateInfos_Click" />
                <br /><br />
                <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="buttons" OnClick="btnSaveChanges_Click" Visible="False" />
                <br /><br /><br /><br /><br /><br />
                <asp:Button ID="btnLogOut" runat="server" Text="Log Out" CssClass="buttons" OnClick="btnLogOut_Click" 
                    style="background-color:#1d5acb; border-color:#1d5acb; color:#ffffcc" />
            </div>
            </asp:Panel>
            
        </div>
    </form>
</body>
</html>
