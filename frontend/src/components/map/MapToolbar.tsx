"use client";

import { MAP_FILTER_CHIPS } from "@/components/map/types";
import type { PlaceCategory } from "@/types/place";

type MapToolbarProps = {
  query: string;
  category: PlaceCategory | "All";
  resultCount: number;
  onQueryChange: (value: string) => void;
  onCategoryChange: (value: PlaceCategory | "All") => void;
};

export function MapToolbar({
  query,
  category,
  resultCount,
  onQueryChange,
  onCategoryChange,
}: MapToolbarProps) {
  return (
    <div className="space-y-4">
      <label className="block">
        <span className="sr-only">Pretraži destinacije</span>
        <input
          type="search"
          value={query}
          onChange={(event) => onQueryChange(event.target.value)}
          placeholder="Pretraži mesta, adrese…"
          className="w-full rounded-2xl border border-border bg-surface px-4 py-3 text-sm text-foreground outline-none transition placeholder:text-muted/80 focus:border-accent focus:ring-2 focus:ring-accent/20"
        />
      </label>

      <div className="-mx-1 flex gap-2 overflow-x-auto px-1 pb-1 [scrollbar-width:none] [&::-webkit-scrollbar]:hidden">
        {MAP_FILTER_CHIPS.map((chip) => {
          const active = category === chip.value;
          return (
            <button
              key={chip.value}
              type="button"
              onClick={() => onCategoryChange(chip.value)}
              className={`shrink-0 rounded-full px-3.5 py-1.5 text-xs font-semibold tracking-wide transition ${
                active
                  ? "bg-brand text-white shadow-[0_8px_18px_rgba(11,61,92,0.22)]"
                  : "border border-border bg-surface text-muted hover:border-accent/40 hover:text-brand"
              }`}
            >
              {chip.label}
            </button>
          );
        })}
      </div>

      <p className="text-xs font-medium uppercase tracking-[0.14em] text-muted">
        {resultCount} {resultCount === 1 ? "lokacija" : "lokacija"}
      </p>
    </div>
  );
}
