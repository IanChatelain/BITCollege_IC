using BITCollege_IC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICollegeRegistration" in both code and config file together.
    [ServiceContract]
    public interface ICollegeRegistration
    {
        [OperationContract]
        void DoWork();

        /// <summary>
        /// Drops a course for a student.
        /// </summary>
        /// <param name="registrationId">The ID of the registration to be dropped.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        [OperationContract]
        bool DropCourse(int registrationId);

        /// <summary>
        /// Registers a course for a student.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="courseId">The ID of the course to register.</param>
        /// <param name="notes">Additional notes for the registration.</param>
        /// <returns>An error code representing the status of the registration.</returns>
        [OperationContract]
        int RegisterCourse(int studentId, int courseId, String notes);

        /// <summary>
        /// Updates the grade for a student's registration.
        /// </summary>
        /// <param name="grade">The new grade to be assigned.</param>
        /// <param name="registrationId">The ID of the registration.</param>
        /// <param name="notes">Additional notes related to the grade update.</param>
        /// <returns>The updated grade point average, if applicable.</returns>
        [OperationContract]
        double? UpdateGrade(double grade, int registrationId, String notes);
    }
}
