using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using BITCollege_IC.Data;
using BITCollege_IC.Models;
using Microsoft.SqlServer.Server;
using Utility;
using System.Globalization;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    /// <summary>
    /// Batch:  This class provides functionality that will validate
    /// and process incoming xml files.
    /// </summary>
    public class Batch
    {
        private BITCollege_ICContext db = new BITCollege_ICContext();
        private String inputFileName;
        private String logFileName;
        private String logData;

        /// <summary>
        /// Processes all detail errors found within the current file being processed,
        /// and appends the log file with a message accordingly.
        /// </summary>
        /// <param name="beforeQuery">The records that existed before the round of validation.</param>
        /// <param name="afterQuery">The records that existed after the round of validation.</param>
        /// <param name="message">The error message that is to be written to the log file.</param>
        private void ProcessErrors(IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, String message)
        {
            IEnumerable<XElement> errors = beforeQuery.Except(afterQuery);

            foreach (XElement item in errors)
            {
                logData += "\r\n-----------ERROR-----------";
                logData += "\r\nFile: " + this.inputFileName;
                logData += "\r\nProgram: " + item.Element("program");
                logData += "\r\nStudent Number: " + item.Element("student_no");
                logData += "\r\nCourse Number: " + item.Element("course_no");
                logData += "\r\nRegistration Number: " + item.Element("registration_no");
                logData += "\r\nType: " + item.Element("type");
                logData += "\r\nGrade: " + item.Element("grade");
                logData += "\r\nNotes: " + item.Element("notes");
                logData += "\r\nNode Count: " + item.Nodes().Count();
                logData += "\r\nError Message: " + message;
                logData += "\r\n---------------------------";
            }
        }

        /// <summary>
        /// Verifies the attributes of the XML file's root element.
        /// </summary>
        private void ProcessHeader()
        {
            int checksum = 0;
            XDocument xDocument = XDocument.Load(this.inputFileName);
            XElement rootElement = xDocument.Element("student_update");
            IEnumerable<XElement> transaction = xDocument.Descendants("transaction");

            DateTime rootElementDate = DateTime.Parse(rootElement.Attribute("date").Value);
            string   rootElementProgram = rootElement.Attribute("program").Value;
            int      rootElementChecksum = int.Parse(rootElement.Attribute("checksum").Value);

            AcademicProgram academicPrograms = db.AcademicPrograms.Where(x => 
                x.ProgramAcronym == rootElementProgram).SingleOrDefault();

            foreach (XElement xele in transaction)
            {
                checksum += int.Parse(xele.Element("student_no").Value);
            }

            if (rootElement.Attributes().Count() != 3)
            {
                throw new Exception("Incorrect number of root elements");
            }

            int rootElementYear = rootElementDate.Year;
            int rootElementDays = rootElementDate.DayOfYear;

            if (rootElementYear != DateTime.Now.Year || rootElementDays != DateTime.Now.DayOfYear)
            {
                throw new Exception("Incorrect date.");
            }

            if (academicPrograms == null || rootElementProgram != academicPrograms.ProgramAcronym)
            {
                throw new Exception("Invalid program acronym.");
            }

            if (rootElementChecksum != checksum)
            {
                throw new Exception("Checksum failure.");
            }
        }

        /// <summary>
        /// Verifies the content of the detail records in the XML file.
        /// </summary>
        private void ProcessDetails()
        {
            XDocument xDocument = XDocument.Load(this.inputFileName);

            IEnumerable<XElement> first = xDocument.Descendants().Elements("transaction");

            // First validation - Node count must be 7.
            IEnumerable<XElement> second = first.Where(x => 
                x.Nodes().Count() == 7);

            ProcessErrors(first, second, "Node count is not 7");

            // Second validation - Program acronym must match root attribute program.
            IEnumerable<XElement> third = second.Where(x => 
                x.Element("program").Value == xDocument.Root.Attribute("program").Value);

            ProcessErrors(second, third, "Program acronym does not match root program");

            // Third validation - Type must be numeric.
            IEnumerable<XElement> fourth = third.Where(x => 
                Numeric.IsNumeric(x.Element("type").Value, NumberStyles.Number));

            ProcessErrors(third, fourth, "Type is not numeric.");

            // Fourth validation - Grade must be numeric or *.
            IEnumerable<XElement> fifth = fourth.Where(x => 
                Numeric.IsNumeric(x.Element("grade").Value, NumberStyles.Number) || 
                x.Element("grade").Value == "*");

            ProcessErrors(fourth, fifth, "Grade is not numeric or '*'.");

            // Fifth validation - Type must be 1 or 2.
            IEnumerable<XElement> sixth = fifth.Where(x => 
                int.Parse(x.Element("type").Value) == 1 || 
                int.Parse(x.Element("type").Value) == 2);

            ProcessErrors(fifth, sixth, "Type is not 1 or 2");

            // Sixth validation - Grade must be * when type is 1, and 0-100 when type is 2.
            IEnumerable<XElement> seventh = sixth.Where(x => 
                (int.Parse(x.Element("type").Value) == 1 && x.Element("grade").Value == "*") || 
                (int.Parse(x.Element("type").Value) == 2 && double.Parse(x.Element("grade").Value) >= 0 &&
                 double.Parse(x.Element("grade").Value) <= 100));

            ProcessErrors(sixth, seventh, "Grade does not match type, or does not exist.");

            // Seventh validation - Student number must match existing student in the database.
            IEnumerable<long> studentNumbers = db.Students.Select(x => x.StudentNumber).ToList();
            IEnumerable<XElement> eighth = seventh.Where(x => 
                studentNumbers.Contains(long.Parse(x.Element("student_no").Value)));

            ProcessErrors(seventh, eighth, "Student number does not exist.");

            // Eighth validation - Course number must match existing course in the database.
            IEnumerable<string> courseNumbers = db.Courses.Select(x => x.CourseNumber).ToList();
            IEnumerable<XElement> ninth = eighth.Where(x =>
                int.Parse(x.Element("type").Value) == 1 && courseNumbers.Contains(x.Element("course_no").Value) ||
                int.Parse(x.Element("type").Value) == 2 && x.Element("course_no").Value == "*");

            ProcessErrors(eighth, ninth, "Course number does not match type, or does not exist.");

            // Ninth validation - Registration number does not match existing registration in the database.
            IEnumerable<long> registrationNumbers = db.Registrations.Select(x => x.RegistrationNumber).ToList();
            IEnumerable<XElement> tenth = ninth.Where(x =>
                int.Parse(x.Element("type").Value) == 2 && registrationNumbers.Contains(long.Parse(x.Element("registration_no").Value)) ||
                int.Parse(x.Element("type").Value) == 1 && x.Element("registration_no").Value == "*");

            ProcessErrors(ninth, tenth, "Registration does not match type, or does not exist.");

            // Call the transaction method with the remaining transactions.
            ProcessTransactions(tenth);
        }

        /// <summary>
        /// Processes all valid transaction records.
        /// </summary>
        /// <param name="transactionRecords">Collection of transaction valid elements.</param>
        private void ProcessTransactions(IEnumerable<XElement> transactionRecords)
        {
            RegistrationService.CollegeRegistrationClient registrationService = new RegistrationService.CollegeRegistrationClient();

            foreach (XElement item in transactionRecords)
            {
                int type = int.Parse(item.Element("type").Value);

                if (type == 1)
                {
                    string notes1 = item.Element("notes").Value;
                    long studentNumber = int.Parse(item.Element("student_no").Value);
                    string courseNumber = item.Element("course_no").Value;

                    int studentId = db.Students.Where(x =>
                        x.StudentNumber == studentNumber).SingleOrDefault().StudentId;

                    int courseId = db.Courses.Where(x =>
                        x.CourseNumber == courseNumber).SingleOrDefault().CourseId;

                    int errorCode = registrationService.RegisterCourse(studentId, courseId, notes1);
                    if (errorCode == 0)
                    {
                        this.logData += String.Format("\r\nStudent: {0} has successfully registered for course: {1}", studentNumber, courseNumber);
                    }
                    else
                    {
                        this.logData += String.Format("\r\nREGISTRATION ERROR: {0}", BusinessRules.RegisterError(errorCode));
                    }

                }

                if (type == 2)
                {
                    string notes2 = item.Element("notes").Value;
                    double grade = double.Parse(item.Element("grade").Value) / 100;
                    long registrationNumber = int.Parse(item.Element("registration_no").Value);

                    int registrationId = db.Registrations.Where(x =>
                        x.RegistrationNumber == registrationNumber).SingleOrDefault().RegistrationId;

                    double? updatedGrade = registrationService.UpdateGrade(grade, registrationId, notes2);

                    if (updatedGrade != null)
                    {
                        this.logData += String.Format("\r\nA grade of: {0} has successfully applied to registration: {1}", updatedGrade, registrationNumber);
                    }
                    else
                    {
                        this.logData += String.Format("\r\nUPDATE GRADE ERRORR");
                    }
                }
            }
        }

        /// <summary>
        /// Writes accumulated log data to a log file.
        /// </summary>
        /// <returns>Error log data from current transmission file.</returns>
        public String WriteLogData()
        {
            StreamWriter streamWriter = new StreamWriter(this.logFileName);

            streamWriter.WriteLine(logData);

            streamWriter.Close();

            string localLogData = this.logData;

            this.logData = string.Empty;
            this.logFileName = string.Empty;

            return localLogData;
        }

        /// <summary>
        /// Verifies XML file existance and file name, and initiate batch process.
        /// </summary>
        /// <param name="programAcronym">The program acronym.</param>
        public void ProcessTransmission(String programAcronym)
        {
            inputFileName = String.Format("{0}-{1}-{2}.xml", DateTime.Now.Year, DateTime.Now.DayOfYear, programAcronym);
            this.logFileName = String.Format("LOG {0}", this.inputFileName.Replace("xml", "txt"));

            if (File.Exists(inputFileName))
            {
                try
                {
                    this.ProcessHeader();
                    this.ProcessDetails();
                }
                catch (Exception e)
                {
                    logData += String.Format("\r\nError: {0}", e.Message);
                    //logData += String.Format("\r\nStack Trace: {0}", e.StackTrace);
                }
            }
            else
            {
                logData += String.Format("\r\nXML file '{0}' does not exist.", this.inputFileName);
            }
        }
    }
}
