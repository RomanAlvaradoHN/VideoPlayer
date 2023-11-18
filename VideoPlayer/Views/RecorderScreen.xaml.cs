using CommunityToolkit.Maui.Views;
using VideoPlayer.Models;
namespace VideoPlayer.Views;

public partial class RecorderScreen : ContentPage
{
    private byte[] videoArray;

    public RecorderScreen()
	{
		InitializeComponent();
	}








    public async void OnBtnVideoClicked(object sender, EventArgs args) {
        await TakeVideo();
    }





    public async Task TakeVideo() {
        if (MediaPicker.Default.IsCaptureSupported) {
            FileResult video = await MediaPicker.Default.CaptureVideoAsync();

            if (video != null) {
                try {

                    using (MemoryStream ms = new MemoryStream()) {
                        Stream st = await video.OpenReadAsync();
                        await st.CopyToAsync(ms);
                        videoArray = ms.ToArray();
                    }


                    // save the file into local storage ( CacheDirectory )
                    string filePath = Path.Combine(FileSystem.CacheDirectory, video.FileName);
                    using (FileStream videoFile = File.OpenWrite(filePath)) {
                        Stream st = new MemoryStream(videoArray);
                        await st.CopyToAsync(videoFile);
                    }

                    videoElement.Source = MediaSource.FromFile(filePath);

                } catch (Exception ex) {
                    await DisplayAlert("Atención", ex.Message, "Aceptar");
                }
            }
        }
    }







    public async void OnBtnGuardarClicked(object sender, EventArgs args) {
        try {

            Datos datos = new Datos(videoArray);

            if (!datos.GetDatosInvalidos().Any()) {
                await App.db.Insert(datos);


                //Guardado del archivo de imagen en fisico.
                string path = Path.Combine(App.videosDirectory, datos.Nombre);
                using (FileStream videoFile = File.OpenWrite(path)) {
                    Stream st = new MemoryStream(videoArray);
                    await st.CopyToAsync(videoFile);
                }

                await DisplayAlert("Guardar", "Datos guardados", "Aceptar");
                LimpiarCampos();

            } else {
                string msj = string.Join("\n", datos.GetDatosInvalidos());
                await DisplayAlert("Datos Invalidos:", msj, "Aceptar");
            }


        } catch (Exception ex) {
            await DisplayAlert("Guardar", ex.Message, "Aceptar");
        }
    }





    public async void OnBtnListaClicked(object sender, EventArgs args) {
        await Navigation.PushAsync(new Listado());
    }







    public void OnBtnLimpiarClicked(object sender, EventArgs args) {
        LimpiarCampos();
    }



    private void LimpiarCampos() {
        videoArray = new byte[0];
        videoElement.Source = null;
    }

}