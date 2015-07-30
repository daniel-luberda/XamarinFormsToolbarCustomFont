using System;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Xamarin.Forms;

namespace ToolbarCustomFont.Droid
{
	public class CustomFontSpan : MetricAffectingSpan
	{
		static LruCache typefaceCache = new LruCache(5);

		Typeface typeFace;

		public CustomFontSpan(string typefaceName)
		{
			typeFace = (Typeface)typefaceCache.Get(typefaceName);

			if (typeFace == null)
			{
				typeFace = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, typefaceName);
				typefaceCache.Put(typefaceName, typeFace);
			}
		}

		public override void UpdateMeasureState(TextPaint p)
		{
			p.SetTypeface(typeFace);
			p.Flags = p.Flags | PaintFlags.SubpixelText;
		}

		public override void UpdateDrawState(TextPaint tp)
		{
			tp.SetTypeface(typeFace);
			tp.Flags = tp.Flags | PaintFlags.SubpixelText;
		}
	}
}

