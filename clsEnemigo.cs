using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace pryJuego
{
    internal class clsEnemigo
    {
        public int vida;
        public string nombre;
        public int puntosDaño;
        public PictureBox imgNave;
        Random aleatorioEnemigo = new Random();

        public PictureBox CrearEnemigo()
        {
            int codigoEnemigo = aleatorioEnemigo.Next(0, 3);
            imgNave = new PictureBox();
            imgNave.SizeMode = PictureBoxSizeMode.StretchImage;

            switch (codigoEnemigo)
            {
                case 0:
                    vida = 25;
                    nombre = "enemigo1";
                    puntosDaño = 2;
                    imgNave.ImageLocation = System.IO.Path.Combine(Application.StartupPath, "imagenes", "Enemigo1.png");
                    break;

                case 1:
                    vida = 20;
                    nombre = "enemigo2";
                    puntosDaño = 2;
                    imgNave.ImageLocation = System.IO.Path.Combine(Application.StartupPath, "imagenes", "Enemigo2.png");
                    break;

                case 2:
                    vida = 20;
                    nombre = "enemigo3";
                    puntosDaño = 2;
                    imgNave.ImageLocation = System.IO.Path.Combine(Application.StartupPath, "imagenes", "Enemigo3.png");
                    break;

            }
            return imgNave;
        }
    }
}
