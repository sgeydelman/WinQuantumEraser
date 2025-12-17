# WinQuantumEraser

A C# Windows Forms simulation of the **Kim et al. (2000) Delayed Choice Quantum Eraser Experiment**, demonstrating one of the most fascinating phenomena in quantum mechanics.

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![.NET](https://img.shields.io/badge/.NET-Framework-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)

## 📋 Table of Contents

- [Overview](#overview)
- [The Kim et al. Experiment](#the-kim-et-al-experiment)
- [What This Simulation Demonstrates](#what-this-simulation-demonstrates)
- [Key Features](#key-features)
- [Installation](#installation)
- [Usage](#usage)
- [Understanding the Results](#understanding-the-results)
- [The Physics Behind It](#the-physics-behind-it)
- [Technical Implementation](#technical-implementation)
- [References](#references)
- [License](#license)

## 🔬 Overview

This application simulates the groundbreaking **delayed choice quantum eraser experiment** published by Kim, Yu, Kulik, Shih, and Scully in *Physical Review Letters* (2000). The experiment demonstrates:

- **Wave-particle duality** through quantum interference
- **Quantum entanglement** between signal and idler photons
- **Complementary interference patterns** revealed by post-selection
- **Delayed choice** - future measurements revealing patterns in past data

The simulation accurately reproduces the experimental results using quantum mechanical probability distributions, allowing you to explore this profound quantum phenomenon interactively.

## 🎯 The Kim et al. Experiment

### Experimental Setup

In the original experiment:

1. **Photon pairs** are created via spontaneous parametric down-conversion (SPDC) in a BBO crystal
2. **Signal photons** pass through a double-slit and are detected at detector D0
3. **Idler photons** take a longer path (2.5m extra, ~8ns delay) to detectors D1, D2, D3, or D4
4. **Post-selection** sorts signal detections based on which idler detector fired

### The Mystery

Signal photons are detected **BEFORE** their entangled idler partners reach the detectors. Yet when we later sort the signal positions based on idler measurements:

- **D1-correlated signals** show interference pattern A
- **D2-correlated signals** show interference pattern B (complementary!)
- **D3-correlated signals** show no interference
- **D4-correlated signals** show no interference

**How do signal photons "know" to organize into patterns that will only be revealed by future measurements?**

This is the heart of quantum weirdness!

## 💡 What This Simulation Demonstrates

### Three Key Plots

The simulation generates three plots showing:

#### Plot 1: Total Signal (No Post-Selection)
```
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
FLAT - No interference visible
```
**Why?** Complementary patterns cancel when combined!

#### Plot 2: With Eraser (Which-Path Information Erased)
```
▓░▓░▓░▓░▓░▓░▓░▓  ← Pattern A: (1-cos φ)/2
░▓░▓░▓░▓░▓░▓░▓░  ← Pattern B: (1+cos φ)/2
TWO COMPLEMENTARY interference patterns!
```
**Why?** D1 and D2 measurements don't reveal which slit - interference preserved!

#### Plot 3: Without Eraser (Which-Path Information Preserved)
```
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
FLAT - No interference
```
**Why?** D3 and D4 measurements reveal which slit - interference destroyed!

### The Profound Result

**TWO complementary interference patterns** (from Plot 2) coexist in the same dataset and sum to a flat line. This demonstrates:

- Patterns aren't destroyed - they're **hidden** in correlations
- Future measurements **reveal** patterns in already-recorded data
- The system exhibits both wave-like and particle-like behavior simultaneously

## ✨ Key Features

- ✅ **Accurate quantum simulation** - Implements correct quantum mechanical probabilities
- ✅ **Interactive visualization** - Real-time plotting with ScottPlot
- ✅ **Configurable parameters** - Adjust number of shots, phase points, and more
- ✅ **True delayed choice** - Random decision made during each shot
- ✅ **Educational** - Clear code comments explaining the physics
- ✅ **Professional UI** - Clean Windows Forms interface

## 🚀 Installation

### Prerequisites

- **Windows** 10 or later
- **.NET Framework 4.7.2** or higher
- **Visual Studio 2019/2022** (for building from source)

### Required NuGet Packages

```xml
<PackageReference Include="ScottPlot.WinForms" Version="5.0.0" />
<PackageReference Include="ScottPlot" Version="5.0.0" />
```

### Building from Source

1. **Clone the repository:**
   ```bash
   git clone https://github.com/sgeydelman/WinQuantumEraser.git
   cd WinQuantumEraser
   ```

2. **Open in Visual Studio:**
   ```
   Open WinQuantumEraser.sln
   ```

3. **Restore NuGet packages:**
   ```
   Right-click solution → Restore NuGet Packages
   ```

4. **Build and Run:**
   ```
   Press F5 or click Start
   ```

## 📖 Usage

### Running a Simulation

1. **Launch the application**
2. **Click "Run Simulation"** button
3. **Wait for completion** (~2-3 seconds for default parameters)
4. **Observe the three plots**:
   - Top: Total signal (should be flat)
   - Middle: Erased which-path (shows TWO complementary patterns)
   - Bottom: Preserved which-path (should be flat)

### Adjusting Parameters

In `FormQEraser.cs`, you can modify:

```csharp
int numPhiPoints = 80;      // Number of phase values (smoothness)
int shotsPerPhi = 3000;     // Shots per phase (statistical quality)
```

**Recommendations:**
- More phase points = smoother curves but slower
- More shots = less noise but slower
- Default values (80, 3000) give good balance

## 📊 Understanding the Results

### What You Should See

**✅ CORRECT Results:**

Plot 1 (Total):
- Approximately flat horizontal line
- Small fluctuations due to statistical noise

Plot 2 (Erased - D1/D2):
- **Two oscillating curves** (green and orange)
- **Out of phase** - peaks of one align with troughs of the other
- **Complementary** - they sum to approximately constant

Plot 3 (Preserved - D3/D4):
- Two approximately flat horizontal lines
- May show slight variations but no clear oscillation

### Interpreting the Physics

**The complementary patterns (Plot 2) are the key:**

```
Pattern A: P ∝ (1 - cos φ)/2    ← D1 correlations
Pattern B: P ∝ (1 + cos φ)/2    ← D2 correlations
Sum:       P ∝ 1/2 (constant)   ← Total is flat!
```

This proves:
1. **Both patterns exist simultaneously** in the data
2. **They're complementary** (peaks vs troughs)
3. **They cancel when combined** (that's why total is flat)
4. **Post-selection reveals them** (sorting by idler detector)

## 🧪 The Physics Behind It

### Entangled State

The simulation creates pairs in the entangled state:

```
|ψ⟩ = (|00⟩ + e^(iφ)|11⟩) / √2
```

Where:
- `|00⟩` = both photons in state 0
- `|11⟩` = both photons in state 1
- `φ` = phase shift applied to the signal path
- Perfect correlation: signal=0 ↔ idler=0, signal=1 ↔ idler=1

### Measurement Scenarios

**Eraser Active (D1/D2 - Diagonal Basis):**
```csharp
// Signal state |0⟩ correlations:
P(idler=0) = (1 - cos φ)/2    // D1 detector
P(idler=1) = (1 + cos φ)/2    // D2 detector

// Signal state |1⟩ correlations:
P(idler=0) = (1 + cos φ)/2    // D1 detector  
P(idler=1) = (1 - cos φ)/2    // D2 detector
```

**Eraser Inactive (D3/D4 - Computational Basis):**
```csharp
// Perfect correlation, no interference:
if (signal == 0) then idler = 0  // D3
if (signal == 1) then idler = 1  // D4
```

### Why Two Complementary Patterns?

The magic happens because:

1. **D1 and D2 are at DIFFERENT output ports** of the beamsplitter
2. **Quantum interference** at the beamsplitter creates opposite phase relationships:
   - D1: Constructive interference with phase φ
   - D2: Constructive interference with phase φ+π
3. **The π phase difference** makes the patterns complementary

From the Kim et al. paper:
```
Ψ(t₀, t₁) = A(t₀, t₁^A) + A(t₀, t₁^B)    ← Paths ADD
Ψ(t₀, t₂) = A(t₀, t₂^A) - A(t₀, t₂^B)    ← Paths SUBTRACT
```

The minus sign creates the complementarity!

## 🔧 Technical Implementation

### Core Algorithm

```csharp
// Step 1: Create entangled pair
bool signalIs0 = Random() < 0.5;

// Step 2: Delayed choice (random)
bool eraseWhichPath = Random() < 0.5;

// Step 3: Determine idler outcome based on choice
if (eraseWhichPath) {
    // Diagonal basis - reveals interference
    if (signalIs0) {
        P(idler=0) = (1 - cos φ)/2;  // Pattern A
    } else {
        P(idler=0) = (1 + cos φ)/2;  // Pattern B
    }
} else {
    // Computational basis - preserves which-path
    idler = signal;  // Perfect correlation
}
```

### Architecture

```
QuantumEraserSimulator
├── SimulateOneShot()     - Single photon pair measurement
├── RunExperiment()       - Execute many shots across phase values
└── ExtractPlotData()     - Organize data for visualization

QuantumEraserPlotter
└── CreatePlots()         - Generate three ScottPlot visualizations

FormQEraser (WinForms)
└── btnRunSimulation_Click() - UI interaction and async execution
```

### Key Design Decisions

**1. True Delayed Choice:**
The `eraseWhichPath` decision is made DURING each shot, not pre-determined. This accurately models the delayed choice aspect.

**2. Complementary Patterns:**
The simulation implements both (1-cos φ) and (1+cos φ) patterns with proper phase relationships, matching the actual experiment.

**3. Statistical Accuracy:**
Uses proper quantum mechanical probability distributions, not approximations.

## 📚 References

### Primary Source
- **Kim, Y-H., Yu, R., Kulik, S.P., Shih, Y., & Scully, M.O.** (2000). "A Delayed Choice Quantum Eraser." *Physical Review Letters*, 84(1), 1-5. [arXiv:quant-ph/9903047](https://arxiv.org/abs/quant-ph/9903047)

### Additional Reading
- **Scully, M.O. & Drühl, K.** (1982). "Quantum eraser: A proposed photon correlation experiment concerning observation and delayed choice in quantum mechanics." *Physical Review A*, 25(4), 2208-2213.

- **Wikipedia**: [Delayed-choice quantum eraser](https://en.wikipedia.org/wiki/Delayed-choice_quantum_eraser)

### Related Concepts
- Quantum entanglement
- Wave-particle duality
- Wheeler's delayed choice experiment
- Complementarity principle (Niels Bohr)

## 🤝 Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

**Ideas for enhancement:**
- Add ability to export data to CSV
- Implement 3D visualization of interference patterns
- Add real-time parameter adjustment
- Include educational tooltips explaining each plot

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Sam Geydelman**
- GitHub: [@sgeydelman](https://github.com/sgeydelman)
- Company: SLGAutomation LLC

## 🙏 Acknowledgments

- Kim et al. for the groundbreaking experimental work
- ScottPlot team for excellent visualization library
- The quantum mechanics community for ongoing discussions

---

## 💬 Discussion Topics

This simulation raises profound questions:

1. **How do signal photons "know" to organize into patterns revealed by future measurements?**
2. **Are the patterns "already there" before post-selection, or created by it?**
3. **Does this challenge our notions of causality and temporal ordering?**

These questions remain at the heart of quantum mechanics interpretation debates!

---

**⭐ If you find this project interesting, please consider giving it a star!**

**🐛 Found a bug? Have a question? Open an issue!**# WinQuantumEraser
