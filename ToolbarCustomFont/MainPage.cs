using System;

using Xamarin.Forms;

namespace ToolbarCustomFont
{
	public class MainPage : ContentPage
	{
		public MainPage()
		{
			Content = new StackLayout { 
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					new Label { Text = "Custom Font on Toolbar is working!" }
				}
			};
					
			ToolbarItems.Add(new ToolbarItem("\uf001", null, () => {
				
			}));

			ToolbarItems.Add(new ToolbarItem("Test", null, () => {

			}));
		}
	}
}


