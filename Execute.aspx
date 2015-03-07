<%@ Page Title="Executer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Execute.aspx.cs" Inherits="PowerAdmin.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <table>
        <tr>
            <td>
                <h1>Executer une commande</h1>
            </td>
        </tr>
        <tr>
            <td>
                <h3>Code</h3>
            </td>
        </tr>

        <tr>
            <td>
                <asp:TextBox ID="PowerShellCodeBox" runat="server" Width=700 TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Button ID="ExecuteCode" runat="server" Text="Execute" OnClientClick = "return confirm('Are you sure you want to execute?');"  OnClick="ExecuteCode_Click" />
            </td>
        </tr>

        <tr>
            <td>
                <h3>Resultat</h3>
            </td>
        </tr>

        <tr>
            <td>
                <asp:TextBox ID="ResultBox" TextMode="MultiLine"  Width=700 runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
