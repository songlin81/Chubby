<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="BindingDemo.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

   <ul>
   <asp:Repeater ID="rptStudents" runat="server">
       <ItemTemplate>
          <li><%#Stu(x=>x.Name + "(" +x.Age+")")%></li>
       </ItemTemplate>
   </asp:Repeater>
   </ul>

    <ul>
    <asp:Repeater ID="rptGroups" runat="server">
        <ItemTemplate>
              <li>
                 <ol>
                     <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#Grp(_=>_.Students) %>'>
                        <ItemTemplate>
                           <li><%#Stu(_=>_.Name + "(" +_.Age+")")%></li>
                        </ItemTemplate>
                     </asp:Repeater>
                 </ol>         
              </li>
        </ItemTemplate>
    </asp:Repeater>
    </ul>

</body>
</html>
