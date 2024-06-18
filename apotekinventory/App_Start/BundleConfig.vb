Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Optimization

Public Class BundleConfig
    Public Shared Sub RegisterBundles(ByVal bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/WebFormsJs").Include(
                        "~/Scripts/WebForms/WebForms.js",
                        "~/Scripts/WebForms/WebUIValidation.js",
                        "~/Scripts/WebForms/MenuStandards.js",
                        "~/Scripts/WebForms/Focus.js",
                        "~/Scripts/WebForms/GridView.js",
                        "~/Scripts/WebForms/DetailsView.js",
                        "~/Scripts/WebForms/TreeView.js",
                        "~/Scripts/WebForms/WebParts.js"))

        bundles.Add(New ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"))

     
        bundles.Add(New ScriptBundle("~/bundles/modernizr-1.0").Include(
                "~/Scripts/modernizr-1.0.js"))

        bundles.Add(New ScriptBundle("~/bundles/modernizr-2.0").Include(
                        "~/Scripts/modernizr-2.0.js"))


        ' Tambahkan bundel untuk Bootstrap CSS dan JavaScript
        bundles.Add(New StyleBundle("~/Content/css").Include(
                        "~/Content/css/bootstrap.min.css"))

        bundles.Add(New ScriptBundle("~/Content/js").Include(
                        "~/Content/js/bootstrap.bundle.min.js"))

    End Sub
End Class
