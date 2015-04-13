using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interseccion_de_Puntos
{
    public partial class Form1 : Form
    {

        private Pantallas pantallas;
        private Operacion operacion;
        private float[] resultado;
        public Form1()
        {
            pantallas = new Pantallas();
            operacion = new Operacion();
            resultado = new float[2];
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtXPantalla.Text == "" ||
                txtYPantalla.Text == "" ||
                txtIPantalla.Text == "" ||
                txtJPantalla.Text == "" ||
                txtNormalI.Text   == "" ||
                txtNormalJ.Text   == ""
                )
            {
                MessageBox.Show("Rellene todos los datos");
            }
            else
            {
                float xPantalla = (float)Convert.ToDouble(txtXPantalla.Text.ToString());
                float yPantalla = (float)Convert.ToDouble(txtYPantalla.Text.ToString());
                float iPantalla = (float)Convert.ToDouble(txtIPantalla.Text.ToString());
                float jPantalla = (float)Convert.ToDouble(txtJPantalla.Text.ToString());
                float iNormal = (float)Convert.ToDouble(txtNormalI.Text.ToString());
                float jNormal = (float)Convert.ToDouble(txtNormalJ.Text.ToString());

                pantallas.AgregarPantalla(xPantalla, yPantalla, iPantalla, jPantalla,iNormal,jNormal);

                MessageBox.Show("Pantalla Agregada");
                txtXPantalla.Text = "";
                txtYPantalla.Text = "";
                txtIPantalla.Text = "";                
                txtJPantalla.Text = "";
                txtNormalI.Text = "";
                txtNormalJ.Text = "";
            }                 

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnTocaPantalla_Click(object sender, EventArgs e)
        {
            if (pantallas.ListaPantallas1.Count == 0)
            {
                MessageBox.Show("Por favor, ingrese una pantalla como mínimo");
            }
            else
            {
                bool tocaAlgunaPantalla = false;
                int IndicePantalla = 1;

                if (txtXDatos.Text == "" ||
                   txtYDatos.Text == "" ||
                   txtIDatos.Text == "" ||
                   txtJDatos.Text == ""
                   )
                {
                    MessageBox.Show("Rellene todos los datos");
                }
                else
                {
                    float xDato = (float)Convert.ToDouble(txtXDatos.Text.ToString());
                    float yDato = (float)Convert.ToDouble(txtYDatos.Text.ToString());
                    float iDato = (float)Convert.ToDouble(txtIDatos.Text.ToString());
                    float jDato = (float)Convert.ToDouble(txtJDatos.Text.ToString());

                    operacion.agregarDatos(xDato, yDato, iDato, jDato);
                    tocaAlgunaPantalla = operacion.chocoPantalla(pantallas.ListaPantallas1);
                    resultado = operacion.ObtenerPuntoInterseccion();
                    IndicePantalla += operacion.IndicePantalla1;

                    if (tocaAlgunaPantalla == false)
                    {
                        MessageBox.Show("No choca con ninguna pantalla. FUENTES!");
                    }
                    else
                    {
                        MessageBox.Show("Choca con la pantalla " + IndicePantalla.ToString() + " en el punto x: " + resultado[0].ToString() + "  y: " + resultado[1].ToString() + " .");
                    }

                }
            }

        }
    }
}
