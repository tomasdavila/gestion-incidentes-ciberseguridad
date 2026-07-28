using Dominio;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Web.Controllers.Cuenta
{
    public class CuentaController : Controller
    {
        Sistema miSistema = Sistema.Instancia;
        public IActionResult ListadoCuentas()
        {
            try
            {
                List<Dominio.Cuenta> cuentas = miSistema.ListadoDeCuentas();
                return View(cuentas);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                // lista vacía para evitar errores al mostrar el listado de cuentas.
                return View(new List<Dominio.Cuenta>());
            }
        }

        public IActionResult VerOperadores()
        {
            try
            {
                List<Dominio.Persona> operadores = miSistema.ListadoDeOperadores();
                return View(operadores);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                // lista vacía para evitar errores al mostrar el listado de operadores.
                return View(new List<Dominio.Persona>());
            }
        }

        public IActionResult CrearCuenta(int id)
        {
            // buscamos la persona y la mandamos a la vista
            try
            {
                Dominio.Persona titular = miSistema.BuscarPersona(id.ToString());
                return View(titular);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }

        [HttpPost]

        //metodo se ejecuta cuando creamos la cuenta,  HttpPost es la forma que recibo la inforamcion
        public IActionResult CrearCuenta(int idUsu, bool mfa, DateTime fecha)
        {
            try
            {
                Dominio.Persona titular = miSistema.BuscarPersona(idUsu.ToString());

                Dominio.Cuenta c = new Dominio.Cuenta(titular, mfa, fecha);
                miSistema.AltaCuenta(c);

                return RedirectToAction("VerCuentas", new { id = titular.Ci });
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }

        public IActionResult VerCuentas(int id)
        {
            try
            {
                List<Dominio.Cuenta> cuentas = miSistema.CuentasPorPersona(id);

                return View(cuentas);
            }
            catch (Exception e)
            {

                ViewBag.Mensaje = e.Message;
                // lista vacía
                return View(new List<Dominio.Cuenta>());
            }
        }
    }
}
