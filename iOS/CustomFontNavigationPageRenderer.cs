using System;
using System.Collections.Generic;
using Foundation;
using ObjCRuntime;
using Xamarin.Forms;
using ToolbarCustomFont.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof (NavigationPage), typeof (CustomFontNavigationPageRenderer))]
namespace ToolbarCustomFont.iOS
{
	// https://blog.xamarin.com/custom-fonts-in-ios/
	public class CustomFontNavigationPageRenderer : NavigationRenderer
	{
		public CustomFontNavigationPageRenderer()
		{
		}

		const string customFontName = "fontawesome";
		nfloat customFontSize = 16;

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (this.NavigationBar == null) return;

			SetNavBarStyle();
//			SetNavBarTitle();
			SetNavBarItems();
		}

	    public override void ViewDidAppear(bool animated)
	    {
	        base.ViewDidAppear(animated);
	        if (this.NavigationBar == null) return;
	        var navPage = this.Element;
	        if (navPage == null) return;
	    }

	    private void SetNavBarStyle()
		{
			NavigationBar.ShadowImage = new UIImage();
			NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
			UINavigationBar.Appearance.ShadowImage = new UIImage();
			UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
		}

		private void SetNavBarItems()
		{
			var navPage = this.Element as NavigationPage;

			if (navPage == null) return;

			var textAttributes = new UITextAttributes()
			{
				Font = UIFont.FromName(customFontName, customFontSize)
			};

			var textAttributesHighlighted = new UITextAttributes()
			{
				TextColor = Color.Black.ToUIColor(),
				Font = UIFont.FromName(customFontName, customFontSize)
			};
            
		    var theFont = UIFont.FromName(customFontName, customFontSize);
		    var nsDictSegmentFont = GetFontNSDictionaryReversed(theFont);
            var segmentTextAttributes = new MyUITextAttributes(nsDictSegmentFont);

            UISegmentedControl.Appearance.SetTitleTextAttributes(segmentTextAttributes, UIControlState.Normal);
            UISegmentedControl.Appearance.SetTitleTextAttributes(segmentTextAttributes, UIControlState.Selected);

            UIBarButtonItem.Appearance.SetTitleTextAttributes(textAttributes,
				UIControlState.Normal);
			UIBarButtonItem.Appearance.SetTitleTextAttributes(textAttributesHighlighted,
				UIControlState.Highlighted);
		}

        // assembles a reversed dictionary to workaround bug in iOS
	    public NSDictionary GetFontNSDictionaryReversed(UIFont font)
	    {
            var handle = Dlfcn.dlopen("/System/Library/Frameworks/UIKit.framework/UIKit", 0);
            var nsfontattributename = "NSFontAttributeName";
            //var nsfontattributename = "UITextAttributeFont";  // deprecated
            NSString UITextAttributeFont = Dlfcn.GetStringConstant(handle, nsfontattributename);

	        var valuekeys = new List<NSObject>();
            if (font != null)
            {
                valuekeys.Add(font);
                valuekeys.Add(UITextAttributeFont);
            }
            return new NSDictionary(valuekeys[0], valuekeys[0]);
        }

//		private void SetNavBarTitle()
//		{
//			var navPage = this.Element as NavigationPage;
//
//			if (navPage == null) return;
//
//			this.NavigationBar.TitleTextAttributes = new UIStringAttributes
//			{
//				Font = UIFont.FromName(customFontName, customFontSize),
//			};
//		}
	}

    public class MyUITextAttributes : UITextAttributes
    {
        private NSDictionary myDictionary;

        public MyUITextAttributes(NSDictionary newDictionary)
        {
            this.myDictionary = newDictionary;
        }

        public new NSDictionary Dictionary
        {
            get
            {
                return ToDictionary();
            }
        }

        public new NSDictionary ToDictionary()
        {
            return myDictionary;
        }

    }

}

