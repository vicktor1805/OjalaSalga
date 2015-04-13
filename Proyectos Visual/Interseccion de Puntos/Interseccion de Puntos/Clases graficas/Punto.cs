using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interseccion_de_Puntos
{
    public class Punto
    {
        private float x;

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y;

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public Punto(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Punto(){}

    }
}
