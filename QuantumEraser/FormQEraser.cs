using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.WinForms;

namespace QuantumEraser
{
    // ============================================================================
    // QUANTUM ERASER SIMULATOR CORE LOGIC
    // ============================================================================

    public class QuantumEraserSimulator
    {
        private Random _random;

        public QuantumEraserSimulator()
        {
            _random = new Random();
        }

        public struct MeasurementResult
        {
            public int SignalBit;  // 0 or 1
            public int IdlerBit;   // 0 or 1
            public bool WasErased; // true if eraser was applied

            public MeasurementResult(int signal, int idler, bool erased)
            {
                SignalBit = signal;
                IdlerBit = idler;
                WasErased = erased;
            }
        }

        /// <summary>
        /// Verify that both branches show complementary interference
        /// </summary>
        public void VerifyComplementaryPatterns(double phi)
        {
            int shots = 10000;
            int count_sig0_idl0 = 0;
            int count_sig0_idl1 = 0;

            for (int i = 0; i < shots; i++)
            {
                bool signalIs0 = _random.NextDouble() < 0.5;
                bool eraseWhichPath = true;  // Always use eraser

                if (signalIs0)
                {
                    double prob0 = 0.5 * (1 - Math.Cos(phi));
                    int idlerBit = _random.NextDouble() < prob0 ? 0 : 1;
                    if (idlerBit == 0) count_sig0_idl0++;
                    else count_sig0_idl1++;
                }
            }

            double theoretical_00 = shots * 0.5 * 0.5 * (1 - Math.Cos(phi));
            double theoretical_01 = shots * 0.5 * 0.5 * (1 + Math.Cos(phi));

            System.Diagnostics.Debug.WriteLine($"φ={phi:F2}: Sig0Idl0={count_sig0_idl0} (expect {theoretical_00:F0}), " +
                                               $"Sig0Idl1={count_sig0_idl1} (expect {theoretical_01:F0})");
        }

        /// <summary>
        /// Simulates one shot of the delayed choice quantum eraser
        /// TRUE delayed choice: random decision made during execution
        /// CORRECTED: Both signal |0⟩ and |1⟩ show complementary interference
        /// </summary>
        private MeasurementResult SimulateOneShot(double phi)
        {
            // STEP 1: Create entangled state (|00⟩ + |11⟩)/√2
            // Signal and idler are perfectly correlated

            // STEP 2: Apply phase shift to signal path
            // State becomes: (|00⟩ + e^(iφ)|11⟩)/√2

            // STEP 3: Measure signal FIRST (before delayed choice)
            // This is KEY - signal measured before we decide to erase or not
            bool signalIs0 = _random.NextDouble() < 0.5;

            // STEP 4: DELAYED CHOICE - randomly decide to erase or keep which-path info
            // This happens AFTER signal is already measured!
            bool eraseWhichPath = _random.NextDouble() < 0.5;

            int signalBit = signalIs0 ? 0 : 1;
            int idlerBit;

            if (eraseWhichPath)
            {
                // ERASE: Measure idler in diagonal basis (|+⟩, |−⟩)
                // This erases which-path information
                // BOTH signal states show interference (complementary patterns)

                if (signalIs0)
                {
                    // Signal |0⟩: Interference with (1 - cos(φ)) dependence
                    // Complementary to signal |1⟩ pattern
                    double prob0 = 0.5 * (1 - Math.Cos(phi));
                    idlerBit = _random.NextDouble() < prob0 ? 0 : 1;
                }
                else
                {
                    // Signal |1⟩: Interference with (1 + cos(φ)) dependence  
                    // Complementary to signal |0⟩ pattern
                    double prob0 = 0.5 * (1 + Math.Cos(phi));
                    idlerBit = _random.NextDouble() < prob0 ? 0 : 1;
                }

                // NOTE: The patterns are complementary and add up to constant:
                // For any given phi and idler result:
                // P(sig=0) × (1-cos φ)/2 + P(sig=1) × (1+cos φ)/2 = 1/2
                // This is why total signal (no post-selection) is FLAT!
            }
            else
            {
                // KEEP: Measure idler in computational basis (|0⟩, |1⟩)
                // This preserves which-path information
                // Due to entanglement: if signal=0 then idler=0, if signal=1 then idler=1
                idlerBit = signalBit;
            }

            return new MeasurementResult(signalBit, idlerBit, eraseWhichPath);
        }

