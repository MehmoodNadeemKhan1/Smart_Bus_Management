using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using smart_bus_verification.Areas.Identity.Data;
using smart_bus_verification.IService;
using smart_bus_verification.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace smart_bus_verification.Controllers
{

    
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hc;



        public HomeController(ApplicationDbContext context, IWebHostEnvironment hc)
        {
           
            _context = context;
            _hc = hc;
        }
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }




        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> List()
        {
            return View(await _context.Students.ToListAsync());
                   
           
        }
        [Authorize(Roles = "Admin , User")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin , User")]
        public async Task<IActionResult> Create(FileUpload file)
        {
            string imageFilename = "";
            string voucherImageFilename = "";

            // Check and save the 'image' file
            if (file.photo != null)
            {
                string uploadFolder = Path.Combine(_hc.WebRootPath, "images");
                imageFilename = Guid.NewGuid().ToString() + "_" + file.photo.FileName;
                string imageFilePath = Path.Combine(uploadFolder, imageFilename);
                file.photo.CopyTo(new FileStream(imageFilePath, FileMode.Create));
            }

            
            if (file.Voucherphoto != null)
            {
                string uploadFolder = Path.Combine(_hc.WebRootPath, "images");
                voucherImageFilename = Guid.NewGuid().ToString() + "_" + file.Voucherphoto.FileName;
                string voucherImageFilePath = Path.Combine(uploadFolder, voucherImageFilename);
                file.Voucherphoto.CopyTo(new FileStream(voucherImageFilePath, FileMode.Create));
            }

            
            Student s = new Student
            {
                Name = file.Name,
                Roll = file.Roll,
                RouteNo = file.RouteNo,
                PickUp = file.PickUp,
                CNIC = file.CNIC,
                Department = file.Department,
                Availability = false,
                phonenumber = file.phonenumber,
                Card_issued_Date = DateTime.Now,
                image = imageFilename,
                Voucherimage = voucherImageFilename
            };

            _context.Add(s);
            _context.SaveChanges();
            ViewBag.success = "Record Added";

            return View();
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }




        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Edit(int? id, FileUpload updatedFile)
        //{
        //    if (id != updatedFile.StudentId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Retrieve the existing record from the database
        //            var existingFile = await _context.Students.FindAsync(id);

        //            if (existingFile == null)
        //            {
        //                return NotFound();
        //            }

        //            // Update the properties with the new values
        //            existingFile.Name = updatedFile.Name;
        //            existingFile.Roll = updatedFile.Roll;
        //            existingFile.RouteNo = updatedFile.RouteNo;
        //            existingFile.PickUp = updatedFile.PickUp;
        //            existingFile.CNIC = updatedFile.CNIC;
        //            existingFile.Department = updatedFile.Department;
        //            existingFile.Availability = updatedFile.Availability;

        //            // Check if a new photo is uploaded
        //            if (updatedFile.photo != null)
        //            {
        //                string uploadFolder = Path.Combine(_hc.WebRootPath, "images");
        //                string newFilename = Guid.NewGuid().ToString() + "_" + updatedFile.photo.FileName;
        //                string newFilePath = Path.Combine(uploadFolder, newFilename);

        //                // Delete the existing photo file
        //                if (!string.IsNullOrEmpty(existingFile.image))
        //                {
        //                    string existingFilePath = Path.Combine(uploadFolder, existingFile.image);
        //                    System.IO.File.Delete(existingFilePath);
        //                }

        //                // Save the new photo file
        //                updatedFile.photo.CopyTo(new FileStream(newFilePath, FileMode.Create));
        //                existingFile.image = newFilename;
        //            }

        //            // Update the entity in the database
        //            _context.Update(existingFile);
        //            await _context.SaveChangesAsync();

        //            ViewBag.success = "Record Updated";

        //            // Redirect to a different action or view after a successful update
        //            return RedirectToAction("Index"); // Replace "Index" with the action you want to redirect to
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!StudentExists(updatedFile.StudentId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //    }

        //    return View("Create"); // Handle the case when ModelState is not valid
        //}



        // GET: Home/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Home/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'CodeFirstDBContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("SelectedDelete")]

        // First action to display the selected items for deletion
        public async Task<IActionResult>SelectedDelete(List<int?> selectedIds)
        {
            try
            {
                if (selectedIds == null || !selectedIds.Any())
                {
                    // No selected IDs, nothing to delete
                    return RedirectToAction("List", "Home");
                }

                var studentsToUpdate = await _context.Students
                    .Where(s => selectedIds.Contains(s.StudentId))
                    .ToListAsync();

                if (studentsToUpdate.Any())
                {
                    _context.Students.RemoveRange(studentsToUpdate);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("List", "Home");
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately (e.g., return an error view)
                // Logging example: _logger.LogError(ex, "An error occurred while processing the delete operation.");
                return StatusCode(500, "An error occurred while processing the delete operation.");
            }
        }




        [HttpPost]
        [Route("ResetAvailability")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetAvailability(List<int> selectedIds)
        {
            if (selectedIds != null && selectedIds.Any())
            {
                // Assuming you have a DbContext to interact with the database

                var studentsToUpdate = await _context.Students.Where(s => selectedIds.Contains(s.StudentId)).ToListAsync();

                foreach (var student in studentsToUpdate)
                {
                   
                    student.Availability = true;
                }

                _context.SaveChanges();

            }


            return RedirectToAction("List", "Home");



        }


        [HttpPost]
        [Route("ResetAvailabilityFalse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetAvailabilityFalse(List<int> selectedIds)
        {
            if (selectedIds != null && selectedIds.Any())
            {
                // Assuming you have a DbContext to interact with the database

                var studentsToUpdate = await _context.Students.Where(s => selectedIds.Contains(s.StudentId)).ToListAsync();

                foreach (var student in studentsToUpdate)
                {

                    student.Availability = false;
                }

                _context.SaveChanges();

            }


            return RedirectToAction("List", "Home");



        }





        [Authorize(Roles = "Admin , User")]
        public async Task<IActionResult> AdminIndex()
        {
          
           var emailClaim = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(emailClaim))
            {
                
                return RedirectToAction("Login");
                }

                var result = await (
                    from user in _context.Users
                    join student in _context.Students on user.RollNo equals student.Roll
                    where user.Email == emailClaim /*&& student.Availability == true*/
                    select new Student
                    {
                        image = student.image, 
                        Name = student.Name,
                        Roll = student.Roll,
                        RouteNo = student.RouteNo,
                        CNIC = student.CNIC,
                        PickUp = student.PickUp,
                        phonenumber = student.phonenumber,
                        Card_issued_Date = student.Card_issued_Date
                        
                    }
                ).ToListAsync();

                return View(result);
            }




        }
}
