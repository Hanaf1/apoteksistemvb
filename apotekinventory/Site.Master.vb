Partial Class SiteMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetActiveNavItem()
            SetPageTitle()
        End If
    End Sub

    Private Sub SetActiveNavItem()
        Dim currentPage As String = System.IO.Path.GetFileName(Request.Url.AbsolutePath)

        Select Case currentPage
            Case "Home.aspx"
                navHome.Attributes.Add("class", "nav-item active")
            Case "Kasir.aspx"
                navKasir.Attributes.Add("class", "nav-item active")
            Case "Inventory.aspx"
                navInventory.Attributes.Add("class", "nav-item active")
            Case "Settings.aspx"
                navSetting.Attributes.Add("class", "nav-item active")
            Case Else
                ' Default case for other pages
        End Select
    End Sub

    Private Sub SetPageTitle()
        Dim currentPage As String = System.IO.Path.GetFileName(Request.Url.AbsolutePath)
        Select Case currentPage
            Case "Home.aspx"
                Page.Title = "Home - Apotek B-18"
            Case "Kasir.aspx"
                Page.Title = "Kasir - Apotek B-18"
            Case "Inventory.aspx"
                Page.Title = "Inventory - Apotek B-18"
            Case "Settings.aspx"
                Page.Title = "Settings - Apotek B-18"
            Case Else
                Page.Title = "Apotek B-18"
        End Select
    End Sub

    Protected Sub Logout_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Clear session, cookies, atau tindakan logout lainnya yang diperlukan
        ' Contoh penghapusan session:
        Session.Clear()
        Session.Abandon()

        ' Redirect ke halaman login atau halaman lain setelah logout
        Response.Redirect("~/Login.aspx")
    End Sub
End Class