        /// <summary>
        /// Run the full delayed choice quantum eraser experiment
        /// </summary>
        public Dictionary<double, Dictionary<string, int>> RunExperiment(
            double[] phiValues,
            int shotsPerPhi)
        {
            var results = new Dictionary<double, Dictionary<string, int>>();

            foreach (var phi in phiValues)
            {
                // Initialize counters for this phase value
                var counts = new Dictionary<string, int>
                {
                    ["erase_00"] = 0,  // Eraser active, signal=0, idler=0
                    ["erase_01"] = 0,  // Eraser active, signal=0, idler=1
                    ["erase_10"] = 0,  // Eraser active, signal=1, idler=0
                    ["erase_11"] = 0,  // Eraser active, signal=1, idler=1
                    ["keep_00"] = 0,   // Which-path kept, signal=0, idler=0
                    ["keep_01"] = 0,   // Which-path kept, signal=0, idler=1
                    ["keep_10"] = 0,   // Which-path kept, signal=1, idler=0
                    ["keep_11"] = 0    // Which-path kept, signal=1, idler=1
                };

                // Run many shots for this phase value
                for (int shot = 0; shot < shotsPerPhi; shot++)
                {
                    // CRITICAL: Random choice made HERE during execution
                    // This is TRUE delayed choice quantum eraser!
                    var result = SimulateOneShot(phi);

                    // Categorize result by eraser state and measurement outcomes
                    string key = $"{(result.WasErased ? "erase" : "keep")}_{result.SignalBit}{result.IdlerBit}";
                    counts[key]++;
                }

                results[phi] = counts;
            }

            return results;
        }

        /// <summary>
        /// Data structure for plot data
        /// </summary>
        public class PlotData
        {
            public double[] PhiValues { get; set; }

            // With eraser (should show interference)
            public double[] EraseSig0Idl0 { get; set; }
            public double[] EraseSig0Idl1 { get; set; }

            // Without eraser (should be flat - no interference)
            public double[] KeepSig0Idl0 { get; set; }
            public double[] KeepSig0Idl1 { get; set; }

            // Total (should be flat - no interference in raw data)
            public double[] TotalSig0 { get; set; }
        }

        /// <summary>
        /// Extract data for plotting from raw results
        /// </summary>
        public PlotData ExtractPlotData(Dictionary<double, Dictionary<string, int>> results)
        {
            var phiArray = results.Keys.OrderBy(x => x).ToArray();
            int n = phiArray.Length;

            var data = new PlotData
            {
                PhiValues = phiArray,
                EraseSig0Idl0 = new double[n],
                EraseSig0Idl1 = new double[n],
                KeepSig0Idl0 = new double[n],
                KeepSig0Idl1 = new double[n],
                TotalSig0 = new double[n]
            };

            for (int i = 0; i < n; i++)
            {
                var phi = phiArray[i];
                var counts = results[phi];

                // Extract counts for signal |0⟩ only (for simplicity)
                data.EraseSig0Idl0[i] = counts["erase_00"];
                data.EraseSig0Idl1[i] = counts["erase_01"];
                data.KeepSig0Idl0[i] = counts["keep_00"];
                data.KeepSig0Idl1[i] = counts["keep_01"];

                // Total = all signal |0⟩ measurements (eraser + no eraser)
                data.TotalSig0[i] = counts["erase_00"] + counts["erase_01"] +
                                     counts["keep_00"] + counts["keep_01"];
            }

            // Apply smoothing to reduce noise
         //   data.EraseSig0Idl0 = ApplyMovingAverage(data.EraseSig0Idl0, windowSize: 3);
        //    data.EraseSig0Idl1 = ApplyMovingAverage(data.EraseSig0Idl1, windowSize: 3);

            return data;
        }

        /// <summary>
        /// Apply moving average smoothing to reduce noise
        /// </summary>
        private double[] ApplyMovingAverage(double[] data, int windowSize)
        {
            if (windowSize <= 1) return data;

            int n = data.Length;
            double[] smoothed = new double[n];
            int halfWindow = windowSize / 2;

            for (int i = 0; i < n; i++)
            {
                int start = Math.Max(0, i - halfWindow);
                int end = Math.Min(n - 1, i + halfWindow);
                double sum = 0;
                int count = 0;

                for (int j = start; j <= end; j++)
                {
                    sum += data[j];
                    count++;
                }

                smoothed[i] = sum / count;
            }

            return smoothed;
        }
    }

