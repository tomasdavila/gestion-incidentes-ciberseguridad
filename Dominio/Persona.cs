using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{

    // 1- para ordenar: suscribirse al IComparable
    public class Persona : IComparable<Persona>
    {

        // Creamos atributos de la clase Persona

        private string _ci;
        private string _nombre;
        private string _mail;
        private string _telefono;
        private string _pass;
        public TipoRol _rol;

        // Creamos propreties de la clase Persona

        public string Ci { get { return _ci; } set { _ci = value; } }

        public string Nombre { get { return _nombre; } set { _nombre = value; } }

        public string Mail { get { return _mail; } set { _mail = value; } }

        public string Telefono { get { return _telefono; } set { _telefono = value; } }

        public string Pass { get { return _pass; } set { _pass = value; } }

        public TipoRol Rol { get { return _rol; } set { _rol = value; } }



        // Definimos el constructor de la clase Persona

        public Persona() { }

        public Persona(string ci, string nombre, string mail, string telefono, string pass, TipoRol rol)
        {
            _ci = ci;
            _nombre = nombre;
            _mail = mail;
            _telefono = telefono;
            _pass = pass;
            _rol = rol;
        }



        public void ValidarPersona()
        {
            // Validamos que los campos no esten vacios
            if (string.IsNullOrEmpty(_ci) || string.IsNullOrEmpty(_nombre) || string.IsNullOrEmpty(_mail)) throw new Exception("Los campos no pueden estar vacios");

            // Validamos que sean numeros y el largo adecuado

            if (!_telefono.All(char.IsDigit) || _telefono.Length != 9) throw new Exception("Telefono invalido");

            if (!_ci.All(char.IsDigit) || _ci.Length != 8) throw new Exception("Cedula invalida");

            // Validamos que el mail contenga @
            if (!_mail.Contains("@")) throw new Exception("El mail no es valido");

        }

        // 2 - si o si hay que hacer el CompareTo
        public int CompareTo(Persona otra)
        {
            // Nombre en este caso es el campo el cual quiero ordenar
            //ASCENDENTE
            return Nombre.CompareTo(otra.Nombre);
            // DESCENDENTE
            //  return -Nombre.CompareTo(otra.Nombre);
        }
    }
}
