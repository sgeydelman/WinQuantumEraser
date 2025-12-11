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
            this.components = new System.ComponentModel.Container();
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            this.formsPlot3 = new ScottPlot.WinForms.FormsPlot();
            this.btnRunSimulation = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // ═════════════════════════════════════════════════════════════════
            // 1️⃣ PLOT 1 (TOP) - TOTAL SIGNAL (NO POST-SELECTION)
            // ═════════════════════════════════════════════════════════════════
            // 
            // formsPlot1
            // 
            this.formsPlot1.DisplayScale = 0F;
            this.formsPlot1.Location = new System.Drawing.Point(12, 50);
            // ⬇️ CHANGE 1: INCREASED SIZE - Width: 1560px, Height: 280px
            this.formsPlot1.Size = new System.Drawing.Size(1560, 280);  // ← LARGER!
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.TabIndex = 0;

            // ═════════════════════════════════════════════════════════════════
            // 2️⃣ PLOT 2 (BOTTOM LEFT) - WITH ERASER (INTERFERENCE APPEARS)
            // ═════════════════════════════════════════════════════════════════
            // 
            // formsPlot2
            // 
            this.formsPlot2.DisplayScale = 0F;
            this.formsPlot2.Location = new System.Drawing.Point(12, 340);
            // ⬇️ CHANGE 2A: INCREASED SIZE - Width: 770px, Height: 520px
            this.formsPlot2.Size = new System.Drawing.Size(770, 520);  // ← LARGER!
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.TabIndex = 1;

            // ═════════════════════════════════════════════════════════════════
            // 3️⃣ PLOT 3 (BOTTOM RIGHT) - WITHOUT ERASER (NO INTERFERENCE)
            // ═════════════════════════════════════════════════════════════════
            // 
            // formsPlot3
            // 
            this.formsPlot3.DisplayScale = 0F;
            // ⬇️ CHANGE 2B: POSITIONED NEXT TO PLOT 2
            this.formsPlot3.Location = new System.Drawing.Point(792, 340);  // ← X=792 (right side)
            // ⬇️ CHANGE 2B: SAME SIZE AS PLOT 2
            this.formsPlot3.Size = new System.Drawing.Size(770, 520);  // ← LARGER!
            this.formsPlot3.Name = "formsPlot3";
            this.formsPlot3.TabIndex = 2;

            // ═════════════════════════════════════════════════════════════════
            // 4️⃣ BUTTON & LABEL - CONTROLS AT TOP
            // ═════════════════════════════════════════════════════════════════
            // 
            // btnRunSimulation
            // 
            // ⬇️ CHANGE 4A: LARGER, BOLDER BUTTON
            this.btnRunSimulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunSimulation.Location = new System.Drawing.Point(12, 12);
            this.btnRunSimulation.Name = "btnRunSimulation";
            this.btnRunSimulation.Size = new System.Drawing.Size(180, 32);  // ← Bigger button
            this.btnRunSimulation.TabIndex = 3;
            this.btnRunSimulation.Text = "Run Simulation";
            this.btnRunSimulation.UseVisualStyleBackColor = true;
            this.btnRunSimulation.Click += new System.EventHandler(this.btnRunSimulation_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            // ⬇️ CHANGE 4B: LARGER FONT FOR STATUS LABEL
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(200, 19);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(150, 15);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Click 'Run Simulation' to start";

            // ═════════════════════════════════════════════════════════════════
            // FORM SETTINGS - OVERALL WINDOW SIZE
            // ═════════════════════════════════════════════════════════════════
            // 
            // FormQEraser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // ⬇️ CHANGE 3: INCREASED FORM SIZE TO FIT LARGER PLOTS
            this.ClientSize = new System.Drawing.Size(1584, 881);  // ← BIGGER WINDOW!
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRunSimulation);
            this.Controls.Add(this.formsPlot3);
            this.Controls.Add(this.formsPlot2);
            this.Controls.Add(this.formsPlot1);
            this.Name = "FormQEraser";
            this.Text = "Delayed Choice Quantum Eraser Simulation";
            // ⬇️ BONUS: CENTER WINDOW ON SCREEN
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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

/*
═══════════════════════════════════════════════════════════════════════════
SUMMARY OF CHANGES:
═══════════════════════════════════════════════════════════════════════════

1️⃣ PLOT 1 (formsPlot1) - Line ~43:
   • Size changed from (1303, 200) → (1560, 280)
   • Now: Full width, 40% taller

2️⃣ PLOT 2 & 3 (formsPlot2, formsPlot3) - Lines ~54 & ~66:
   • Plot 2: Size changed from (650, 420) → (770, 520)
   • Plot 3: Size changed from (650, 420) → (770, 520)
   • Plot 3: X position from 666 → 792 (better spacing)
   • Now: Side-by-side, 24% taller

3️⃣ FORM SIZE (ClientSize) - Line ~89:
   • Changed from (1327, 682) → (1584, 881)
   • Now: 19% wider, 29% taller
   • Added: StartPosition.CenterScreen (line 95)

4️⃣ BUTTON & LABEL - Lines ~73 & ~82:
   • Button: Font size 10 → 11, Bold style, Size (162,27) → (180,32)
   • Label: Added Font size 9 for consistency

═══════════════════════════════════════════════════════════════════════════
*/