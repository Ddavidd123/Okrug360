"use client";

import { useDeferredValue, useMemo, useState } from "react";
import { MapCanvas } from "@/components/map/MapCanvas";
import { MapToolbar } from "@/components/map/MapToolbar";
import { PlaceDetailPanel } from "@/components/map/PlaceDetailPanel";
import { PlaceList } from "@/components/map/PlaceList";
import { filterMapPlaces } from "@/components/map/types";
import type { MapPlace } from "@/components/map/types";
import type { PlaceCategory } from "@/types/place";

type MapExplorerProps = {
  places: MapPlace[];
  usingFallback?: boolean;
};

export function MapExplorer({
  places,
  usingFallback = false,
}: MapExplorerProps) {
  const [query, setQuery] = useState("");
  const [category, setCategory] = useState<PlaceCategory | "All">("All");
  const [selectedId, setSelectedId] = useState<string | null>(null);
  const [listOpen, setListOpen] = useState(true);

  const deferredQuery = useDeferredValue(query);

  const filtered = useMemo(
    () => filterMapPlaces(places, deferredQuery, category),
    [places, deferredQuery, category],
  );

  const selectedPlace =
    filtered.find((place) => place.id === selectedId) ?? null;
  const activeSelectedId = selectedPlace?.id ?? null;

  function handleSelect(id: string) {
    setSelectedId(id);
  }

  function handleCategoryChange(value: PlaceCategory | "All") {
    setCategory(value);
    setSelectedId(null);
  }

  return (
    <div className="map-explorer overflow-hidden rounded-[1.75rem] border border-border bg-surface shadow-[0_18px_40px_rgba(16,42,67,0.08)]">
      {usingFallback && (
        <p className="border-b border-border bg-accent-soft/60 px-5 py-2.5 text-xs text-muted">
          Prikazan je demo set lokacija. Pokreni Places API da vidiš stvarna
          mesta.
        </p>
      )}

      <div className="grid min-h-[min(72vh,720px)] lg:grid-cols-[320px_minmax(0,1fr)]">
        <aside className="flex flex-col border-b border-border lg:border-b-0 lg:border-r">
          <div className="border-b border-border px-4 py-4 sm:px-5">
            <div className="mb-4 flex items-center justify-between gap-3 lg:block">
              <div>
                <p className="text-[11px] font-semibold uppercase tracking-[0.16em] text-accent">
                  Destinacije
                </p>
                <h2 className="mt-1 font-(family-name:--font-fraunces) text-xl text-brand">
                  Istraži mapu
                </h2>
              </div>
              <button
                type="button"
                className="rounded-full border border-border px-3 py-1.5 text-xs font-semibold text-muted lg:hidden"
                onClick={() => setListOpen((open) => !open)}
              >
                {listOpen ? "Sakrij listu" : "Prikaži listu"}
              </button>
            </div>

            <MapToolbar
              query={query}
              category={category}
              resultCount={filtered.length}
              onQueryChange={setQuery}
              onCategoryChange={handleCategoryChange}
            />
          </div>

          <div
            className={`flex-1 overflow-y-auto px-4 py-4 sm:px-5 ${
              listOpen ? "block" : "hidden lg:block"
            }`}
          >
            <PlaceList
              places={filtered}
              selectedId={activeSelectedId}
              onSelect={handleSelect}
            />
          </div>
        </aside>

        <div className="relative min-h-[420px]">
          <MapCanvas
            places={filtered}
            selectedId={activeSelectedId}
            onSelect={handleSelect}
          />
          <PlaceDetailPanel
            place={selectedPlace}
            onClose={() => setSelectedId(null)}
          />
        </div>
      </div>
    </div>
  );
}
