using ForOffer.Models.DB;
using ForOffer.Models.DB.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForOffer.Controllers
{
    public class NuevaObservacion
    {
        public MfGeneralObservationsOffer Observacion { get; set; }
        public List<SelectListItem> Lugares { get; set; }

    }
    public class ObservacionesController : Controller
    {
        
        public async Task<IActionResult> Index()
        {

            using var db = new ForOfferContext();
            try
            {
                List<SelectListItem> items = new();
                items = (from d in db.MfServicesPlaces
                         select new SelectListItem
                         {
                             Value = d.ServicesPlaceId.ToString(),
                             Text = d.ServicesPlaces.ToString(),
                         }
                               ).ToList();

                ViewData["Services"] = items;
                var lst = await db.MfGeneralObservationsOffers.Include(x => x.GeneralObservationServices).ToListAsync();
                var estados = new List<SelectListItem>{
                new SelectListItem
                              {
                                  Value = "1",
                                  Text = "activo",
                              },
                new SelectListItem
                              {
                                  Value = "2",
                                  Text = "inactivo"
                              },
                new SelectListItem
                              {
                                  Value = "3",
                                  Text = "otro"
                              }
            };
                //modelo que se usa para crear una nueva observacion desde la vista parcial en el modal
                //tiene el modelo de la observacion y la lista de item(lugares)
                NuevaObservacion nueva = new();
                MfGeneralObservationsOffer nuevaObservacion = new();
                nueva.Observacion = nuevaObservacion;
                nueva.Lugares = (from d in db.MfServicesPlaces
                                 select new SelectListItem
                                 {
                                     Value = d.ServicesPlaceId.ToString(),
                                     Text = d.ServicesPlaces
                                 }
                           ).ToList();
                ViewBag.Nueva = nueva;
                ViewBag.estados = estados;
                return View(lst);
            }
            catch (Exception)
            {

            }
            return null;

        }
        
        // GET: observaciones/Create
        public IActionResult Crear()
        {
            var observacion = new MfGeneralObservationsOffer();
            using var db = new ForOfferContext();
            ViewBag.places = (from d in db.MfServicesPlaces
                              select new SelectListItem
                              {
                                  Value = d.ServicesPlaceId.ToString(),
                                  Text = d.ServicesPlaces
                              }
                       ).ToList();
            return View(observacion);
        }
        //POST crear sin el modal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(MfGeneralObservationsOffer observacion)
        {

            if (ModelState.IsValid)
            {
                using var db = new ForOfferContext();
                observacion.RegState=1;
                db.MfGeneralObservationsOffers.Add(observacion);               
                await db.SaveChangesAsync();

                TempData["Mensaje"] = "Se ha creado un nuevo registro.";
                return RedirectToAction("Index");
            }
            //nos podemos reunir es para decirle algo si de una yalo llamo mejorlisto
            return View(observacion);
        }
        //editar viejo
        //[HttpGet]
        //public async Task<IActionResult> Editar(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var observacion = await _db.MfGeneralObservationsOffers.FindAsync(id);
        //    if (observacion == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.places = (from d in _db.MfServicesPlaces
        //                      select new SelectListItem
        //                      {
        //                          Value = d.ServicesPlaceId.ToString(),
        //                          Text = d.ServicesPlaces
        //                      }
        //   ).ToList();
        //    return View(observacion);
        //}
        //editar sin modal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ObservationViewModel observacion)
        {

            if (ModelState.IsValid)
            {

                using var db = new ForOfferContext();
                try
                {
                    MfGeneralObservationsOffer obserTemp = await db.MfGeneralObservationsOffers.FindAsync(observacion.GeneralObservationOfferId);
                    if (obserTemp != null) //si se escuentra se actualizan los datos
                    {
                        //obserTemp.GeneralObservationServicesId = observacion.GeneralObservationOfferId;
                        obserTemp.GeneralObservationText = observacion.GeneralObservationText;
                        obserTemp.GeneralObservationServicesId = observacion.GeneralObservationServicesId;
                        obserTemp.RegState = observacion.RegState;

                        db.MfGeneralObservationsOffers.Update(obserTemp);
                        await db.SaveChangesAsync();
                        TempData["Mensaje"] = "Se ha actualizado el registro.";
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    ViewData["Message"] = ex.Message.ToString();

                }
            }
            return View(observacion);
        }

        //eliminar corregido sin inyeccion de dependencias
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            using var db = new ForOfferContext();
            var observacion = await db.MfGeneralObservationsOffers.FindAsync(id);
            if (observacion == null)
            {
                return NotFound();
            }
            db.MfGeneralObservationsOffers.Remove(observacion);
            await db.SaveChangesAsync();
            return Content("1");
        }


        public IActionResult EliminarOb(int id) {

            using var db = new ForOfferContext();
            var reg = db.MfGeneralObservationsOffers.Find(id);
            if (reg == null) {

                return Content("nada");
            }

            db.MfGeneralObservationsOffers.Remove(reg);
            db.SaveChanges();

            return Content("1");
        }

        //listar de manera sencilla con el view model
        public IActionResult ObservationList() {

            ObservationViewModel model = new();
            using (var db = new ForOfferContext())
            {
                model.Lista = (from d in db.MfGeneralObservationsOffers
                               join s in db.MfServicesPlaces on d.GeneralObservationServicesId equals s.ServicesPlaceId

                               select new ObservationViewModel
                               {
                                   GeneralObservationText=d.GeneralObservationText,
                                   ServicesPlace=s.ServicesPlaces,
                               }
                             ).ToList();
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult AddObservations() {

            List<SelectListItem> items =new();


            using (var db = new ForOfferContext())
            {

                items = (from d in db.MfServicesPlaces
                         select new SelectListItem
                         {

                             Value = d.ServicesPlaceId.ToString(),
                             Text = d.ServicesPlaces.ToString(),
                         }
                       ).ToList();

                ViewData["Services"] = items;
            }

            return View();
        }

        //tambien crear sin modal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddObservations(ObservationViewModel model) {
            List<SelectListItem> items = new();
            using (var db = new ForOfferContext())
            {
                items = (from d in db.MfServicesPlaces
                         select new SelectListItem
                         {

                             Value = d.ServicesPlaceId.ToString(),
                             Text = d.ServicesPlaces.ToString(),
                         }
                       ).ToList();

                ViewData["Services"] = items;
            }

            if (!ModelState.IsValid) {


                return View(model);
            }

            using (var db = new ForOfferContext())
            {
                MfGeneralObservationsOffer reg = new()
                {
                    GeneralObservationText = model.GeneralObservationText,
                    RegState = 1,
                    GeneralObservationServicesId = model.GeneralObservationServicesId
                };

                db.MfGeneralObservationsOffers.Add(reg);
                db.SaveChanges();

            }
            return RedirectToAction("ObservationList");
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ObservationViewModel observacion = new();
            List<SelectListItem> items = new();
            using (var db = new ForOfferContext())
            {
                try
                {
                    var observacionTemp = db.MfGeneralObservationsOffers.Find(id);
                    if (observacionTemp!=null)
                    {
                        observacion.GeneralObservationOfferId = observacionTemp.GeneralObservationOfferId;
                        observacion.GeneralObservationText = observacionTemp.GeneralObservationText;
                        observacion.GeneralObservationServicesId = observacionTemp.GeneralObservationServicesId;
                        observacion.RegState = observacionTemp.RegState;
                        

                            items = (from d in db.MfServicesPlaces
                                     select new SelectListItem
                                     {

                                         Value = d.ServicesPlaceId.ToString(),
                                         Text = d.ServicesPlaces.ToString(),
                                     }
                                   ).ToList();

                            ViewData["Services"] = items;
                        
                        return View(observacion);
                    }
                    

                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = ex.Message.ToString();
                }
            }
            return View("Index");
        }
    
        //cambiar de estado solo si son dos estados
        //[HttpPost]
        //public async Task<IActionResult> Estado(int? id)
        //{
        //    if (id!=null)
        //    {
        //        using var db = new ForOfferContext();
        //        var observacion = db.MfGeneralObservationsOffers.Find(id);
        //        if (observacion != null)
        //        {
        //            if (observacion.RegState == 1)
        //            {
        //                observacion.RegState = 0;
        //            }
        //            else
        //            {
        //                observacion.RegState = 1;
        //            }
        //            db.MfGeneralObservationsOffers.Update(observacion);
        //            await db.SaveChangesAsync();
        //            TempData["Mensaje"] = "Estado Modificado";
        //            return Content("1");
        //        }
        //        else return Content("2");
        //    }
        //    else return Content("2");
        //}

        //cambiar estado por el dropdown
        [HttpPost]
        public async Task<IActionResult> SelectState(int? id, int? estado)
        {
            if (id != null && estado!=null)
            {
                using var db = new ForOfferContext();
                var observacion = db.MfGeneralObservationsOffers.Find(id);
                if (observacion != null)
                {
                    observacion.RegState = (int)estado;
                    db.MfGeneralObservationsOffers.Update(observacion);
                    await db.SaveChangesAsync();
                    TempData["Mensaje"] = "Estado Modificado";
                    return Content("1");
                }
                else return Content("observacion igual a null");
            }
            else return Content("id o estado igual a null");
        }
        public async Task<IActionResult> ModalCrear(NuevaObservacion nuevaObs)
        {
            MfGeneralObservationsOffer nuevaObservacion = new() 
            { 
                GeneralObservationServicesId = nuevaObs.Observacion.GeneralObservationServicesId,
                GeneralObservationText = nuevaObs.Observacion.GeneralObservationText,
                RegState = 1,
            };
            if (ModelState.IsValid)
            {
                using var db = new ForOfferContext();
                db.MfGeneralObservationsOffers.Add(nuevaObservacion);
                await db.SaveChangesAsync();
                TempData["Mensaje"] = "Se ha creado un nuevo registro.";
                return RedirectToAction("Index");
            }
            
            return Content("2");
        }

        

        public IActionResult ModalEditar(int? id)
        {

            if (id == null || id == 0)
            {
                return Content("ID no existe o es cero");
            }
            ObservationViewModel observacion = new();

            using var db = new ForOfferContext();
            try
            {
                var observacionTemp = db.MfGeneralObservationsOffers.Find(id);
                List<SelectListItem> items = new();
                items = (from d in db.MfServicesPlaces
                         select new SelectListItem
                         {
                             Value = d.ServicesPlaceId.ToString(),
                             Text = d.ServicesPlaces.ToString(),
                         }
                               ).ToList();

                ViewData["Services"] = items;
                if (observacionTemp != null) //si existe un registro con ese id
                {
                    observacion.GeneralObservationOfferId = observacionTemp.GeneralObservationOfferId;
                    observacion.GeneralObservationText = observacionTemp.GeneralObservationText;
                    observacion.GeneralObservationServicesId = observacionTemp.GeneralObservationServicesId;
                    observacion.RegState = observacionTemp.RegState;


                    return PartialView("_parcialEditar", observacion);
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message.ToString();
            }
            return Content("No existe un registro");
        }

        [HttpPost]
        public async Task<IActionResult> ModalEditar(ObservationViewModel observacion)
        {
            if (ModelState.IsValid)
            {

                using var db = new ForOfferContext();
                try
                {
                    MfGeneralObservationsOffer obserTemp = await db.MfGeneralObservationsOffers.FindAsync(observacion.GeneralObservationOfferId);
                    if (obserTemp != null) //si se escuentra se actualizan los datos
                    {
                        //obserTemp.GeneralObservationServicesId = observacion.GeneralObservationOfferId;
                        obserTemp.GeneralObservationText = observacion.GeneralObservationText;
                        obserTemp.GeneralObservationServicesId = observacion.GeneralObservationServicesId;
                        obserTemp.RegState = observacion.RegState;

                        db.MfGeneralObservationsOffers.Update(obserTemp);
                        await db.SaveChangesAsync();
                        TempData["Mensaje"] = "Se ha actualizado el registro.";
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    ViewData["Message"] = ex.Message.ToString();

                }
            }
            return PartialView("_parcialEditar", observacion);

        }
    }


 

}
