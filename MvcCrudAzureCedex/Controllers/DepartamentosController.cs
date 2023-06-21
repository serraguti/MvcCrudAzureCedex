using Microsoft.AspNetCore.Mvc;

namespace MvcCrudAzureCedex.Controllers
{
    public class DepartamentosController : Controller
    {
        public IActionResult DepartamentosCliente()
        {
            return View();
        }
    }
}
