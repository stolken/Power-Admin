<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Script.aspx.cs" Inherits="PowerAdmin.EditScript" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <asp:FormView ID="scriptFormView" runat="server" ItemType="PowerAdmin.Models.Script" SelectMethod="GetScript" OnItemUpdated="FormViewUpdatedEventHandler" UpdateMethod="UpdateScript" RenderOuterTable="false">
        <ItemTemplate>
            <div>
                <h1><%#:Item.Name %></h1>
            </div>
            <br />
            <%#:Item.Description %>
            <asp:Button ID="Edit" runat="server"
                CommandName="Edit" Font-Size="XX-Small" Text="..." />
        </ItemTemplate>
        <EditItemTemplate>
            <div>
                <h1><%#:Item.Name %></h1>
            </div>
            <br />
            <table>
                <tr>
                    <td>Description</td>
                    <td>
                        <asp:TextBox ID="EditScriptDescriptionTextBox"
                            Text='<%# Bind("Description") %>'
                            runat="Server" />
                    </td>
                </tr>
                <tr>
                    <td>Category</td>
                    <td>
                        <asp:DropDownList ID="CategoryScriptDropDown" runat="server"
                            ItemType="PowerAdmin.Models.Category"
                            SelectMethod="GetCategories" DataTextField="Name"
                            DataValueField="ID"
                            SelectedValue='<%# Bind("CategoryID") %>'>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Hide</td>
                    <td>
                        <asp:CheckBox ID="HiddenScriptCheckbox" runat="server" Checked='<%# Bind("Hidden") %>' />
                    </td>
                </tr>
                <tr>
                    <td>ID :
                    </td>
                    <td>
                        <%#:Item.ID %>
                        <asp:HiddenField runat="server" ID="Id" Value='<%# BindItem.ID %>' />
                    </td>
                </tr>
            </table>
            <asp:Button ID="Update" runat="server"
                CommandName="Update" Font-Size="Small" Text="Update" />
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Menu ID="ParameterSetMenu" runat="server" StaticSelectedStyle-BackColor="White" Orientation="Horizontal"></asp:Menu>
    <asp:FormView ID="ParameterSetFormView" runat="server" ItemType="PowerAdmin.Models.ParameterSet" SelectMethod="GetParameterSet" UpdateMethod="UpdateParameterSet" RenderOuterTable="false">
        <ItemTemplate>
            <%#:Item.Description %>
            <asp:Button ID="Edit" runat="server"
                CommandName="Edit" Font-Size="XX-Small" Text="..." />
        </ItemTemplate>
        <EditItemTemplate>
            <div>
                <h1><%#:Item.Name %></h1>
            </div>
            <br />
            <table>
                <tr>
                    <td>Description</td>
                    <td>
                        <asp:TextBox ID="EditParamaterSetDescriptionTextBox"
                            Text='<%# Bind("Description") %>'
                            runat="Server" />
                    </td>
                </tr>
                <tr>
                    <td>Caché</td>
                    <td>
                        <asp:CheckBox ID="HiddenCheckbox" runat="server" Checked='<%# Bind("Hidden") %>' />
                    </td>
                </tr>
                <tr>
                    <td>ID :
                    </td>
                    <td>
                        <%#:Item.ID %>
                        <asp:HiddenField runat="server" ID="Id" Value='<%# BindItem.ID %>' />
                    </td>
                </tr>
            </table>
            <asp:Button ID="Update" runat="server"
                CommandName="Update" Font-Size="Small" Text="Update" />
        </EditItemTemplate>
    </asp:FormView>
    <asp:Table ID="ParametersTable" runat="server">
    </asp:Table>   
    <asp:ListView ID="OthersParametersListView" runat="server"
        DataKeyNames="ID"
        ItemType="PowerAdmin.Models.Parameter"
        SelectMethod="GetOthersParameters">
        <EmptyDataTemplate>
            <table id="Table1" runat="server">
                <tr>
                    <td>No others parameters were returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <table runat="server" id="table1">
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server">
                    <asp:HiddenField runat="server" ID="InputControlHiddenField" Value='<%# BindItem.InputControl %>' />
                    <asp:Label ID="NameLabel" runat="server"
                        Text='<%#Eval("Name") %>' />
                </td>
                <td id="Td2" runat="server">
                    <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <asp:Button ID="ExecuteButton" Text="Execute" runat="server" OnClientClick="return confirm('Are you sure you want to execute?');" OnClick="ExecuteButton_Click"></asp:Button>
    <asp:GridView ID="GridViewResult" AutoGenerateColumns="true" runat="server">
    </asp:GridView>
    <asp:GridView ID="GridViewError" AutoGenerateColumns="true" runat="server">
    </asp:GridView>
</asp:Content>
