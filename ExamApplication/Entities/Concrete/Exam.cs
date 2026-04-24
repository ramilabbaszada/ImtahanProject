using ExamApplication.Entities.Abstract;

namespace ExamApplication.Entities.Concrete
{
    public class Exam:IEntity
    {
        public int Id { get; set; }
        public string SubjectCode { get; set; } = null!;

        public int StudentNumber { get; set; }

        public DateTime ExamDate { get; set; }

        public int Grade { get; set; }

        public Subject? Subject { get; set; }

        public Student? Student { get; set; }
    }
}