    // ============================================================================
    // SCOTTPLOT PLOTTING HELPER CLASS
    // ============================================================================

    public class QuantumEraserPlotter
    {
        /// <summary>
        /// Create all three plots showing different aspects of quantum eraser
        /// </summary>
        public void CreatePlots(
            FormsPlot plotTotal,
            FormsPlot plotWithEraser,
            FormsPlot plotWithoutEraser,
            QuantumEraserSimulator.PlotData data)
        {
            CreateTotalPlot(plotTotal, data);
            CreateEraserPlot(plotWithEraser, data);
            CreateNoEraserPlot(plotWithoutEraser, data);
        }

        /// <summary>
        /// Plot 1: Total signal (no post-selection) - should be FLAT
        /// </summary>
        private void CreateTotalPlot(FormsPlot formsPlot, QuantumEraserSimulator.PlotData data)
        {
            formsPlot.Plot.Clear();

            // Main data line
            var scatter = formsPlot.Plot.Add.Scatter(data.PhiValues, data.TotalSig0);
            scatter.LineWidth = 3.0f;
            scatter.Color = ScottPlot.Colors.Black;
            scatter.MarkerSize = 7;
            scatter.LegendText = "All signal |0⟩ detections";

            // Add average reference line
            double avg = data.TotalSig0.Average();
            var hline = formsPlot.Plot.Add.HorizontalLine(avg);
            hline.Color = ScottPlot.Colors.Gray;
            hline.LinePattern = LinePattern.Dashed;
            hline.LineWidth = 2;
            hline.LegendText = $"Average: {avg:F0}";

            // Labels and formatting
            formsPlot.Plot.Title("1. TOTAL Signal (No Post-Selection)\n→ NO INTERFERENCE (flat line)");
            formsPlot.Plot.XLabel("Phase φ (radians) = Screen Position");
            formsPlot.Plot.YLabel("Detection Counts");

            formsPlot.Plot.Legend.IsVisible = true;
            formsPlot.Plot.Axes.AutoScale();

            formsPlot.Refresh();
        }

        /// <summary>
        /// Plot 2: With eraser (post-selected) - should show INTERFERENCE
        /// </summary>
        private void CreateEraserPlot(FormsPlot formsPlot, QuantumEraserSimulator.PlotData data)
        {
            formsPlot.Plot.Clear();

            // Add theoretical curves (smooth overlay)
            var phiDense = Enumerable.Range(0, 200)
                .Select(i => i * 4.0 * Math.PI / 199).ToArray();

            double avgCount = (data.EraseSig0Idl0.Average() + data.EraseSig0Idl1.Average()) / 2;

            var theoreticalHigh = phiDense.Select(p => avgCount * (1 + Math.Cos(p))).ToArray();
            var theoreticalLow = phiDense.Select(p => avgCount * (1 - Math.Cos(p))).ToArray();

            var theoryHigh = formsPlot.Plot.Add.Scatter(phiDense, theoreticalHigh);
            theoryHigh.LineWidth = 2f;
            theoryHigh.Color = ScottPlot.Colors.Gray.WithAlpha(0.4);
            theoryHigh.LinePattern = LinePattern.Dotted;
            theoryHigh.LegendText = "Theory (1+cos φ)";

            var theoryLow = formsPlot.Plot.Add.Scatter(phiDense, theoreticalLow);
            theoryLow.LineWidth = 2f;
            theoryLow.Color = ScottPlot.Colors.Gray.WithAlpha(0.4);
            theoryLow.LinePattern = LinePattern.Dotted;
            theoryLow.LegendText = "Theory (1-cos φ)";

            // Experimental data (on top of theory)
            var scatter1 = formsPlot.Plot.Add.Scatter(data.PhiValues, data.EraseSig0Idl0);
            scatter1.LineWidth = 3.0f;
            scatter1.Color = ScottPlot.Colors.Green;
            scatter1.MarkerSize = 7;
            scatter1.LegendText = "Data: Idler |0⟩";

            var scatter2 = formsPlot.Plot.Add.Scatter(data.PhiValues, data.EraseSig0Idl1);
            scatter2.LineWidth = 3.0f;
            scatter2.Color = ScottPlot.Colors.Orange;
            scatter2.MarkerSize = 7;
            scatter2.LegendText = "Data: Idler |1⟩";

            // Labels and formatting
            formsPlot.Plot.Title("2. WITH Eraser: Post-Selected by Idler State\n→ INTERFERENCE APPEARS! (oscillating curves)");
            formsPlot.Plot.XLabel("Phase φ (radians) = Screen Position");
            formsPlot.Plot.YLabel("Detection Counts");

            formsPlot.Plot.Legend.IsVisible = true;
            formsPlot.Plot.Axes.AutoScale();

            formsPlot.Refresh();
        }

