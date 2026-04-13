// Programmer: Rob Garner (rgarner7@cnm.edu)
// Date: 20161003
// Purpose: Holds information for one student
// 20260413 - Updated for MAUI Data Demo. RJG

namespace DataDemo.Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StudentNumber { get; set; }
        public int? MajorId { get; set; }

        public Major? Major { get; set; }

        public Student()
            : this("Number Pending", "TBD", "TBD", null)
        {
        }

        public Student(string sID)
            : this(sID, "TBD", "TBD", null)
        {
        }

        public Student(string sID, string firstName, string lastName, int? majorId)
        {
            StudentNumber = sID;
            FirstName = firstName;
            LastName = lastName;
            MajorId = majorId;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " Major: " + Major?.Title ?? "TBD";
        }
    }
}