using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using car1.Models;

namespace car1.Controllers
{
    [JWTAction]
    public class CarsController : Controller
    {
        private readonly CarinfomanagementContext _context;

        public CarsController(CarinfomanagementContext context)
        {
            _context = context;
        }

        [JWTAction(allowedRoles: "admin,customer")]
        // GET: Cars
        public async Task<IActionResult> Index(string searchQuery, string manufacturerId, string carType)
        {
            // Get list of manufacturers and car types for the dropdowns
            var manufacturers = await _context.Cars
                                              .Select(c => c.ManufacturerName)
                                              .Distinct()
                                              .ToListAsync();

            var carTypes = await _context.CarTypes.ToListAsync();
            var userType = HttpContext.Session.GetString("UserType");

            // Pass the userType to the view
            ViewData["UserType"] = userType;

            // Store in ViewData to populate the dropdowns
            ViewData["Manufacturers"] = manufacturers;
            ViewData["CarTypes"] = carTypes;

            // Filter cars based on search and selected filters
            var cars = from c in _context.Cars.Include(c => c.CarTransmissionType).Include(c => c.CarType)
                       select c;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                cars = cars.Where(c => c.ManufacturerName.Contains(searchQuery) || c.Model.Contains(searchQuery));
            }

            if (!string.IsNullOrEmpty(manufacturerId))
            {
                cars = cars.Where(c => c.ManufacturerName == manufacturerId);
            }

            if (!string.IsNullOrEmpty(carType))
            {
                cars = cars.Where(c => c.CarType.Type == carType);
            }

            return View(await cars.ToListAsync());
        }


        [JWTAction(allowedRoles: "admin,customer")]
        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarTransmissionType)
                .Include(c => c.CarType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [JWTAction(allowedRoles: "admin")]

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.CarTransmissionTypes, "Id", "Id");
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Id");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [JWTAction(allowedRoles: "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ManufacturerName,Model,Type,Engine,Bhp,Transmission,Mileage,Seat,AirBagDetails,BootSpace,Price,CarTypeId,CarTransmissionTypeId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.CarTransmissionTypes, "Id", "Id", car.CarTransmissionTypeId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Id", car.CarTypeId);
            return View(car);
        }

        // GET: Cars/Edit/5
        [JWTAction(allowedRoles: "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.CarTransmissionTypes, "Id", "Id", car.CarTransmissionTypeId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Id", car.CarTypeId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [JWTAction(allowedRoles: "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ManufacturerName,Model,Type,Engine,Bhp,Transmission,Mileage,Seat,AirBagDetails,BootSpace,Price,CarTypeId,CarTransmissionTypeId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.CarTransmissionTypes, "Id", "Id", car.CarTransmissionTypeId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Id", car.CarTypeId);
            return View(car);
        }

        // GET: Cars/Delete/5
        [JWTAction(allowedRoles: "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarTransmissionType)
                .Include(c => c.CarType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [JWTAction(allowedRoles: "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [JWTAction(allowedRoles: "admin,customer")]
        // GET: Cars/Search
        public async Task<IActionResult> Search(string searchQuery)
        {
            var cars = from c in _context.Cars.Include(c => c.CarTransmissionType).Include(c => c.CarType)
                       select c;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Filter cars by ManufacturerName or Model
                cars = cars.Where(c => c.ManufacturerName.Contains(searchQuery) || c.Model.Contains(searchQuery));
            }

            return View(await cars.ToListAsync());
        }

        [JWTAction(allowedRoles: "admin,customer")]
        // GET: Cars/Filter
        public async Task<IActionResult> Filter(string? manufacturerId, string? carType)
        {
            var cars = from c in _context.Cars.Include(c => c.CarTransmissionType).Include(c => c.CarType)
                       select c;

            // Filter by ManufacturerName if the parameter is provided
            if (!string.IsNullOrEmpty(manufacturerId))
            {
                cars = cars.Where(c => c.ManufacturerName == manufacturerId);
            }

            // Filter by CarType if the parameter is provided
            if (!string.IsNullOrEmpty(carType))
            {
                cars = cars.Where(c => c.CarType.Type == carType);
            }

            return View(await cars.ToListAsync());
        }


        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
