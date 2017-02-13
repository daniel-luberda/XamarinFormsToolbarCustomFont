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

			ToolbarItems.Add(new ToolbarItem("Right", null, () => {
				Device.BeginInvokeOnMainThread(() => {
					Title = "Right";
				});
			}));

            ToolbarItems.Add(new ToolbarItem("First", null, () => {
                Device.BeginInvokeOnMainThread(() => {
                    Title = "First";
                });
            }, ToolbarItemOrder.Secondary, 0));

            ToolbarItems.Add(new ToolbarItem("Second", null, () => {
                Device.BeginInvokeOnMainThread(() => {
                    Title = "Second";
                });
            }, ToolbarItemOrder.Secondary, 1));

            ToolbarItems.Add(new ToolbarItem("Third", null, () => {
                Device.BeginInvokeOnMainThread(() => {
                    Title = "Third";
                });
            }, ToolbarItemOrder.Secondary, 2));


            Title = "\uf002";
		}
	}
}


