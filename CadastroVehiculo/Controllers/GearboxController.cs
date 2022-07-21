using CadastroVehiculo.Context;
using CadastroVehiculo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroVehiculo.Controllers
{
    public class GearboxController : Controller
    {
        private readonly AppDbContext _context;

        public GearboxController(AppDbContext context)
        {
            _context = context;
        }
       


        // GET: GearboxController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GearboxController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GearboxType")] Gearbox gearbox)
        {
            if (ModelState.IsValid)
            {
                var gearbox1 = _context.Gearbox
                    .Where(g => g.GearboxType.ToLower().Equals(gearbox.GearboxType.ToLower()));
                if (gearbox1.Count()>0)
                {
                    return View("~/Views/Home/Privacy.cshtml");
                }
                else
                {                    
                    _context.Add(gearbox);
                    await _context.SaveChangesAsync();
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


       
      
    }
}
