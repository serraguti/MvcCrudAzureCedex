using Microsoft.AspNetCore.Mvc;
using MvcCrudAzureCedex.Models;
using MvcCrudAzureCedex.Services;

namespace MvcCrudAzureCedex.Controllers
{
    public class DepartamentosController : Controller
    {
        private ServiceApiDepartamentos service;

        public DepartamentosController(ServiceApiDepartamentos service)
        {
            this.service = service;
        }

        public async Task<IActionResult> DepartamentosServidor()
        {
            List<Departamento> departamentos = await this.service.GetDepartamentosAsync();
            return View(departamentos);
        }

        public IActionResult DepartamentosCliente()
        {
            return View();
        }
    }
}
