// todo controlador usa dominio
using Dominio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Web.Controllers.Persona
{
    public class PersonaController : Controller
    {

        //En cada controlador que quiera llegar a sistema va:

        Sistema miSistema = Sistema.Instancia;
        public IActionResult Login()
        {
            return View();
        }
        //cada vez que trabajemos con formularios
        // van a existir dos metodos iguales
        // el primero en este caso Login() que lo que hace es mostrarlo
        // el segundo Login(correo,pass) que lo que hace es procesarla
        // pero solo existe una unica vista
        // hay que poner delante la forma en la que recibo la inforamcion [HttpPost]
        [HttpPost]
        public IActionResult Login(string mail, string pass)
        {

            //Utilicé bloques try-catch para manejar posibles errores durante la ejecución
            try
            {
                // para intentar loguearme llamo al metodo de sistema
                Dominio.Persona logueada = miSistema.Login(mail, pass);
                if (logueada != null)
                {
                    //me guardo en la sesion los datos del usuario logueado
                    HttpContext.Session.SetString("LogueadaNombre", logueada.Nombre);
                    HttpContext.Session.SetString("LogueadaRol", logueada.Rol.ToString());
                    HttpContext.Session.SetString("LogueadaCI", logueada.Ci);

                    //cuando el login es exitoso lo tiramos alguna parte ej el Home
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // ViewBag.Mensaje enviamos información desde el controlador hacia la vista
                    // Acá mostramos mensajes de error cuando ocurre una excepción
                    ViewBag.Mensaje = "Datos incorrectos";
                }
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(string Ci, string Nombre, string Mail, string Telefono, string Pass, TipoRol Rol)
        {
            try
            {
                //crear un objeto persona
                Dominio.Persona p = new Dominio.Persona(Ci, Nombre, Mail, Telefono, Pass, Rol);
                //agregar la persona a la lista de personas
                miSistema.AltaPersona(p);

                HttpContext.Session.SetString("LogueadaNombre", p.Nombre);
                HttpContext.Session.SetString("LogueadaRol", p.Rol.ToString());
                HttpContext.Session.SetString("LogueadaCI", p.Ci);

                return RedirectToAction("Index", "Home");
                //  return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
            }
            return View();
        }

        public IActionResult ListadoPersonas()
        {
            try
            {
                List<Dominio.Persona> personas = miSistema.ListadoDePersonas();
                return View(personas);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;

                // Retornamos una lista vacía para evitar errores en la vista
                // cuando se intenta mostrar el listado de personas.
                return View(new List<Dominio.Persona>());
            }
        }

        public IActionResult MisActivos()
        {
            try
            {
                string ci = HttpContext.Session.GetString("LogueadaCI");

                //barrera para el ingreso directo desde la url
                if (ci == null)
                {
                    return RedirectToAction("Login");
                }

                Dominio.Persona p = miSistema.BuscarPersona(ci);

                List<Dominio.Activo> activos = miSistema.ActivosPorPersona(p);

                return View(activos);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;

                // Retornamos una lista vacía para evitar que la vista reciba un modelo nulo
                // y falle al recorrerlo con foreach.
                return View(new List<Dominio.Activo>());
            }

        }

        public IActionResult VerPerfil()
        {
            try
            {
                string ci = HttpContext.Session.GetString("LogueadaCI");

                //barrera para el ingreso directo desde la url
                if (ci == null)
                {
                    return RedirectToAction("Login");
                }

                Dominio.Persona p = miSistema.BuscarPersona(ci);
                ViewBag.cuentas = miSistema.CuentasPorPersona(int.Parse(ci));

                return View(p);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;

                // Inicializamos una lista vacía para evitar errores al acceder
                // a ViewBag.cuentas en la vista.
                ViewBag.cuentas = new List<Dominio.Cuenta>();
            }
            return View();
        }
    }
}
