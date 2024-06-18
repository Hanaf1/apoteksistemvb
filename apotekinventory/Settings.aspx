<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Settings.aspx.vb" Inherits="apotekinventory.Settings" MasterPageFile="~/Site.Master" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Settings</h2>
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">User Information</h5>
                <p class="card-text">
                    <strong>Username: </strong>
                    <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                </p>
                <asp:LinkButton ID="lnkLogout" runat="server" CssClass="btn btn-danger" OnClick="lnkLogout_Click">Logout</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
