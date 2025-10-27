using Microsoft.AspNetCore.Mvc;
using CasoPracticoBLL.Interfaces;
using CasoPracticoBLL.Dtos;

namespace Casopractico.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteServicio _clienteServicio;

        public ClienteController(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        public async Task<IActionResult> Index()
        {
            var respuesta = await _clienteServicio.ObtenerClientesAsync();
            if (!respuesta.EsError)
            {
                // Pasar la lista de clientes a la vista
                return View(respuesta.Data);
            }

            TempData["Error"] = respuesta.Mensaje;
            return View(new List<ClienteDto>());
        }

        public IActionResult Crear()
        {
            ViewData["Title"] = "Crear Cliente";
            ViewData["Action"] = "Crear";
            return View(new ClienteDto());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ClienteDto clienteDto)
        {
            if (!ModelState.IsValid) return View(clienteDto);

            // Validación de edad
            if (clienteDto.FechaNacimiento > DateTime.Now.AddYears(-18))
            {
                ModelState.AddModelError("", "El cliente debe ser mayor de edad");
                return View(clienteDto);
            }

            var resultado = await _clienteServicio.AgregarClienteAsync(clienteDto);
            TempData["Mensaje"] = resultado.Mensaje;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var respuesta = await _clienteServicio.ObtenerClientePorIdAsync(id);
            if (respuesta == null || respuesta.EsError) return NotFound();

            ViewData["Title"] = "Editar Cliente";
            ViewData["Action"] = "Editar";
            return View(respuesta.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ClienteDto clienteDto)
        {
            if (!ModelState.IsValid) return View(clienteDto);

            var resultado = await _clienteServicio.ActualizarClienteAsync(clienteDto);
            TempData["Mensaje"] = resultado.Mensaje;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _clienteServicio.EliminarClienteAsync(id);
            TempData["Mensaje"] = resultado.EsError ? "No se pudo eliminar el cliente" : "Cliente eliminado correctamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
