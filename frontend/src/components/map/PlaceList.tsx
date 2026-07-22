"use client";

import { formatPlaceCategory } from "@/lib/formatPlaceCategory";
import { markerColor } from "@/components/map/mapMarkers";
import type { MapPlace } from "@/components/map/types";

type PlaceListProps = {
  places: MapPlace[];
  selectedId: string | null;
  onSelect: (id: string) => void;
};

export function PlaceList({ places, selectedId, onSelect }: PlaceListProps) {
  if (places.length === 0) {
    return (
      <p className="rounded-2xl border border-dashed border-border bg-surface/70 px-4 py-8 text-center text-sm text-muted">
        Nema lokacija za ovu pretragu.
      </p>
    );
  }

  return (
    <ul className="space-y-2">
      {places.map((place) => {
        const selected = place.id === selectedId;
        const color = markerColor(place.category);

        return (
          <li key={place.id}>
            <button
              type="button"
              onClick={() => onSelect(place.id)}
              className={`group flex w-full gap-3 rounded-2xl border px-3.5 py-3 text-left transition ${
                selected
                  ? "border-accent/40 bg-accent-soft shadow-[0_10px_24px_rgba(16,42,67,0.08)]"
                  : "border-border bg-surface hover:border-accent/30 hover:bg-accent-soft/40"
              }`}
            >
              <span
                className="mt-1 h-2.5 w-2.5 shrink-0 rounded-full"
                style={{ backgroundColor: color }}
                aria-hidden
              />
              <span className="min-w-0 flex-1">
                <span className="block truncate font-(family-name:--font-fraunces) text-[15px] text-brand">
                  {place.name}
                </span>
                <span className="mt-1 block text-[11px] font-semibold uppercase tracking-[0.12em] text-accent">
                  {formatPlaceCategory(place.category)}
                </span>
                <span className="mt-1 block truncate text-xs text-muted">
                  {place.address}, {place.city}
                </span>
              </span>
            </button>
          </li>
        );
      })}
    </ul>
  );
}
