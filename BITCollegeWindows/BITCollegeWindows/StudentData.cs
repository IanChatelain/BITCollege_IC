using BITCollege_IC.Data;
using BITCollege_IC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using System.Globalization;

namespace BITCollegeWindows
{
    public partial class StudentData : Form
    {
        BITCollege_ICContext db = new BITCollege_ICContext();

        ///Given: Student and Registration data will be retrieved
        ///in this form and passed throughout application
        ///These variables will be used to store the current
        ///Student and selected Registration
        ConstructorData constructorData = new ConstructorData();

        /// <summary>
        /// This constructor will be used when this form is opened from
        /// the MDI Frame.
        /// </summary>
        public StudentData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when returning to StudentData
        /// from another form.  This constructor will pass back
        /// specific information about the student and registration
        /// based on activites taking place in another form.
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public StudentData (ConstructorData constructor)
        {
            InitializeComponent();
        }

        /// <summary>
        /// given: Open grading form passing constructor data.
        /// </summary>
        private void lnkUpdateGrade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.constructorData.Student = (Student)studentBindingSource.Current;
            this.constructorData.Registration = (Registration)registrationBindingSource.Current;
            Grading grading = new Grading(constructorData);
            grading.MdiParent = this.MdiParent;
            grading.Show();
            this.Close();
        }


        /// <summary>
        /// given: Open history form passing constructor data.
        /// </summary>
        private void lnkViewDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.constructorData.Student = (Student)studentBindingSource.Current;
            this.constructorData.Registration = (Registration)registrationBindingSource.Current;
            History history = new History(constructorData);
            history.MdiParent = this.MdiParent;
            history.Show();
            this.Close();
        }

        /// <summary>
        /// given:  Opens the form in top right corner of the frame.
        /// </summary>
        private void StudentData_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);

            IQueryable<Student> students = from results in db.Students select results;
            //IQueryable<Registration> registrations = from results in db.Registrations select results;


            this.studentBindingSource.DataSource = students.ToList();
            IQueryable<Registration> studentRegistrations = db.Registrations.Where(x => x.StudentId == ((Student)studentBindingSource.Current).StudentId);

            this.registrationBindingSource.DataSource = studentRegistrations.ToList();
        }

        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            // TODO: This is being fired by form closing as well which is causing message box to pop up.
            MaskedTextBox studentNumberTextBox = (MaskedTextBox)sender;

            if (!studentNumberTextBox.MaskCompleted)
            {
                MessageBox.Show("Please complete the student number.", "Incomplete Input", MessageBoxButtons.OK);
                studentNumberTextBox.Focus();
                setState(studentNumberTextBox);
                return;
            }

            if (Numeric.IsNumeric(studentNumberTextBox.Text, NumberStyles.Integer))
            {
                int studentNum = int.Parse(studentNumberTextBox.Text);
                Student student = db.Students.Where(x => x.StudentNumber == studentNum).SingleOrDefault();

                if (student == null)
                {
                    string message = String.Format("Student {0} does not exist", studentNum);
                    setState(studentNumberTextBox);
                    MessageBox.Show(message, "Invalid Student Number", MessageBoxButtons.OK);
                }
                else
                {
                    this.studentBindingSource.DataSource = student;
                    IQueryable<Registration> studentRegistrations = db.Registrations.Where(x => x.StudentId == student.StudentId);

                    if (studentRegistrations == null)
                    {
                        setState(studentNumberTextBox);
                    }
                    else
                    {
                        this.registrationBindingSource.DataSource = studentRegistrations.ToList();
                        setControlsEnabled(true);
                    }
                }
            }
        }

        /// <summary>
        /// Sets initial state of the StudentData form.
        /// </summary>
        /// <param name="maskedTextBox">A form control that will be set to focus.</param>
        private void setState(Control controlToFocus = null)
        {
            if (controlToFocus != null)
            {
                controlToFocus.Focus();
            }

            setControlsEnabled(false);
            studentBindingSource.DataSource = typeof(Student);
            registrationBindingSource.DataSource = typeof(Registration);
        }

        /// <summary>
        /// Sets the enabled state of the link labels on the StudentData form.
        /// </summary>
        /// <param name="enable">A boolean to enable or disable the controls.</param>
        private void setControlsEnabled(bool enable)
        {
            lnkViewDetails.Enabled = enable;
            lnkUpdateGrade.Enabled = enable;
        }
    }
}
