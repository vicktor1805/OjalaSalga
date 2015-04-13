using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Interseccion_de_Puntos
{
    public class Operacion
    {
        private readonly float EPS = (float)(1*(Math.Pow(10f,-7f)));
        private float[] Datos = new float[4];
        private Punto puntoInterseccion;
        private int IndicePantalla;

        public int IndicePantalla1
        {
            get { return IndicePantalla; }
            set { IndicePantalla = value; }
        }
        public Punto PuntoInterseccion
        {
            get { return puntoInterseccion; }
            set { puntoInterseccion = value; }
        }

        public void agregarDatos(float x,float y, float i, float j)
        {
            Datos[0] = x;
            Datos[1] = y;
            Datos[2] = i;
            Datos[3] = j;

        }
        public bool chocoPantalla(List<float[]> Pantallas)
        {
            puntoInterseccion = null;
            bool choco = false;
            bool dentro = false;
            float m = 0f;
            if(Datos[2] != 0)
                m =  Datos[3] / Datos[2];
            Punto datoA = new Punto(Datos[0],Datos[1]);
            Linea dato = PuntoPendienteALinea(datoA,m);
            foreach(float[] pantalla in Pantallas)
            {
                IndicePantalla1 = Pantallas.IndexOf(pantalla);
                Punto A = new Punto(pantalla[0],pantalla[1]);
                Punto B = new Punto(pantalla[2],pantalla[3]);
                Punto N = new Punto(pantalla[4],pantalla[5]);
                Linea pant = PuntoALinea(A,B);
                puntoInterseccion = Interseccion(pant, dato);
                if (puntoInterseccion != null)
                {
                    dentro = DentroDeLaPantalla(new Punto(pantalla[0], pantalla[1]), new Punto(pantalla[2], pantalla[3]));
                    if (dentro)
                    {
                        dentro = validarDireccionChoque(datoA, A, B, N);
                        break;
                    }
                }
            }

            if(dentro)
                choco = true;

            return choco;
        }

        public float[] ObtenerPuntoInterseccion()
        {
            if (puntoInterseccion == null)
                return null;

            float[] punto = new float[2];
            punto[0] = puntoInterseccion.X;
            punto[1] = puntoInterseccion.Y;

            return punto;
        }
        private Linea PuntoALinea(Punto punto1, Punto punto2)
        {
            Linea linea = new Linea();

            if (punto1.X == punto2.X)
            {
                linea.X = 1;
                linea.Y = 0;
                linea.Z = -punto1.X;
            }
            else
            {
                linea.X = -(punto1.Y - punto2.Y) / (punto1.X - punto2.X);
                linea.Y = 1;
                linea.Z = -(linea.X * punto1.X) - (linea.Y * punto1.Y);
            }

            return linea;
        }

        private Linea PuntoPendienteALinea(Punto p,float m)
        {
            Linea linea = new Linea();

            linea.X = -m;
            linea.Y = 1;
            linea.Z = -((linea.X*p.X)+(linea.Y*p.Y));

            return linea;
        }
        private bool SonParalelas(Linea lineaEstatica,Linea lineaDinamica)
        {
            return ((Math.Abs(lineaEstatica.X-lineaDinamica.X) <= EPS) && Math.Abs(lineaEstatica.Y-lineaDinamica.Y) <= EPS) ;
        }

        private bool SonIguales(Linea lineaEstatica, Linea lineaDinamica)
        {
            return (SonParalelas(lineaEstatica,lineaDinamica) && (Math.Abs(lineaEstatica.Z-lineaDinamica.Z) <= EPS));
        }

        public Punto Interseccion(Linea lineaEstatica,Linea lineaDinamica)
        {
            Punto p = new Punto();
            if (SonParalelas(lineaEstatica, lineaDinamica))
            {
                p = null;
            }
            else if (SonIguales(lineaEstatica, lineaDinamica))
            {
                p = null;
            }
            else
            {
                p.X = (lineaDinamica.Y * lineaEstatica.Z - lineaEstatica.Y * lineaDinamica.Z) / (lineaDinamica.X * lineaEstatica.Y - lineaEstatica.X * lineaDinamica.Y);
                if (Math.Abs(lineaEstatica.Y) > EPS)
                    p.Y = -(lineaEstatica.X * p.X + lineaEstatica.Z) / (lineaEstatica.Y);
                else
                    p.Y = -(lineaDinamica.X * p.X + lineaDinamica.Z) / (lineaDinamica.Y);
            }
            return p;
        }

        private bool DentroDeLaPantalla(Punto punto1, Punto punto2)
        {
            float Xmayor = 0;
            float Xmenor = 100;
            float Ymayor = 0;
            float Ymenor = 100;

            if (punto1.X > Xmayor)
                Xmayor = punto1.X;
            if (punto2.X > Xmayor)
                Xmayor = punto2.X;
            if (punto1.Y > Ymayor)
                Ymayor = punto1.Y;
            if (punto2.Y > Ymayor)
                Ymayor = punto2.Y;

            if (punto1.X < Xmenor)
                Xmenor = punto1.X;
            if (punto2.X < Xmenor)
                Xmenor = punto2.X;
            if (punto1.Y < Ymenor)
                Ymenor = punto1.Y;
            if (punto2.Y < Ymenor)
                Ymenor = punto2.Y;

            return ((puntoInterseccion.X<=Xmayor && puntoInterseccion.X>=Xmenor) && (puntoInterseccion.Y<=Ymayor && puntoInterseccion.Y>=Ymenor));


        }

        private bool validarDireccionChoque(Punto inicio,Punto A,Punto B,Punto Normal)
        {
            Punto puntoANuevo = new Punto(A.X - inicio.X, A.Y - inicio.Y);
            Punto puntoBNuevo = new Punto(B.X - inicio.X, B.Y - inicio.Y);
            Double hipotenusaA = Math.Sqrt(Math.Pow(puntoANuevo.X,2) + Math.Pow(puntoANuevo.Y,2));
            Double hipotenusaB = Math.Sqrt(Math.Pow(puntoBNuevo.X,2) + Math.Pow(puntoBNuevo.Y,2));
            Double anguloA = Math.Abs(Math.Asin(puntoANuevo.Y / hipotenusaA));
            Double anguloB = Math.Abs(Math.Asin(puntoBNuevo.Y / hipotenusaB));

            anguloA = anguloCorrecto(puntoANuevo,anguloA);
            anguloB = anguloCorrecto(puntoBNuevo, anguloB);

            return validaDireccion(Normal,anguloA,anguloB);
        }

        private int determinarCuadrante(Punto a)
        {
            int res = 0;
            if (a.X > EPS && a.Y > EPS)
                res = 1;
            else if (a.X < EPS && a.Y > EPS)
                res = 2;
            else if (a.X < EPS && a.Y < EPS)
                res = 3;
            else if (a.X > EPS && a.Y < EPS)
                res = 4;
            return res;
        }

        private double anguloCorrecto(Punto a,double angulo)
        {
            int cuadrante = determinarCuadrante(a);

            if (cuadrante == 1)
                return angulo;

            if (cuadrante == 2)
                return 180 - angulo;

            if (cuadrante == 3)
                return 180 + angulo;

            if (cuadrante == 4)
                return 360 - angulo;

            return 0;
        }

        private bool validaDireccion(Punto N,double A,double B)
        {
            bool res = false;

            int cuadrante = determinarCuadrante(N);
            if (cuadrante == 0)
                return false;

            double difAngulos = 0;

            if (cuadrante == 1 || cuadrante == 2)
                difAngulos = B - A;

            if (cuadrante == 3 || cuadrante == 4)
                difAngulos = A - B;

            if (difAngulos > 0)
                res = true;

            return res;
        }
    }
}
