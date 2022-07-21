using CadastroVehiculo.Context;
using CadastroVehiculo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroVehiculo.Controllers
{
    public class FuelController : Controller
    {
        private readonly AppDbContext _context;

        public FuelController(AppDbContext context)
        {
            _context = context;
        }
        

        // GET: FuelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FuelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuelType")] Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                var fuel1 = _context.Fuels
                    .Where(f => f.FuelType.ToLower().Equals(fuel.FuelType.ToLower()));
                if (fuel1.Count()>0)
                {
                    return View("~/Views/Home/Privacy.cshtml");
                }
                else
                {                   
                    _context.Add(fuel);
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
