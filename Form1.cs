using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace pryJuego
{
    public partial class Form1 : Form
    {
        //Declaracion de variables globales
        clsNave objNaveJugador;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            objNaveJugador = new clsNave();

            objNaveJugador.crearJugador();

            objNaveJugador.imgNave.Location = new Point(200,200) ;
            
            Controls.Add(objNaveJugador.imgNave);
            //MessageBox.Show(objNaveJugador.nombre);


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                objNaveJugador.imgNave.Location = new Point(
                    objNaveJugador.imgNave.Location.X + 5,
                    objNaveJugador.imgNave.Location.Y);
            }

            if (e.KeyCode == Keys.Left)
            {
                objNaveJugador.imgNave.Location = new Point(
                    objNaveJugador.imgNave.Location.X - 5,
                    objNaveJugador.imgNave.Location.Y);
            }
        }
    }
}
