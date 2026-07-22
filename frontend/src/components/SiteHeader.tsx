import Link from "next/link";

export function SiteHeader() {
  return (
    <header className="border-b border-border bg-surface">
      <div className="mx-auto flex max-w-6xl items-center justify-between px-6 py-4">
        <Link href="/" className="flex items-center gap-3 text-brand">
          <span className="flex h-10 w-10 items-center justify-center rounded-full bg-accent text-sm font-bold tracking-tight text-white">
            360
          </span>
          <span className="font-(family-name:--font-fraunces) text-2xl tracking-tight">
            Okrug360
          </span>
        </Link>

        <nav className="hidden items-center gap-8 text-sm font-medium text-muted md:flex">
          <Link href="/vesti" className="hover:text-brand">
            Vesti
          </Link>
          <Link href="/dogadjaji" className="hover:text-brand">
            Događaji
          </Link>
          <Link href="/mesta" className="hover:text-brand">
            Mesta
          </Link>
          <Link href="/mapa" className="hover:text-brand">
            Mapa
          </Link>
        </nav>

        <div className="flex items-center gap-3">
          <Link
            href="/prijava"
            className="hidden rounded-full px-4 py-2 text-sm font-medium text-muted hover:text-brand sm:inline-flex"
          >
            Prijavi se
          </Link>
          <Link
            href="/registracija"
            className="rounded-full bg-brand px-4 py-2 text-sm font-semibold text-white hover:bg-brand-deep"
          >
            Registruj se
          </Link>
        </div>
      </div>
    </header>
  );
}