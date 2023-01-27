using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase12
{
    internal class Mascota
    {
        private string nombre;
        private int edad;

        public Mascota(string nombre, int edad)
        {
            this.Nombre = nombre;
            this.Edad = edad;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Edad { get => edad; set => edad = value; }

        public override string ToString()
        {
            return $"Nombre: {nombre}\tEdad:{edad}";
        }
    }
}
