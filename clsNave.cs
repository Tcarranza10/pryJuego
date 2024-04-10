using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryJuego
{
    internal class clsNave
    {
        //caracteristicas 
        public int vida;
        public string nombre;
        public int puntosDaño;
        public PictureBox imgNave;

        public void crearJugador()
        {
            vida = 100;
            nombre = "jugador1";
            puntosDaño = 1;
            imgNave = new PictureBox();
            imgNave.SizeMode = PictureBoxSizeMode.StretchImage;
            imgNave.ImageLocation = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQInlJtDw4AOVWmJ8EBzQoUBE5CIXDPNDyXeA&s";
        }

        public void crearEnemigo()
        {
            //enemigo 1
            vida = 25;
            nombre = "enemigo1";
            puntosDaño = 2;
            imgNave = new PictureBox();
            imgNave.SizeMode = PictureBoxSizeMode.StretchImage;
            imgNave.ImageLocation = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTX9AAx6-LDfy0xe8B1-QzMRkp19ZzwlaESKA&s";

            //enemigo 2
            vida = 25;
            nombre = "enemigo2";
            puntosDaño = 2;
            imgNave=new PictureBox();
            imgNave.SizeMode = PictureBoxSizeMode.StretchImage;
            imgNave.ImageLocation = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRbYhg6FfNcC6Kd04YZn1o44Mg4kCXLRvQkbw&s";
        }

    }
}
