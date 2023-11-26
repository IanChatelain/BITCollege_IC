﻿using BITCollege_IC.Data;
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
            //Further code to be added.
        }

        /// <summary>
        /// given: Open grading form passing constructor data.
        /// </summary>
        private void lnkUpdateGrade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
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


            studentBindingSource.DataSource = students.ToList();
            IQueryable<Registration> studentRegistrations = db.Registrations.Where(x => x.StudentId == ((Student)studentBindingSource.Current).StudentId);

            registrationBindingSource.DataSource = studentRegistrations.ToList();
        }

        private void dateCreatedLabel1_Click(object sender, EventArgs e)
        {

        }

        private void grpRegistration_Enter(object sender, EventArgs e)
        {

        }

        private void registrationNumberLabel_Click(object sender, EventArgs e)
        {

        }

        private void creditHoursLabel_Click(object sender, EventArgs e)
        {

        }

        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            // Ensure user has completed requirements for the Mask.
            int studentId = int.Parse(((MaskedTextBox)sender).Text);
            Student student = db.Students.Where(x => x.StudentId == studentId).SingleOrDefault();
            ((Student)studentBindingSource.Current).StudentId)
        }
    }
}
