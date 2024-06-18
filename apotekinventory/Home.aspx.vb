Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DisplayTodayRevenueAndDate()
        End If
    End Sub

    Private Sub DisplayTodayRevenueAndDate()
        Dim connectionString As String = "server=localhost;user=root;password='';database=apotek;"
        Dim todayRevenue As Decimal = 0
        Dim todayDate As DateTime = DateTime.Today

        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "SELECT SUM(TotalBiaya) FROM riwayat_pembelian WHERE DATE(Tanggal) = CURDATE()"
            Using command As New MySqlCommand(query, connection)
                connection.Open()
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    todayRevenue = Convert.ToDecimal(result)
                End If
            End Using
        End Using

        lblOmzet.Text = todayRevenue.ToString("C")
        lblTanggal.Text = todayDate.ToString("dddd, dd MMMM yyyy")
    End Sub
End Class
