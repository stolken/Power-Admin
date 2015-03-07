<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewScript.aspx.cs" Inherits="PowerAdmin.ViewScript" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="ArgumentsTable" runat="server">
    </asp:Table>
    <asp:Table ID="HiddenArgumentsTable" runat="server">
    </asp:Table>
    <asp:Table ID="UnmanagedArgumentsTable" runat="server">
    </asp:Table>
    <asp:Button ID="ExecuteButton" text="Executer"  runat="server" OnClientClick = "return confirm('Are you sure you want to execute?');" OnClick="ExecuteButton_Click">
    </asp:Button>
    <br />
<%--    <asp:TextBox ID="OutputTextBox" TextMode="MultiLine"  Width=700 runat="server"></asp:TextBox>
    <br />--%>
    <asp:GridView ID="GridViewResult" AutoGenerateColumns="true"  runat="server">
    </asp:GridView>
    <asp:GridView ID="GridViewError" AutoGenerateColumns="true"  runat="server">
    </asp:GridView>
</asp:Content>
