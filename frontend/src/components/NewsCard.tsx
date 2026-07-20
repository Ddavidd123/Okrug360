import Link from "next/link";
import { formatDate } from "@/lib/formatDate";
import type { NewsArticle } from "@/types/news";

export function NewsCard({ article }: { article: NewsArticle }) {
  return (
    <article className="flex flex-col rounded-2xl border border-border bg-surface p-6 shadow-[0_8px_24px_rgba(16,42,67,0.06)] transition hover:-translate-y-0.5 hover:shadow-[0_12px_28px_rgba(16,42,67,0.1)]">
      <time
        className="text-sm text-muted"
        dateTime={article.publishedAt ?? undefined}
      >
        {formatDate(article.publishedAt)}
      </time>

      <h3 className="mt-3 font-[family-name:var(--font-fraunces)] text-xl leading-snug text-brand">
        <Link href={`/vesti/${article.id}`} className="hover:text-accent">
          {article.title}
        </Link>
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