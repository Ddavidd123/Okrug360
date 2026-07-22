import Link from "next/link";
import { NewsCard } from "@/components/NewsCard";
import { SiteHeader } from "@/components/SiteHeader";
import { getPublishedNews } from "@/lib/api";
import type { NewsArticle } from "@/types/news";

type VestiPageProps = {
  searchParams: Promise<{
    page?: string;
  }>;
};

export default async function VestiPage({ searchParams }: VestiPageProps) {
  const params = await searchParams;
  const page = Math.max(1, Number(params.page ?? "1") || 1);
  const pageSize = 9;

  let errorMessage: string | null = null;
  let items: NewsArticle[] = [];
  let totalPages = 0;

  try {
    const result = await getPublishedNews(page, pageSize);
    items = result.items;
    totalPages = result.totalPages;
  } catch {
    errorMessage =
      "Trenutno ne možemo da učitamo vesti. Proveri da li Content API radi.";
  }

  return (
    <main className="min-h-screen bg-background">
      <SiteHeader />

      <section className="mx-auto max-w-6xl px-6 py-12">
        <p className="mb-2 text-sm font-semibold uppercase tracking-[0.18em] text-accent">
          Informacije
        </p>
        <h1 className="font-(family-name:--font-fraunces) text-4xl tracking-tight text-brand">
          Vesti
        </h1>
        <p className="mt-3 max-w-2xl text-muted">
          Sve objavljene vesti i obaveštenja iz Pčinjskog okruga.
        </p>

        {errorMessage && (
          <div className="mt-10 rounded-2xl border border-border bg-surface px-6 py-8 text-muted">
            {errorMessage}
          </div>
        )}

        {!errorMessage && items.length === 0 && (
          <div className="mt-10 rounded-2xl border border-border bg-surface px-6 py-8 text-muted">
            Još nema objavljenih vesti.
          </div>
        )}

        {!errorMessage && items.length > 0 && (
          <>
            <div className="mt-10 grid gap-6 md:grid-cols-2 lg:grid-cols-3">
              {items.map((article) => (
                <NewsCard key={article.id} article={article} />
              ))}
            </div>

            {totalPages > 1 && (
              <div className="mt-12 flex items-center justify-center gap-3">
                {page > 1 && (
                  <Link
                    href={`/vesti?page=${page - 1}`}
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
                    href={`/vesti?page=${page + 1}`}
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
