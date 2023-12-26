using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_bus_verification.Areas.Identity.Data;
using smart_bus_verification.Models;

namespace smart_bus_verification.Controllers
{
    [Authorize]
    public class ExpendController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExpendController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment )
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Expend
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Expenditures != null ? 
                          View(await _context.Expenditures.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Expenditures'  is null.");
        }

        // GET: Expend/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Expenditures == null)
            {
                return NotFound();
            }

            var expenditures = await _context.Expenditures
                .FirstOrDefaultAsync(m => m.ExpenditureID == id);
            if (expenditures == null)
            {
                return NotFound();
            }

            return View(expenditures);
        }

        // GET: Expend/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ExpendUpload file)
        {
            string imageFilename = "";
            string voucherImageFilename = "";

            // Check and save the 'image' file
            if (file.ReceiptPhoto != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                imageFilename = Guid.NewGuid().ToString() + "_" + file.ReceiptPhoto.FileName;
                string imageFilePath = Path.Combine(uploadFolder, imageFilename);
                file.ReceiptPhoto.CopyTo(new FileStream(imageFilePath, FileMode.Create));
            }

           
          

            // Create a new Student object with distinct filenames
            Expenditures s = new Expenditures
            {
                ExpenditureID = file.ExpenditureID,
                Category = file.Category,
                Description = file.Description,
                DateOfExpenditure = file.DateOfExpenditure,
                Amount = file.Amount,
                ReceiptReference = file.ReceiptReference,
                ReceiptImage = imageFilename,
               
            };

            _context.Add(s);
            _context.SaveChanges();
            ViewBag.success = "Record Added";

            return View();
        }


        // GET: Expend/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Expenditures == null)
            {
                return NotFound();
            }

            var expenditures = await _context.Expenditures.FindAsync(id);
            if (expenditures == null)
            {
                return NotFound();
            }
            return View(expenditures);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, [Bind("ExpenditureID,Category,Description,Amount,DateOfExpenditure,ReceiptImage,ReceiptReference")] Expenditures expenditures)
        {
            if (id != expenditures.ExpenditureID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenditures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpendituresExists(expenditures.ExpenditureID))
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
            return View(expenditures);
        }

        // GET: Expend/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Expenditures == null)
            {
                return NotFound();
            }

            var expenditures = await _context.Expenditures
                .FirstOrDefaultAsync(m => m.ExpenditureID == id);
            if (expenditures == null)
            {
                return NotFound();
            }

            return View(expenditures);
        }

        // POST: Expend/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Expenditures == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Expenditures'  is null.");
            }
            var expenditures = await _context.Expenditures.FindAsync(id);
            if (expenditures != null)
            {
                _context.Expenditures.Remove(expenditures);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        private bool ExpendituresExists(int id)
        {
          return (_context.Expenditures?.Any(e => e.ExpenditureID == id)).GetValueOrDefault();
        }
    }
}
