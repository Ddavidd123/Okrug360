import Image from "next/image";
import Link from "next/link";

export function Hero() {
  return (
    <section className="relative min-h-screen overflow-hidden">
      <Image
        src="/images/hero-pcinjski.jpg"
        alt="Manastir Prohor Pčinjski u klisuri reke Pčinje"
        fill
        priority
        className="object-cover object-[center_40%]"
      />

      <div className="absolute inset-0 bg-linear-to-r from-brand-deep/85 via-brand/55 to-transparent" />

      <div className="relative z-10 mx-auto flex min-h-screen max-w-6xl items-center px-6 pb-20 pt-28">
        <div className="max-w-xl text-white">
          <p className="mb-4 text-sm font-semibold uppercase tracking-[0.2em] text-white/75">
            Pčinjski okrug
          </p>

          <h1 className="font-(family-name:--font-fraunces) text-5xl leading-tight tracking-tight sm:text-6xl">
            Okrug360
          </h1>

          <p className="mt-4 font-(family-name:--font-fraunces) text-2xl text-white/95 sm:text-3xl">
            Pitaj. Pronađi. Učestvuj.
          </p>

          <p className="mt-5 max-w-md text-base leading-relaxed text-white/85 sm:text-lg">
            Lokalne vesti, događaji i mesta koja vredi posetiti — sve na jednom
            mestu za građane Pčinjskog okruga.
          </p>

          <div className="mt-8 flex flex-wrap gap-3">
            <Link
              href="/vesti"
              className="rounded-full bg-accent px-6 py-3 text-sm font-semibold text-white hover:bg-teal-700"
            >
              Pogledaj vesti
            </Link>
            <Link
              href="/mesta"
              className="rounded-full border border-white/40 bg-white/10 px-6 py-3 text-sm font-semibold text-white backdrop-blur hover:bg-white/20"
            >
              Istraži mesta
            </Link>
          </div>
        </div>
      </div>

      <p className="absolute bottom-4 right-4 z-10 max-w-xs text-right text-[11px] leading-snug text-white/65">
        Foto:{" "}
        <a
          href="https://commons.wikimedia.org/wiki/File:Pcinja_river_valley.jpg"
          target="_blank"
          rel="noopener noreferrer"
          className="underline underline-offset-2 hover:text-white"
        >
          Geograf208
        </a>
        {" · "}
        <a
          href="https://creativecommons.org/licenses/by-sa/4.0/"
          target="_blank"
          rel="noopener noreferrer"
          className="underline underline-offset-2 hover:text-white"
        >
          CC BY-SA 4.0
        </a>
      </p>
    </section>
  );
}
