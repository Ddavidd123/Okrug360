"use client";

import Link from "next/link";
import { formatPlaceCategory } from "@/lib/formatPlaceCategory";
import type { MapPlace } from "@/components/map/types";

type PlaceDetailPanelProps = {
  place: MapPlace | null;
  onClose: () => void;
};

export function PlaceDetailPanel({ place, onClose }: PlaceDetailPanelProps) {
  const open = Boolean(place);

  return (
    <>
      <div
        className={`absolute inset-x-0 bottom-0 z-20 px-3 pb-3 transition duration-300 md:inset-y-4 md:right-4 md:left-auto md:w-[340px] md:px-0 md:pb-0 ${
          open
            ? "pointer-events-auto translate-y-0 opacity-100"
            : "pointer-events-none translate-y-6 opacity-0 md:translate-x-4 md:translate-y-0"
        }`}
        aria-hidden={!open}
      >
        {place && (
          <article className="pointer-events-auto overflow-hidden rounded-[1.5rem] border border-border bg-surface shadow-[0_22px_48px_rgba(16,42,67,0.16)]">
            <div className="relative h-44 overflow-hidden bg-[#e8eef2] md:h-48">
              {place.imageUrl ? (
                // eslint-disable-next-line @next/next/no-img-element
                <img
                  src={place.imageUrl}
                  alt={place.name}
                  className="h-full w-full object-cover"
                />
              ) : (
                <div className="flex h-full items-center justify-center text-sm text-muted">
                  Nema slike
                </div>
              )}
              <button
                type="button"
                onClick={onClose}
                className="absolute right-3 top-3 rounded-full bg-brand-deep/70 px-3 py-1.5 text-xs font-semibold text-white backdrop-blur hover:bg-brand-deep"
              >
                Zatvori
              </button>
            </div>

            <div className="space-y-3 p-5">
              <span className="inline-flex rounded-full bg-accent-soft px-3 py-1 text-[10px] font-bold uppercase tracking-[0.14em] text-accent">
                {formatPlaceCategory(place.category)}
              </span>
              <h2 className="font-(family-name:--font-fraunces) text-2xl leading-snug text-brand">
                {place.name}
              </h2>
              <p className="text-sm text-muted">
                {place.address}, {place.city}
              </p>
              <p className="line-clamp-4 text-sm leading-relaxed text-foreground/85">
                {place.description}
              </p>
              {!place.id.startsWith("dummy-") && (
                <Link
                  href={`/mesta/${place.id}`}
                  className="inline-flex text-sm font-semibold text-accent transition hover:text-brand"
                >
                  Pogledaj detalje →
                </Link>
              )}
            </div>
          </article>
        )}
      </div>

      {open && (
        <button
          type="button"
          aria-label="Zatvori panel"
          onClick={onClose}
          className="absolute inset-0 z-10 bg-brand-deep/10 md:hidden"
        />
      )}
    </>
  );
}
