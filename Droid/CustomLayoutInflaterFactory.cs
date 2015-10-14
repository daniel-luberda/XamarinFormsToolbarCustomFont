using System;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Lang.Reflect;
using Android.OS;
using Android.Graphics;

/*** Based on http://stackoverflow.com/a/5205945/5064986 ***/

namespace ToolbarCustomFont.Droid
{
	public class CustomLayoutInflaterFactory : Java.Lang.Object, LayoutInflater.IFactory
	{
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

		public View OnCreateView(string name, Context context, IAttributeSet attrs)
		{
			if (name.Equals("com.android.internal.view.menu.ActionMenuItemView", StringComparison.InvariantCultureIgnoreCase))
			{
				View view = null;

				try
				{
					if (ActionMenuItemViewClass == null)
						ActionMenuItemViewClass = ClassLoader.SystemClassLoader.LoadClass(name);
				}
				catch (ClassNotFoundException)
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

				new Handler().Post(() => {

					try
					{
						var tv = (TextView)v;

						string title = tv.Text;

						if (!string.IsNullOrEmpty(title) && title.Length == 1)
						{
							tv.SetTypeface(Typeface, TypefaceStyle.Normal);
						}
					}
					catch (ClassCastException)
					{
					}
				});

				return view;
			}

			return null;
		}
	}
}

