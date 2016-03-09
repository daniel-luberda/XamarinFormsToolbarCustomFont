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
namespace ToolbarCustomFont.Droid.AppCompat
{
    public class CustomLayoutInflaterFactory : Java.Lang.Object, Android.Support.V4.View.ILayoutInflaterFactory
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

        public View OnCreateView(View parent, string name, Context context, IAttributeSet attrs)
        {
            System.Diagnostics.Debug.WriteLine (name);

            if (name.Equals("android.support.v7.internal.view.menu.ActionMenuItemView", StringComparison.InvariantCultureIgnoreCase))
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
                        if(v is LinearLayout) {
                            var ll = (LinearLayout)v;
                            for(int i = 0; i < ll.ChildCount; i++) {
                                var button = ll.GetChildAt(i) as Button;

                                if(button != null) {
                                    var title = button.Text;

                                    if (!string.IsNullOrEmpty(title) && title.Length == 1)
                                    {
                                        button.SetTypeface(Typeface, TypefaceStyle.Normal);
                                    }
                                }
                            }
                        }
                        else if(v is TextView) {
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

            return null;
        }
    }
}

