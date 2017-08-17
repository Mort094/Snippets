<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CRUD.aspx.cs" Inherits="CRUD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <table class="table">
        <tr>
            <td>Name
                        <br />
                <asp:TextBox runat="server" ID="tb_name" /></td>
        </tr>
        <tr>
            <td>Email
                        <br />
                <asp:TextBox runat="server" ID="tb_email" /></td>
        </tr>
        <tr>
            <td>Password
                        <br />
                <asp:TextBox runat="server" ID="tb_password" /></td>
        </tr>
        <tr>
            <td>Gender
                        <br />
                <asp:DropDownList ID="dd_gender" runat="server" DataTextField="gender" DataValueField="gender_id" Style="color: black;"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Button Text="Create" CssClass="btn btn-primary btn-sm" runat="server" ID="btn_create" OnClick="btn_create_Click" />
                <asp:Button Text="Save" CssClass="btn btn-primary btn-sm" runat="server" ID="btn_save" OnClick="btn_save_Click" Visible="false" /></td>
        </tr>

    </table>

    <hr />

    <asp:Repeater runat="server" ID="repeater_data" OnItemCommand="repeater_data_ItemCommand">
        <ItemTemplate>
            <table class="table">

                <tr>
                    <td class="col-xs-3"><%# Eval("user_name") %></td>
                    <td class="col-xs-3"><%# Eval("user_email") %></td>
                    <td class="col-xs-3"><%# Eval("gender") %></td>
                    <td class="col-xs-3">
                        <asp:Button Text="Ret" CssClass="btn-success" runat="server" CommandArgument='<%# Eval("user_id") %>' CommandName="edit" /></td>
                    <td class="col-xs-3">
                        <asp:Button Text="Slet" CssClass="btn-danger" runat="server" CommandArgument='<%# Eval("user_id") %>' CommandName="delete" OnClientClick="return confirm('You are about to delete this user, are you suer about it?');" /></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
