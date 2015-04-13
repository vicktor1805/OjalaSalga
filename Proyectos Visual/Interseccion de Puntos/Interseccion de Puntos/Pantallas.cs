using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interseccion_de_Puntos
{
    public class Pantallas
    {
        private List<float[]> ListaPantallas = new List<float[]>();

        public List<float[]> ListaPantallas1
        {
            get { return ListaPantallas; }
            set { ListaPantallas = value; }
        }

        public void AgregarPantalla(float x1, float y1, float x2, float y2,float i, float j)
        {
            float[] ListaDatos = new float[6];
            ListaDatos[0] = x1;
            ListaDatos[1] = y1;
            ListaDatos[2] = x2;
            ListaDatos[3] = y2;
            ListaDatos[4] = i;
            ListaDatos[5] = j;

            ListaPantallas.Add(ListaDatos);
        }
    }

    
}
