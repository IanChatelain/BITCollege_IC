namespace BITCollegeWindows
{
    partial class Grading
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
            System.Windows.Forms.Label fullNameLabel;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lnkReturn = new System.Windows.Forms.LinkLabel();
            this.lnkUpdate = new System.Windows.Forms.LinkLabel();
            this.lblExisting = new System.Windows.Forms.Label();
            this.studentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.studentNumberMaskedLabel = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.fullNameLabel1 = new System.Windows.Forms.Label();
            studentNumberLabel = new System.Windows.Forms.Label();
            fullNameLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(fullNameLabel);
            this.groupBox1.Controls.Add(this.fullNameLabel1);
            this.groupBox1.Controls.Add(studentNumberLabel);
            this.groupBox1.Controls.Add(this.studentNumberMaskedLabel);
            this.groupBox1.Location = new System.Drawing.Point(66, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 118);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Student Data";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lnkReturn);
            this.groupBox2.Controls.Add(this.lnkUpdate);
            this.groupBox2.Controls.Add(this.lblExisting);
            this.groupBox2.Location = new System.Drawing.Point(130, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 197);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grading Information";
            // 
            // lnkReturn
            // 
            this.lnkReturn.AutoSize = true;
            this.lnkReturn.Location = new System.Drawing.Point(240, 161);
            this.lnkReturn.Name = "lnkReturn";
            this.lnkReturn.Size = new System.Drawing.Size(117, 13);
            this.lnkReturn.TabIndex = 2;
            this.lnkReturn.TabStop = true;
            this.lnkReturn.Text = "Return to Student Data";
            this.lnkReturn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReturn_LinkClicked);
            // 
            // lnkUpdate
            // 
            this.lnkUpdate.AutoSize = true;
            this.lnkUpdate.Location = new System.Drawing.Point(122, 161);
            this.lnkUpdate.Name = "lnkUpdate";
            this.lnkUpdate.Size = new System.Drawing.Size(74, 13);
            this.lnkUpdate.TabIndex = 1;
            this.lnkUpdate.TabStop = true;
            this.lnkUpdate.Text = "Update Grade";
            this.lnkUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdate_LinkClicked);
            // 
            // lblExisting
            // 
            this.lblExisting.AutoSize = true;
            this.lblExisting.Location = new System.Drawing.Point(240, 101);
            this.lblExisting.Name = "lblExisting";
            this.lblExisting.Size = new System.Drawing.Size(174, 13);
            this.lblExisting.TabIndex = 0;
            this.lblExisting.Text = "Existing grades cannot be modified.";
            // 
            // studentBindingSource
            // 
            this.studentBindingSource.DataSource = typeof(BITCollege_IC.Models.Student);
            // 
            // studentNumberLabel
            // 
            studentNumberLabel.AutoSize = true;
            studentNumberLabel.Location = new System.Drawing.Point(40, 32);
            studentNumberLabel.Name = "studentNumberLabel";
            studentNumberLabel.Size = new System.Drawing.Size(87, 13);
            studentNumberLabel.TabIndex = 0;
            studentNumberLabel.Text = "Student Number:";
            // 
            // studentNumberMaskedLabel
            // 
            this.studentNumberMaskedLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.studentNumberMaskedLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "StudentNumber", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.studentNumberMaskedLabel.Location = new System.Drawing.Point(160, 32);
            this.studentNumberMaskedLabel.Mask = "0000-0000";
            this.studentNumberMaskedLabel.Name = "studentNumberMaskedLabel";
            this.studentNumberMaskedLabel.Size = new System.Drawing.Size(100, 23);
            this.studentNumberMaskedLabel.TabIndex = 1;
            this.studentNumberMaskedLabel.Text = "    -";
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.Location = new System.Drawing.Point(353, 32);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new System.Drawing.Size(57, 13);
            fullNameLabel.TabIndex = 2;
            fullNameLabel.Text = "Full Name:";
            // 
            // fullNameLabel1
            // 
            this.fullNameLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "FullName", true));
            this.fullNameLabel1.Location = new System.Drawing.Point(416, 32);
            this.fullNameLabel1.Name = "fullNameLabel1";
            this.fullNameLabel1.Size = new System.Drawing.Size(100, 23);
            this.fullNameLabel1.TabIndex = 3;
            this.fullNameLabel1.Text = "label1";
            // 
            // Grading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Grading";
            this.Text = "Grading";
            this.Load += new System.EventHandler(this.Grading_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lnkReturn;
        private System.Windows.Forms.LinkLabel lnkUpdate;
        private System.Windows.Forms.Label lblExisting;
        private EWSoftware.MaskedLabelControl.MaskedLabel studentNumberMaskedLabel;
        private System.Windows.Forms.BindingSource studentBindingSource;
        private System.Windows.Forms.Label fullNameLabel1;
    }
}