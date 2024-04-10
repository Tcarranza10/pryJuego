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
        clsNave objEnemigo;
        Random posX = new Random();
        Random posY = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            objNaveJugador = new clsNave();

            objNaveJugador.crearJugador();

            objNaveJugador.imgNave.Location = new Point(350,600);
            
            
            Controls.Add(objNaveJugador.imgNave);
            //MessageBox.Show(objNaveJugador.nombre);

            
            int contador = 0;
            while (contador < 5)
            {
                objEnemigo = new clsNave();

                objEnemigo.crearEnemigo();
                
                int valorX = posX.Next(0, 400);
                int valorY = posY.Next(0, 400);

                objEnemigo.imgNave.Location = new Point(valorX, valorY);
                Controls.Add(objEnemigo.imgNave);

                contador++;
            }
            




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

            if (e.KeyCode == Keys.Up)
            {
                objNaveJugador.imgNave.Location = new Point(
                    objNaveJugador.imgNave.Location.X ,
                    objNaveJugador.imgNave.Location.Y - 5);
            }

            if (e.KeyCode == Keys.Down)
            {
                objNaveJugador.imgNave.Location = new Point(
                    objNaveJugador.imgNave.Location.X ,
                    objNaveJugador.imgNave.Location.Y + 5);
            }
        }
    }
}
