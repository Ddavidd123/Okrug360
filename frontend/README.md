# Okrug360 Frontend

Next.js app for Okrug360 — vesti, mesta i premium mapa destinacija.

## Pokretanje

1. Instaliraj zavisnosti:

```bash
npm install
```

2. (Opciono) podesi API URL u `.env.local`:

```env
NEXT_PUBLIC_PLACES_API_URL=http://localhost:5174
```

Mapa koristi **MapLibre + OpenFreeMap** — **nema potrebe za Mapbox token / karticu**.

3. Pokreni Places API (`Okrug360.Places.Api` na portu `5174`).

4. Pokreni frontend:

```bash
npm run dev
```

Otvori [http://localhost:3000/mapa](http://localhost:3000/mapa).

## Mapa destinacija

- Podaci dolaze iz Places API (`GET /api/places`). Ako API nije dostupan, koristi se dummy set.
- Novo mesto se pojavljuje na mapi kada je **published** i ima `latitude` / `longitude` (opciono `imageUrl`).
- Primer: `POST /api/places` sa koordinatama + `publishImmediately: true`, pa osveži `/mapa`.
