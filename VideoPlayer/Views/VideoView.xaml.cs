using CommunityToolkit.Maui.Views;
using VideoPlayer.Models;

namespace VideoPlayer.Views;

public partial class VideoView : ContentPage
{
	string videoPath;



	public VideoView(string videoPath){
		InitializeComponent();
		this.videoPath = videoPath;
	}



    protected override void OnAppearing() {
        base.OnAppearing();
        videoElement.Source = MediaSource.FromFile(videoPath);
    }




    protected override void OnDisappearing() {
        base.OnDisappearing();
        videoElement.Handler?.DisconnectHandler();
        //videoElement.Source = null;
    }
}