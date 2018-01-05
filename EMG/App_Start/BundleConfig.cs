using System.Web;
using System.Web.Optimization;

namespace EMG
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-theme.css"));
            bundles.Add(new StyleBundle("~/Content/dataTables/css").Include(
                    "~/Content/DataTables-1.10.0/css/jquery.dataTables.css",
                    "~/Content/DataTables-1.10.0/css/dataTables.bootstrap.css",
                    "~/Content/DataTables-1.10.0/css/dataTables.custom.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/scripts/datatables").Include(
                    "~/Scripts/DataTables-1.10.0/jquery.dataTables.js",
                    "~/Scripts/DataTables-1.10.0/dataTables.bootstrap.js",
                    "~/Scripts/DataTables-1.10.0/dataTables.pipeline.js",
                    "~/Scripts/DataTables-1.10.0/dataTables-boker.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-datepicker.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/datepicker.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-datepicker.css"
                ));
        }
    }
}
