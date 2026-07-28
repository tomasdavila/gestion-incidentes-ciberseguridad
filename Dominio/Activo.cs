using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Activo : IComparable<Activo>
    {
        // Creamos atributos de la clase Activo

        private string _codA;
        static int s_ultCodA = 1;
        private string _nombre;
        private TipoActivo _tipoActivo;
        private int _criticidad;
        private Cuenta _cuenta;
        private bool _tieneBackUp;
        private string _prefijo = "AC";



        // Creamos propreties de la clase Activo

        public string CodA { get { return _codA; } set { _codA = value; } }

        public string Nombre { get { return _nombre; } set { _nombre = value; } }

        public TipoActivo TipoActivo
        {
            get { return _tipoActivo; }
            set { _tipoActivo = value; }
        }

        public int Criticidad { get { return _criticidad; } set { _criticidad = value; } }

        public Cuenta Cuenta { get { return _cuenta; } set { _cuenta = value; } }

        public bool TieneBackUp { get { return _tieneBackUp; } set { _tieneBackUp = value; } }


        // Definimos el constructor de la clase Activo

        public Activo() { }


        public Activo(string nombre, TipoActivo tipoActivo, int criticidad, Cuenta cuenta, bool tieneBackUp)
        {
            _codA = _prefijo + s_ultCodA++.ToString();
            _nombre = nombre;
            _tipoActivo = tipoActivo;
            _criticidad = criticidad;
            _cuenta = cuenta;
            _tieneBackUp = tieneBackUp;
        }


        public void ValidarActivo()
        {

            // Validamos que los campos no esten vacios
            if (string.IsNullOrEmpty(_codA) && string.IsNullOrEmpty(_nombre)) throw new Exception("Los campos no pueden estar vacios");

            // Validar que la criticidad sea un valor entre 1 y 5
            if (_criticidad < 1 || _criticidad > 5) throw new Exception("La criticidad debe ser un valor entre 1 y 5");

        }

        public int CompareTo(Activo otro)
        {
            return CodA.CompareTo(otro.CodA);
        }

    }
}
