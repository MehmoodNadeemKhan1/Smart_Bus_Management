using smart_bus_verification.Models;

namespace smart_bus_verification.IService
{
    public interface IStudentService
    {
        Student save(Student ostudent);
        Student GetSavedStudent();
    }
}
