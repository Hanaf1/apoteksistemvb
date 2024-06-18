Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Inventory
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindKategoriDropDown()
            BindGridView()
            ' Register JavaScript for GridView scroll position
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetScrollPosition", "SetGridViewScrollPosition();", True)
        End If
    End Sub

    Private Sub BindKategoriDropDown()
        Dim dt As New DataTable()
        Dim connString As String = "server=localhost;user id=root;password='';database=apotek"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand("SELECT ID, Nama FROM kategori", conn)
                conn.Open()
                Using sda As New MySqlDataAdapter(cmd)
                    sda.Fill(dt)
                End Using
            End Using
        End Using

        ddlKategori.DataSource = dt
        ddlKategori.DataTextField = "Nama"
        ddlKategori.DataValueField = "ID"
        ddlKategori.DataBind()

        ddlSearchKategori.DataSource = dt
        ddlSearchKategori.DataTextField = "Nama"
        ddlSearchKategori.DataValueField = "ID"
        ddlSearchKategori.DataBind()
        ddlSearchKategori.Items.Insert(0, New ListItem("All Categories", "0"))
    End Sub

    Private Sub BindGridView(Optional ByVal categoryId As Integer = 0)
        Dim dt As New DataTable()
        Dim connString As String = "server=localhost;user id=root;password='';database=apotek"
        Dim query As String = "SELECT p.ID, p.Nama, p.KategoriID, k.Nama AS KategoriNama, p.Kode, p.Harga, p.Jumlah FROM produk p JOIN kategori k ON p.KategoriID = k.ID"

        If categoryId > 0 Then
            query &= " WHERE p.KategoriID = @KategoriID"
        End If

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                If categoryId > 0 Then
                    cmd.Parameters.AddWithValue("@KategoriID", categoryId)
                End If
                conn.Open()
                Using sda As New MySqlDataAdapter(cmd)
                    sda.Fill(dt)
                End Using
            End Using
        End Using

        gvProduk.DataSource = dt
        gvProduk.DataBind()
    End Sub

    Protected Sub gvProduk_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvProduk.EditIndex = e.NewEditIndex
        BindGridView(Convert.ToInt32(ddlSearchKategori.SelectedValue)) ' Rebind with the current filter

        ' Store selected product ID in hidden field
        Dim productId As Integer = Convert.ToInt32(gvProduk.DataKeys(e.NewEditIndex).Value)
        hfProdukID.Value = productId.ToString()
        gvProduk.Attributes("data-editindex") = e.NewEditIndex.ToString()

        ' Register JavaScript for GridView scroll position
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetScrollPosition", "SetGridViewScrollPosition();", True)
    End Sub

    Protected Sub gvProduk_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        gvProduk.EditIndex = -1
        BindGridView()
        gvProduk.Attributes.Remove("data-editindex")
        ' Register JavaScript for GridView scroll position
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetScrollPosition", "SetGridViewScrollPosition();", True)
    End Sub

    Protected Sub gvProduk_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim productId As Integer = Convert.ToInt32(hfProdukID.Value)

        ' Retrieve updated values from controls
        Dim row As GridViewRow = gvProduk.Rows(e.RowIndex)
        Dim nama As String = TryCast(row.FindControl("txtNama"), TextBox).Text.Trim()
        Dim kode As String = TryCast(row.FindControl("txtKode"), TextBox).Text.Trim()
        Dim harga As Decimal = Convert.ToDecimal(TryCast(row.FindControl("txtHarga"), TextBox).Text.Trim())
        Dim jumlah As Integer = Convert.ToInt32(TryCast(row.FindControl("txtJumlah"), TextBox).Text.Trim())
        Dim kategoriID As Integer = Convert.ToInt32(TryCast(row.FindControl("ddlEditKategori"), DropDownList).SelectedValue)

        ' Update database
        Dim connString As String = "server=localhost;user id=root;password='';database=apotek"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand("UPDATE produk SET Nama=@Nama, Kode=@Kode, Harga=@Harga, Jumlah=@Jumlah, KategoriID=@KategoriID WHERE ID=@ID", conn)
                cmd.Parameters.AddWithValue("@ID", productId)
                cmd.Parameters.AddWithValue("@Nama", nama)
                cmd.Parameters.AddWithValue("@Kode", kode)
                cmd.Parameters.AddWithValue("@Harga", harga)
                cmd.Parameters.AddWithValue("@Jumlah", jumlah)
                cmd.Parameters.AddWithValue("@KategoriID", kategoriID)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        gvProduk.EditIndex = -1
        BindGridView(Convert.ToInt32(ddlSearchKategori.SelectedValue)) ' Rebind with the current filter
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetScrollPosition", "SetGridViewScrollPosition();", True)
        ShowMessage("Product updated successfully!")
    End Sub

    Protected Sub gvProduk_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim productId As Integer = Convert.ToInt32(gvProduk.DataKeys(e.RowIndex).Value)

        ' Delete from database
        Dim connString As String = "server=localhost;user id=root;password='';database=apotek"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand("DELETE FROM produk WHERE ID=@ID", conn)
                cmd.Parameters.AddWithValue("@ID", productId)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        BindGridView()
        ShowMessage("Product deleted successfully!")
    End Sub

    Private Sub ShowMessage(ByVal message As String)
        Dim script As String = "$(document).ready(function() { $('#messageModalBody').text('" & message & "'); $('#messageModal').modal('show'); });"
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", script, True)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim nama As String = txtNama.Text.Trim()
        Dim kode As String = txtKode.Text.Trim()
        Dim harga As Decimal = Convert.ToDecimal(txtHarga.Text.Trim())
        Dim jumlah As Integer = Convert.ToInt32(txtJumlah.Text.Trim())
        Dim kategoriID As Integer = Convert.ToInt32(ddlKategori.SelectedValue)

        Dim connString As String = "server=localhost;user id=root;password='';database=apotek"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand("INSERT INTO produk (Nama, Kode, Harga, Jumlah, KategoriID) VALUES (@Nama, @Kode, @Harga, @Jumlah, @KategoriID)", conn)
                cmd.Parameters.AddWithValue("@Nama", nama)
                cmd.Parameters.AddWithValue("@Kode", kode)
                cmd.Parameters.AddWithValue("@Harga", harga)
                cmd.Parameters.AddWithValue("@Jumlah", jumlah)
                cmd.Parameters.AddWithValue("@KategoriID", kategoriID)

                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using
        End Using

        BindGridView()

        ' Clear the form fields
        txtNama.Text = ""
        txtKode.Text = ""
        txtHarga.Text = ""
        txtJumlah.Text = ""
        ddlKategori.SelectedIndex = 0
        ShowMessage("Product added successfully!")

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Clear the form fields
        txtNama.Text = ""
        txtKode.Text = ""
        txtHarga.Text = ""
        txtJumlah.Text = ""
        ddlKategori.SelectedIndex = 0
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim selectedCategoryId As Integer = Convert.ToInt32(ddlSearchKategori.SelectedValue)
        BindGridView(selectedCategoryId)
    End Sub

    Protected Sub gvProduk_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow AndAlso (e.Row.RowState And DataControlRowState.Edit) = DataControlRowState.Edit Then
            BindEditKategoriDropDown(e.Row)
        End If
    End Sub

    Private Sub BindEditKategoriDropDown(ByVal row As GridViewRow)
        Dim dt As New DataTable()
        Dim connString As String = "server=localhost;user id=root;password='';database=apotek"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand("SELECT ID, Nama FROM kategori", conn)
                conn.Open()
                Using sda As New MySqlDataAdapter(cmd)
                    sda.Fill(dt)
                End Using
            End Using
        End Using

        Dim ddlEditKategori As DropDownList = TryCast(row.FindControl("ddlEditKategori"), DropDownList)
        If ddlEditKategori IsNot Nothing Then
            ddlEditKategori.DataSource = dt
            ddlEditKategori.DataTextField = "Nama"
            ddlEditKategori.DataValueField = "ID"
            ddlEditKategori.DataBind()

            ' Set the selected value
            Dim kategoriID As Integer = Convert.ToInt32(DataBinder.Eval(row.DataItem, "KategoriID"))
            ddlEditKategori.SelectedValue = kategoriID.ToString()
        End If
    End Sub
End Class