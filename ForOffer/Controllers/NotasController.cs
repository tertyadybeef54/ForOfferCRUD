using ForOffer.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForOffer.Controllers
{
    public class NotasController : Controller
    {
        readonly ForOfferContext _db = new();
        public NotasController(ForOfferContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var lst = _db.MfGeneralNotesOffers.Include(x=>x.GeneralNotesServices).ToList();
            return View(lst);
        }

        // GET: notas/Create
        public IActionResult Crear()
        {
            var nota = new MfGeneralNotesOffer();
            ViewBag.places = (from d in _db.MfServicesPlaces
                              select new SelectListItem
                              {
                                  Value = d.ServicesPlaceId.ToString(),
                                  Text = d.ServicesPlaces
                              }
                       ).ToList();
            return View(nota);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(MfGeneralNotesOffer nota)
        {
            if (ModelState.IsValid)
            {
                _db.MfGeneralNotesOffers.Add(nota);
                await _db.SaveChangesAsync();
                TempData["Mensaje"] = "Se ha creado un nuevo registro.";
                return RedirectToAction("Index");
            }
            
            return View(nota);
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var nota = await _db.MfGeneralNotesOffers.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewBag.places = (from d in _db.MfServicesPlaces
                              select new SelectListItem
                              {
                                  Value = d.ServicesPlaceId.ToString(),
                                  Text = d.ServicesPlaces
                              }
           ).ToList();
            return View(nota);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(MfGeneralNotesOffer nota)
        {
            if (ModelState.IsValid)
            {
                _db.MfGeneralNotesOffers.Update(nota);
                await _db.SaveChangesAsync();
                TempData["Mensaje"] = "Se ha actualizado el registro.";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var nota = await _db.MfGeneralNotesOffers.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            _db.MfGeneralNotesOffers.Remove(nota);
            await _db.SaveChangesAsync();
            TempData["Mensaje"] = "Se ha Eliminado el registro.";
            return Content("1");
        }
    }
}
