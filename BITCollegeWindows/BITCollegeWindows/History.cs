using BITCollege_IC.Data;
using BITCollege_IC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class History : Form
    {
        BITCollege_ICContext db = new BITCollege_ICContext();

        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;

        /// <summary>
        /// given:  This constructor will be used when called from the
        /// Student form.  This constructor will receive 
        /// specific information about the student and registration
        /// further code required:  
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public History(ConstructorData constructorData)
        {
            InitializeComponent();

            this.studentBindingSource.DataSource = constructorData.Student;
            this.registrationBindingSource.DataSource = constructorData.Registration;
        }


        /// <summary>
        /// given: This code will navigate back to the Student form with
        /// the specific student and registration data that launched
        /// this form.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to student with the data selected for this form
            StudentData student = new StudentData(constructorData);
            student.MdiParent = this.MdiParent;
            student.Show();
            this.Close();
        }

        /// <summary>
        /// given:  Open this form in top right corner of the frame.
        /// further code required:
        /// </summary>
        private void History_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            try
            {
                var registrationCourses =
                from Registrations in db.Registrations
                join Courses in db.Courses
                on Registrations.CourseId equals Courses.CourseId
                select new
                {
                    RegistrationNumber = Registrations.RegistrationNumber,
                    RegistrationDate = Registrations.RegistrationDate,
                    CourseTitle = Courses.Title,
                    Grade = Registrations.Grade,
                    Notes = Registrations.Notes
                };

                this.registrationBindingSource.DataSource = registrationCourses.ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
