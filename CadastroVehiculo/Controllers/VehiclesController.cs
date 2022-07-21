using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroVehiculo.Context;
using CadastroVehiculo.Models;

namespace CadastroVehiculo.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly AppDbContext _context;

        public VehiclesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Vehicules.Include(v => v.Fuel).Include(v => v.Gearbox);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicules == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicules
                .Include(v => v.Fuel)
                .Include(v => v.Gearbox)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["FuelId"] = new SelectList(_context.Fuels, "FuelId", "FuelType");
            ViewData["GearboxId"] = new SelectList(_context.Gearbox, "GearboxId", "GearboxType");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,VehicleModel,VehicleBrand,VehicleNomberOfDoors,VehicleYearOfManifacture,GearboxId,FuelId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {                
                if (VehicleExisteByAll(vehicle))
                {
                    return View("~/Views/Home/Privacy.cshtml");
                }
                else
                {
                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            ViewData["FuelId"] = new SelectList(_context.Fuels, "FuelId", "FuelType", vehicle.FuelId);
            ViewData["GearboxId"] = new SelectList(_context.Gearbox, "GearboxId", "GearboxType", vehicle.GearboxId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicules == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicules.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["FuelId"] = new SelectList(_context.Fuels, "FuelId", "FuelType", vehicle.FuelId);
            ViewData["GearboxId"] = new SelectList(_context.Gearbox, "GearboxId", "GearboxType", vehicle.GearboxId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,VehicleModel,VehicleBrand,VehicleNomberOfDoors,VehicleYearOfManifacture,GearboxId,FuelId")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                if (VehicleExisteByAll(vehicle))
                {
                    return View("~/Views/Home/Privacy.cshtml");
                }
                else
                {

                    try
                    {
                        _context.Update(vehicle);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!VehicleExists(vehicle.VehicleId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuelId"] = new SelectList(_context.Fuels, "FuelId", "FuelType", vehicle.FuelId);
            ViewData["GearboxId"] = new SelectList(_context.Gearbox, "GearboxId", "GearboxType", vehicle.GearboxId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicules == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicules
                .Include(v => v.Fuel)
                .Include(v => v.Gearbox)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicules == null)
            {
                return Problem("Entity set 'AppDbContext.Vehicules'  is null.");
            }
            var vehicle = await _context.Vehicules.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicules.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return _context.Vehicules.Any(e => e.VehicleId == id);
        }
        private bool VehicleExisteByAll( Vehicle vehicle)
        {
            return _context.Vehicules
                    .Where(v => v.VehicleBrand.ToUpper().Equals(vehicle.VehicleBrand.ToUpper()) &&
                    v.VehicleModel.ToUpper().Equals(vehicle.VehicleModel.ToUpper()) &&
                    v.VehicleNomberOfDoors == vehicle.VehicleNomberOfDoors &&
                    v.VehicleYearOfManifacture.Year.Equals(vehicle.VehicleYearOfManifacture.Year) &&
                    v.GearboxId == vehicle.GearboxId &&
                    v.FuelId == vehicle.FuelId).Count()>0;
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Vehicle> vehiculos;
            string valorAtual = string.Empty;


            if (string.IsNullOrEmpty(searchString))
            {
                vehiculos = _context.Vehicules.DefaultIfEmpty();
                valorAtual = "Nenhum Vehiculo foi encontrado";
            }
            else
            {
                vehiculos = _context.Vehicules.Include(f=> f.Fuel).Include(g=> g.Gearbox)
                    .Where(v => v.VehicleModel.ToLower().Contains(searchString.ToLower()));
                if (vehiculos.Any())
                    valorAtual = "Vehiculos de Modelo " + searchString;
                else
                {
                    vehiculos = _context.Vehicules.Include(f => f.Fuel).Include(g => g.Gearbox)
                    .Where(v => v.VehicleBrand.ToLower().Contains(searchString.ToLower()));
                }
                if (vehiculos.Any())
                    valorAtual = "Vehiculos de Marca " + searchString;
                else
                {
                    vehiculos = _context.Vehicules.Include(f => f.Fuel).Include(g => g.Gearbox)
                    .Where(v => v.Gearbox.GearboxType.ToLower().Contains(searchString.ToLower()));
                }
                if (vehiculos.Any())
                    valorAtual = "Vehiculos com Tipo de Cambio " + searchString;
                else
                {
                    vehiculos = _context.Vehicules.Include(f => f.Fuel).Include(g => g.Gearbox)
                    .Where(v => v.Fuel.FuelType.ToLower().Contains(searchString.ToLower()));
                }
                if (vehiculos.Any())
                    valorAtual = "Vehiculos com Combustivel " + searchString;
                else
                {
                    int dateFab = 0;
                    try
                    {
                        dateFab = int.Parse(searchString);
                        if (dateFab != 0)
                        {
                            vehiculos = _context.Vehicules
                            .Where(v => v.VehicleYearOfManifacture.Year.Equals(dateFab));
                        }
                    }
                    catch (Exception e) { }
                }
                if (vehiculos.Any())
                    valorAtual = "Vehiculos Fabricado em  " + searchString;
                else
                    valorAtual = "Nenum veiculo encontrada  ";
            }
           
            return View("~/Views/Vehicles/Index.cshtml", vehiculos);
        }


    }
}
