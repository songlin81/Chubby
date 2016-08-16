<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Repeater.WebForm1" %>

<%@ Register Src="~/WebUserControlClickTest.ascx" TagPrefix="uc1" TagName="WebUserControlClickTest" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
	    <form id="form1" runat="server">
	        <div>
		        <asp:Repeater id="repeater1" runat="server" >
		            <HeaderTemplate>
		                <table border="2">
		                    <tr><td colspan ="2"><b><u>Website Listing</u></b><br /></td></tr>
		            </HeaderTemplate>

		            <ItemTemplate>
		                <tr>
		                    <td>
		                        <asp:Image ID="Image1" height="31" width="127" src='<%# DataBinder.Eval(Container.DataItem, "website_logo")%>' runat="server"/>
		                    </td>
                            <td>
		                        <%# DataBinder.Eval(Container.DataItem, "website_name")%> <br />
		                    </td>
		                </tr>
		            </ItemTemplate>

		            <FooterTemplate>
		                    <tr><td colspan ="2">All Rights Reserved. </td></tr>
		                </tabel>
		            </FooterTemplate>
		        </asp:Repeater>
	        </div>

            <br/>
            
            <div>
		        <asp:TreeView ID="TreeView1" runat="server" onselectednodechanged="TreeView1_SelectedNodeChanged"></asp:TreeView>
	        </div>
	        <asp:XmlDataSource id="site" DataFile="sites.xml" Runat="server" />
	        <asp:Label ID="Label1" runat="server" Text="Tree view selection"></asp:Label>
           
            <br />
            <uc1:WebUserControlClickTest runat="server" id="WebUserControlClickTest" />
            <br />
            <asp:Label ID="Label2" runat="server" Text="User Control contents"></asp:Label>
            
            <br />
            <p>
                Username:<asp:TextBox id="tbxUserID" runat="server"></asp:TextBox><BR>
                Password:<asp:TextBox id="tbxPassword" runat="server" TextMode="Password"></asp:TextBox>
            </p>
            <p>
                <asp:Button id="btnLogin" runat="server" Text="Log On"></asp:Button>
                <asp:Label id="lblLoginMessage" runat="server"></asp:Label>
            </p>

            <asp:Panel id="Panel1" runat="server" Visible="False">
                <p>
                    <asp:Button id="btnAdmin" runat="server" Text="Role1"></asp:Button>
                    <asp:Button id="btnUser" runat="server" Text="Role2"></asp:Button>
                </p>
                <p>
                    <asp:Label id="lblRoleMessage" runat="server"></asp:Label>
                </p>
            </asp:Panel> 
	    </form>
    </body>
</html>
