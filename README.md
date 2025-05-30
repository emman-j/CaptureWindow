# Window Capture (Workspace Management)

**CaptureWindow** is a suite of lightweight Windows desktop applications developed in C# using both WinForms and WPF. 
Designed to enhance workspace efficiency, these tools allow users to capture screenshots of the active window or the entire screen seamlessly. 

---

## Features

- Capture active window or full screen with a single click
- Implementations in both WinForms and WPF for flexibility
- Includes test projects for experimentation and learning
- Modular code structure for easy integration and extension

---


- **CaptureWindow-Winforms/**: Contains the WinForms-based implementation.
- **CaptureWindow-WPF/**: Contains the WPF-based implementation.
- **(Obsolete)CaptureWindowTest/**: An outdated test project retained for reference.

---

## Getting Started

### Prerequisites

- **Operating System**: Windows 7 or later
- **Development Environment**: Visual Studio 2019 or newer
- **.NET Framework**: .NET 8.0 or later

### Build Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/emman-j/CaptureWindow.git
   ```
2. **Open the solution file**  
   Navigate to the cloned directory and open `CaptureWindow.sln` in Visual Studio.

3. **Restore NuGet packages**  
   In Visual Studio:  
   - Go to `Tools` > `NuGet Package Manager` > `Manage NuGet Packages for Solution`.  
   - Restore any missing packages.

4. **Build the solution**  
   - Press `Ctrl + Shift + B`  
   - Or go to `Build` > `Build Solution`

---

## Usage

After building the solution:

1. Navigate to the output directory (e.g., `bin\Debug\net6.0-windows`)
2. Run the executable corresponding to the desired implementation:
   - `CaptureWindow-Winforms.exe` – WinForms version  
   - `CaptureWindow-WPF.exe` – WPF version

## 📝 License

This project is licensed under the [MIT License](LICENSE).

---

## 🤝 Contributing

Contributions are welcome! If you have suggestions or improvements, feel free to fork the repository and submit a pull request.



