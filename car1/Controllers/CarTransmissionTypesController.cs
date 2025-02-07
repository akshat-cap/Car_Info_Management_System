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
    public class CarTransmissionTypesController : Controller
    {
        private readonly CarinfomanagementContext _context;

        public CarTransmissionTypesController(CarinfomanagementContext context)
        {
            _context = context;
        }

        // GET: CarTransmissionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarTransmissionTypes.ToListAsync());
        }

        // GET: CarTransmissionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carTransmissionType = await _context.CarTransmissionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carTransmissionType == null)
            {
                return NotFound();
            }

            return View(carTransmissionType);
        }

        // GET: CarTransmissionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarTransmissionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] CarTransmissionType carTransmissionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carTransmissionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carTransmissionType);
        }

        // GET: CarTransmissionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carTransmissionType = await _context.CarTransmissionTypes.FindAsync(id);
            if (carTransmissionType == null)
            {
                return NotFound();
            }
            return View(carTransmissionType);
        }

        // POST: CarTransmissionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] CarTransmissionType carTransmissionType)
        {
            if (id != carTransmissionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carTransmissionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarTransmissionTypeExists(carTransmissionType.Id))
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
            return View(carTransmissionType);
        }

        // GET: CarTransmissionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carTransmissionType = await _context.CarTransmissionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carTransmissionType == null)
            {
                return NotFound();
            }

            return View(carTransmissionType);
        }

        // POST: CarTransmissionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carTransmissionType = await _context.CarTransmissionTypes.FindAsync(id);
            if (carTransmissionType != null)
            {
                _context.CarTransmissionTypes.Remove(carTransmissionType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarTransmissionTypeExists(int id)
        {
            return _context.CarTransmissionTypes.Any(e => e.Id == id);
        }
    }
}
