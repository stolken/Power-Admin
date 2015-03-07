<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="PowerAdmin.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="progressbar"></div>
    <div id="dialog" title="Dialog Title">I'm a dialog</div>
<script>
    $("#progressbar").progressbar({value: 37});
    $( "#dialog" ).dialog({ autoOpen: true });
    //$( "#dialog" ).dialog( "open" );

</script>
    
    <table>
        <tr>
            <td>
                <h1>Reset</h1>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="ReInitDatabaseButton" Text="Rebuild database" runat="server" OnClientClick="return confirm('Are you sure you want to execute?');" OnClick="RebuildDatabaseButton_Click"></asp:Button>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="ResetSingleScriptButton" Text="Reset a single script" runat="server" OnClientClick="return confirm('Are you sure you want to execute?');" OnClick="ResetSingleScriptButton_Click"></asp:Button>
            </td>
            <td>
                <asp:TextBox ID="ResetSingleScriptTextbox" Width="700" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="ResetByQueryButton" Text="Reset by query" runat="server" OnClientClick="return confirm('Are you sure you want to execute?');" OnClick="ResetByQueryButton_Click"></asp:Button>
            </td>
            <td>
                <asp:TextBox ID="ResetByQueryTextbox" Width="700" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:CheckBox ID="ResetByQueryCheckBox" Text="Overwrite" runat="server" TextAlign="Left"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="AddNewScriptButton" Text="Add new scripts" runat="server" OnClientClick="return confirm('Are you sure you want to execute?');" OnClick="AddNewScriptButton_Click"></asp:Button>
            </td>
        </tr>
    </table>

</asp:Content>
