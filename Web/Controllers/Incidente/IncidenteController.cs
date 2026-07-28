using Dominio;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Incidente
{
    public class IncidenteController : Controller
    {
        Sistema miSistema = Sistema.Instancia;
        public IActionResult ListadoIncidentes()
        {
            try
            {
                List<Dominio.Incidente> incidentes = miSistema.ListadoDeIncidentes();
                return View(incidentes);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                // lista vacia
                return View(new List<Dominio.Incidente>());
            }
        }
    }
}
