using System.Web;
using System.Web.Optimization;

namespace MyPlaceProject
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js","~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération à l'adresse https://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/sb-admin").Include(
                      "~/Scripts/sb-admin-2.js",
                      "~/Scripts/front-js/moment.min.js",
                      "~/Scripts/front-js/bootstrap-datetimepicker.min.js",
                      "~/Scripts/metisMenu.min.js"));

            bundles.Add(new StyleBundle("~/Content/cssAdmin").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/front-assets/bootstrap-datetimepicker.min.css",
                      "~/Content/sb-admin-2.min.css"));
            bundles.Add(new StyleBundle("~/Content/cssClient").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/sb-admin-2.min.css",
                      "~/Content/cssClient.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/front").Include(
                      "~/Content/front-assets/animate.css",
                      "~/Content/front-assets/icomoon.css",
                      "~/Content/front-assets/themify-icons.css",
                      "~/Content/front-assets/magnific-popup.css",
                      "~/Content/front-assets/bootstrap-datetimepicker.min.css",
                      "~/Content/front-assets/owl.carousel.min.css",
                      "~/Content/front-assets/owl.theme.default.min.css",
                      "~/Content/front-assets/style.css"));
            bundles.Add(new ScriptBundle("~/bundles/front").Include(
                      "~/Scripts/front-js/jquery.easing.1.3.js",
                        "~/Scripts/front-js/jquery.waypoints.min.js",
                        "~/Scripts/front-js/owl.carousel.min.js",
                        "~/Scripts/front-js/jquery.countTo.js",
                        "~/Scripts/front-js/jquery.stellar.min.js",
                        "~/Scripts/front-js/jquery.magnific-popup.min.js",
                        "~/Scripts/front-js/magnific-popup-options.js",
                        "~/Scripts/front-js/moment.min.js",
                        "~/Scripts/front-js/bootstrap-datetimepicker.min.js",
                        "~/Scripts/front-js/main.js"));
        }
    }
}
