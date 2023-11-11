using BITCollege_IC.Controllers;
using BITCollege_IC.Data;
using BITCollege_IC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;
using Utility;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CollegeRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CollegeRegistration.svc or CollegeRegistration.svc.cs at the Solution Explorer and start debugging.
    public class CollegeRegistration : ICollegeRegistration
    {
        BITCollege_ICContext db = new BITCollege_ICContext();

        public void DoWork()
        {

        }

        /// <summary>
        /// Drops a course for a student.
        /// </summary>
        /// <param name="registrationId">The ID of the registration to be dropped.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public bool DropCourse(int registrationId)
        {
            try
            {
                Registration registration = 
                    (from results in db.Registrations 
                     where results.RegistrationId == registrationId 
                     select results).SingleOrDefault();

                if (registration != null)
                {
                    db.Registrations.Remove(registration);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Registers a course for a student.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="courseId">The ID of the course to register.</param>
        /// <param name="notes">Additional notes for the registration.</param>
        /// <returns>An error code representing the status of the registration.</returns>
        public int RegisterCourse(int studentId, int courseId, string notes)
        {
            int errorCode = 0;

            IQueryable<Registration> allRecords = db.Registrations.Where
            (
                x => x.StudentId == studentId && 
                x.CourseId == courseId
            );

            Course course = db.Courses.Where(c => c.CourseId == courseId).SingleOrDefault();
            Student student = db.Students.Where(s => s.StudentId == studentId).SingleOrDefault();

            IEnumerable<Registration> nullRecords = allRecords.Where
            (
                x => x.Grade == null
            );

            IEnumerable<Registration> completeRecords = allRecords.Where
            (
                x => x.Grade != null
            );

            if (nullRecords.Count() > 0)
            {
                errorCode = -100;
            }

            if(BusinessRules.CourseTypeLookup(course.CourseType) == CourseType.MASTERY)
            {
                int attempts = ((MasteryCourse)course).MaximumAttempts;
                if (completeRecords.Count() > attempts)
                {
                    errorCode = -200;
                }
            }

            if (errorCode == 0)
            {
                try
                {
                    Registration newRegistration = new Registration();

                    newRegistration.StudentId = studentId;
                    newRegistration.CourseId = courseId;
                    newRegistration.Notes = notes;
                    newRegistration.RegistrationDate = DateTime.Now;
                    newRegistration.SetNextRegistrationNumber();

                    db.Registrations.Add(newRegistration);
                    db.SaveChanges();

                    double tuition = course.TuitionAmount;

                    student.OutstandingFees += tuition * student.GradePointState.TuitionRateAdjustment(student);
                    db.SaveChanges();
                }
                catch
                {
                    errorCode = -300;
                }
            }

            return errorCode;
        }

        public double? UpdateGrade(double grade, int registrationId, string notes)
        {
            Registration registration = db.Registrations.Where(x => x.RegistrationId == registrationId).SingleOrDefault();

            registration.Grade = grade;
            registration.Notes = notes;
            db.SaveChanges();

            double? gpa = CalculateGradePointAverage(registration.StudentId);

            return gpa;
        }

        /// <summary>
        /// Updates the grade for a student's registration.
        /// </summary>
        /// <param name="grade">The new grade to be assigned.</param>
        /// <param name="registrationId">The ID of the registration.</param>
        /// <param name="notes">Additional notes related to the grade update.</param>
        /// <returns>The updated grade point average, if applicable.</returns>
        private double? CalculateGradePointAverage(int studentId)
        {
            double grade;
            CourseType courseType;
            double gradePoint;
            double gradePointValue;
            double totalCreditHours;
            double totalGradePointValue;
            Double? calculatedGradePointAverage;

            Registration registration =
                (from results in db.Registrations
                where results.StudentId == studentId
               select results).SingleOrDefault();

            return null;
        }
    }
}
