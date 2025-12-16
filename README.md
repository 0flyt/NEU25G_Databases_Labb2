# Bokhandel – Entity Framework (Labb 2)

Detta är ett skolprojekt i kursen Databaser / C# där syftet är att utveckla en
relationsdatabas-applikation med Entity Framework.

Projektet är en WPF-applikation för administration av en bokhandel och är baserad
på databasen som skapades i Labb 1.

---

## Uppgift
**Labb 2 – Utveckla en relationsdatabasapp med Entity Framework**

Applikationen uppfyller *Förslag 1 – App för administration av bokhandel*.

---

## Funktionalitet
Applikationen låter användaren:

- Visa lagersaldo per butik
- Lägga till och ta bort böcker från butiker
- Hantera befintliga titlar i sortimentet
- (VG) Skapa, redigera och ta bort böcker och författare
- (VG) Koppla böcker till befintliga eller nya författare

All databaskommunikation sker via **Entity Framework Core**.

---

## Uppfyllda betygskriterier

### För godkänt
- CRUD-operationer (Create, Read, Update, Delete) via Entity Framework
- Relationsdatabas med flera tabeller (Books, Authors, Stores, StoreBooks m.fl.)
- Samtliga tabeller används i applikationen
- Projektet kan klonas och köras lokalt
- README med instruktioner för hur applikationen körs
- Minst 10 genomtänkta commits i GitHub-repot

### För väl godkänt
- All kommunikation mot databasen sker asynkront (`async` / `await`)
- Utökad funktionalitet enligt VG-krav
- Färdig och sammanhängande applikation med genomtänkt UI

---

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
