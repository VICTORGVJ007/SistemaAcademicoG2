using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaAcademicoG2.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Controllers
{
    public class AsignaturaFController : Controller
    {
        private readonly HttpClient _httpClient;

        public AsignaturaFController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // GET: /AsignaturaMvc/
        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7206/api/asignatura";
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            var asignaturas = JsonConvert.DeserializeObject<List<Asignatura>>(json);
            return View(asignaturas);
        }

        // GET: /AsignaturaMvc/Detalles/5
        public async Task<IActionResult> Detalles(int id)
        {
            var url = $"https://localhost:7206/api/asignatura/{id}";
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            var asignatura = JsonConvert.DeserializeObject<Asignatura>(json);
            return View(asignatura);
        }

        // GET: /AsignaturaMvc/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /AsignaturaMvc/Crear
        [HttpPost]
        public async Task<IActionResult> Crear(Asignatura asignatura)
        {
            var url = "https://localhost:7182/api/asignatura";
            var response = await _httpClient.PostAsJsonAsync(url, asignatura);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error al guardar la asignatura");
            return View(asignatura);
        }

        // GET: /AsignaturaMvc/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var url = $"https://localhost:7182/api/asignatura/{id}";
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            var asignatura = JsonConvert.DeserializeObject<Asignatura>(json);
            return View(asignatura);
        }

        // POST: /AsignaturaMvc/Editar/5
        [HttpPost]
        public async Task<IActionResult> Editar(int id, Asignatura asignatura)
        {
            var url = $"https://localhost:7182/api/asignatura/{id}";
            var response = await _httpClient.PutAsJsonAsync(url, asignatura);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error al actualizar la asignatura");
            return View(asignatura);
        }
    }
}