        /// <summary>
        /// Plot 3: Without eraser (post-selected) - should be FLAT
        /// </summary>
        private void CreateNoEraserPlot(FormsPlot formsPlot, QuantumEraserSimulator.PlotData data)
        {
            formsPlot.Plot.Clear();

            // Two flat curves (which-path info preserved)
            var scatter1 = formsPlot.Plot.Add.Scatter(data.PhiValues, data.KeepSig0Idl0);
            scatter1.LineWidth = 3.0f;
            scatter1.Color = ScottPlot.Colors.Magenta;
            scatter1.MarkerSize = 7;
            scatter1.LegendText = "Idler |0⟩ (path A known)";

            var scatter2 = formsPlot.Plot.Add.Scatter(data.PhiValues, data.KeepSig0Idl1);
            scatter2.LineWidth = 3.0f;
            scatter2.Color = ScottPlot.Colors.Cyan;
            scatter2.MarkerSize = 7;
            scatter2.LegendText = "Idler |1⟩ (path B known)";

            // Labels and formatting
            formsPlot.Plot.Title("3. WITHOUT Eraser: Which-Path Information Preserved\n→ NO INTERFERENCE (flat lines)");
            formsPlot.Plot.XLabel("Phase φ (radians) = Screen Position");
            formsPlot.Plot.YLabel("Detection Counts");

            formsPlot.Plot.Legend.IsVisible = true;
            formsPlot.Plot.Axes.AutoScale();

            formsPlot.Refresh();
        }
    }

    // ============================================================================
    // WINFORMS IMPLEMENTATION
    // ============================================================================

    public partial class FormQEraser : Form
    {
        private QuantumEraserSimulator _simulator;
        private QuantumEraserPlotter _plotter;

        public FormQEraser()
        {
            InitializeComponent();

            // Initialize simulator and plotter
            _simulator = new QuantumEraserSimulator();
            _plotter = new QuantumEraserPlotter();

            // Set initial status
            if (lblStatus != null)
            {
                lblStatus.Text = "Ready. Click 'Run Simulation' to start the Delayed Choice Quantum Eraser experiment.";
                lblStatus.ForeColor = System.Drawing.Color.Blue;
            }
        }

