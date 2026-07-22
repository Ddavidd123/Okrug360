"use client";

import dynamic from "next/dynamic";
import type { MapPlace } from "@/components/map/types";

const MapExplorer = dynamic(
  () =>
    import("@/components/map/MapExplorer").then((module) => module.MapExplorer),
  {
    ssr: false,
    loading: () => (
      <div className="flex h-[min(72vh,720px)] min-h-[420px] items-center justify-center rounded-[1.75rem] border border-border bg-surface text-muted">
        Učitavanje mape...
      </div>
    ),
  },
);

type MapExplorerLoaderProps = {
  places: MapPlace[];
  usingFallback?: boolean;
};

export function MapExplorerLoader({
  places,
  usingFallback,
}: MapExplorerLoaderProps) {
  return <MapExplorer places={places} usingFallback={usingFallback} />;
}
