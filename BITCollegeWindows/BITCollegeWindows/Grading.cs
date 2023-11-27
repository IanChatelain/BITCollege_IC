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
using Utility;

namespace BITCollegeWindows
{
    public partial class Grading : Form
    {

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
        public Grading(ConstructorData constructor)
        {
            InitializeComponent();
            this.constructorData = constructor;

            this.studentBindingSource.DataSource = constructor.Student;
            this.registrationBindingSource.DataSource = constructor.Registration;

            setState(constructor);
        }

        /// <summary>
        /// given: This code will navigate back to the Student form with
        /// the specific student and registration data that launched
        /// this form.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StudentData student = new StudentData(constructorData);
            student.MdiParent = this.MdiParent;
            student.Show();
            this.Close();
        }

        /// <summary>
        /// given:  Always open in this form in the top right corner of the frame.
        /// further code required:
        /// </summary>
        private void Grading_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            var courseType = constructorData.Registration.Course.CourseType;

            courseNumberMaskedLabel.Mask = BusinessRules.CourseFormat(courseType.ToString());
        }

        /// <summary>
        /// Handles the logic for updating a student grade
        /// </summary>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        /// <summary>
        /// Sets the state of the controls.
        /// </summary>
        /// <param name="constructorData">An object that represents the data source data.</param>
        private void setState(ConstructorData constructorData)
        {
            if(constructorData.Registration.Grade == null)
            {
                setControlsEnabled(true);
            }
            else
            {
                setControlsEnabled(false);
            }

        }

        /// <summary>
        /// Sets the enabled state of the link labels on the Grading form.
        /// </summary>
        /// <param name="enable">A boolean to enable or disable the controls.</param>
        private void setControlsEnabled(bool enable)
        {
            this.lblExisting.Visible = !enable;
            this.gradeTextBox.Enabled = enable;
            this.lnkUpdate.Enabled = enable;
        }
    }
}
