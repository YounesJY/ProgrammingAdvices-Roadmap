using Contracts;
using StudentDataAccessLayer;



namespace StudentAPIBusinessLayer
{
    public class Student
    {
        public enum enMode { AddNew = 0, Update = 1 }

        public int ID { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
        public enMode Mode { get; private set; }
        public StudentDTO ToDTO()
        {
            return new StudentDTO(ID, Name, Age, Grade);
        }


        private Student(StudentDTO dto, enMode mode)
        {
            ID = dto.Id;
            Name = dto.Name;
            Age = dto.Age;
            Grade = dto.Grade;

            Mode = mode;
        }
        public Student(StudentDTO dto) : this(dto, enMode.AddNew) { }


        public static Student? Find(int id)
        {
            StudentDTO? dto = StudentData.GetStudentById(id);

            if (dto == null)
                return null;

            return new Student(dto, enMode.Update);
        }
        public static List<StudentDTO> GetAllStudents()
        {
            return StudentData.GetAllStudents();
        }
        public static List<StudentDTO> GetPassedStudents()
        {
            return StudentData.GetPassedStudents();
        }
        public static double GetAverageGrade()
        {
            return StudentData.GetAverageGrade();
        }
        public static bool DeleteStudent(int id)
        {
            return StudentData.DeleteStudent(id);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddNewStudent())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return UpdateStudent();

                default:
                    return false;
            }
        }

        private bool AddNewStudent()
        {
            ID = StudentData.AddStudent(ToDTO());
            return ID != -1;
        }
        private bool UpdateStudent()
        {
            return StudentData.UpdateStudent(ToDTO());
        }
    }
}