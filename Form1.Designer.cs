namespace _100_CPU
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelLog = new System.Windows.Forms.Panel();
            this.richTextLog = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.comboBoxProcName = new System.Windows.Forms.ComboBox();
            this.textBoxRuntime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCollect = new System.Windows.Forms.Button();
            this.panelLog.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLog
            // 
            this.panelLog.Controls.Add(this.richTextLog);
            this.panelLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLog.Location = new System.Drawing.Point(0, 88);
            this.panelLog.Name = "panelLog";
            this.panelLog.Size = new System.Drawing.Size(1496, 533);
            this.panelLog.TabIndex = 0;
            // 
            // richTextLog
            // 
            this.richTextLog.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextLog.Location = new System.Drawing.Point(0, 0);
            this.richTextLog.Name = "richTextLog";
            this.richTextLog.Size = new System.Drawing.Size(1496, 533);
            this.richTextLog.TabIndex = 0;
            this.richTextLog.Text = "";
            this.richTextLog.WordWrap = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuCopy});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(164, 28);
            this.contextMenuStrip1.Text = "Copy";
            // 
            // toolStripMenuCopy
            // 
            this.toolStripMenuCopy.Name = "toolStripMenuCopy";
            this.toolStripMenuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuCopy.Size = new System.Drawing.Size(163, 24);
            this.toolStripMenuCopy.Text = "Copy";
            this.toolStripMenuCopy.Click += new System.EventHandler(this.toolStripMenuCopy_Click);
            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Location = new System.Drawing.Point(674, 13);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(75, 23);
            this.buttonAnalyze.TabIndex = 1;
            this.buttonAnalyze.Text = "Analyze ";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            this.buttonAnalyze.Click += new System.EventHandler(this.buttonAnalyze_Click);
            // 
            // comboBoxProcName
            // 
            this.comboBoxProcName.FormattingEnabled = true;
            this.comboBoxProcName.Location = new System.Drawing.Point(378, 12);
            this.comboBoxProcName.Name = "comboBoxProcName";
            this.comboBoxProcName.Size = new System.Drawing.Size(271, 24);
            this.comboBoxProcName.TabIndex = 2;
            // 
            // textBoxRuntime
            // 
            this.textBoxRuntime.Location = new System.Drawing.Point(164, 43);
            this.textBoxRuntime.Name = "textBoxRuntime";
            this.textBoxRuntime.Size = new System.Drawing.Size(61, 22);
            this.textBoxRuntime.TabIndex = 3;
            this.textBoxRuntime.Text = "15";
            this.textBoxRuntime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Log Time";
            // 
            // buttonCollect
            // 
            this.buttonCollect.Location = new System.Drawing.Point(26, 12);
            this.buttonCollect.Name = "buttonCollect";
            this.buttonCollect.Size = new System.Drawing.Size(93, 53);
            this.buttonCollect.TabIndex = 5;
            this.buttonCollect.Text = "Collect Log";
            this.buttonCollect.UseVisualStyleBackColor = true;
            this.buttonCollect.Click += new System.EventHandler(this.buttonCollect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1496, 621);
            this.Controls.Add(this.buttonCollect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRuntime);
            this.Controls.Add(this.comboBoxProcName);
            this.Controls.Add(this.buttonAnalyze);
            this.Controls.Add(this.panelLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "100 CPU";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelLog.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.RichTextBox richTextLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCopy;
        private System.Windows.Forms.Button buttonAnalyze;
        private System.Windows.Forms.ComboBox comboBoxProcName;
        private System.Windows.Forms.TextBox textBoxRuntime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCollect;
    }
}

