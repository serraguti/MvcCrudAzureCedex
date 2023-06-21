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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Departamento departamento)
        {
            await this.service.CreateDepartamentoAsync
                (departamento.Nombre, departamento.Localidad);
            //AL INSERTAR, VAMOS A DEVOLVERLO A LA VISTA DONDE ESTA 
            //EL DIBUJO DE TODOS LOS DEPARTAMENTOS
            return RedirectToAction("DepartamentosServidor");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Departamento departamento =
                await this.service.FindDepartamentoAsync(id);
            return View(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Departamento departamento)
        {
            await this.service.UpdateDepartamentoAsync
                (departamento.IdDepartamento, departamento.Nombre, departamento.Localidad);
            return RedirectToAction("DepartamentosServidor");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteDepartamentoAsync(id);
            return RedirectToAction("DepartamentosServidor");
        }

        public IActionResult DepartamentosCliente()
        {
            return View();
        }
    }
}
