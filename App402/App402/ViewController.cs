using CoreGraphics;
using Foundation;
using ObjCRuntime;
using QuickLook;
using System;
using UIKit;

namespace App402
{

    public partial class ViewController : UIViewController
    {

        QLPreviewController previewController;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.AutomaticallyAdjustsScrollViewInsets = false;
            this.ExtendedLayoutIncludesOpaqueBars = false;
            this.EdgesForExtendedLayout = UIRectEdge.None;

            string path = NSBundle.MainBundle.PathForResource("myPdf.pdf","");
            var reportName = "Report name";
            previewController = new QLPreviewController();
            var url = new NSUrl(path, true);
            var _dataSource = new PreviewControllerSource(this, url, reportName);
            previewController.DataSource = _dataSource;
 
            this.NavigationController.PresentViewController(previewController, true, test);

        }

        public void test() {

                var firstChild = previewController.ChildViewControllers[0];

                if (firstChild is UINavigationController)
                {
                    var naviVc = firstChild as UINavigationController;
                    naviVc.NavigationBar.BarTintColor = UIColor.Red ;
                }
            }
    }


    public class naviDeleagte : UINavigationControllerDelegate {

        public override void WillShowViewController(UINavigationController navigationController, [Transient] UIViewController viewController, bool animated)
        {
            //call test here if you use a push instead of modal
        }
    }

    public partial class ViewController : UIViewController
    {
        class PreviewControllerSource : QLPreviewControllerDataSource
        {
            ViewController _parentClass = null;
            NSUrl _url = null;
            string _title = null;

            public PreviewControllerSource(ViewController parentClass, NSUrl url, string title)
            {
                _parentClass = parentClass;
                _url = url;
                _title = title;
            }

            public override nint PreviewItemCount(QLPreviewController controller)
            {
                return 1;
            }           

            public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
            {
                return new PreviewItem { title = _title, url = _url };
            }
        }
        public class PreviewItem : QLPreviewItem
        {
            public string title { get; set; }
            public NSUrl url { get; set; }
            public override string ItemTitle { get { return title; } }
            public override NSUrl ItemUrl { get { return url; } }
        }
    }
}