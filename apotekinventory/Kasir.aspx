<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Kasir.aspx.vb" Inherits="apotekinventory.Kasir" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Kasir</h1>
        <div class="form-group">
            <label for="dropdownProduk">Pilih Produk:</label>
            <asp:DropDownList ID="dropdownProduk" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="txtJumlah">Jumlah:</label>
            <asp:TextBox ID="txtJumlah" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnTambah" runat="server" Text="Tambah Produk" CssClass="btn btn-success" OnClick="btnTambah_Click" />
        </div>
        <hr />
        <h4>Detail Pembelian</h4>
        <asp:GridView ID="gvPembelian" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowEditing="gvPembelian_RowEditing" OnRowUpdating="gvPembelian_RowUpdating" OnRowCancelingEdit="gvPembelian_RowCancelingEdit" OnRowDeleting="gvPembelian_RowDeleting">
            <Columns>
                <asp:BoundField DataField="NamaProduk" HeaderText="Nama Produk" ReadOnly="True" />
                <asp:BoundField DataField="Jumlah" HeaderText="Jumlah" />
                <asp:BoundField DataField="Harga" HeaderText="Harga" DataFormatString="{0:C}" ReadOnly="True" />
                <asp:BoundField DataField="TotalHarga" HeaderText="Total Harga" DataFormatString="{0:C}" ReadOnly="True" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
        <div class="form-group">
            <asp:Button ID="btnHitungTotal" runat="server" Text="Hitung Total" CssClass="btn btn-primary" OnClick="btnHitungTotal_Click" />
        </div>
        <div class="form-group">
            <h4>Total Pembelian: <asp:Label ID="lblTotalPembelian" runat="server" Text="0"></asp:Label></h4>
        </div>
        <div class="form-group">
            <label for="txtCustomerName">Nama Konsumen:</label>
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtUangDiterima">Uang Diterima:</label>
            <asp:TextBox ID="txtUangDiterima" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnKonfirmasi" runat="server" Text="Konfirmasi Pembelian" CssClass="btn btn-primary" OnClick="btnKonfirmasi_Click" />
        </div>
        <div class="form-group">
            <h4>Kembalian: <asp:Label ID="lblKembalian" runat="server" Text="0"></asp:Label></h4>
        </div>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
    </div>
</asp:Content>
