import Link from "next/link";

export function Header() {
  return (
    <header className="absolute inset-x-0 top-0 z-20">
      <div className="mx-auto flex max-w-6xl items-center justify-between px-6 py-5">
        <Link href="/" className="flex items-center gap-3 text-white">
          <span className="flex h-10 w-10 items-center justify-center rounded-full bg-accent text-sm font-bold tracking-tight">
            360
          </span>
          <span className="font-[family-name:var(--font-fraunces)] text-2xl tracking-tight">
            Okrug360
          </span>
        </Link>

        <nav className="hidden items-center gap-8 text-sm font-medium text-white/90 md:flex">
          <Link href="/vesti" className="hover:text-white">
            Vesti
          </Link>
          <Link href="/dogadjaji" className="hover:text-white">
            Događaji
          </Link>
          <Link href="/mesta" className="hover:text-white">
            Mesta
          </Link>
          <Link href="/mapa" className="hover:text-white">
            Mapa
          </Link>
        </nav>

        <div className="flex items-center gap-3">
          <Link
            href="/prijava"
            className="hidden rounded-full px-4 py-2 text-sm font-medium text-white/90 hover:text-white sm:inline-flex"
          >
            Prijavi se
          </Link>
          <Link
            href="/registracija"
            className="rounded-full bg-white px-4 py-2 text-sm font-semibold text-brand hover:bg-accent-soft"
          >
            Registruj se
          </Link>
        </div>
      </div>
    </header>
  );
}