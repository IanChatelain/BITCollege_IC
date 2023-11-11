using BITCollege_IC.Data;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using Utility;

namespace BITCollege_IC.Models
{
    /// <summary>
    /// Student Model. Represents the Students table in the database.
    /// </summary>
    public class Student
    {
        private BITCollege_ICContext db = new BITCollege_ICContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [ForeignKey("GradePointState")]
        public int GradePointStateId { get; set; }

        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        [Display(Name = "Student\nNumber")]
        public long StudentNumber { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "First\nName")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Last\nName")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public String Address { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public String City { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK|YT)",
            ErrorMessage = "A Canadian province code must be entered")]
        public String Province { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "GradePoint\nAverage")]
        [Range(0.00, 4.50,
            ErrorMessage = "The value for {0} must be between {1} and {2}")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double? GradePointAverage { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Fees")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double OutstandingFees { get; set; }

        public String Notes { get; set; }

        [Display(Name = "Name")]
        public String FullName
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

                [Display(Name = "Address")]
        public String FullAddress
        {
            get
            {
                return String.Format("{0}, {1}, {2}", 
                    Address, City, Province);
            }
        }

        // Navigation Properties
        public virtual GradePointState GradePointState { get; set; }
        public virtual AcademicProgram AcademicProgram { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }

        // Methods

        /// <summary>
        /// Completes a state change depending on the GPA.
        /// </summary>
        public void ChangeState()
        {
            GradePointState before = db.GradePointStates.Find(this.GradePointStateId);
            int after = 0;

            while (before.GradePointStateId != after)
            {
                before.StateChangeCheck(this);
                after = before.GradePointStateId;
                before = db.GradePointStates.Find(this.GradePointStateId);
            }
        }

        /// <summary>
        /// Sets the next student number.
        /// </summary>
        public void SetNextStudentNumber()
        {
            StudentNumber = StoredProcedure.NextNumber("NextStudent").Value;
        }
    }

    /// <summary>
    /// AcademicProgram Model. Represents the AcademicPrograms table in the database.
    /// </summary>
    public class AcademicProgram
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AcademicProgramId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Program")]
        public String ProgramAcronym { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Program\nName")]
        public String Description { get; set; }

        // Navigation Properties
        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Student> Student { get; set; }

    }

    /// <summary>
    /// GradePointState Model. Represents the GradePointStates table in the database.
    /// </summary>
    public abstract class GradePointState
    {
        protected static BITCollege_ICContext db = new BITCollege_ICContext();

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GradePointStateId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Lower\nLimit")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double LowerLimit { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Upper\nLimit")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double UpperLimit { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "TuitionRate\nFactor")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double TuitionRateFactor { get; set; }

        [Display(Name = "State")]
        public String Description
        {
            get
            {
                return BusinessRules.ParseString(GetType().Name, "State");
            }
        }

        // Navigation Properties
        public virtual ICollection<Student> Student { get; set; }

        // Methods

        /// <summary>
        /// Adjusts the tuition rate depending on the GPA.
        /// </summary>
        /// <param name="student">A student object.</param>
        /// <returns>The adjusted tuition rate.</returns>
        public abstract double TuitionRateAdjustment(Student student);

        /// <summary>
        /// Checks the grade point state and moves states according to GPA
        /// </summary>
        /// <param name="student">A student object.</param>
        public abstract void StateChangeCheck(Student student);
    }


    /// <summary>
    /// SuspendedState Model. Represents the SuspendedStates table in the database.
    /// </summary>
    public class SuspendedState : GradePointState
    {
        private static SuspendedState suspendedState;

        private SuspendedState()
        {
            this.LowerLimit = 0.00;
            this.UpperLimit = 1.00;
            this.TuitionRateFactor = 1.1;
        }

        // Methods
        /// <summary>
        /// Instantiates a suspended grade point state if one doesn't exist.
        /// </summary>
        /// <returns>A suspended grade point state.</returns>
        public static SuspendedState GetInstance()
        {
            if(suspendedState == null)
            {
                suspendedState = db.SuspendedStates.SingleOrDefault();
                if(suspendedState == null)
                {
                    suspendedState = new SuspendedState();
                    db.SuspendedStates.Add(suspendedState);
                    db.SaveChanges();
                }
            }
            return suspendedState;
        }

        /// <summary>
        /// Adjusts the tuition rate depending on the GPA.
        /// </summary>
        /// <param name="student">A student object.</param>
        /// <returns>The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double localRate = this.TuitionRateFactor;

            if(student.GradePointAverage < .50)
            {
                localRate += .05;
            }
            else if(student.GradePointAverage < .75)
            {
                localRate += .02;
            }

            return localRate;
        }

        /// <summary>
        /// Checks the grade point state and moves states according to GPA
        /// </summary>
        /// <param name="student">A student object.</param>
        public override void StateChangeCheck(Student student)
        {
            double upperLimit = this.UpperLimit;

            if(student.GradePointAverage > upperLimit)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
            }
        }
    }

    /// <summary>
    /// ProbationState Model. Represents the ProbationStates table in the database.
    /// </summary>
    public class ProbationState : GradePointState
    {
        private static ProbationState probationState;

        private ProbationState()
        {
            this.LowerLimit = 1.00;
            this.UpperLimit = 2.00;
            this.TuitionRateFactor = 1.075;
        }

        // Methods

        /// <summary>
        /// Instantiates a probation grade point state if one doesn't exist.
        /// </summary>
        /// <returns>A probation grade point state.</returns>
        public static ProbationState GetInstance()
        {
            if(probationState == null)
            {
                probationState = db.ProbationStates.SingleOrDefault();
                if(probationState == null)
                {
                    probationState = new ProbationState();
                    db.ProbationStates.Add(probationState);
                    db.SaveChanges();
                }
            }
            return probationState;
        }

        /// <summary>
        /// Adjusts the tuition rate depending on the GPA.
        /// </summary>
        /// <param name="student">A student object.</param>
        /// <returns>The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double localRate = probationState.TuitionRateFactor;
            IQueryable<Registration> studentCourses = db.Registrations.Where(x => x.StudentId == student.StudentId
                                                      && x.Grade != null);

            if(studentCourses.Count() >= 5)
            {
                localRate += .035;
            }

            return localRate;
        }

        /// <summary>
        /// Checks the grade point state and moves states according to GPA
        /// </summary>
        /// <param name="student">A student object.</param>
        public override void StateChangeCheck(Student student)
        {
            double lowerLimit = this.LowerLimit;
            double upperLimit = this.UpperLimit;

            if(student.GradePointAverage < lowerLimit)
            {
                student.GradePointStateId = SuspendedState.GetInstance().GradePointStateId;
            }

            if(student.GradePointAverage > upperLimit)
            {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
            }
        }
    }

    /// <summary>
    /// RegularState Model. Represents the RegularStates table in the database.
    /// </summary>
    public class RegularState : GradePointState
    {
        private static RegularState regularState;

        private RegularState()
        {
            this.LowerLimit = 2.00;
            this.UpperLimit = 3.70;
            this.TuitionRateFactor = 1.0;
        }

        // Methods

        /// <summary>
        /// Instantiates a regular grade point state if one doesn't exist.
        /// </summary>
        /// <returns>A regular grade point state.</returns>
        public static RegularState GetInstance()
        {
            if(regularState == null)
            {
                regularState = db.RegularStates.SingleOrDefault();
                if(regularState == null)
                {
                    regularState = new RegularState();
                    db.RegularStates.Add(regularState);
                    db.SaveChanges();
                }
            }
            return regularState;
        }

        /// <summary>
        /// Adjusts the tuition rate depending on the GPA.
        /// </summary>
        /// <param name="student">A student object.</param>
        /// <returns>The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            return regularState.TuitionRateFactor;
        }

        /// <summary>
        /// Checks the grade point state and moves states according to GPA
        /// </summary>
        /// <param name="student">A student object.</param>
        public override void StateChangeCheck(Student student)
        {
            double lowerLimit = this.LowerLimit;
            double upperLimit = this.UpperLimit;

            if(student.GradePointAverage < lowerLimit)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
            }

            if(student.GradePointAverage > upperLimit)
            {
                student.GradePointStateId = HonorState.GetInstance().GradePointStateId;
            }
        }
    }

    /// <summary>
    /// HonorState Model. Represents the HonorStates table in the database.
    /// </summary>
    public class HonorState : GradePointState
    {
        private static HonorState honorState;

        private HonorState()
        {
            this.LowerLimit = 3.70;
            this.UpperLimit = 4.50;
            this.TuitionRateFactor = 0.9;
        }

        // Methods

        /// <summary>
        /// Instantiates an honor grade point state if one doesn't exist.
        /// </summary>
        /// <returns>The honor grade point state.</returns>
        public static HonorState GetInstance()
        {
            if(honorState == null)
            {
                honorState = db.HonorStates.SingleOrDefault();
                if(honorState == null)
                {
                    honorState = new HonorState();
                    db.HonorStates.Add(honorState);
                    db.SaveChanges();
                }
            }
            return honorState;
        }

        /// <summary>
        /// Adjusts the tuition rate depending on the GPA.
        /// </summary>
        /// <param name="student">A student object.</param>
        /// <returns>The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double localRate = honorState.TuitionRateFactor;
            IQueryable<Registration> completedCourses = db.Registrations.Where(x => x.StudentId == student.StudentId
                                                      && x.Grade != null);

            int completedCount = completedCourses.Count();

            if(completedCount >= 5)
            {
                localRate -= .05;
            }
            if(student.GradePointAverage > 4.25)
            {
                localRate -= .02;
            }

            return localRate;
        }

        /// <summary>
        /// Checks the grade point state and moves states according to GPA
        /// </summary>
        /// <param name="student">A student object.</param>
        public override void StateChangeCheck(Student student)
        {
            double lowerLimit = this.LowerLimit;

            if (student.GradePointAverage < lowerLimit)
            {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
            }
        }

    }

    /// <summary>
    /// Course Model. Represents the Courses table in the database.
    /// </summary>
    public abstract class Course
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        [Display(Name = "Course\nNumber")]
        public String CourseNumber { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public String Title { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Credit\nHours")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double CreditHours { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Tuition")]
        [DisplayFormat(DataFormatString = "{0:c2}")]
        public double TuitionAmount { get; set; }

        [Display(Name = "Course\nType")]
        public String CourseType
        {
            get
            {
                return BusinessRules.ParseString(GetType().Name, "Course");
            }
        }

        public String Notes { get;set; }

        // Navigation Properties
        public virtual AcademicProgram AcademicProgram { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }

        // Methods

        /// <summary>
        /// Sets the next course number.
        /// </summary>
        public abstract void SetNextCourseNumber();
    }

    /// <summary>
    /// GradedCourse Model. Represents the GradedCourses table in the database.
    /// </summary>
    public class GradedCourse : Course
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Assignments")]
        [DisplayFormat(DataFormatString = "{0:p2}", ApplyFormatInEditMode = true)]
        public double AssignmentWeight { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Exams")]
        [DisplayFormat(DataFormatString = "{0:p2}", ApplyFormatInEditMode = true)]
        public double ExamWeight { get; set; } 

        // Methods

        /// <summary>
        /// Sets the next graded course number.
        /// </summary>
        public override void SetNextCourseNumber()
        {
            CourseNumber = String.Format("G-{0}", StoredProcedure.NextNumber("NextGradedCourse").Value);
        }
    }

    /// <summary>
    /// MasteryCourse Model. Represents the MasteryCourses table in the database.
    /// </summary>
    public class MasteryCourse : Course
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Maximum\nAttempts")]
        public int MaximumAttempts { get;set; }

        // Methods

        /// <summary>
        /// Sets the next mastery course number.
        /// </summary>
        public override void SetNextCourseNumber()
        {
            CourseNumber = String.Format("M-{0}", StoredProcedure.NextNumber("NextMasteryCourse").Value);
        }
    }

    /// <summary>
    /// AuditCourse Model. Represents the AuditCourses table in the database.
    /// </summary>
    public class AuditCourse : Course
    {
        // Methods

        /// <summary>
        /// Sets the next audit course number.
        /// </summary>
        public override void SetNextCourseNumber()
        {
            CourseNumber = String.Format("A-{0}", StoredProcedure.NextNumber("NextAuditCourse").Value);
        }
    }

    /// <summary>
    /// Registration Model. Represents the Registrations table in the database.
    /// </summary>
    public class Registration
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [Display(Name = "Registration\nNumber")]
        public long RegistrationNumber { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegistrationDate { get; set; }

        [Range(0, 1, ErrorMessage = "The value for {0} must be between {1} and {2}")]
        [DisplayFormat(NullDisplayText = "Ungraded")]
        public double? Grade { get; set; }

        public String Notes { get; set; }

        // Navigation Properties
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }

        // Methods

        /// <summary>
        /// Sets the next registration course number.
        /// </summary>
        public void SetNextRegistrationNumber()
        {
            NextRegistration nextRegistration = NextRegistration.GetInstance();

            long? newNumber = StoredProcedure.NextNumber("NextRegistration");
            nextRegistration.NextAvailableNumber = newNumber.Value;

            RegistrationNumber = nextRegistration.NextAvailableNumber;
        }
    }

    /// <summary>
    /// NextUniqueNumber model. Represents the next unique number.
    /// </summary>
    public abstract class NextUniqueNumber
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "{0} is required")]
        public int NextUniqueNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        protected static BITCollege_ICContext db = new BITCollege_ICContext();
    }

    /// <summary>
    /// NextStudent mode. Represents the next student.
    /// </summary>
    public class NextStudent : NextUniqueNumber
    {
        private static NextStudent nextStudent;

        private NextStudent()
        {
            this.NextAvailableNumber = 20000000;
        }

        /// <summary>
        /// Instantiates a NextStudent if one doesn't exist.
        /// </summary>
        /// <returns>The NextStudent instance or null.</returns>
        public static NextStudent GetInstance()
        {
            if (nextStudent == null)
            {
                nextStudent = db.NextStudents.SingleOrDefault();
                if (nextStudent == null)
                {
                    nextStudent = new NextStudent();
                    db.NextStudents.Add(nextStudent);
                    db.SaveChanges();
                }
            }
            return nextStudent;
        }
    }

    /// <summary>
    /// NextGradedCourse model. Represents the next graded cousre.
    /// </summary>
    public class NextGradedCourse : NextUniqueNumber
    {
        private static NextGradedCourse nextGradedCourse;

        private NextGradedCourse()
        {
            this.NextAvailableNumber = 200000;
        }

        /// <summary>
        /// Instantiates a NextGradedCourse if one doesn't exist.
        /// </summary>
        /// <returns>The NextGradedCourse instance or null.</returns>
        public static NextGradedCourse GetInstance()
        {
            if (nextGradedCourse == null)
            {
                nextGradedCourse = db.NextGradedCourses.SingleOrDefault();
                if (nextGradedCourse == null)
                {
                    nextGradedCourse = new NextGradedCourse();
                    db.NextGradedCourses.Add(nextGradedCourse);
                    db.SaveChanges();
                }
            }
            return nextGradedCourse;
        }
    }


    /// <summary>
    /// NextMasteryCourse model. Represents a next mastery course instance.
    /// </summary>
    public class NextMasteryCourse : NextUniqueNumber
    {
        private static NextMasteryCourse nextMasteryCourse;

        private NextMasteryCourse()
        {
            this.NextAvailableNumber = 20000;
        }

        /// <summary>
        /// Instantiates a NextMasteryCourse if one doesn't exist.
        /// </summary>
        /// <returns>The NextMasteryCourse instance or null.</returns>
        public static NextMasteryCourse GetInstance()
        {
            if (nextMasteryCourse == null)
            {
                nextMasteryCourse = db.NextMasteryCourses.SingleOrDefault();
                if (nextMasteryCourse == null)
                {
                    nextMasteryCourse = new NextMasteryCourse();
                    db.NextMasteryCourses.Add(nextMasteryCourse);
                    db.SaveChanges();
                }
            }
            return nextMasteryCourse;
        }
    }

    /// <summary>
    /// NextRegistration model. Represents a next registration instance.
    /// </summary>
    public class NextRegistration : NextUniqueNumber
    {
        private static NextRegistration nextRegistration;

        private NextRegistration()
        {
            this.NextAvailableNumber = 700;
        }

        /// <summary>
        /// Instantiates a NextRegistration if one doesn't exist.
        /// </summary>
        /// <returns>The NextRegistration instance or null.</returns>
        public static NextRegistration GetInstance()
        {
            if (nextRegistration == null)
            {
                nextRegistration = db.NextRegistrations.SingleOrDefault();
                if (nextRegistration == null)
                {
                    nextRegistration = new NextRegistration();
                    db.NextRegistrations.Add(nextRegistration);
                    db.SaveChanges();
                }
            }
            return nextRegistration;
        }
    }

    /// <summary>
    /// NextAuditCourse model. Represents a next audit course instance.
    /// </summary>
    public class NextAuditCourse : NextUniqueNumber
    {
        private static NextAuditCourse nextAuditCourse;

        private NextAuditCourse()
        {
            this.NextAvailableNumber = 2000;
        }

        /// <summary>
        /// Instantiates a NextAuditCourse if one doesn't exist.
        /// </summary>
        /// <returns>The NextAuditCourse instance or null.</returns>
        public static NextAuditCourse GetInstance()
        {
            if (nextAuditCourse == null)
            {
                nextAuditCourse = db.NextAuditCourses.SingleOrDefault();
                if (nextAuditCourse == null)
                {
                    nextAuditCourse = new NextAuditCourse();
                    db.NextAuditCourses.Add(nextAuditCourse);
                    db.SaveChanges();
                }
            }
            return nextAuditCourse;
        }
    }

    /// <summary>
    /// Represents a stored procedure instance.
    /// </summary>
    public static class StoredProcedure
    {
        // Methods

        /// <summary>
        /// Executes a database stored procedure and returns an incremented value.
        /// </summary>
        /// <param name="discriminator">A discriminator of which object to execute the stored procedure on.</param>
        /// <returns>An incremented value</returns>
        public static long? NextNumber(String discriminator)
        {
            long? returnValue;

            try
            {
                SqlConnection connection = new SqlConnection("Data Source=localhost; " +
                    "Initial Catalog=BITCollege_ICContext;Integrated Security=True");
                SqlCommand storedProcedure = new SqlCommand("next_number", connection);

                storedProcedure.CommandType = CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@Discriminator", discriminator);

                SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };

                storedProcedure.Parameters.Add(outputParameter);
                connection.Open();
                storedProcedure.ExecuteNonQuery();
                connection.Close();
                returnValue = (long?)outputParameter.Value;
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }
    }
}