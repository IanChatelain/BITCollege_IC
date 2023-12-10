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
    public partial class BatchUpdate : Form
    {
        private BITCollege_ICContext db = new BITCollege_ICContext();
        Batch batch = new Batch();

        public BatchUpdate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Batch processing
        /// Further code to be added.
        /// </summary>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (radSelect.Checked)
            {
                batch.ProcessTransmission(this.descriptionComboBox.SelectedValue.ToString());
                this.rtxtLog.Text = batch.WriteLogData();
            }
            else if (radAll.Checked)
            {
                foreach(AcademicProgram item in descriptionComboBox.Items)
                {
                    batch.ProcessTransmission(item.ProgramAcronym);
                    this.rtxtLog.Text += batch.WriteLogData();
                }
            }
        }

        /// <summary>
        /// given:  Always open this form in top right of frame.
        /// Further code to be added.
        /// </summary>
        private void BatchUpdate_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            IQueryable<AcademicProgram> academicPrograms = db.AcademicPrograms;
            this.academicProgramBindingSource.DataSource = academicPrograms.ToList();
        }

        /// <summary>
        /// Handles the radio button change event.
        /// </summary>
        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                this.descriptionComboBox.Enabled = false;
            }
            else
            {
                this.descriptionComboBox.Enabled = true;
            }
        }
    }
}
