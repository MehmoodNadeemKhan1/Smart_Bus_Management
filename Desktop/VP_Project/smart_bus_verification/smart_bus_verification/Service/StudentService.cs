using smart_bus_verification.IService;
using smart_bus_verification.Models;
using smart_bus_verification.Areas.Identity.Data;

namespace smart_bus_verification.Service
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context) { 
            _context = context;
        }
        public Student GetSavedStudent()
        {
            return _context.Students.SingleOrDefault();
        }

        public Student save(Student ostudent)
        {
             _context.Students.Add(ostudent);
            _context.SaveChanges();
            return ostudent;
        }
    }
}
