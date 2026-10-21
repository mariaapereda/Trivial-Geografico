using System;
using Microsoft.Maui.Controls;

namespace trivialGeografico
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCapitalesClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new PaisesPage());
        }

        private async void OnPaisesClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new CapitalesPage());
        }
    }
}
