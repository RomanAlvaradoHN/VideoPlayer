using VideoPlayer.Controllers;
namespace VideoPlayer {
    public partial class App : Application {
        public static readonly DBController db = new DBController();
        public static readonly string videosDirectory = Path.Combine(FileSystem.AppDataDirectory, "Videos");




        public App() {
            InitializeComponent();


            //valida existencia del directorio de los videos, si no existe, lo crea.
            if (!Directory.Exists(videosDirectory)) {
                Directory.CreateDirectory(videosDirectory);
            }

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.RecorderScreen());
        }
    }
}
