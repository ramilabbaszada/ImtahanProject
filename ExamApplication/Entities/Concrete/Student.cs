using ExamApplication.Entities.Abstract;

namespace ExamApplication.Entities.Concrete
{
    public class Student:IEntity
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int Class { get; set; } 
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
