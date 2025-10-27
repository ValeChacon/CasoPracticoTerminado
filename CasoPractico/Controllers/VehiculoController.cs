using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CasoPracticoBLL.Interfaces;
using CasoPracticoBLL.Dtos;

namespace Casopractico.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly IVehiculoServicio _vehiculoServicio;
        private readonly IClienteServicio _clienteServicio;

        public VehiculoController(IVehiculoServicio vehiculoServicio, IClienteServicio clienteServicio)
        {
            _vehiculoServicio = vehiculoServicio;
            _clienteServicio = clienteServicio;
        }

        // Lista de veh�culos
        public async Task<IActionResult> Index()
        {
            var response = await _vehiculoServicio.ObtenerVehiculosAsync();
            var vehiculos = response.Data ?? new List<VehiculoDto>();

            if (response.EsError)
                TempData["Error"] = response.Mensaje;

            return View(vehiculos);
        }

        // Crear veh�culo
        public async Task<IActionResult> Crear()
        {
            await CargarClientesDropdown();
            ViewData["Title"] = "Crear Veh�culo";
            ViewData["Action"] = "Crear";
            return View(new VehiculoDto());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(VehiculoDto dto)
        {
            await CargarClientesDropdown(dto.ClienteId);
            ViewData["Title"] = "Crear Veh�culo";
            ViewData["Action"] = "Crear";

            if (!ModelState.IsValid)
                return View(dto);

            var response = await _vehiculoServicio.AgregarVehiculoAsync(dto);
            TempData["Mensaje"] = response.EsError ? response.Mensaje : "Veh�culo registrado correctamente";

            return RedirectToAction(nameof(Index));
        }

        // Editar veh�culo
        public async Task<IActionResult> Editar(int id)
        {
            var response = await _vehiculoServicio.ObtenerVehiculoPorIdAsync(id);
            if (response.EsError || response.Data == null)
            {
                TempData["Error"] = response.Mensaje ?? "Veh�culo no encontrado";
                return RedirectToAction(nameof(Index));
            }

            await CargarClientesDropdown(response.Data.ClienteId);
            ViewData["Title"] = "Editar Veh�culo";
            ViewData["Action"] = "Editar";
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(VehiculoDto dto)
        {
            await CargarClientesDropdown(dto.ClienteId);
            ViewData["Title"] = "Editar Veh�culo";
            ViewData["Action"] = "Editar";

            if (!ModelState.IsValid)
                return View(dto);

            var response = await _vehiculoServicio.ActualizarVehiculoAsync(dto);
            TempData["Mensaje"] = response.EsError ? response.Mensaje : "Veh�culo actualizado correctamente";

            return RedirectToAction(nameof(Index));
        }

        // Eliminar veh�culo
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = await _vehiculoServicio.EliminarVehiculoAsync(id);
            TempData["Mensaje"] = response.EsError ? response.Mensaje : "Veh�culo eliminado correctamente";

            return RedirectToAction(nameof(Index));
        }

        // Helper: cargar clientes para dropdown
        private async Task CargarClientesDropdown(int? selectedClienteId = null)
        {
            var clientesResponse = await _clienteServicio.ObtenerClientesAsync();
            var clientes = clientesResponse.Data ?? new List<ClienteDto>();

            ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre", selectedClienteId);
        }
    }
}
