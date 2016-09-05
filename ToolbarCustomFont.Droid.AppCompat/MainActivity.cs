using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Util;
using Android.Views;
using Java.Lang.Reflect;
using Java.Lang;
using System;
using Android.Graphics;

namespace ToolbarCustomFont.Droid.AppCompat
{
    [Activity (Label = "ToolbarCustomFont.Droid.AppCompat", MainLauncher = true, Theme="@style/MyTheme")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate (savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

		static Class ActionMenuItemViewClass = null;
		static Constructor ActionMenuItemViewConstructor = null;

		static Typeface typeface = null;
		public static Typeface Typeface
		{
			get
			{
				if (typeface == null)
					typeface = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "Fonts/fontawesome.ttf");

				return typeface;
			}
		}

		public override View OnCreateView(string name, Context context, IAttributeSet attrs)
		{
			if (name.Equals("android.support.v7.view.menu.ActionMenuItemView", StringComparison.InvariantCultureIgnoreCase))
			{
				System.Diagnostics.Debug.WriteLine(name);
				var customLoginIfNeeded = CreateCustomToolbarItem(name, context, attrs);
				if (customLoginIfNeeded != null)
					return customLoginIfNeeded;
			}

			return base.OnCreateView(name, context, attrs);
		}

		public override View OnCreateView(View parent, string name, Context context, IAttributeSet attrs)
		{
			if (name.Equals("android.support.v7.view.menu.ActionMenuItemView", StringComparison.InvariantCultureIgnoreCase))
			{
				System.Diagnostics.Debug.WriteLine(name);
				var customLoginIfNeeded = CreateCustomToolbarItem(name, context, attrs);
				if (customLoginIfNeeded != null)
					return customLoginIfNeeded;
			}

			return base.OnCreateView(parent, name, context, attrs);
		}

		private View CreateCustomToolbarItem(string name, Context context, IAttributeSet attrs)
		{
			// android.support.v7.widget.Toolbar
			// android.support.v7.view.menu.ActionMenuItemView
			View view = null;

			try
			{
				if (ActionMenuItemViewClass == null)
					ActionMenuItemViewClass = ClassLoader.LoadClass(name);
			}
			catch (ClassNotFoundException ex)
			{
				return null;
			}

			if (ActionMenuItemViewClass == null)
				return null;

			if (ActionMenuItemViewConstructor == null)
			{
				try
				{
					ActionMenuItemViewConstructor = ActionMenuItemViewClass.GetConstructor(new Class[] {
							Class.FromType(typeof(Context)),
								 Class.FromType(typeof(IAttributeSet))
						});
				}
				catch (SecurityException)
				{
					return null;
				}
				catch (NoSuchMethodException)
				{
					return null;
				}
			}
			if (ActionMenuItemViewConstructor == null)
				return null;

			try
			{
				Java.Lang.Object[] args = new Java.Lang.Object[] { context, (Java.Lang.Object)attrs };
				view = (View)(ActionMenuItemViewConstructor.NewInstance(args));
			}
			catch (IllegalArgumentException)
			{
				return null;
			}
			catch (InstantiationException)
			{
				return null;
			}
			catch (IllegalAccessException)
			{
				return null;
			}
			catch (InvocationTargetException)
			{
				return null;
			}
			if (null == view)
				return null;

			View v = view;

			new Handler().Post(() =>
			{

				try
				{
					if (v is LinearLayout)
					{
						var ll = (LinearLayout)v;
						for (int i = 0; i < ll.ChildCount; i++)
						{
							var button = ll.GetChildAt(i) as Button;

							if (button != null)
							{
								var title = button.Text;

								if (!string.IsNullOrEmpty(title) && title.Length == 1)
								{
									button.SetTypeface(Typeface, TypefaceStyle.Normal);
								}
							}
						}
					}
					else if (v is TextView)
					{
						var tv = (TextView)v;
						string title = tv.Text;

						if (!string.IsNullOrEmpty(title) && title.Length == 1)
						{
							tv.SetTypeface(Typeface, TypefaceStyle.Normal);
						}
					}
				}
				catch (ClassCastException)
				{
				}
			});

			return view;
		}
    }
}


