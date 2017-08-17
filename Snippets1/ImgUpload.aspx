<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ImgUpload.aspx.cs" Inherits="ImgUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="table">
        <tr>
            <td>Choosen an image
                <br />
                
                <asp:FileUpload ID="fu_img" runat="server" />
                <asp:HiddenField ID="hf_oldImg" runat="server" />
        
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button Text="Upload" ID="btn_upload" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btn_upload_Click" />
                 <asp:Button Text="Save" CssClass="btn btn-primary btn-sm" runat="server" ID="btn_save" OnClick="btn_save_Click" Visible="false" />
            </td>
        </tr>
         <tr>
                    <td>
                       </td>
                </tr>
    </table>
    <hr />
    <asp:Repeater runat="server" ID="repeater_data" OnItemCommand="repeater_data_ItemCommand" >
                <ItemTemplate>
                    <table class="table">

                        <tr>
                            <td class="col-xs-3">
                                <img src="Images/<%# Eval("image_file") %>" alt="Text"  width="100" /></td>
                            
                            <td class="col-xs-3">
                                <asp:Button Text="Ret" CssClass="btn-success" runat="server" CommandArgument='<%# Eval("image_id") %>' CommandName="edit" /></td>
                            <td class="col-xs-3">
                                <asp:Button Text="Slet" CssClass="btn-danger" runat="server" CommandArgument='<%# Eval("image_id") %>' CommandName="delete" OnClientClick="return confirm('You are about to delete this user, are you suer about it?');" /></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>




</asp:Content>


