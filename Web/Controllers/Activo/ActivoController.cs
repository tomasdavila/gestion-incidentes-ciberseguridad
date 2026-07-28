using Dominio;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace Web.Controllers.Activo
{
    public class ActivoController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        public IActionResult VerActivos(int id)
        {
            try
            {
                List<Dominio.Activo> activos = miSistema.ActivosPorCuenta(id);
                return View(activos);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;

                // lista vacia para evitar errores cuando se intenta mostrar el listado de activos.
                return View(new List<Dominio.Activo>());
            }
        }

        public IActionResult CrearActivo(int id)
        {
            try
            {
                Dominio.Cuenta cuenta = miSistema.BuscarCuenta(id);
                return View(cuenta);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }

        [HttpPost]

        public IActionResult CrearActivo(int idC, string nombre, TipoActivo tipoActivo, int criticidad, bool tieneBackUp)
        {
            try
            {
                Dominio.Cuenta cuenta = miSistema.BuscarCuenta(idC);

                Dominio.Activo a = new Dominio.Activo(nombre, tipoActivo, criticidad, cuenta, tieneBackUp);
                miSistema.AltaActivo(a);

                return RedirectToAction("VerActivos", new { id = cuenta.CodU });
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }
        public IActionResult DesvincularActivo(string idActivo, int idCuenta)
        {
            try
            {
                // Desvinculamos el activo de su cuenta.
                miSistema.DesvincularActivo(idActivo);

                return RedirectToAction("VerActivos", new { id = idCuenta });
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;

                return RedirectToAction("VerActivos", new { id = idCuenta });
            }
        }
    }
}
