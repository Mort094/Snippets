<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Repeater runat="server" ID="repeater_test" OnItemDataBound="repeater_test_ItemDataBound">
        <ItemTemplate>
         
        </ItemTemplate>
        <FooterTemplate>
            <asp:Label Text="If you see this you are screwed, and your database is empty" runat="server" ID="lbl_fail" />
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

