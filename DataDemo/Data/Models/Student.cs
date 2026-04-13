// Programmer: Rob Garner (rgarner7@cnm.edu)
// Date: 20161003
// Purpose: Holds information for one student
// 20260413 - Updated for MAUI Data Demo. RJG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDemo.Data.Models
{
    /// <summary>
    /// Stores information related to one student.
    /// </summary>
    public class Student
    {
        #region Properties
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StudentNumber { get; set; }
        public int? MajorId { get; set; }

        public Major? Major { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Defaults StudentNumber to "Number Pending", names to "TBD" and Major to "Undeclared"
        /// </summary>
        public Student()
            : this("Number Pending", "TBD", "TBD", null)
        {
        }

        /// <summary>
        /// Defaults names to "TBD" and Major to "Undeclared"
        /// </summary>
        /// <param name="sID">String representing students ID</param>
        public Student(string sID)
            : this(sID, "TBD", "TBD", null)
        {
        }

        /// <summary>
        /// Set all parameters including scores.
        /// </summary>
        /// <param name="sID">String representing students ID</param>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Student's last name.</param>
        /// <param name="majorId">Id of the major student is assigned to</param>
        public Student(string sID, string firstName, string lastName,
            int? majorId)
        {
            StudentNumber = sID;
            FirstName = firstName;
            LastName = lastName;
            MajorId = majorId;
        }
        #endregion

        #region Instance Methods

        /// <summary>
        /// Displays first name, last name, major and average.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return
                FirstName + " " + LastName
                + " Major: " + Major.Title;
        }
        #endregion
    }
}
