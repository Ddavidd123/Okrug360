# Okrug360

Okrug360 je moj lokalni portal za Pčinjski okrug. Ideja mi je da napravim jedno mesto gde ljudi iz okruga mogu lako da pronađu sve važne informacije — vesti, događaje, ustanove, zanimljiva mesta, a u budućnosti i da prijave lokalne probleme ili pitaju AI asistenta nešto o svom kraju.

Moj moto kroz ceo projekat je: **Pitaj. Pronađi. Učestvuj.**

## O čemu se radi

Umesto da informacije budu razbacane po raznim sajtovima, grupama i objavama, želim da ih okupim na jednom mestu. Portal će obuhvatiti:

- lokalne vesti i obaveštenja
- događaje u okrugu
- ustanove i mesta koja vredi posetiti
- prijave građana za lokalne probleme
- AI asistenta koji odgovara na osnovu proverenih podataka sa portala

Ne pravim sve odjednom, već gradim projekat korak po korak kao skup manjih servisa koji svaki rade svoj deo posla.

## Dokle sam stigao

Trenutno radim na prvom servisu — Content servisu, koji se bavi vestima.

Do sada imam:

- servis `Okrug360.Content.Api`
- bazu `Okrug360_ContentDb`
- kreiranje, izmenu i brisanje vesti
- objavljivanje i arhiviranje
- paginaciju kod izlistavanja
- validaciju unosa
- health check koji proverava i bazu

## Šta ti treba da pokreneš projekat

- .NET 8 SDK
- Docker Desktop
- Visual Studio 2022 ili VS Code
- Git

## Kako se pokreće

### 1. Skini projekat

```bash
git clone <URL-REPOZITORIJUMA>
cd Okrug360
```

### 2. Napravi svoj .env fajl

Kopiraj primer koji sam ostavio:

```bash
copy .env.example .env
```

Pa u `.env` upiši svoju lozinku za bazu:

```env
MSSQL_SA_PASSWORD=TvojaJakaLozinka123!
MSSQL_PORT=1435
```

### 3. Podigni SQL Server

```bash
docker compose up -d
```

Baza će ti biti dostupna na `localhost,1435`.

### 4. Podesi tajne za Content API

Lozinku ne držim u kodu, nego u .NET User Secrets:

```bash
cd Okrug360.Content.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:ContentDatabase" "Server=localhost,1435;Database=Okrug360_ContentDb;User Id=sa;Password=TvojaJakaLozinka123!;TrustServerCertificate=True"
```

Bitno je da je ova lozinka ista kao ona u `.env`.

### 5. Primeni migracije

```bash
dotnet ef database update
```

### 6. Pokreni API

```bash
dotnet run --launch-profile https
```

Ili jednostavno pokreni projekat iz Visual Studio preko `F5`.

### 7. Proveri da sve radi

- Swagger je na `https://localhost:7213/swagger`
- Health check je na `https://localhost:7213/health`

Ako sve štima, health check će vratiti `Healthy`.

## Endpointi za vesti

| Metoda | Endpoint | Šta radi |
|--------|----------|----------|
| GET | `/api/news?page=1&pageSize=10` | Lista objavljenih vesti |
| GET | `/api/news/{id}` | Jedna objavljena vest |
| POST | `/api/news` | Nova vest |
| PUT | `/api/news/{id}` | Izmena vesti |
| DELETE | `/api/news/{id}` | Brisanje vesti |
| POST | `/api/news/{id}/publish` | Objavljivanje drafta |
| POST | `/api/news/{id}/archive` | Arhiviranje vesti |
| GET | `/health` | Provera servisa i baze |

## Primer kreiranja vesti

```http
POST /api/news
Content-Type: application/json

{
  "title": "Prva vest",
  "summary": "Kratak opis",
  "content": "Pun tekst vesti.",
  "publishImmediately": true
}
```

## Kako sam zamislio arhitekturu

Plan mi je da svaki deo portala bude poseban servis, a da ispred svega stoji gateway:

```text
Next.js aplikacija
      │
      ▼
YARP API Gateway
      │
      ├── Identity servis
      ├── Content servis   (na ovome trenutno radim)
      ├── Events servis
      ├── Reports servis
      └── AI servis
```

Pravilo kog se držim je da svaki servis ima svoju bazu i da ne dira tuđu.

## Bezbednost

Par stvari kojih se držim od početka:

- `.env` nikad ne ide na GitHub
- lozinke ne stavljam u `appsettings` fajlove
- lokalne tajne držim u User Secrets
- ako je neka lozinka ikada završila u Gitu, menjam je pre nego što projekat ode u produkciju

## Šta planiram dalje

- testovi (unit i integration)
- CI preko GitHub Actions
- frontend u Next.js-u
- API Gateway
- Identity servis sa prijavom i registracijom
