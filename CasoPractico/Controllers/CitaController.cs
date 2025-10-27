using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CasoPracticoBLL.Interfaces;
using CasoPracticoBLL.Dtos;

namespace Casopractico.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaServicio _citaServicio;
        private readonly IClienteServicio _clienteServicio;
        private readonly IVehiculoServicio _vehiculoServicio;

        public CitaController(ICitaServicio citaServicio,
                              IClienteServicio clienteServicio,
                              IVehiculoServicio vehiculoServicio)
        {
            _citaServicio = citaServicio;
            _clienteServicio = clienteServicio;
            _vehiculoServicio = vehiculoServicio;
        }

        // Lista de citas
        public async Task<IActionResult> Index()
        {
            var response = await _citaServicio.ListarDtoAsync();
            var citas = response.Data ?? new List<CitaDto>();

            if (response.EsError)
                TempData["Error"] = response.Mensaje;

            return View(citas);
        }

        // Crear cita
        public async Task<IActionResult> Crear()
        {
            await CargarClientesVehiculos();
            ViewData["Title"] = "Crear Cita";
            ViewData["Action"] = "Crear";
            return View(new CitaDto());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CitaDto dto)
        {
            await CargarClientesVehiculos(dto.ClienteId, dto.VehiculoId);
            ViewData["Title"] = "Crear Cita";
            ViewData["Action"] = "Crear";

            if (!ModelState.IsValid)
                return View(dto);

            var response = await _citaServicio.RegistrarAsync(dto);
            TempData["Mensaje"] = response.EsError ? response.Mensaje : "Cita registrada correctamente";

            return RedirectToAction(nameof(Index));
        }

        // Editar cita
        public async Task<IActionResult> Editar(int id)
        {
            var response = await _citaServicio.ObtenerPorIdAsync(id);
            if (response.EsError || response.Data == null)
            {
                TempData["Error"] = response.Mensaje ?? "Cita no encontrada";
                return RedirectToAction(nameof(Index));
            }

            var cita = response.Data;
            await CargarClientesVehiculos(cita.ClienteId, cita.VehiculoId);

            ViewData["Title"] = "Editar Cita";
            ViewData["Action"] = "Editar";
            return View(cita);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(CitaDto dto)
        {
            await CargarClientesVehiculos(dto.ClienteId, dto.VehiculoId);
            ViewData["Title"] = "Editar Cita";
            ViewData["Action"] = "Editar";

            if (!ModelState.IsValid)
                return View(dto);

            var response = await _citaServicio.CambiarEstadoAsync(dto.Id, dto.Estado);
            TempData["Mensaje"] = response.EsError ? response.Mensaje : "Estado actualizado";

            return RedirectToAction(nameof(Index));
        }

        // Eliminar cita
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = await _citaServicio.EliminarAsync(id);
            TempData["Mensaje"] = response.EsError ? response.Mensaje : "Cita eliminada correctamente";

            return RedirectToAction(nameof(Index));
        }

        // Helper: cargar clientes y vehículos para dropdowns
        private async Task CargarClientesVehiculos(int? selectedClienteId = null, int? selectedVehiculoId = null)
        {
            var clientesResponse = await _clienteServicio.ObtenerClientesAsync();
            var clientes = clientesResponse.Data ?? new List<ClienteDto>();
            ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre", selectedClienteId);

            var vehiculosResponse = await _vehiculoServicio.ObtenerVehiculosAsync();
            var vehiculos = vehiculosResponse.Data ?? new List<VehiculoDto>();
            ViewBag.Vehiculos = new SelectList(vehiculos, "Id", "Placa", selectedVehiculoId);
        }
    }
}
