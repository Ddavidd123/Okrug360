import Link from "next/link";
import { getPublishedNews } from "@/lib/api";
import type { NewsArticle } from "@/types/news";

function formatDate(value: string | null) {
  if (!value) {
    return "";
  }

  return new Intl.DateTimeFormat("sr-Latn-RS", {
    day: "numeric",
    month: "long",
    year: "numeric",
  }).format(new Date(value));
}

function NewsCard({ article }: { article: NewsArticle }) {
  return (
    <article className="flex flex-col rounded-2xl border border-border bg-surface p-6 shadow-[0_8px_24px_rgba(16,42,67,0.06)] transition hover:-translate-y-0.5 hover:shadow-[0_12px_28px_rgba(16,42,67,0.1)]">
      <time
        className="text-sm text-muted"
        dateTime={article.publishedAt ?? undefined}
      >
        {formatDate(article.publishedAt)}
      </time>

      <h3 className="mt-3 font-[family-name:var(--font-fraunces)] text-xl leading-snug text-brand">
        {article.title}
      </h3>

      <p className="mt-3 flex-1 text-sm leading-relaxed text-muted">
        {article.summary}
      </p>

      <Link
        href={`/vesti/${article.id}`}
        className="mt-6 text-sm font-semibold text-accent hover:text-brand"
      >
        Pročitaj više →
      </Link>
    </article>
  );
}

export async function NewsSection() {
  let articles: NewsArticle[] = [];
  let errorMessage: string | null = null;

  try {
    const result = await getPublishedNews(1, 3);
    articles = result.items;
  } catch {
    errorMessage =
      "Trenutno ne možemo da učitamo vesti. Proveri da li Content API radi.";
  }

  return (
    <section className="bg-background px-6 py-20">
      <div className="mx-auto max-w-6xl">
        <div className="mb-10 flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between">
          <div>
            <p className="mb-2 text-sm font-semibold uppercase tracking-[0.18em] text-accent">
              Informacije
            </p>
            <h2 className="font-[family-name:var(--font-fraunces)] text-3xl tracking-tight text-brand sm:text-4xl">
              Najnovije vesti
            </h2>
            <p className="mt-3 max-w-xl text-muted">
              Obaveštenja i lokalne vesti iz Pčinjskog okruga.
            </p>
          </div>

          <Link
            href="/vesti"
            className="inline-flex items-center justify-center rounded-full border border-border bg-surface px-5 py-2.5 text-sm font-semibold text-brand hover:border-accent hover:text-accent"
          >
            Sve vesti
          </Link>
        </div>

        {errorMessage && (
          <div className="rounded-2xl border border-border bg-surface px-6 py-8 text-muted">
            {errorMessage}
          </div>
        )}

        {!errorMessage && articles.length === 0 && (
          <div className="rounded-2xl border border-border bg-surface px-6 py-8 text-muted">
            Još nema objavljenih vesti. Kreiraj jednu preko Swaggera sa{" "}
            <code className="text-brand">publishImmediately: true</code>.
          </div>
        )}

        {!errorMessage && articles.length > 0 && (
          <div className="grid gap-6 md:grid-cols-3">
            {articles.map((article) => (
              <NewsCard key={article.id} article={article} />
            ))}
          </div>
        )}
      </div>
    </section>
  );
}