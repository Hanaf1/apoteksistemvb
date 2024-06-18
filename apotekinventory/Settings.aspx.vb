Imports System.Web.Security

Public Class Settings
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("username") IsNot Nothing Then
                lblUsername.Text = Session("username").ToString()
            Else
                lblUsername.Text = "Guest"
            End If
        End If
    End Sub

    Protected Sub lnkLogout_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Log out the user
        FormsAuthentication.SignOut()
        Session.Abandon()
        Response.Redirect("~/Login.aspx")
    End Sub
End Class
