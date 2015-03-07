<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListScripts.aspx.cs" Inherits="PowerAdmin.ListScripts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ListView ID="scriptList" runat="server"
        DataKeyNames="ID"
        GroupItemCount="3" ItemType="PowerAdmin.Models.Script" SelectMethod="GetScripts">
        <EmptyDataTemplate>
            <table id="Table1" runat="server">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <td id="Td1" runat="server" />
        </EmptyItemTemplate>
        <GroupTemplate>
            <tr id="itemPlaceholderContainer" runat="server">
                <td id="itemPlaceholder" runat="server"></td>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <%#:Item.Name%>
        </ItemTemplate>
        <LayoutTemplate>
        </LayoutTemplate>
    </asp:ListView>--%>
    <asp:GridView ID="GridView" runat="server" 
        ItemType="PowerAdmin.Models.Script" SelectMethod="GetScripts" DataKeyNames="ID"
        AutoGenerateColumns="True" AllowPaging="True" >
 <%-- BorderWidth="1px" BackColor="White"
        CellPadding="4" BorderStyle="None" BorderColor="#3366CC">
        <FooterStyle ForeColor="#003399"
            BackColor="#99CCCC"></FooterStyle>
        <PagerStyle ForeColor="#003399" HorizontalAlign="Left"
            BackColor="#99CCCC"></PagerStyle>
        <HeaderStyle ForeColor="#CCCCFF" Font-Bold="True"
            BackColor="#003399"></HeaderStyle>
<Columns>
            <asp:CommandField ShowEditButton="True"></asp:CommandField>
            <asp:BoundField ReadOnly="True" HeaderText="ProductID"
                InsertVisible="False" DataField="ID"
                SortExpression="ProductID"></asp:BoundField>
            <asp:BoundField HeaderText="Product"
                DataField="Name"
                SortExpression="ProductName"></asp:BoundField>
            <asp:BoundField HeaderText="Unit Price"
                DataField="UnitPrice" SortExpression="UnitPrice">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderText="Units In Stock"
                DataField="UnitsInStock" SortExpression="UnitsInStock">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
        </Columns>
        <SelectedRowStyle ForeColor="#CCFF99" Font-Bold="True"
            BackColor="#009999"></SelectedRowStyle>
        <RowStyle ForeColor="#003399" BackColor="White"></RowStyle>--%>
    </asp:GridView>
</asp:Content>
