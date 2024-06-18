Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Kasir
    Inherits System.Web.UI.Page

    <Serializable>
    Private Class PembelianItem
        Public Property ProdukId As Integer
        Public Property NamaProduk As String
        Public Property Jumlah As Integer
        Public Property Harga As Decimal

        Public Sub New(ByVal produkId As Integer, ByVal namaProduk As String, ByVal jumlah As Integer, ByVal harga As Decimal)
            Me.ProdukId = produkId
            Me.NamaProduk = namaProduk
            Me.Jumlah = jumlah
            Me.Harga = harga
        End Sub

        Public ReadOnly Property TotalHarga As Decimal
            Get
                Return Me.Jumlah * Me.Harga
            End Get
        End Property
    End Class

    ' Daftar produk yang dipilih untuk pembelian
    Private Property PembelianItems As List(Of PembelianItem)
        Get
            If ViewState("PembelianItems") Is Nothing Then
                ViewState("PembelianItems") = New List(Of PembelianItem)()
            End If
            Return DirectCast(ViewState("PembelianItems"), List(Of PembelianItem))
        End Get
        Set(ByVal value As List(Of PembelianItem))
            ViewState("PembelianItems") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "Kasir"
            BindProdukDropDown()
            BindGridPembelian()
        End If
    End Sub

    Private Sub BindProdukDropDown()
        Dim connectionString As String = "server=localhost;user=root;password='';database=apotek;"
        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "SELECT ID, Nama, Harga FROM produk"
            Using command As New MySqlCommand(query, connection)
                connection.Open()
                Dim reader As MySqlDataReader = command.ExecuteReader()
                dropdownProduk.DataSource = reader
                dropdownProduk.DataTextField = "Nama"
                dropdownProduk.DataValueField = "ID"
                dropdownProduk.DataBind()
                dropdownProduk.Items.Insert(0, New ListItem("Pilih Produk", String.Empty))
                reader.Close()
            End Using
        End Using
    End Sub

    Protected Sub btnTambah_Click(ByVal sender As Object, ByVal e As EventArgs)
        Page.Title = "Kasir"
        lblMessage.Text = "" ' Clear previous messages

        If dropdownProduk.SelectedIndex = 0 Then
            lblMessage.Text = "Silakan pilih produk."
            Return
        End If

        Dim selectedProdukId As Integer
        If Not Integer.TryParse(dropdownProduk.SelectedValue, selectedProdukId) Then
            lblMessage.Text = "Produk yang dipilih tidak valid."
            Return
        End If

        Dim jumlahBeli As Integer
        If Not Integer.TryParse(txtJumlah.Text, jumlahBeli) OrElse jumlahBeli <= 0 Then
            lblMessage.Text = "Jumlah harus berupa angka positif."
            Return
        End If

        Dim namaProduk As String = dropdownProduk.SelectedItem.Text
        Dim hargaProduk As Decimal = GetHargaProdukById(selectedProdukId)
        Dim newItem As New PembelianItem(selectedProdukId, namaProduk, jumlahBeli, hargaProduk)
        PembelianItems.Add(newItem)

        BindGridPembelian()
        ClearInputFields()
    End Sub

    Private Sub BindGridPembelian()
        gvPembelian.DataSource = PembelianItems
        gvPembelian.DataBind()
    End Sub

    Protected Sub btnHitungTotal_Click(ByVal sender As Object, ByVal e As EventArgs)
        HitungTotalPembelian()
    End Sub

    Private Sub HitungTotalPembelian()
        Dim total As Decimal = PembelianItems.Sum(Function(item) item.TotalHarga)
        lblTotalPembelian.Text = total.ToString("C")
    End Sub

    Private Sub ClearInputFields()
        dropdownProduk.SelectedIndex = 0
        txtJumlah.Text = String.Empty
    End Sub

    Private Function GetHargaProdukById(ByVal produkId As Integer) As Decimal
        Dim connectionString As String = "server=localhost;user=root;password='';database=apotek;"
        Dim harga As Decimal = 0
        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "SELECT Harga FROM produk WHERE ID = @ProdukId"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@ProdukId", produkId)
                connection.Open()
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing Then
                    harga = Convert.ToDecimal(result)
                End If
            End Using
        End Using
        Return harga
    End Function

    Protected Sub gvPembelian_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvPembelian.EditIndex = e.NewEditIndex
        BindGridPembelian()
    End Sub

    Protected Sub gvPembelian_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim index As Integer = e.RowIndex
        Dim row As GridViewRow = gvPembelian.Rows(index)
        Dim jumlahTextBox As TextBox = DirectCast(row.Cells(1).Controls(0), TextBox)

        If Integer.TryParse(jumlahTextBox.Text, PembelianItems(index).Jumlah) Then
            PembelianItems(index).Jumlah = Integer.Parse(jumlahTextBox.Text)
            gvPembelian.EditIndex = -1
            BindGridPembelian()
            HitungTotalPembelian()
        Else
            lblMessage.Text = "Jumlah harus berupa angka."
        End If
    End Sub

    Protected Sub gvPembelian_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        gvPembelian.EditIndex = -1
        BindGridPembelian()
    End Sub

    Protected Sub gvPembelian_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim index As Integer = e.RowIndex
        PembelianItems.RemoveAt(index)
        BindGridPembelian()
        HitungTotalPembelian()
    End Sub

    Protected Sub btnKonfirmasi_Click(ByVal sender As Object, ByVal e As EventArgs)
        lblMessage.Text = "" ' Clear previous messages
        lblKembalian.Text = "0" ' Clear previous change

        ' Validate input fields
        Dim customerName As String = txtCustomerName.Text.Trim()
        If String.IsNullOrEmpty(customerName) Then
            lblMessage.Text = "Nama konsumen harus diisi."
            Return
        End If

        Dim uangDiterima As Decimal
        If Not Decimal.TryParse(txtUangDiterima.Text, uangDiterima) OrElse uangDiterima <= 0 Then
            lblMessage.Text = "Uang yang diterima harus berupa angka positif."
            Return
        End If

        ' Calculate total purchase and change
        Dim totalPembelian As Decimal = PembelianItems.Sum(Function(item) item.TotalHarga)
        If uangDiterima < totalPembelian Then
            lblMessage.Text = "Uang yang diterima tidak cukup untuk total pembelian."
            Return
        End If

        Dim kembalian As Decimal = uangDiterima - totalPembelian
        lblKembalian.Text = kembalian.ToString("C")

        ' Update stock for each product
        For Each item In PembelianItems
            If Not UpdateProductStock(item.ProdukId, item.Jumlah) Then
                lblMessage.Text = "Stok produk tidak mencukupi untuk produk: " & item.NamaProduk
                Return
            End If
        Next

        ' Log the transaction
        LogTransaction(customerName, totalPembelian)

        ' Clear the purchase list
        PembelianItems.Clear()
        BindGridPembelian()
        HitungTotalPembelian()
        ClearInputFields()

        lblMessage.Text = "Pembelian berhasil dikonfirmasi."
    End Sub

    Private Function UpdateProductStock(ByVal produkId As Integer, ByVal jumlah As Integer) As Boolean
        Dim connectionString As String = "server=localhost;user=root;password='';database=apotek;"
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using transaction = connection.BeginTransaction()
                Try
                    Dim queryCheckStock As String = "SELECT Jumlah FROM produk WHERE ID = @ProdukId"
                    Using commandCheckStock As New MySqlCommand(queryCheckStock, connection, transaction)
                        commandCheckStock.Parameters.AddWithValue("@ProdukId", produkId)
                        Dim stok As Integer = Convert.ToInt32(commandCheckStock.ExecuteScalar())

                        If stok < jumlah Then
                            ' Not enough stock
                            transaction.Rollback()
                            Return False
                        End If

                        Dim queryUpdateStock As String = "UPDATE produk SET Jumlah = Jumlah - @Jumlah WHERE ID = @ProdukId"
                        Using commandUpdateStock As New MySqlCommand(queryUpdateStock, connection, transaction)
                            commandUpdateStock.Parameters.AddWithValue("@Jumlah", jumlah)
                            commandUpdateStock.Parameters.AddWithValue("@ProdukId", produkId)
                            commandUpdateStock.ExecuteNonQuery()
                        End Using
                    End Using
                    transaction.Commit()
                    Return True
                Catch ex As Exception
                    transaction.Rollback()
                    lblMessage.Text = "Terjadi kesalahan saat memperbarui stok: " & ex.Message
                    Return False
                End Try
            End Using
        End Using
    End Function

    Private Sub LogTransaction(ByVal customerName As String, ByVal totalPembelian As Decimal)
        Dim connectionString As String = "server=localhost;user=root;password='';database=apotek;"
        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "INSERT INTO riwayat_pembelian (NamaKonsumen, TotalBiaya, Tanggal) VALUES (@NamaKonsumen, @TotalBiaya, @Tanggal)"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@NamaKonsumen", customerName)
                command.Parameters.AddWithValue("@TotalBiaya", totalPembelian)
                command.Parameters.AddWithValue("@Tanggal", DateTime.Now)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class
