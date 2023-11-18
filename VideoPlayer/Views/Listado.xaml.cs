using VideoPlayer.Models;
namespace VideoPlayer.Views;

public partial class Listado : ContentPage
{

	public Listado(){
		InitializeComponent();
    }



    protected override async void OnAppearing() {
        base.OnAppearing();

        viewListado.ItemsSource = await App.db.SelectAll();
    }


    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args) {
        Datos d = args.SelectedItem as Datos;
        await Navigation.PushAsync(new VideoView(d.VideoFilePath));
    }
}