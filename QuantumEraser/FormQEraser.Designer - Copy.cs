namespace QuantumEraser
{
    partial class FormQEraser
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
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            this.formsPlot3 = new ScottPlot.WinForms.FormsPlot();
            this.btnRunSimulation = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.DisplayScale = 0F;
            this.formsPlot1.Location = new System.Drawing.Point(33, 22);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(442, 364);
            this.formsPlot1.TabIndex = 0;
            // 
            // formsPlot2
            // 
            this.formsPlot2.DisplayScale = 0F;
            this.formsPlot2.Location = new System.Drawing.Point(520, 34);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(442, 364);
            this.formsPlot2.TabIndex = 1;
            // 
            // formsPlot3
            // 
            this.formsPlot3.DisplayScale = 0F;
            this.formsPlot3.Location = new System.Drawing.Point(1025, 22);
            this.formsPlot3.Name = "formsPlot3";
            this.formsPlot3.Size = new System.Drawing.Size(442, 364);
            this.formsPlot3.TabIndex = 2;
            // 
            // btnRunSimulation
            // 
            this.btnRunSimulation.Location = new System.Drawing.Point(236, 475);
            this.btnRunSimulation.Name = "btnRunSimulation";
            this.btnRunSimulation.Size = new System.Drawing.Size(125, 63);
            this.btnRunSimulation.TabIndex = 3;
            this.btnRunSimulation.Text = "Run Simulation";
            this.btnRunSimulation.UseVisualStyleBackColor = true;
            this.btnRunSimulation.Click += new System.EventHandler(this.btnRunSimulation_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(520, 504);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "lblStatus";
            // 
            // FormQEraser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1479, 693);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRunSimulation);
            this.Controls.Add(this.formsPlot3);
            this.Controls.Add(this.formsPlot2);
            this.Controls.Add(this.formsPlot1);
            this.Name = "FormQEraser";
            this.Text = "Quantum Eraser Simulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private ScottPlot.WinForms.FormsPlot formsPlot2;
        private ScottPlot.WinForms.FormsPlot formsPlot3;
        private System.Windows.Forms.Button btnRunSimulation;
        private System.Windows.Forms.Label lblStatus;
    }
}