        /// <summary>
        /// Button click handler - runs the quantum eraser simulation
        /// This should be connected to your btnRunSimulation button in the designer
        /// </summary>
        private async void btnRunSimulation_Click(object sender, EventArgs e)
        {
            // Verify all controls exist
            if (formsPlot1 == null || formsPlot2 == null || formsPlot3 == null)
            {
                MessageBox.Show(
                    "FormsPlot controls are missing!\n\n" +
                    "Please ensure formsPlot1, formsPlot2, and formsPlot3 exist in the designer.",
                    "Setup Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Disable button during simulation
            btnRunSimulation.Enabled = false;

            if (lblStatus != null)
            {
                lblStatus.Text = "Running simulation... Please wait.";
                lblStatus.ForeColor = System.Drawing.Color.Orange;
            }

            try
            {
                // SIMULATION PARAMETERS
                // More phase points + smoothing = better visual appearance
                int numPhiPoints = 80;      // INCREASED for smoother curves
                int shotsPerPhi = 3000;     // Balanced for speed vs quality

                // You can make these adjustable via UI controls if desired:
                // numPhiPoints: 50-100 (more = smoother but slower)
                // shotsPerPhi: 1000-5000 (more = less noise but slower)

                // Create phase values from 0 to 4π (two full periods)
                double[] phiValues = Enumerable.Range(0, numPhiPoints)
                    .Select(i => i * 4.0 * Math.PI / (numPhiPoints - 1))
                    .ToArray();

                // Run simulation asynchronously (doesn't freeze UI)
                var results = await Task.Run(() =>
                    _simulator.RunExperiment(phiValues, shotsPerPhi));

                // Extract data for plotting
                var plotData = _simulator.ExtractPlotData(results);

                // Create the three plots
                _plotter.CreatePlots(formsPlot1, formsPlot2, formsPlot3, plotData);

                // Update status
                if (lblStatus != null)
                {
                    lblStatus.Text = "✓ Simulation complete! Observe the quantum eraser effect in the plots.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }

                // Show explanation dialog
                MessageBox.Show(
                    "═══════════════════════════════════════════════\n" +
                    "  DELAYED CHOICE QUANTUM ERASER RESULTS\n" +
                    "═══════════════════════════════════════════════\n\n" +

                    "KEY OBSERVATIONS:\n\n" +

                    "📊 PLOT 1 (Top):\n" +
                    "   Total signal is FLAT → NO interference visible\n" +
                    "   Without post-selection, no pattern emerges\n" +
                    "   Why? Complementary patterns cancel out!\n\n" +

                    "🌊 PLOT 2 (Middle):\n" +
                    "   WITH eraser → INTERFERENCE appears!\n" +
                    "   • Green and orange curves OSCILLATE\n" +
                    "   • They are OUT OF PHASE (complementary)\n" +
                    "   • BOTH show interference patterns!\n" +
                    "   • When added: (1-cos φ)/2 + (1+cos φ)/2 = 1 (flat!)\n" +
                    "   • This is the quantum eraser effect!\n\n" +

                    "📏 PLOT 3 (Bottom):\n" +
                    "   WITHOUT eraser → NO interference\n" +
                    "   • Flat lines (no oscillation)\n" +
                    "   • Which-path info prevents interference\n\n" +

                    "═══════════════════════════════════════════════\n" +
                    "  WHY THIS MATTERS:\n" +
                    "═══════════════════════════════════════════════\n\n" +

                    "✓ TRUE delayed choice:\n" +
                    "  Each shot randomly chooses erase/keep\n" +
                    "  DURING execution (not pre-determined)\n\n" +

                    "✓ Causality preserved:\n" +
                    "  Signal measured BEFORE the choice\n" +
                    "  No retrocausality needed!\n\n" +

                    "✓ Post-selection reveals correlations:\n" +
                    "  Interference exists in CORRELATIONS\n" +
                    "  Only visible when sorting by idler state\n\n" +

                    "✓ Complementary patterns cancel:\n" +
                    "  Both signal states show interference\n" +
                    "  But they're out of phase → sum = flat!\n\n" +

                    $"Simulation: {numPhiPoints} positions × {shotsPerPhi} shots = {numPhiPoints * shotsPerPhi:N0} total measurements",
                    "Quantum Eraser Experiment Results",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle errors
                if (lblStatus != null)
                {
                    lblStatus.Text = "✗ Error during simulation!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }

                MessageBox.Show(
                    $"Error running simulation:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Simulation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable button
                btnRunSimulation.Enabled = true;
            }
        }
    }
}

// ============================================================================
// SETUP INSTRUCTIONS
// ============================================================================
/*
 * REQUIRED COMPONENTS IN YOUR FORM DESIGNER:
 * 
 * 1. Three FormsPlot controls (from ScottPlot.WinForms):
 *    - formsPlot1 (top plot - total signal)
 *    - formsPlot2 (middle plot - with eraser)
 *    - formsPlot3 (bottom plot - without eraser)
 * 
 * 2. One Button control:
 *    - btnRunSimulation
 *    - Text: "Run Simulation"
 *    - Click event: btnRunSimulation_Click (wire this in designer)
 * 
 * 3. One Label control (optional):
 *    - lblStatus
 *    - For displaying status messages
 * 
 * WHAT TO EXPECT:
 * 
 * When you click "Run Simulation", you should see:
 * 
 * Plot 1: A flat horizontal line
 *         (Total counts - no interference without post-selection)
 * 
 * Plot 2: Two oscillating curves (green and orange)
 *         OUT OF PHASE with each other
 *         (With eraser - interference revealed!)
 * 
 * Plot 3: Two flat horizontal lines
 *         (Without eraser - which-path info destroys interference)
 * 
 * This demonstrates that:
 * - Individual measurements show NO interference
 * - Post-selecting by idler measurement REVEALS interference
 * - Only when which-path info is erased (not preserved)
 * - The choice to erase happens AFTER signal is measured (delayed choice!)
 */