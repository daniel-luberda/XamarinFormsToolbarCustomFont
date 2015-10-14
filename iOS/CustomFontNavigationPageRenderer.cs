using System;
using Xamarin.Forms;
using ToolbarCustomFont.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof (NavigationPage), typeof (CustomFontNavigationPageRenderer))]
namespace ToolbarCustomFont.iOS
{
	public class CustomFontNavigationPageRenderer : NavigationRenderer
	{
		public CustomFontNavigationPageRenderer()
		{
		}

		const string customFontName = "FontAwesome.ttf";
		nfloat customFontSize = 10;

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (this.NavigationBar == null) return;

			SetNavBarStyle();
//			SetNavBarTitle();
			SetNavBarItems();
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

			UIBarButtonItem.Appearance.SetTitleTextAttributes(textAttributes,
				UIControlState.Normal);
			UIBarButtonItem.Appearance.SetTitleTextAttributes(textAttributesHighlighted,
				UIControlState.Highlighted);
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
}

