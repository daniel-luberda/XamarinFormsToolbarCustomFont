using System;

using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ToolbarCustomFont.Droid
{
	[Activity(Label = "ToolbarCustomFont.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			LayoutInflater.Factory = new CustomLayoutInflaterFactory();

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}
	}
}

