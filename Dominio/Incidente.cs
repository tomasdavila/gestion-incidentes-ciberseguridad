using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public abstract class Incidente : IComparable<Incidente>
    {

        // Creamos atributos de la clase Incidente

        private int _id;
        static int s_ultId = 1;
        private DateTime _fechaRep;
        private Activo _activoAfectado;
        private string _descripcion;
        private Estado _estado;
        private int _impacto;
        private int _probabilidad;


        // Creamos propreties de la clase Incidente

        public int Id { get { return _id; } set { _id = value; } }

        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }


        public DateTime FechaRep { get { return _fechaRep; } set { _fechaRep = value; } }

        public Activo ActivoAfectado { get { return _activoAfectado; } set { _activoAfectado = value; } }

        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public int Impacto { get { return _impacto; } set { _impacto = value; } }

        public int Probabilidad { get { return _probabilidad; } set { _probabilidad = value; } }


        // Definimos el constructor de la clase Incidente

        public Incidente() { }

        public Incidente(DateTime fechaRep, Activo activoAfectado, string descripcion, Estado estado, int impacto, int probabilidad)
        {
            _id = s_ultId++;
            _fechaRep = fechaRep;
            _activoAfectado = activoAfectado;
            _descripcion = descripcion;
            _estado = estado;
            _impacto = impacto;
            _probabilidad = probabilidad;
        }

        public Persona ObtenerPersona()
        {
            if (_activoAfectado.Cuenta == null)
            {
                return null;
            }
            return _activoAfectado.Cuenta.Titular;
        }

        public void ValidarIncidente()
        {
            //Validamos que los campos no esten vacios
            if (_fechaRep == DateTime.MinValue) throw new Exception("La fecha de reporte no puede estar vacia");

            if (string.IsNullOrEmpty(_descripcion)) throw new Exception("la descripcion no puede ser nulo");

            // Validamos que el impacto sea un valor entre 1 y 5
            if (_impacto < 1 || _impacto > 5) throw new Exception("El impacto debe ser un valor entre 1 y 5");
            // Validamos que la probabilidad sea un valor entre 1 y 5
            if (_probabilidad < 1 || _probabilidad > 5) throw new Exception("La probabilidad debe ser un valor entre 1 y 5");
        }

        public virtual int CalculoSeveridad()
        {
            int severidad = (_impacto * 12) + (_probabilidad * 8);

            if (_activoAfectado.TieneBackUp == true)
            {
                severidad -= 15;
            }

            if (severidad >= 100)
            {
                severidad = 100;
            }
            return severidad;
        }

        public int CompareTo(Incidente i)
        {
           return -CalculoSeveridad().CompareTo(i.CalculoSeveridad());
        }

    }
}
