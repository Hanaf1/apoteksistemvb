Imports MySql.Data.MySqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtUsername.Attributes.Add("placeholder", "Enter your username")
            txtPassword.Attributes.Add("placeholder", "Enter your password")
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        If username = "" Then
            lblMessage.Text = "Username Harus Diisi"
            lblMessage.CssClass = "alert alert-danger text-center"
            lblMessage.Visible = True
            Return
        ElseIf password = "" Then
            lblMessage.Text = "Password Harus Diisi"
            lblMessage.CssClass = "alert alert-danger text-center"
            lblMessage.Visible = True
            Return
        End If

        Dim connectionString As String = "server=localhost; user=root; password=''; database=apotek"
        Using koneksi As New MySqlConnection(connectionString)
            Try
                koneksi.Open()
                Dim query As String = "SELECT role FROM users WHERE Username=@username AND Password=@password"
                Dim cmd As New MySqlCommand(query, koneksi)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password)

                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    reader.Read()
                    Dim role As String = reader("role").ToString()
                    Session("username") = username
                    Session("role") = role

                    If role = "admin" Then
                        Response.Redirect("Home.aspx", False)
                    ElseIf role = "staff" Then
                        Response.Redirect("Home.aspx", False)
                    Else
                        lblMessage.Text = "Role tidak dikenal"
                        lblMessage.CssClass = "alert alert-danger text-center"
                        lblMessage.Visible = True
                    End If
                Else
                    lblMessage.Text = "Username atau password salah"
                    lblMessage.CssClass = "alert alert-danger text-center"
                    lblMessage.Visible = True
                End If
                reader.Close()
            Catch ex As Exception
                lblMessage.Text = "Terjadi kesalahan: " & ex.Message
                lblMessage.CssClass = "alert alert-danger text-center"
                lblMessage.Visible = True
            End Try
        End Using
    End Sub
End Class
