<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Inventory.aspx.vb" Inherits="apotekinventory.Inventory" MasterPageFile="~/Site.Master" EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <script type="text/javascript">
             function SetGridViewScrollPosition() {
            var gridView = document.getElementById('<%= gvProduk.ClientID %>');
                if (gridView) {
                    var editIndex = gridView.getAttribute('data-editindex');
                    if (editIndex !== null && editIndex !== '') {
                        var editRow = gridView.rows[parseInt(editIndex)];
                        if (editRow) {
                            var yOffset = editRow.getBoundingClientRect().top + window.pageYOffset - 100;
                            window.scrollTo(0, yOffset);
                        }
                    }
                }
                }
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SetGridViewScrollPosition);
        </script>

        <!-- Bootstrap Modal -->
    <div class="modal fade" id="messageModal" tabindex="-1" role="dialog" aria-labelledby="messageModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="messageModalLabel">Notification</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" id="messageModalBody">
                    <!-- Message will be inserted here -->
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid">

        <h1>Inventory</h1>

        <!-- Dropdown for Category Search -->
        <div class="form-group">
            <asp:Label ID="lblSearchKategori" runat="server" Text="Search by Category:" AssociatedControlID="ddlSearchKategori"></asp:Label>
            <asp:DropDownList ID="ddlSearchKategori" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        </div>

        <!-- GridView to display products -->
        <asp:GridView ID="gvProduk" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="ID"
             OnRowEditing="gvProduk_RowEditing" OnRowCancelingEdit="gvProduk_RowCancelingEdit"
             OnRowUpdating="gvProduk_RowUpdating" OnRowDeleting="gvProduk_RowDeleting"
             OnRowDataBound="gvProduk_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Product Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNama" runat="server" Text='<%# Bind("Nama") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("Nama") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <%# Eval("KategoriNama") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEditKategori" runat="server" CssClass="form-control"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Code">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtKode" runat="server" Text='<%# Bind("Kode") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("Kode") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <%# String.Format("{0:C}", Eval("Harga")) %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtHarga" runat="server" Text='<%# Bind("Harga") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <%# Eval("Jumlah") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtJumlah" runat="server" Text='<%# Bind("Jumlah") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Are you sure you want to delete this product?');"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-success btn-sm"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-secondary btn-sm"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br />


        <h2>Tambah Barang</h2>
        <div class="form-group">
            <asp:Label ID="lblNama" runat="server" Text="Nama:" AssociatedControlID="txtNama"></asp:Label>
            <asp:TextBox ID="txtNama" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label ID="lblKategori" runat="server" Text="Kategori:" AssociatedControlID="ddlKategori"></asp:Label>
            <asp:DropDownList ID="ddlKategori" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Label ID="lblKode" runat="server" Text="Kode:" AssociatedControlID="txtKode"></asp:Label>
            <asp:TextBox ID="txtKode" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label ID="lblHarga" runat="server" Text="Harga:" AssociatedControlID="txtHarga"></asp:Label>
            <asp:TextBox ID="txtHarga" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label ID="lblJumlah" runat="server" Text="Jumlah:" AssociatedControlID="txtJumlah"></asp:Label>
            <asp:TextBox ID="txtJumlah" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
        </div>

        <asp:HiddenField ID="hfProdukID" runat="server" />

        <!-- Placeholder for error or success message -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
    </div>
</asp:Content>
