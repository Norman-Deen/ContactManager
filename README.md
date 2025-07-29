📇 ContactManager

**ContactManager** is a simple console application built with **.NET 8** that manages contacts using a structured model and interface-based design.

---

## 🛠 Features

- 🔍 Add, search, and list contacts
- 📁 Data source: JSON file (`text.json`)
- 🧩 Clean architecture with Models and Interfaces
- ✅ Built using .NET 8

---

## 📂 Project Structure

```

ContactManager/
├── Interfaces/
│   └── IContactManager.cs      # Interface definition for contact operations
├── Models/
│   └── ContactManager.cs       # Logic implementation
├── JSON/
│   └── text.json               # Sample contact data
├── Program.cs                  # Entry point of the application
├── ContactManager.csproj       # Project file

````

---

## 🚀 Getting Started

### Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Run the application

```bash
dotnet run --project ContactManager.csproj
````

The program will load contacts from `JSON/text.json` and provide console interaction.

---

## 📄 License

This project was created as part of coursework at EC Utbildning.
Feel free to reuse or modify it for learning or personal projects.

---

👨‍💻 Created by Nour — .NET Developer & Visual Designer
