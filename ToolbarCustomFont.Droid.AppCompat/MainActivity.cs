using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Util;
using Android.Views;
using Java.Lang.Reflect;
using Java.Lang;

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

            using (var currentInflaterCopy = LayoutInflater.CloneInContext (BaseContext)) 
            {
                //Android.Support.V4.View.LayoutInflaterCompat.SetFactory(currentInflaterCopy, new CustomLayoutInflaterFactory());
                SetFactory(currentInflaterCopy, new CustomLayoutInflaterFactory());
            }

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        private static void SetFactory(LayoutInflater layoutInflater, Android.Support.V4.View.ILayoutInflaterFactory factory)
        {
            layoutInflater.Factory2 = (factory != null ? new FactoryWrapper2(factory) : null);
        }
    }

    public class FactoryWrapper2 : FactoryWrapper, LayoutInflater.IFactory2
    {
        public FactoryWrapper2(Android.Support.V4.View.ILayoutInflaterFactory delegateFactory)
            : base(delegateFactory)
        {}

        public View OnCreateView(View parent, string name, Context context, IAttributeSet attrs)
        {
            return DelegateFactory.OnCreateView(parent, name, context, attrs);
        }
    }

    public class FactoryWrapper : Java.Lang.Object, LayoutInflater.IFactory
    {
        protected readonly Android.Support.V4.View.ILayoutInflaterFactory DelegateFactory;

        public FactoryWrapper(Android.Support.V4.View.ILayoutInflaterFactory delegateFactory)
        {
            this.DelegateFactory = delegateFactory;
        }

        public View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
            return this.DelegateFactory.OnCreateView(null, name, context, attrs);
        }
    }
}


