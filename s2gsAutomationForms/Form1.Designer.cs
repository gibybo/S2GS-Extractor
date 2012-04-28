namespace s2gsAutomationForms
{
    partial class Form1
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
            this.extractBtn = new System.Windows.Forms.Button();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.codeTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.urlStatusLabel = new System.Windows.Forms.Label();
            this.resultsTxt = new System.Windows.Forms.TextBox();
            this.serverBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.matchHistoryChkBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tmpFileLink = new System.Windows.Forms.LinkLabel();
            this.hashesChkBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // extractBtn
            // 
            this.extractBtn.Location = new System.Drawing.Point(428, 32);
            this.extractBtn.Name = "extractBtn";
            this.extractBtn.Size = new System.Drawing.Size(54, 30);
            this.extractBtn.TabIndex = 0;
            this.extractBtn.Text = "Extract";
            this.extractBtn.UseVisualStyleBackColor = true;
            this.extractBtn.Click += new System.EventHandler(this.extractBtn_Click);
            // 
            // nameTxt
            // 
            this.nameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTxt.Location = new System.Drawing.Point(11, 34);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(127, 29);
            this.nameTxt.TabIndex = 1;
            // 
            // codeTxt
            // 
            this.codeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTxt.Location = new System.Drawing.Point(144, 34);
            this.codeTxt.Name = "codeTxt";
            this.codeTxt.Size = new System.Drawing.Size(100, 29);
            this.codeTxt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Character Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Character Code";
            // 
            // urlStatusLabel
            // 
            this.urlStatusLabel.AutoSize = true;
            this.urlStatusLabel.Location = new System.Drawing.Point(12, 109);
            this.urlStatusLabel.Name = "urlStatusLabel";
            this.urlStatusLabel.Size = new System.Drawing.Size(59, 13);
            this.urlStatusLabel.TabIndex = 6;
            this.urlStatusLabel.Text = "s2gs URLs";
            // 
            // resultsTxt
            // 
            this.resultsTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsTxt.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultsTxt.Location = new System.Drawing.Point(15, 125);
            this.resultsTxt.Multiline = true;
            this.resultsTxt.Name = "resultsTxt";
            this.resultsTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resultsTxt.Size = new System.Drawing.Size(485, 435);
            this.resultsTxt.TabIndex = 7;
            // 
            // serverBox
            // 
            this.serverBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serverBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.serverBox.FormattingEnabled = true;
            this.serverBox.Location = new System.Drawing.Point(250, 34);
            this.serverBox.Name = "serverBox";
            this.serverBox.Size = new System.Drawing.Size(172, 28);
            this.serverBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Server";
            // 
            // matchHistoryChkBox
            // 
            this.matchHistoryChkBox.AutoSize = true;
            this.matchHistoryChkBox.Location = new System.Drawing.Point(11, 70);
            this.matchHistoryChkBox.Name = "matchHistoryChkBox";
            this.matchHistoryChkBox.Size = new System.Drawing.Size(208, 17);
            this.matchHistoryChkBox.TabIndex = 10;
            this.matchHistoryChkBox.Text = "I\'m already looking at the match history";
            this.matchHistoryChkBox.UseVisualStyleBackColor = true;
            this.matchHistoryChkBox.CheckedChanged += new System.EventHandler(this.matchHistoryChkBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.hashesChkBox);
            this.groupBox1.Controls.Add(this.codeTxt);
            this.groupBox1.Controls.Add(this.matchHistoryChkBox);
            this.groupBox1.Controls.Add(this.nameTxt);
            this.groupBox1.Controls.Add(this.extractBtn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.serverBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 94);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extract";
            // 
            // tmpFileLink
            // 
            this.tmpFileLink.AutoSize = true;
            this.tmpFileLink.Location = new System.Drawing.Point(102, 109);
            this.tmpFileLink.Name = "tmpFileLink";
            this.tmpFileLink.Size = new System.Drawing.Size(60, 13);
            this.tmpFileLink.TabIndex = 12;
            this.tmpFileLink.TabStop = true;
            this.tmpFileLink.Text = "tmpFileLink";
            this.tmpFileLink.Visible = false;
            this.tmpFileLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.tmpFileLink_LinkClicked);
            // 
            // hashesChkBox
            // 
            this.hashesChkBox.AutoSize = true;
            this.hashesChkBox.Location = new System.Drawing.Point(226, 70);
            this.hashesChkBox.Name = "hashesChkBox";
            this.hashesChkBox.Size = new System.Drawing.Size(119, 17);
            this.hashesChkBox.TabIndex = 11;
            this.hashesChkBox.Text = "Display hashes only";
            this.hashesChkBox.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 563);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(510, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLbl
            // 
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(39, 17);
            this.statusLbl.Text = "Status";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 585);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tmpFileLink);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resultsTxt);
            this.Controls.Add(this.urlStatusLabel);
            this.Name = "Form1";
            this.Text = "Gibybo\'s S2GS Extractor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button extractBtn;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.TextBox codeTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label urlStatusLabel;
        private System.Windows.Forms.TextBox resultsTxt;
        private System.Windows.Forms.ComboBox serverBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox matchHistoryChkBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel tmpFileLink;
        private System.Windows.Forms.CheckBox hashesChkBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLbl;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
    }
}

