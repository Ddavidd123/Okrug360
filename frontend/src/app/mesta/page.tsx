import Link from "next/link";
import { PlaceCard } from "@/components/PlaceCard";
import { SiteHeader } from "@/components/SiteHeader";
import { getPlaces } from "@/lib/placesApi";
import type { Place } from "@/types/place";

type MestaPageProps = {
  searchParams: Promise<{
    page?: string;
  }>;
};

export default async function MestaPage({ searchParams }: MestaPageProps) {
  const params = await searchParams;
  const page = Math.max(1, Number(params.page ?? "1") || 1);
  const pageSize = 9;

  let errorMessage: string | null = null;
  let items: Place[] = [];
  let totalPages = 0;

  try {
    const result = await getPlaces({ page, pageSize, city: "Vranje" });
    items = result.items;
    totalPages = result.totalPages;
  } catch {
    errorMessage =
      "Trenutno ne možemo da učitamo mesta. Proveri da li Places API radi.";
  }

  return (
    <main className="min-h-screen bg-background">
      <SiteHeader />

      <section className="mx-auto max-w-6xl px-6 py-12">
        <p className="mb-2 text-sm font-semibold uppercase tracking-[0.18em] text-accent">
          Otkrij okrug
        </p>
        <h1 className="font-(family-name:--font-fraunces) text-4xl tracking-tight text-brand">
          Mesta
        </h1>
        <p className="mt-3 max-w-2xl text-muted">
          Restorani, manastiri, muzeji i druga mesta u Vranju i okolini.
        </p>

        {errorMessage && (
          <div className="mt-10 rounded-2xl border border-border bg-surface px-6 py-8 text-muted">
            {errorMessage}
          </div>
        )}

        {!errorMessage && items.length === 0 && (
          <div className="mt-10 rounded-2xl border border-border bg-surface px-6 py-8 text-muted">
            Još nema objavljenih mesta.
          </div>
        )}

        {!errorMessage && items.length > 0 && (
          <>
            <div className="mt-10 grid gap-6 md:grid-cols-2 lg:grid-cols-3">
              {items.map((place) => (
                <PlaceCard key={place.id} place={place} />
              ))}
            </div>

            {totalPages > 1 && (
              <div className="mt-12 flex items-center justify-center gap-3">
                {page > 1 && (
                  <Link
                    href={`/mesta?page=${page - 1}`}
                    className="rounded-full border border-border bg-surface px-5 py-2 text-sm font-semibold text-brand hover:border-accent"
                  >
                    ← Prethodna
                  </Link>
                )}

                <span className="text-sm text-muted">
                  Strana {page} od {totalPages}
                </span>

                {page < totalPages && (
                  <Link
                    href={`/mesta?page=${page + 1}`}
                    className="rounded-full border border-border bg-surface px-5 py-2 text-sm font-semibold text-brand hover:border-accent"
                  >
                    Sledeća →
                  </Link>
                )}
              </div>
            )}
          </>
        )}
      </section>
    </main>
  );
}