
# 🪵 LogYourDayAway

A secure, offline-first daily journaling application built with .NET MAUI. 

LogYourDayAway is designed with a focus on privacy and user experience. It features a completely local architecture, ensuring that personal journal entries, passwords, and recovery codes never leave the device.

## ✨ Key Features

* **Offline-First Architecture:** All data is stored locally on the device using a SQLite database, requiring no internet connection or external servers.
* **Secure Local Authentication:** Utilizes one-way SHA-256 hashing for passwords. Hashes are safely stored in the device's native encrypted Keystore/Keychain via `SecureStorage`.
* **Robust Account Recovery:** Features a zero-knowledge recovery code system. Users are provided a one-time plaintext code upon registration, while the app stores only the hash, ensuring security even if the device is compromised.
* **Day Ranking System:** A visual 5-tier daily ranking system (Great, Good, Average, Bad, Awful) utilizing a custom, calming "Sage & Paper" earth-tone UI theme.
* **Data Management:** Includes a secure "Factory Reset" feature requiring explicit typed user confirmation to drop database connections, wipe the SQLite file, and clear all secure storage keys.
* **Custom Theming:** Features custom splash screens, icons, and native control handlers (like underline-free pickers) for a seamless cross-platform look.

## 🛠️ Tech Stack

* **Framework:** .NET MAUI (Multi-platform App UI)
* **Language:** C#
* **Architecture:** MVVM (Model-View-ViewModel) using `CommunityToolkit.Mvvm`
* **Database:** SQLite (`sqlite-net-pcl`)
* **Security:** `Microsoft.Maui.Storage.SecureStorage`, `System.Security.Cryptography`

## 📱 Screenshots

| Login & Security | Journal Entries | Day Ranking |
| :---: | :---: | :---: |
| <img width="250" alt="Screenshot 2026-02-19 203356" src="https://github.com/user-attachments/assets/295c38b5-eb58-4185-96ee-444c03aeb85a" /> | <img width="250" alt="Screenshot 2026-02-19 203425" src="https://github.com/user-attachments/assets/89dc1ba9-b0bf-4076-a77a-bb06bfcb9ba7" /> | <img width="250" alt="Screenshot 2026-02-19 203442" src="https://github.com/user-attachments/assets/39f06848-dee2-4d34-ac94-e4e17e9923e4" /> |

## 🚀 Getting Started

To build and run this project locally, you will need Visual Studio 2022 with the .NET MAUI workload installed.

### Prerequisites
* Visual Studio 2022 (v17.8 or newer)
* .NET 8.0 SDK
* Android SDK / iOS Mac Build Host (for deployment)

### Installation
1. Clone the repository:
   ```bash
   git clone [https://github.com/yourusername/LogYourDayAway.git](https://github.com/yourusername/LogYourDayAway.git](https://github.com/Jeckman03/LogYourDayAwayApp.git)

2. Open LogYourDayAway.sln in Visual Studio.

3. Select your target framework (e.g., net8.0-android or net8.0-ios).

4. Build and run (F5).

🔐 Security Model Notes
As a privacy-focused journal, this app treats user data with strict isolation:

1. No Cloud Sync: The application makes zero external network requests.

2. Hash-Only Storage: Plaintext passwords and recovery codes are never stored in memory longer than necessary. Only cryptographic hashes are written to the device's secure enclave.

3. Database Integrity: The SQLite connection relies on dependency-injected singleton services that properly close and release file locks during application resets to prevent corruption.

👨‍💻 Author
Jeff Eckman

LinkedIn: [www.linkedin.com/in/jeff-eckman-b514a111]

GitHub: [(https://github.com/Jeckman03)]
