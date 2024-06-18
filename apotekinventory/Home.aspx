<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Home.aspx.vb" Inherits="apotekinventory.Home" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Konten spesifik halaman Home.aspx -->
    <div>
        <h1 class="h1 p-5">Selamat Datang</h1>
          <div class="form-group">
            <label for="lblTanggal">Tanggal Hari Ini:</label>
            <asp:Label ID="lblTanggal" runat="server" CssClass="form-control"></asp:Label>
        </div>
        <div class="form-group">
            <label for="lblOmzet">Omzet Hari Ini:</label>
            <asp:Label ID="lblOmzet" runat="server" CssClass="form-control" Text="0"></asp:Label>
        </div>
    </div>
</asp:Content>
