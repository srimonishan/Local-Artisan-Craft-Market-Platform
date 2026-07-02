<div align="center">

# 🎨 Local Artisan & Craft Market Platform

[![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.8-512BD4?style=for-the-badge&logo=.net)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![WinForms](https://img.shields.io/badge/Windows_Forms-0078D7?style=for-the-badge&logo=windows&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![License: MIT](https://img.shields.io/badge/License-MIT-success?style=for-the-badge)](https://opensource.org/licenses/MIT)

**A professional desktop application for managing local artisans, handmade crafts, and customer orders.**

---

</div>

## 📖 Project Overview

The **Local Artisan & Craft Market Platform** is a secure, robust Windows Desktop Application designed to help administrators efficiently manage local artisans, their unique handmade craft products, and customer orders. Built as a university project, it demonstrates practical application of Object-Oriented Programming (OOP) concepts, database design, and modern UI/UX principles within a WinForms environment.

## ✨ Key Features

- 🔐 **Secure Admin Login:** robust authentication system using parameterized SQL queries.
- 📊 **Interactive Dashboard:** Centralized view of key metrics and quick navigation.
- 🛍️ **Product Management:** Complete CRUD (Create, Read, Update, Delete) operations for craft products.
- 👥 **Artisan Management:** View, verify, and approve local artisan profiles.
- 🔍 **Advanced Search:** Quick filtering and search functionality for products and artisans.
- 📈 **Sales Reporting:** Generate and view sales reports for business insights.
- 🎨 **Modern UI/UX:** Premium dark theme with professional typography and responsive design.

## 🛠️ Technologies Used

| Technology | Purpose |
|------------|---------|
| **C#** | Core programming language |
| **.NET Framework 4.8** | Application framework |
| **Windows Forms (WinForms)** | Graphical User Interface (GUI) |
| **SQL Server** | Relational Database Management System |
| **ADO.NET** | Data access technology (`System.Data.SqlClient`) |
| **Visual Studio 2022** | Integrated Development Environment (IDE) |

## 🏗️ System Architecture

The application follows a clean 3-tier architecture approach (adapted for WinForms):

1. **Presentation Layer (UI):** WinForms elements (`.cs` and `.Designer.cs` files) handling user interaction and rendering.
2. **Business Logic Layer:** C# classes and methods handling data processing, validation, and core application rules.
3. **Data Access Layer:** ADO.NET implementation (`DatabaseConnection.cs`) managing secure SQL Server interactions using parameterized queries to prevent SQL injection.

## 🗄️ Database Overview

The system uses a SQL Server database (`CraftMarketDB`) consisting of structured tables to maintain referential integrity. 

**Core Tables:**
- `Admins`: Stores administrator credentials (hashed passwords).
- `Artisans`: Records of registered craft makers.
- `Products`: Details of handmade items linked to specific artisans.
- `Orders`: Customer transaction records.

## 📁 Project Folder Structure

```text
Local-Artisan-Craft-Market-Platform/
│
├── Database/                 # Database connection and helper classes
│   └── DatabaseConnection.cs # Centralized DB connection logic
│
├── Forms/                    # Windows Forms (UI)
│   ├── LoginForm.cs          # Authentication UI
│   └── DashboardForm.cs      # Main admin dashboard
│
├── Models/                   # Data transfer objects (Entities)
├── Properties/               # Application settings and resources
├── App.config                # Configuration file (Contains Connection Strings)
├── Program.cs                # Application entry point
└── README.md                 # Project documentation
```

## 🚀 Installation and Setup Guide

### Prerequisites
- Windows OS (10 or 11)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community Edition or higher) with ".NET desktop development" workload enabled.
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).

### Steps to Run
1. **Clone the repository:**
   ```bash
   git clone https://github.com/srimonishan/Local-Artisan-Craft-Market-Platform.git
   ```
2. **Open the solution:**
   - Double-click `Craft-Market-Platform.slnx` or `.sln` to open the project in Visual Studio.
3. **Update Connection String:**
   - Open `App.config`.
   - Locate the `<connectionStrings>` section.
   - Update the `Data Source` property to point to your local SQL Server instance name.
4. **Build and Run:**
   - Press `F5` or click **Start** in Visual Studio to compile and launch the application.

## 💽 Database Setup Instructions

1. Open SQL Server Management Studio (SSMS).
2. Create a new database named `CraftMarketDB`.
3. Execute the provided SQL script (located in the repository or append your SQL schema here) to generate the required tables:
   ```sql
   -- Example snippet
   CREATE TABLE Admins (
       AdminID INT IDENTITY(1,1) PRIMARY KEY,
       Username VARCHAR(50) NOT NULL,
       PasswordHash VARCHAR(255) NOT NULL,
       FullName VARCHAR(100) NOT NULL
   );

   -- Insert default admin
   INSERT INTO Admins (Username, PasswordHash, FullName) 
   VALUES ('admin', 'admin123', 'System Administrator');
   ```

## 🔮 Future Improvements

- [ ] Implement role-based access control (RBAC).
- [ ] Add an Artisan-facing portal alongside the Admin portal.
- [ ] Implement Entity Framework Core instead of ADO.NET for ORM capabilities.
- [ ] Add rich charts using a charting library for the Sales Report.
- [ ] Export reports to PDF/Excel.

## 🎓 Learning Outcomes (OOP Concepts)

This project successfully demonstrated the application of core Object-Oriented paradigms:
- **Encapsulation:** Used access modifiers (private, public) to protect data states within classes (like `DatabaseConnection` and models) and exposed functionality through public properties/methods.
- **Abstraction:** Hid complex database logic (SQL connection strings and command execution) behind simple method calls (`GetConnection()`, `TestConnection()`).
- **Inheritance:** Leveraged the WinForms framework where all custom forms inherit from the base `System.Windows.Forms.Form` class.
- **Polymorphism:** Overrode built-in methods (like `Dispose(bool disposing)`) to manage application resources efficiently.

## 👥 Contributors

- **Srimonishan** - *Lead Developer / Ai engineer* - [GitHub Profile](https://github.com/srimonishan)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
<div align="center">
  <b>Built with ❤️ for Local Craftsmanship</b>
</div>
