import { SiteHeader } from "@/components/SiteHeader";
import { MapExplorerLoader } from "@/components/map/MapExplorerLoader";
import { dummyPlaces } from "@/components/map/dummyPlaces";
import { getPlacesForMap } from "@/lib/placesApi";
import type { Place } from "@/types/place";

export default async function MapaPage() {
  let places: Place[] = [];
  let usingFallback = false;

  try {
    places = await getPlacesForMap({ city: "Vranje" });
    if (places.length === 0) {
      places = dummyPlaces;
      usingFallback = true;
    }
  } catch {
    places = dummyPlaces;
    usingFallback = true;
  }

  return (
    <main className="min-h-screen bg-background">
      <SiteHeader />

      <section className="mx-auto max-w-7xl px-4 py-8 sm:px-6 sm:py-12">
        <p className="mb-2 text-sm font-semibold uppercase tracking-[0.18em] text-accent">
          Otkrij okrug
        </p>
        <h1 className="font-(family-name:--font-fraunces) text-4xl tracking-tight text-brand">
          Mapa destinacija
        </h1>
        <p className="mt-3 max-w-2xl text-muted">
          Pretraži, filtriraj i istraži mesta u Vranju i okolini — od manastira
          do kafića.
        </p>

        <div className="mt-8">
          <MapExplorerLoader places={places} usingFallback={usingFallback} />
        </div>
      </section>
    </main>
  );
}
