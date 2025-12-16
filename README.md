## Databas och konfiguration

Applikationen använder en lokal SQL Server-databas.  
Av säkerhetsskäl finns ingen connection string incheckad i GitHub.

Varje användare behöver därför skapa sin egen lokala databas och ange sin egen connection string.

---

### Förutsättningar
- Windows
- SQL Server (LocalDB eller full SQL Server)
- Visual Studio
- .NET (samma version som projektet)

---

### Steg 1 – Skapa databasen
1. Öppna SQL Server Management Studio
2. Skapa en tom databas (valfritt namn)
3. Kör SQL-scriptet som finns i: ! ! ! TODO ! ! !

Scriptet skapar tabeller, vyer, procedurer samt exempeldata.

---

### Steg 2 – Skapa connection string
1. Kopiera filen: connections.config.example
2. Döp kopian till: connections.config
3. Uppdatera connection string "YOUR_DATABASE_NAME_HERE" till samma namn du gav din databas.

Filen `connections.config` är ignorerad av Git och ska inte committas.

---

### Steg 3 – Kör applikationen
1. Öppna lösningen i Visual Studio
2. Bygg och kör projektet
3. Applikationen ansluter nu till din lokala databas

---

### Teknisk information
- WPF-applikation
- Entity Framework Core (Database First / Scaffold)
- Connection string laddas via `App.config` och extern fil (`connections.config`)
