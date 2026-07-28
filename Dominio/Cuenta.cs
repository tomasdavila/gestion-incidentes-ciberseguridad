using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Cuenta : IComparable<Cuenta>
    {

        // Creamos atributos de la clase Cuenta

        private int _codU;
        static int s_ultCodU = 1;
        private Persona _titular;
        private bool _mfa;
        private DateTime _fechaPass;

        // Creamos propreties de la clase Cuenta


        public int CodU { get { return _codU; } set { _codU = value; } }

        public static int S_ultCodU { get { return s_ultCodU; } set { s_ultCodU = value; } }

        public Persona Titular { get { return _titular; } set { _titular = value; } }

        public bool Mfa { get { return _mfa; } set { _mfa = value; } }

        public DateTime FechaPass { get { return _fechaPass; } set { _fechaPass = value; } }


        // Definimos el constructor de la clase Cuenta

        public Cuenta() { }

        public Cuenta(Persona titular, bool mfa, DateTime fechaPass)
        {
            _codU = s_ultCodU++;
            _titular = titular;
            _mfa = mfa;
            _fechaPass = fechaPass;
        }

        // Validamos que los campos no esten vacios

        public void ValidarCuenta()
        {
            if (string.IsNullOrEmpty(_codU.ToString()) && string.IsNullOrEmpty(_titular.Nombre) && string.IsNullOrEmpty(_titular.Ci) && string.IsNullOrEmpty(_titular.Mail)) throw new Exception("Los campos no pueden estar vacios");
        }

        public int CompareTo(Cuenta c)
        {
            return Titular.CompareTo(c.Titular);
        }

    }
}
