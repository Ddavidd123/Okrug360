import Link from "next/link";
import { formatPlaceCategory } from "@/lib/formatPlaceCategory";
import type { Place } from "@/types/place";

export function PlaceCard({ place }: { place: Place }) {
  return (
    <article className="flex flex-col rounded-2xl border border-border bg-surface p-6 shadow-[0_8px_24px_rgba(16,42,67,0.06)] transition hover:-translate-y-0.5 hover:shadow-[0_12px_28px_rgba(16,42,67,0.1)]">
      <span className="inline-flex w-fit rounded-full bg-accent/10 px-3 py-1 text-xs font-semibold uppercase tracking-[0.12em] text-accent">
        {formatPlaceCategory(place.category)}
      </span>

      <h3 className="mt-4 font-[family-name:var(--font-fraunces)] text-xl leading-snug text-brand">
        <Link href={`/mesta/${place.id}`} className="hover:text-accent">
          {place.name}
        </Link>
      </h3>

      <p className="mt-2 text-sm text-muted">
        {place.address}, {place.city}
      </p>

      <p className="mt-3 flex-1 line-clamp-3 text-sm leading-relaxed text-muted">
        {place.description}
      </p>

      <Link
        href={`/mesta/${place.id}`}
        className="mt-6 text-sm font-semibold text-accent hover:text-brand"
      >
        Pogledaj detalje →
      </Link>
    </article>
  );
}