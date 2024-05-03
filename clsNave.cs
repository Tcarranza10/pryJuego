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




        public clsNave()
        {

        }

        public void crearJugador()
        {
           
            vida = 100;
            nombre = "jugador1";
            puntosDaño = 1;
            imgNave = new PictureBox();
            imgNave.SizeMode = PictureBoxSizeMode.StretchImage;
            imgNave.Image = Properties.Resources.SpaceShip;
        }


     
    }
}
