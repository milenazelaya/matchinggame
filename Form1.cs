using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matchinggame
{
    public partial class Form1 : Form
    {
        // secondClicked apunta al segundo control Label 
        // que el jugador hace clic
        Label secondClicked = null;

        // firstClicked apunta al primer control Label 
        // que el jugador hace clic, pero será nulo 
        // si el jugador aún no ha hecho clic en una etiqueta
        Label firstClicked = null;

        // Utilice este objeto aleatorio para elegir iconos aleatorios para los cuadrados
        Random random = new Random();

        // Cada una de estas letras es un icono interesante
        // en la fuente Webdings, 
        // y cada icono aparece dos veces en esta lista

        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    };



        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        /// <summary>
        /// Asignar cada icono de la lista de iconos a un cuadrado aleatorio
        /// </summary>
        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                // El TableLayoutPanel tiene 16 etiquetas,
                // y la lista de iconos tiene 16 iconos, 
                // por lo que un icono se extrae al azar de la lista
                // y agregado a cada etiqueta
                {
                    Label iconLabel = control as Label;
                    if (iconLabel != null)
                    {
                        int randomNumber = random.Next(icons.Count);
                        iconLabel.Text = icons[randomNumber];
                        iconLabel.ForeColor = iconLabel.BackColor;
                        icons.RemoveAt(randomNumber);
                    }
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        /// <summary>
        /// Every label's Click event is handled by this event handler
        /// </summary>
        /// <param name="sender">The label that was clicked</param>
        /// <param name="e"></param>
        private void label_Click(object sender, EventArgs e)
            //sirve para cuando se le de click haga la orden que se le ha asignado 
        {
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (timer1.Enabled == true)
                    return;


                if (clickedLabel != null)
                {
                    // Si la etiqueta en la que se hizo clic es negra, el jugador hizo clic
                    // un icono que ya ha sido revelado -
                    // ignora el clic
                    if (clickedLabel.ForeColor == Color.Black)
                        return;

                    // Si firstClicked es nulo, este es el primer icono
                    // en el par en el que el jugador hizo clic,
                    // así que establezca firstClicked en la etiqueta que el jugador
                    // hace clic, cambia su color a negro y regresa
                    if (firstClicked == null)
                    {
                        firstClicked = clickedLabel;
                        firstClicked.ForeColor = Color.Black;
                        return;
                    }

                    // Si el jugador llega tan lejos, el temporizador no
                    // en ejecución y firstClicked no es nulo,
                    // por lo que este debe ser el segundo icono en el que el jugador hizo clic
                    // Establecer su color en negro
                    secondClicked = clickedLabel;
                    secondClicked.ForeColor = Color.Black;

                    CheckForWinner();

                    if (firstClicked.Text == secondClicked.Text)
                    {
                        firstClicked = null;
                        secondClicked = null;
                        return;
                    }

                    timer1.Start();
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Detén el cronómetro
            timer1.Stop();

            // Ocultar ambos iconos
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Restablecer el primer clic y el segundo clic
           // para que la próxima vez que se haga clic en una etiqueta
          //, el programa sepa que es el primer clic
            firstClicked = null;
            secondClicked = null;
        }
        private void CheckForWinner()
        {
            // Revise todas las etiquetas en TableLayoutPanel, 
            // revisando cada uno para ver si su ícono coincide
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            // If the loop didn’t return, it didn't find
            // any unmatched icons
            // That means the user won. Show a message and close the form
            MessageBox.Show("Has coincidido con todos los iconos!", "Felicidades");
            Close();
        }
    }
}