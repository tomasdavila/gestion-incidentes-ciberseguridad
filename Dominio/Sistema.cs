using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Sistema
    {
        // Patrón Singleton para que SIEMPRE exista un único sistema

        private static Sistema _instancia;

        public static Sistema Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new Sistema();
                return _instancia;
            }
        }


        // Creamos las listas

        private List<Persona> _personas = new List<Persona>();
        private List<Activo> _activos = new List<Activo>();
        private List<Cuenta> _cuentas = new List<Cuenta>();
        private List<Incidente> _incidentes = new List<Incidente>();


        // Definimos las properties de las listas

        public List<Persona> Personas { get { return new List<Persona>(_personas); } }
        public List<Activo> Activos { get { return new List<Activo>(_activos); } }
        public List<Cuenta> Cuentas { get { return new List<Cuenta>(_cuentas); } }
        public List<Incidente> Incidentes { get { return new List<Incidente>(_incidentes); } }

        // Definimos el constructor de la clase Sistema 

        public Sistema()
        {
            _personas = new List<Persona>();
            _cuentas = new List<Cuenta>();
            _activos = new List<Activo>();
            _incidentes = new List<Incidente>();

            PrecargarPersonas();
            PrecargarCuentas();
            PrecargarActivos();
            PrecargarIncidentes();
        }

        public List<Activo> ActivosPorPersona(Persona p)
        {
            List<Activo> resultado = new List<Activo>();

            foreach (Activo a in _activos)
            {
                if (AsociadoPersona(a, p))
                {
                    resultado.Add(a);
                }
            }

            resultado.Sort();

            return resultado;
        }
        public bool AsociadoPersona(Activo ac, Persona pe)
        {
            foreach (Activo a in _activos)
            {
                if (a == ac)
                {
                    Cuenta cuenta = a.Cuenta;          // tomo la cuenta del activo

                    if (cuenta != null &&  cuenta.Titular == pe)           // si el titular es la persona
                    {
                        return true;
                    }
                }
            }
            return false;
        }



        public List<Incidente> IncidentesPorPersona(Persona p)
        {
            List<Incidente> resultado = new List<Incidente>();

            foreach (Incidente i in _incidentes)
            {
                if (i.ObtenerPersona() == p)
                {
                    resultado.Add(i);
                }
            }
            return resultado;
        }



        public List<Activo> ActivoSinBackUp()
        {

            List<Activo> resultado = new List<Activo>();

            foreach (Activo ac in _activos)
            {
                if (ac.TieneBackUp == false)
                {
                    resultado.Add(ac);
                }
            }
            return resultado;
        }

        public void AltaPersona(Persona p)
        {

            if (ExisteCedula(p.Ci)) throw new Exception("La cedula ya existe en el sistema");

            p.ValidarPersona();

            _personas.Add(p);

        }

        public void AltaActivo(Activo a)
        {

            a.ValidarActivo();

            _activos.Add(a);
        }

        public void AltaCuenta(Cuenta c)
        {

            c.ValidarCuenta();
            _cuentas.Add(c);
        }

        public void AltaIncidente(Incidente i)
        {

            i.ValidarIncidente();

            _incidentes.Add(i);
        }

        public bool ExisteCedula(string ci)
        {

            // Validamos que la cedula no exista en el sistema
            foreach (Persona p in _personas)
            {
                if (p.Ci == ci) return true;
            }
            return false;
        }

        public Persona BuscarPersona(string ci)
        {
            foreach (Persona p in _personas)
            {
                if (p.Ci == ci)
                    return p;
            }
            throw new Exception("Ci no existe");
        }

        public Cuenta BuscarCuenta(int cod)
        {
            foreach (Cuenta c in _cuentas)
            {
                if (c.CodU == cod) return c;
            }
            throw new Exception("No existe Cuenta");
        }

        public List<Cuenta> CuentasPorPersona(int id)
        {
            Persona p = BuscarPersona(id.ToString());
            List<Cuenta> cuentas = new List<Cuenta>();

            foreach (Cuenta c in _cuentas)
            {
                if (p == c.Titular)
                {
                    cuentas.Add(c);
                }
            }

            return cuentas;
        }

        public List<Activo> ActivosPorCuenta(int id)
        {
            Cuenta c = BuscarCuenta(id);
            List<Activo> activos = new List<Activo>();

            foreach (Activo a in _activos)
            {
                if (a.Cuenta != null && a.Cuenta == c)
                {
                    activos.Add(a);
                }
            }
            return activos;
        }


        public Activo BuscarActivo(string codA)
        {
            foreach (Activo a in _activos)
            {
                if (a.CodA == codA) return a;
            }
            throw new Exception("No existe activo");
        }

        // Precarga de datos

        private void PrecargarPersonas()
        {
            AltaPersona(new Persona("11111111", "Juan Pérez", "juan.perez@gmail.com", "099111111", "Juan123", TipoRol.ADMIN));
            AltaPersona(new Persona("22222222", "María González", "maria.gonzalez@hotmail.com", "099222222", "Maria123", TipoRol.OPERADOR));
            AltaPersona(new Persona("33333333", "Carlos Rodríguez", "carlos.rodriguez@outlook.com", "099333333", "Carlos123", TipoRol.OPERADOR));
            AltaPersona(new Persona("44444444", "Lucía Martínez", "lucia.martinez@gmail.com", "099444444", "Lucia123", TipoRol.OPERADOR));
            AltaPersona(new Persona("55555555", "Diego Fernández", "diego.fernandez@yahoo.com", "099555555", "Diego123", TipoRol.OPERADOR));
            AltaPersona(new Persona("66666666", "Sofía López", "sofia.lopez@gmail.com", "099666666", "Sofia123", TipoRol.OPERADOR));
            AltaPersona(new Persona("77777777", "Martín Silva", "martin.silva@empresa.com.uy", "099777777", "Martin123", TipoRol.ADMIN));
            AltaPersona(new Persona("88888888", "Valentina Castro", "valentina.castro@gmail.com", "099888888", "Valentina123", TipoRol.OPERADOR));
            AltaPersona(new Persona("99999999", "Andrés Morales", "andres.morales@outlook.com", "099999999", "Andres123", TipoRol.OPERADOR));
            AltaPersona(new Persona("10101010", "Camila Romero", "camila.romero@gmail.com", "099101010", "Camila123", TipoRol.ADMIN));
        }

        private void PrecargarCuentas()
        {
            AltaCuenta(new Cuenta(BuscarPersona("11111111"), true, new DateTime(2026, 1, 15)));
            AltaCuenta(new Cuenta(BuscarPersona("11111111"), false, new DateTime(2025, 11, 3)));
            AltaCuenta(new Cuenta(BuscarPersona("22222222"), true, new DateTime(2026, 2, 20)));
            AltaCuenta(new Cuenta(BuscarPersona("33333333"), true, new DateTime(2026, 3, 1)));
            AltaCuenta(new Cuenta(BuscarPersona("44444444"), false, new DateTime(2025, 12, 10)));
            AltaCuenta(new Cuenta(BuscarPersona("55555555"), true, new DateTime(2026, 4, 5)));
            AltaCuenta(new Cuenta(BuscarPersona("66666666"), true, new DateTime(2026, 1, 28)));
            AltaCuenta(new Cuenta(BuscarPersona("77777777"), false, new DateTime(2025, 9, 15)));
            AltaCuenta(new Cuenta(BuscarPersona("77777777"), true, new DateTime(2026, 3, 22)));
            AltaCuenta(new Cuenta(BuscarPersona("88888888"), true, new DateTime(2026, 2, 8)));
            AltaCuenta(new Cuenta(BuscarPersona("99999999"), false, new DateTime(2025, 10, 30)));
            AltaCuenta(new Cuenta(BuscarPersona("10101010"), true, new DateTime(2026, 4, 12)));
        }

        private void PrecargarActivos()
        {
            AltaActivo(new Activo("Notebook RRHH", TipoActivo.PC, 3, BuscarCuenta(1), true));
            AltaActivo(new Activo("PC Contabilidad", TipoActivo.PC, 4, BuscarCuenta(2), true));
            AltaActivo(new Activo("PC Recepción", TipoActivo.PC, 2, BuscarCuenta(3), false));
            AltaActivo(new Activo("Notebook Gerencia", TipoActivo.PC, 5, BuscarCuenta(4), true));
            AltaActivo(new Activo("PC Diseño", TipoActivo.PC, 3, BuscarCuenta(5), false));
            AltaActivo(new Activo("Servidor Web", TipoActivo.SERVER, 5, BuscarCuenta(6), true));
            AltaActivo(new Activo("Servidor BD", TipoActivo.SERVER, 5, BuscarCuenta(7), true));
            AltaActivo(new Activo("Servidor Mail", TipoActivo.SERVER, 4, BuscarCuenta(8), true));
            AltaActivo(new Activo("Servidor Backup", TipoActivo.SERVER, 4, BuscarCuenta(9), false));
            AltaActivo(new Activo("Servidor Aplicaciones", TipoActivo.SERVER, 5, BuscarCuenta(10), true));
            AltaActivo(new Activo("iPhone Gerencia", TipoActivo.MOVIL, 4, BuscarCuenta(4), false));
            AltaActivo(new Activo("Samsung Ventas", TipoActivo.MOVIL, 3, BuscarCuenta(5), false));
            AltaActivo(new Activo("Tablet Soporte", TipoActivo.MOVIL, 2, BuscarCuenta(11), false));
            AltaActivo(new Activo("iPhone Dirección", TipoActivo.MOVIL, 5, BuscarCuenta(12), true));
            AltaActivo(new Activo("Samsung Logística", TipoActivo.MOVIL, 3, BuscarCuenta(7), false));
        }

        private void PrecargarIncidentes()
        {
            // PHISHING
            AltaIncidente(new Phising(new DateTime(2026, 1, 5), BuscarActivo("AC1"), "Mail sospechoso con link malicioso", Estado.CERRADO, 2, 3, "Email", true, false));
            AltaIncidente(new Phising(new DateTime(2026, 1, 12), BuscarActivo("AC2"), "Intento de phishing bancario", Estado.CONTENIDO, 4, 4, "Email", true, true));
            AltaIncidente(new Phising(new DateTime(2026, 1, 20), BuscarActivo("AC3"), "Llamada haciéndose pasar por IT", Estado.ABIERTO, 3, 2, "Teléfono", false, false));
            AltaIncidente(new Phising(new DateTime(2026, 2, 1), BuscarActivo("AC4"), "WhatsApp con link fraudulento", Estado.EN_ANALISIS, 5, 4, "WhatsApp", true, true));
            AltaIncidente(new Phising(new DateTime(2026, 2, 8), BuscarActivo("AC5"), "Link malicioso en redes sociales", Estado.CERRADO, 2, 2, "RRSS", false, false));
            AltaIncidente(new Phising(new DateTime(2026, 2, 15), BuscarActivo("AC6"), "Intento de phishing al administrador", Estado.CONTENIDO, 5, 3, "Email", true, false));
            AltaIncidente(new Phising(new DateTime(2026, 2, 22), BuscarActivo("AC7"), "Mail con adjunto malicioso", Estado.ABIERTO, 4, 4, "Email", false, true));
            AltaIncidente(new Phising(new DateTime(2026, 3, 1), BuscarActivo("AC8"), "Formulario falso de actualización de clave", Estado.CERRADO, 3, 3, "Email", true, false));
            AltaIncidente(new Phising(new DateTime(2026, 3, 7), BuscarActivo("AC9"), "Página falsa de login corporativo", Estado.EN_ANALISIS, 4, 5, "Email", true, true));
            AltaIncidente(new Phising(new DateTime(2026, 3, 14), BuscarActivo("AC10"), "Spear phishing dirigido a gerencia", Estado.CONTENIDO, 3, 3, "Email", true, true));
            AltaIncidente(new Phising(new DateTime(2026, 3, 20), BuscarActivo("AC11"), "SMS con link fraudulento", Estado.ABIERTO, 2, 4, "SMS", true, false));
            AltaIncidente(new Phising(new DateTime(2026, 3, 28), BuscarActivo("AC12"), "App maliciosa que robó credenciales", Estado.CERRADO, 4, 3, "App", true, false));
            AltaIncidente(new Phising(new DateTime(2026, 4, 3), BuscarActivo("AC13"), "WhatsApp solicitando credenciales", Estado.EN_ANALISIS, 3, 4, "WhatsApp", true, false));
            AltaIncidente(new Phising(new DateTime(2026, 4, 10), BuscarActivo("AC14"), "Llamada solicitando código MFA", Estado.CONTENIDO, 3, 3, "Teléfono", true, false));
            AltaIncidente(new Phising(new DateTime(2026, 4, 18), BuscarActivo("AC15"), "Estafa por redes sociales con robo de datos", Estado.ABIERTO, 2, 2, "RRSS", false, true));

            // RANSOMWARE
            AltaIncidente(new Ramsonware(new DateTime(2026, 1, 8), BuscarActivo("AC1"), "Cifrado de archivos por ransomware", Estado.CONTENIDO, 5, 4, true, false));
            AltaIncidente(new Ramsonware(new DateTime(2026, 1, 15), BuscarActivo("AC2"), "Infección por USB, cifrado masivo", Estado.EN_ANALISIS, 5, 5, true, true));
            AltaIncidente(new Ramsonware(new DateTime(2026, 1, 25), BuscarActivo("AC3"), "Malware cifrador detectado en correo", Estado.CERRADO, 4, 3, true, false));
            AltaIncidente(new Ramsonware(new DateTime(2026, 2, 3), BuscarActivo("AC4"), "Cifrado de archivos locales por LockBit", Estado.CONTENIDO, 3, 4, true, true));
            AltaIncidente(new Ramsonware(new DateTime(2026, 2, 10), BuscarActivo("AC5"), "Intento de cifrado bloqueado por antivirus", Estado.ABIERTO, 5, 5, false, false));
            AltaIncidente(new Ramsonware(new DateTime(2026, 2, 17), BuscarActivo("AC6"), "Ataque tipo LockBit al servidor web", Estado.CERRADO, 3, 2, true, false));
            AltaIncidente(new Ramsonware(new DateTime(2026, 2, 25), BuscarActivo("AC7"), "Cifrado parcial del servidor de BD", Estado.CONTENIDO, 2, 3, true, false));
            AltaIncidente(new Ramsonware(new DateTime(2026, 3, 4), BuscarActivo("AC8"), "Exfiltración y cifrado de base de datos", Estado.EN_ANALISIS, 4, 4, true, true));
            AltaIncidente(new Ramsonware(new DateTime(2026, 3, 11), BuscarActivo("AC9"), "Intento de ransomware bloqueado en backup", Estado.CERRADO, 2, 2, false, false));
            AltaIncidente(new Ramsonware(new DateTime(2026, 3, 18), BuscarActivo("AC10"), "Cifrado en servidor de aplicaciones", Estado.ABIERTO, 3, 3, true, true));
            AltaIncidente(new Ramsonware(new DateTime(2026, 3, 25), BuscarActivo("AC11"), "Ransomware en dispositivo móvil Android", Estado.EN_ANALISIS, 5, 4, true, false));
            AltaIncidente(new Ramsonware(new DateTime(2026, 4, 1), BuscarActivo("AC12"), "App comprometida cifrando almacenamiento", Estado.CONTENIDO, 4, 3, true, true));
            AltaIncidente(new Ramsonware(new DateTime(2026, 4, 8), BuscarActivo("AC13"), "Robo y cifrado de credenciales del navegador", Estado.CERRADO, 3, 4, true, false));
            AltaIncidente(new Ramsonware(new DateTime(2026, 4, 15), BuscarActivo("AC14"), "Acceso no autorizado con cifrado posterior", Estado.ABIERTO, 2, 5, true, true));
            AltaIncidente(new Ramsonware(new DateTime(2026, 4, 22), BuscarActivo("AC15"), "Aplicación falsa con payload de ransomware", Estado.EN_ANALISIS, 4, 4, false, true));
        }

        public Persona Login(string correo, string pass)
        {
            foreach (Persona p in _personas)
            {
                if (p.Mail == correo && p.Pass == pass) return p;
            }
            return null;
        }

        public List<Persona> ListadoDePersonas()
        {
            // esto hace que la lista se orden por el criterio
            // definido en el CompareTo
            List<Persona> listaPersona = Personas;

           //ordenamos la lista, el criterio lo definimos en la clase
            listaPersona.Sort();
            return listaPersona;
        }

        public List<Persona> ListadoDeOperadores()
        {
     
            List<Persona> resultados = new List<Persona>();
            foreach (Persona p in _personas)
            {
                if (p.Rol == TipoRol.OPERADOR)
                {
                    resultados.Add(p);
                }
            }

            // Ordenamos la lista según el criterio definido
            // en el método CompareTo de la clase Persona.
            resultados.Sort();
            return resultados;
        }
        public List<Incidente> ListadoDeIncidentes()
        {
            List<Incidente> listaIncidente = Incidentes;
            listaIncidente.Sort();
            return listaIncidente;
        }

        public List<Cuenta> ListadoDeCuentas()
        {

            List<Cuenta> listaCuenta = Cuentas;
            listaCuenta.Sort();
            return listaCuenta;
        }

        public List<Activo> ListadoDeActivos()
        {
            List<Activo> listaActivo = Activos;
            listaActivo.Sort();
            return listaActivo;
        }

        public void DesvincularActivo(string codA)
        {
            Activo a = BuscarActivo(codA);
            a.Cuenta = null;
        }
    }
}
