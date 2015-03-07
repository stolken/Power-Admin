<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <h1>Sam Account Name : </h1>
                </td>
                <td>
                    <asp:TextBox ID="SamAccountNameTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <h1>Given Name : </h1>
                </td>

                <td>
                    <asp:TextBox ID="GivenNameTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <h1>Surname : </h1>
                </td>
                <td>
                    <asp:TextBox ID="SurnameTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="CreateUser" runat="server" Text="Create user" OnClick="CreateUser_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
