using ExamApplication.Entities.Abstract;

namespace ExamApplication.Entities.Concrete
{
    public class Subject:IEntity
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Class { get; set; }
        public string TeacherFirstName { get; set; } = null!;

        public string TeacherLastName { get; set; } = null!;

        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
