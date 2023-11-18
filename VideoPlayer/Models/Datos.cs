using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPlayer.Models {
    public class Datos {
        private List<string> invalidData = new List<string>();
        private byte[] video;
        private string nombre;
        private string videoFilePath;
        private string fecha;


        public Datos() { }



        public Datos(byte[] video) {
            this.Video = video;
            this.Nombre = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.videoFilePath = Path.Combine(App.videosDirectory, this.nombre);
            this.Fecha = DateTime.Today.ToString("d");
        }




        public List<string> GetDatosInvalidos() {
            return this.invalidData;
        }





        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }




        [Column("Video")]
        public byte[] Video {
            get { return this.video; }

            set {
                if (value != null && value.Length > 0) {
                    this.video = value;
                } else {
                    this.invalidData.Add("No hay grabacion de video.");
                }
            }
        }




        [Column("Nombre")]
        public string Nombre {
            get { return this.nombre; }
            set { this.nombre = value; }
        }





        [Column("VideoFilePath")]
        public string VideoFilePath {
            get { return this.videoFilePath; }
            set { this.videoFilePath = value; }
        }




        [Column("Fecha")]
        public string Fecha {
            get { return this.fecha; }
            set { this.fecha = value; }
        }
    }
}
