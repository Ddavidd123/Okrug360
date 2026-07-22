import Link from "next/link";
import { notFound } from "next/navigation";
import { SiteHeader } from "@/components/SiteHeader";
import { getPublishedNewsById } from "@/lib/api";
import { formatDate } from "@/lib/formatDate";

type VestDetailPageProps = {
  params: Promise<{
    id: string;
  }>;
};

export default async function VestDetailPage({ params }: VestDetailPageProps) {
  const { id } = await params;

  let article = null;

  try {
    article = await getPublishedNewsById(id);
  } catch {
    notFound();
  }

  if (!article) {
    notFound();
  }

  return (
    <main className="min-h-screen bg-background">
      <SiteHeader />

      <article className="mx-auto max-w-3xl px-6 py-12">
        <Link
          href="/vesti"
          className="text-sm font-semibold text-accent hover:text-brand"
        >
          ← Nazad na vesti
        </Link>

        <p className="mt-8 text-sm text-muted">
          <time dateTime={article.publishedAt ?? undefined}>
            {formatDate(article.publishedAt)}
          </time>
        </p>

        <h1 className="mt-3 font-(family-name:--font-fraunces) text-4xl leading-tight tracking-tight text-brand sm:text-5xl">
          {article.title}
        </h1>

        <p className="mt-5 text-lg leading-relaxed text-muted">
          {article.summary}
        </p>

        <div className="mt-10 whitespace-pre-wrap text-base leading-8 text-foreground">
          {article.content}
        </div>
      </article>
    </main>
  );
}
