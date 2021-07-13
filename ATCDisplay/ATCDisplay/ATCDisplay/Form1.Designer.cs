namespace ATCDisplay
{
    partial class ATCDisp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ATCDisp));
            this.AnimTicker = new System.Windows.Forms.Timer(this.components);
            this.btnApproach = new System.Windows.Forms.Button();
            this.btnDepart = new System.Windows.Forms.Button();
            this.btnCollide = new System.Windows.Forms.Button();
            this.CtrlBox = new System.Windows.Forms.TextBox();
            this.CtrlLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AnimTicker
            // 
            this.AnimTicker.Enabled = true;
            this.AnimTicker.Interval = 32;
            this.AnimTicker.Tick += new System.EventHandler(this.AnimTicker_Tick);
            // 
            // btnApproach
            // 
            this.btnApproach.Location = new System.Drawing.Point(1096, 617);
            this.btnApproach.Name = "btnApproach";
            this.btnApproach.Size = new System.Drawing.Size(75, 23);
            this.btnApproach.TabIndex = 0;
            this.btnApproach.Text = "Approach";
            this.btnApproach.UseVisualStyleBackColor = true;
            this.btnApproach.Click += new System.EventHandler(this.BtnApproach_Click);
            // 
            // btnDepart
            // 
            this.btnDepart.Location = new System.Drawing.Point(1177, 617);
            this.btnDepart.Name = "btnDepart";
            this.btnDepart.Size = new System.Drawing.Size(75, 23);
            this.btnDepart.TabIndex = 1;
            this.btnDepart.Text = "Depart";
            this.btnDepart.UseVisualStyleBackColor = true;
            this.btnDepart.Click += new System.EventHandler(this.BtnDepart_Click);
            // 
            // btnCollide
            // 
            this.btnCollide.Location = new System.Drawing.Point(1177, 646);
            this.btnCollide.Name = "btnCollide";
            this.btnCollide.Size = new System.Drawing.Size(75, 23);
            this.btnCollide.TabIndex = 3;
            this.btnCollide.Text = "Collision";
            this.btnCollide.UseVisualStyleBackColor = true;
            this.btnCollide.Click += new System.EventHandler(this.BtnCollide_Click);
            // 
            // CtrlBox
            // 
            this.CtrlBox.Enabled = false;
            this.CtrlBox.Location = new System.Drawing.Point(709, 643);
            this.CtrlBox.Name = "CtrlBox";
            this.CtrlBox.Size = new System.Drawing.Size(119, 20);
            this.CtrlBox.TabIndex = 4;
            this.CtrlBox.KeyUp += this.CtrlBox_KeyUp;
            this.CtrlBox.Visible = false;
            // 
            // CtrlLabel
            // 
            this.CtrlLabel.AutoSize = true;
            this.CtrlLabel.BackColor = System.Drawing.Color.Transparent;
            this.CtrlLabel.ForeColor = System.Drawing.Color.Lime;
            this.CtrlLabel.Location = new System.Drawing.Point(635, 646);
            this.CtrlLabel.Name = "CtrlLabel";
            this.CtrlLabel.Size = new System.Drawing.Size(68, 13);
            this.CtrlLabel.TabIndex = 5;
            this.CtrlLabel.Text = "Enter vector:";
            this.CtrlLabel.Visible = false;
            // 
            // ATCDisp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.CtrlLabel);
            this.Controls.Add(this.CtrlBox);
            this.Controls.Add(this.btnCollide);
            this.Controls.Add(this.btnDepart);
            this.Controls.Add(this.btnApproach);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ATCDisp";
            this.Text = "ATC Display NEXTGEN";
            this.Load += new System.EventHandler(this.ATCDisp_Load);
            this.Click += new System.EventHandler(this.ATCDisp_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ATCDisp_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer AnimTicker;
        private System.Windows.Forms.Button btnApproach;
        private System.Windows.Forms.Button btnDepart;
        private System.Windows.Forms.Button btnCollide;
        private System.Windows.Forms.TextBox CtrlBox;
        private System.Windows.Forms.Label CtrlLabel;
    }
}

