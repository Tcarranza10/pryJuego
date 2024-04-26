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
        clsEnemigo objEnemigo;
        // Lista para almacenar los enemigos
        List<clsEnemigo> enemigos = new List<clsEnemigo>();

        List<PictureBox> disparos = new List<PictureBox>();

        Random posX = new Random();
        Random posY = new Random();
        Timer movimientoDisparosTimer = new Timer();
        private Timer cadenciaDisparoTimer = new Timer();
        private bool puedeDisparar = true;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Crear jugador
            objNaveJugador = new clsNave();

            objNaveJugador.crearJugador();

            objNaveJugador.imgNave.Location = new Point(350, 650);

            Controls.Add(objNaveJugador.imgNave);

            Timer enemigoTimer = new Timer();
            enemigoTimer.Interval = 1200; // Intervalo en milisegundos (1 segundo)
            enemigoTimer.Tick += EnemigoTimer_Tick;
            enemigoTimer.Start();

            //Crear enemigos
            for (int contador = 0; contador < 6; contador++)
            {
                objEnemigo = new clsEnemigo();
                PictureBox imgEnemigo = objEnemigo.CrearEnemigo();
                imgEnemigo.Location = new Point(posX.Next(0, 700), posY.Next(0, 580));
                Controls.Add(imgEnemigo);
                enemigos.Add(objEnemigo);
            }

            // Configurar temporizador para mover disparos
            movimientoDisparosTimer.Interval = 50; // Intervalo en milisegundos (0.05 segundo)
            movimientoDisparosTimer.Tick += MovimientoDisparosTimer_Tick;
            movimientoDisparosTimer.Start();


            // Configurar temporizador de cadencia de disparo
            cadenciaDisparoTimer.Interval = 15200;
            cadenciaDisparoTimer.Tick += CadenciaDisparoTimer_Tick;
            cadenciaDisparoTimer.Start();

        }

        private void CadenciaDisparoTimer_Tick(object sender, EventArgs e)
        {
            // Restablecer la capacidad de disparo
            puedeDisparar = true;
            // Detener el temporizador de cadencia
            cadenciaDisparoTimer.Stop();
        }

        private void EnemigoTimer_Tick(object sender, EventArgs e)
        {
            List<clsEnemigo> enemigosAEliminar = new List<clsEnemigo>();

            // Verificar colisión con el jugador y mover los enemigos
            foreach (clsEnemigo enemigo in enemigos)
            {
                if (objNaveJugador.imgNave.Bounds.IntersectsWith(enemigo.imgNave.Bounds))
                {
                    // Detener todos los temporizadores
                    movimientoDisparosTimer.Stop();
                    ((Timer)sender).Stop(); // Detener el temporizador de enemigos

                    // Mostrar mensaje de fin de juego
                    MessageBox.Show("¡Game Over!", "Fin del Juego", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Terminar la aplicación
                    Application.Exit();
                    return; // Salir del método para evitar continuar moviendo enemigos
                }

                // Mover cada enemigo hacia abajo
                enemigo.imgNave.Top += 10;

                // Verificar si el enemigo está fuera de la pantalla
                if (enemigo.imgNave.Top > ClientSize.Height)
                {
                    // Agregar a la lista de enemigos a eliminar
                    enemigosAEliminar.Add(enemigo);
                }
            }

            // Eliminar enemigos que están fuera de la pantalla
            foreach (clsEnemigo enemigoAEliminar in enemigosAEliminar)
            {
                Controls.Remove(enemigoAEliminar.imgNave);
                enemigos.Remove(enemigoAEliminar);
            }

            // Verificar si necesitamos generar nuevos enemigos
            if (enemigos.Count <6) 
            {
                // Crear un nuevo enemigo dentro de los límites del formulario
                clsEnemigo nuevoEnemigo = new clsEnemigo();
                PictureBox imgEnemigo = nuevoEnemigo.CrearEnemigo();

                // Generar una posición aleatoria dentro de los límites del formulario
                Random rnd = new Random();
                int minX = 0;
                int minY = 0;
                int maxX = 600 - imgEnemigo.Width;
                int maxY = 580 - imgEnemigo.Height;

                bool posicionValida = false;
                int intentos = 0;
                while (!posicionValida && intentos < 60) // Intentar hasta 60 veces
                {
                    // Generar coordenadas aleatorias
                    int posX = rnd.Next(minX, maxX);
                    int posY = rnd.Next(minY, maxY);

                    // Verificar la distancia mínima con otros enemigos
                    // y con el jugador
                    bool distanciaAceptable = true;
                    foreach (clsEnemigo otroEnemigo in enemigos)
                    {
                        if (Math.Abs(posX - otroEnemigo.imgNave.Location.X) < 140 &&
                            Math.Abs(posY - otroEnemigo.imgNave.Location.Y) < 140)
                        {
                            distanciaAceptable = false;
                            break;
                        }
                    }

                    if (distanciaAceptable)
                    {
                        // Verificar la distancia con el jugador
                        if (Math.Abs(posX - objNaveJugador.imgNave.Location.X) >= 80 ||
                            Math.Abs(posY - objNaveJugador.imgNave.Location.Y) >= 80)
                        {
                            imgEnemigo.Location = new Point(posX, posY);
                            Controls.Add(imgEnemigo);
                            enemigos.Add(nuevoEnemigo);
                            posicionValida = true;
                        }
                    }

                    intentos++; // Incrementar el contador de intentos
                }
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Manejar el movimiento del jugador
            if (e.KeyCode == Keys.Right)
            {
                MovePlayerRight();
            }
            else if (e.KeyCode == Keys.Left)
            {
                MovePlayerLeft();
            }
            else if (e.KeyCode == Keys.Up)
            {
                MovePlayerUp();
            }
            else if (e.KeyCode == Keys.Down)
            {
                MovePlayerDown();
            }
            // Manejar el disparo del jugador
            else if (e.KeyCode == Keys.Space)
            {
                Disparar();
                // Desactivar la capacidad de disparo
                puedeDisparar = false;
                // Reiniciar temporizador de cadencia
                cadenciaDisparoTimer.Start();
            }
        }

        private void MovePlayerRight()
        {
            int newLocationX = objNaveJugador.imgNave.Location.X + 5;
            if (newLocationX + objNaveJugador.imgNave.Width <= ClientSize.Width)
            {
                objNaveJugador.imgNave.Location = new Point(newLocationX, objNaveJugador.imgNave.Location.Y);
            }
        }

        private void MovePlayerLeft()
        {
            int newLocationX = objNaveJugador.imgNave.Location.X - 5;
            if (newLocationX >= 0)
            {
                objNaveJugador.imgNave.Location = new Point(newLocationX, objNaveJugador.imgNave.Location.Y);
            }
        }

        private void MovePlayerUp()
        {
            int newLocationY = objNaveJugador.imgNave.Location.Y - 5;
            if (newLocationY >= 0)
            {
                objNaveJugador.imgNave.Location = new Point(objNaveJugador.imgNave.Location.X, newLocationY);
            }
        }

        private void MovePlayerDown()
        {
            int newLocationY = objNaveJugador.imgNave.Location.Y + 5;
            if (newLocationY + objNaveJugador.imgNave.Height <= ClientSize.Height)
            {
                objNaveJugador.imgNave.Location = new Point(objNaveJugador.imgNave.Location.X, newLocationY);
            }
        }

        private void MovimientoDisparosTimer_Tick(object sender, EventArgs e)
        {
            // Crear una copia de la lista de disparos
            List<PictureBox> copiaDisparos = new List<PictureBox>(disparos);

            // Mover todos los disparos hacia arriba
            foreach (PictureBox disparo in copiaDisparos)
            {
                // Cambiar la velocidad de movimiento 
                disparo.Top -= 10;

                // Comprobar colisiones entre disparos y enemigos
                foreach (clsEnemigo enemigo in enemigos)
                {
                    if (disparo.Bounds.IntersectsWith(enemigo.imgNave.Bounds))
                    {
                        // Si hay colisión, quitar el disparo y dañar al enemigo
                        Controls.Remove(disparo);
                        disparos.Remove(disparo);
                        enemigo.vida -= objNaveJugador.puntosDaño; // Reducir la vida del enemigo
                        if (enemigo.vida <= 0)
                        {
                            // Si la vida del enemigo llega a cero o menos, eliminarlo
                            Controls.Remove(enemigo.imgNave);
                            enemigos.Remove(enemigo);
                        }
                        break; // Salir del bucle interno una vez que se haya procesado una colisión
                    }
                }
            }
        }

        private void Disparar()
        {
            // Crear un nuevo disparo
            PictureBox disparo = new PictureBox();
            // Tamaño del disparo
            disparo.Size = new Size(5, 10);
            // Color del disparo
            disparo.BackColor = Color.Yellow;
            disparo.Location = new Point(
                // Centrar el disparo horizontalmente
                objNaveJugador.imgNave.Location.X + objNaveJugador.imgNave.Width / 2 - disparo.Width / 2,
                // Colocar el disparo encima del jugador
                objNaveJugador.imgNave.Location.Y - disparo.Height);
            Controls.Add(disparo);
            disparos.Add(disparo);
        }
    }
}
