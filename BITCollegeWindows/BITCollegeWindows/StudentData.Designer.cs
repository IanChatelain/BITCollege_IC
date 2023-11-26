namespace BITCollegeWindows
{
    partial class StudentData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label studentNumberLabel;
            System.Windows.Forms.Label dateCreatedLabel;
            System.Windows.Forms.Label outstandingFeesLabel;
            System.Windows.Forms.Label gradePointAverageLabel;
            System.Windows.Forms.Label registrationNumberLabel;
            this.grpStudent = new System.Windows.Forms.GroupBox();
            this.gradePointAverageLabel1 = new System.Windows.Forms.Label();
            this.studentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.outstandingFeesLabel1 = new System.Windows.Forms.Label();
            this.dateCreatedLabel1 = new System.Windows.Forms.Label();
            this.studentNumberMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.lnkUpdateGrade = new System.Windows.Forms.LinkLabel();
            this.lnkViewDetails = new System.Windows.Forms.LinkLabel();
            this.grpRegistration = new System.Windows.Forms.GroupBox();
            this.registrationNumberComboBox = new System.Windows.Forms.ComboBox();
            this.registrationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            studentNumberLabel = new System.Windows.Forms.Label();
            dateCreatedLabel = new System.Windows.Forms.Label();
            outstandingFeesLabel = new System.Windows.Forms.Label();
            gradePointAverageLabel = new System.Windows.Forms.Label();
            registrationNumberLabel = new System.Windows.Forms.Label();
            this.grpStudent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).BeginInit();
            this.grpRegistration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // studentNumberLabel
            // 
            studentNumberLabel.AutoSize = true;
            studentNumberLabel.Location = new System.Drawing.Point(37, 35);
            studentNumberLabel.Name = "studentNumberLabel";
            studentNumberLabel.Size = new System.Drawing.Size(87, 13);
            studentNumberLabel.TabIndex = 0;
            studentNumberLabel.Text = "Student Number:";
            // 
            // dateCreatedLabel
            // 
            dateCreatedLabel.AutoSize = true;
            dateCreatedLabel.Location = new System.Drawing.Point(37, 125);
            dateCreatedLabel.Name = "dateCreatedLabel";
            dateCreatedLabel.Size = new System.Drawing.Size(73, 13);
            dateCreatedLabel.TabIndex = 2;
            dateCreatedLabel.Text = "Date Created:";
            // 
            // outstandingFeesLabel
            // 
            outstandingFeesLabel.AutoSize = true;
            outstandingFeesLabel.Location = new System.Drawing.Point(377, 125);
            outstandingFeesLabel.Name = "outstandingFeesLabel";
            outstandingFeesLabel.Size = new System.Drawing.Size(93, 13);
            outstandingFeesLabel.TabIndex = 4;
            outstandingFeesLabel.Text = "Outstanding Fees:";
            // 
            // gradePointAverageLabel
            // 
            gradePointAverageLabel.AutoSize = true;
            gradePointAverageLabel.Location = new System.Drawing.Point(37, 156);
            gradePointAverageLabel.Name = "gradePointAverageLabel";
            gradePointAverageLabel.Size = new System.Drawing.Size(109, 13);
            gradePointAverageLabel.TabIndex = 6;
            gradePointAverageLabel.Text = "Grade Point Average:";
            // 
            // registrationNumberLabel
            // 
            registrationNumberLabel.AutoSize = true;
            registrationNumberLabel.Location = new System.Drawing.Point(40, 38);
            registrationNumberLabel.Name = "registrationNumberLabel";
            registrationNumberLabel.Size = new System.Drawing.Size(106, 13);
            registrationNumberLabel.TabIndex = 0;
            registrationNumberLabel.Text = "Registration Number:";
            // 
            // grpStudent
            // 
            this.grpStudent.Controls.Add(gradePointAverageLabel);
            this.grpStudent.Controls.Add(this.gradePointAverageLabel1);
            this.grpStudent.Controls.Add(outstandingFeesLabel);
            this.grpStudent.Controls.Add(this.outstandingFeesLabel1);
            this.grpStudent.Controls.Add(dateCreatedLabel);
            this.grpStudent.Controls.Add(this.dateCreatedLabel1);
            this.grpStudent.Controls.Add(studentNumberLabel);
            this.grpStudent.Controls.Add(this.studentNumberMaskedTextBox);
            this.grpStudent.Location = new System.Drawing.Point(35, 47);
            this.grpStudent.Name = "grpStudent";
            this.grpStudent.Size = new System.Drawing.Size(617, 202);
            this.grpStudent.TabIndex = 0;
            this.grpStudent.TabStop = false;
            this.grpStudent.Text = "Student Data";
            // 
            // gradePointAverageLabel1
            // 
            this.gradePointAverageLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradePointAverageLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "GradePointAverage", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.gradePointAverageLabel1.Location = new System.Drawing.Point(152, 156);
            this.gradePointAverageLabel1.Name = "gradePointAverageLabel1";
            this.gradePointAverageLabel1.Size = new System.Drawing.Size(100, 23);
            this.gradePointAverageLabel1.TabIndex = 7;
            this.gradePointAverageLabel1.Text = "label1";
            this.gradePointAverageLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // studentBindingSource
            // 
            this.studentBindingSource.DataSource = typeof(BITCollege_IC.Models.Student);
            // 
            // outstandingFeesLabel1
            // 
            this.outstandingFeesLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outstandingFeesLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "OutstandingFees", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.outstandingFeesLabel1.Location = new System.Drawing.Point(476, 124);
            this.outstandingFeesLabel1.Name = "outstandingFeesLabel1";
            this.outstandingFeesLabel1.Size = new System.Drawing.Size(100, 23);
            this.outstandingFeesLabel1.TabIndex = 5;
            this.outstandingFeesLabel1.Text = "label1";
            this.outstandingFeesLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dateCreatedLabel1
            // 
            this.dateCreatedLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateCreatedLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "DateCreated", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "d"));
            this.dateCreatedLabel1.Location = new System.Drawing.Point(152, 124);
            this.dateCreatedLabel1.Name = "dateCreatedLabel1";
            this.dateCreatedLabel1.Size = new System.Drawing.Size(100, 23);
            this.dateCreatedLabel1.TabIndex = 3;
            this.dateCreatedLabel1.Text = "label1";
            this.dateCreatedLabel1.Click += new System.EventHandler(this.dateCreatedLabel1_Click);
            // 
            // studentNumberMaskedTextBox
            // 
            this.studentNumberMaskedTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "StudentNumber", true));
            this.studentNumberMaskedTextBox.Location = new System.Drawing.Point(152, 32);
            this.studentNumberMaskedTextBox.Mask = "0000-0000";
            this.studentNumberMaskedTextBox.Name = "studentNumberMaskedTextBox";
            this.studentNumberMaskedTextBox.Size = new System.Drawing.Size(100, 20);
            this.studentNumberMaskedTextBox.TabIndex = 1;
            // 
            // lnkUpdateGrade
            // 
            this.lnkUpdateGrade.AutoSize = true;
            this.lnkUpdateGrade.Location = new System.Drawing.Point(197, 464);
            this.lnkUpdateGrade.Name = "lnkUpdateGrade";
            this.lnkUpdateGrade.Size = new System.Drawing.Size(74, 13);
            this.lnkUpdateGrade.TabIndex = 2;
            this.lnkUpdateGrade.TabStop = true;
            this.lnkUpdateGrade.Text = "Update Grade";
            this.lnkUpdateGrade.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdateGrade_LinkClicked);
            // 
            // lnkViewDetails
            // 
            this.lnkViewDetails.AutoSize = true;
            this.lnkViewDetails.Location = new System.Drawing.Point(381, 464);
            this.lnkViewDetails.Name = "lnkViewDetails";
            this.lnkViewDetails.Size = new System.Drawing.Size(65, 13);
            this.lnkViewDetails.TabIndex = 3;
            this.lnkViewDetails.TabStop = true;
            this.lnkViewDetails.Text = "View Details";
            this.lnkViewDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewDetails_LinkClicked);
            // 
            // grpRegistration
            // 
            this.grpRegistration.Controls.Add(registrationNumberLabel);
            this.grpRegistration.Controls.Add(this.registrationNumberComboBox);
            this.grpRegistration.Location = new System.Drawing.Point(35, 269);
            this.grpRegistration.Name = "grpRegistration";
            this.grpRegistration.Size = new System.Drawing.Size(600, 154);
            this.grpRegistration.TabIndex = 1;
            this.grpRegistration.TabStop = false;
            this.grpRegistration.Text = "Registration Data";
            this.grpRegistration.Enter += new System.EventHandler(this.grpRegistration_Enter);
            // 
            // registrationNumberComboBox
            // 
            this.registrationNumberComboBox.DataSource = this.registrationBindingSource;
            this.registrationNumberComboBox.DisplayMember = "RegistrationNumber";
            this.registrationNumberComboBox.FormattingEnabled = true;
            this.registrationNumberComboBox.Location = new System.Drawing.Point(152, 35);
            this.registrationNumberComboBox.Name = "registrationNumberComboBox";
            this.registrationNumberComboBox.Size = new System.Drawing.Size(121, 21);
            this.registrationNumberComboBox.TabIndex = 1;
            this.registrationNumberComboBox.ValueMember = "RegistrationId";
            // 
            // registrationBindingSource
            // 
            this.registrationBindingSource.DataSource = typeof(BITCollege_IC.Models.Registration);
            // 
            // StudentData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 511);
            this.Controls.Add(this.lnkViewDetails);
            this.Controls.Add(this.lnkUpdateGrade);
            this.Controls.Add(this.grpRegistration);
            this.Controls.Add(this.grpStudent);
            this.Name = "StudentData";
            this.Text = "StudentData";
            this.Load += new System.EventHandler(this.StudentData_Load);
            this.grpStudent.ResumeLayout(false);
            this.grpStudent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).EndInit();
            this.grpRegistration.ResumeLayout(false);
            this.grpRegistration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStudent;
        private System.Windows.Forms.LinkLabel lnkUpdateGrade;
        private System.Windows.Forms.LinkLabel lnkViewDetails;
        private System.Windows.Forms.MaskedTextBox studentNumberMaskedTextBox;
        private System.Windows.Forms.BindingSource studentBindingSource;
        private System.Windows.Forms.Label gradePointAverageLabel1;
        private System.Windows.Forms.Label outstandingFeesLabel1;
        private System.Windows.Forms.Label dateCreatedLabel1;
        private System.Windows.Forms.GroupBox grpRegistration;
        private System.Windows.Forms.ComboBox registrationNumberComboBox;
        private System.Windows.Forms.BindingSource registrationBindingSource;
    }
}