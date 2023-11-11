using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BITCollege_IC.Data
{
    public class BITCollege_ICContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BITCollege_ICContext() : base("name=BITCollege_ICContext")
        {

        }
        public System.Data.Entity.DbSet<BITCollege_IC.Models.MasteryCourse> MasteryCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.AuditCourse> AuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.GradedCourse> GradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.HonorState> HonorStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.RegularState> RegularStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.ProbationState> ProbationStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.SuspendedState> SuspendedStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.AcademicProgram> AcademicPrograms { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.GradePointState> GradePointStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.NextUniqueNumber> NextUniqueNumbers { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.NextAuditCourse> NextAuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.NextGradedCourse> NextGradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.NextMasteryCourse> NextMasteryCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.NextRegistration> NextRegistrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_IC.Models.NextStudent> NextStudents { get; set; }
    }
}
