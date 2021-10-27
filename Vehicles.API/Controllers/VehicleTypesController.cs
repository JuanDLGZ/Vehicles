using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Controllers
{
    public class VehicleTypesController : Controller
    {
        private readonly DataContext _context;

        public VehicleTypesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleTypes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description")] VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType == null)
            {
                return NotFound();
            }
            return View(vehicleType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description")] VehicleType vehicleType)
        {
            if (id != vehicleType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleTypeExists(vehicleType.ID))
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
            return View(vehicleType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            _context.VehicleTypes.Remove(vehicleType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleTypeExists(int id)
        {
            return _context.VehicleTypes.Any(e => e.ID == id);
        }
    }
}
