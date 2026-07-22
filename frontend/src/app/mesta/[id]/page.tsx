import Link from "next/link";
import { notFound } from "next/navigation";
import { SiteHeader } from "@/components/SiteHeader";
import { formatPlaceCategory } from "@/lib/formatPlaceCategory";
import { getPlaceById } from "@/lib/placesApi";

type MestoDetailPageProps = {
  params: Promise<{
    id: string;
  }>;
};

export default async function MestoDetailPage({ params }: MestoDetailPageProps) {
  const { id } = await params;

  let place = null;

  try {
    place = await getPlaceById(id);
  } catch {
    notFound();
  }

  if (!place) {
    notFound();
  }

  const mapsQuery = encodeURIComponent(
    `${place.name}, ${place.address}, ${place.city}, Srbija`,
  );
  const mapsUrl = `https://www.google.com/maps/search/?api=1&query=${mapsQuery}`;

  return (
    <main className="min-h-screen bg-background">
      <SiteHeader />

      <article className="mx-auto max-w-3xl px-6 py-12">
        <Link
          href="/mesta"
          className="text-sm font-semibold text-accent hover:text-brand"
        >
          ← Nazad na mesta
        </Link>

        <span className="mt-8 inline-flex rounded-full bg-accent/10 px-3 py-1 text-xs font-semibold uppercase tracking-[0.12em] text-accent">
          {formatPlaceCategory(place.category)}
        </span>

        <h1 className="mt-4 font-(family-name:--font-fraunces) text-4xl leading-tight tracking-tight text-brand sm:text-5xl">
          {place.name}
        </h1>

        <p className="mt-5 text-lg leading-relaxed text-muted">
          {place.address}, {place.city}
        </p>

        <div className="mt-10 whitespace-pre-wrap text-base leading-8 text-foreground">
          {place.description}
        </div>

        <div className="mt-10 rounded-2xl border border-border bg-surface px-6 py-5">
          <p className="text-sm font-semibold uppercase tracking-[0.12em] text-accent">
            Lokacija
          </p>
          <p className="mt-2 text-sm text-muted">
            {place.latitude.toFixed(5)}, {place.longitude.toFixed(5)}
          </p>
          <a
            href={mapsUrl}
            target="_blank"
            rel="noreferrer"
            className="mt-4 inline-flex text-sm font-semibold text-accent hover:text-brand"
          >
            Otvori u Google Maps →
          </a>
        </div>
      </article>
    </main>
  );
}