<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewParameterSet.aspx.cs" Inherits="PowerAdmin.ViewParameterSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>
                <br />
                <asp:Label ID="ScriptDescriptionLabel" runat="server"></asp:Label>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Menu ID="ParameterSetMenu" Orientation="Horizontal" ForeColor="White" StaticMenuItemStyle-HorizontalPadding="10" StaticMenuItemStyle-BackColor="#7ac0da" DynamicMenuItemStyle-BackColor="#7ac0da" StaticHoverStyle-BackColor="#52adcf" DynamicHoverStyle-BackColor="#52adcf" runat="server"></asp:Menu>
    <br />
    <asp:Table ID="ArgumentsTable" runat="server">
    </asp:Table>
    <asp:Table ID="HiddenArgumentsTable" runat="server">
    </asp:Table>
    <asp:Table ID="UnmanagedArgumentsTable" runat="server">
    </asp:Table>
    <asp:Button ID="ExecuteButton" Text="Executer" runat="server" OnClientClick="return confirm('Are you sure you want to execute?');" OnClick="ExecuteButton_Click"></asp:Button>
    <asp:GridView ID="GridViewResult" AutoGenerateColumns="true" runat="server">
    </asp:GridView>
    <asp:GridView ID="GridViewError" AutoGenerateColumns="true" runat="server">
    </asp:GridView>
</asp:Content>
