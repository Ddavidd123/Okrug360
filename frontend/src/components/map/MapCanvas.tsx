"use client";

import { useEffect, useRef, useState } from "react";
import {
  LngLatBounds,
  Map as MapLibreMap,
  Marker,
  NavigationControl,
  type StyleSpecification,
} from "maplibre-gl";
import { createMarkerElement } from "@/components/map/mapMarkers";
import type { MapPlace } from "@/components/map/types";

import "maplibre-gl/dist/maplibre-gl.css";

const VRANJE_CENTER: [number, number] = [21.9, 42.551];

/** Reliable raster basemap — no API key, works without vector tile quirks. */
const RASTER_STYLE: StyleSpecification = {
  version: 8,
  sources: {
    carto: {
      type: "raster",
      tiles: [
        "https://a.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}.png",
        "https://b.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}.png",
        "https://c.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}.png",
      ],
      tileSize: 256,
      attribution:
        '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> &copy; <a href="https://carto.com/attributions">CARTO</a>',
    },
  },
  layers: [
    {
      id: "carto",
      type: "raster",
      source: "carto",
    },
  ],
};

type MapCanvasProps = {
  places: MapPlace[];
  selectedId: string | null;
  onSelect: (id: string) => void;
};

function syncMarkers(
  map: MapLibreMap,
  places: MapPlace[],
  markers: Map<string, Marker>,
  selectedId: string | null,
  onSelect: (id: string) => void,
  fitIfNeeded: boolean,
) {
  const nextIds = new Set(places.map((place) => place.id));

  markers.forEach((marker, id) => {
    if (!nextIds.has(id)) {
      marker.remove();
      markers.delete(id);
    }
  });

  places.forEach((place) => {
    const existing = markers.get(place.id);
    const selected = place.id === selectedId;

    if (existing) {
      existing.getElement().classList.toggle("is-selected", selected);
      return;
    }

    const el = createMarkerElement(place.category, {
      selected,
      label: place.name,
    });

    el.addEventListener("click", (event) => {
      event.stopPropagation();
      onSelect(place.id);
    });

    const marker = new Marker({ element: el, anchor: "center" })
      .setLngLat([place.longitude, place.latitude])
      .addTo(map);

    markers.set(place.id, marker);
  });

  if (!fitIfNeeded || places.length === 0 || selectedId) {
    return;
  }

  const bounds = new LngLatBounds();
  places.forEach((place) => {
    bounds.extend([place.longitude, place.latitude]);
  });

  map.fitBounds(bounds, {
    padding: 72,
    maxZoom: 12.5,
    pitch: 40,
    bearing: -10,
    duration: 800,
  });
}

export function MapCanvas({ places, selectedId, onSelect }: MapCanvasProps) {
  const containerRef = useRef<HTMLDivElement | null>(null);
  const mapRef = useRef<MapLibreMap | null>(null);
  const markersRef = useRef<Map<string, Marker>>(new Map());
  const onSelectRef = useRef(onSelect);
  const selectedIdRef = useRef(selectedId);
  const placesRef = useRef(places);
  const [initError, setInitError] = useState<string | null>(null);

  useEffect(() => {
    onSelectRef.current = onSelect;
  }, [onSelect]);

  useEffect(() => {
    selectedIdRef.current = selectedId;
  }, [selectedId]);

  useEffect(() => {
    placesRef.current = places;
  }, [places]);

  useEffect(() => {
    if (!containerRef.current || mapRef.current) {
      return;
    }

    const container = containerRef.current;
    let map: MapLibreMap;

    try {
      map = new MapLibreMap({
        container,
        style: RASTER_STYLE,
        center: VRANJE_CENTER,
        zoom: 10.2,
        pitch: 40,
        bearing: -10,
        maxPitch: 60,
        attributionControl: { compact: true },
      });
    } catch (error) {
      console.error("[MapCanvas] init failed", error);
      setInitError(
        "Mapa nije mogla da se pokrene (WebGL). Proveri da li je hardversko ubrzanje uključeno u browseru.",
      );
      return;
    }

    map.addControl(
      new NavigationControl({
        visualizePitch: true,
        showCompass: true,
      }),
      "bottom-right",
    );

    const onLoad = () => {
      map.resize();
      syncMarkers(
        map,
        placesRef.current,
        markersRef.current,
        selectedIdRef.current,
        (id) => onSelectRef.current(id),
        true,
      );
    };

    map.on("load", onLoad);
    map.on("error", (event) => {
      console.error("[MapCanvas]", event.error);
    });

    mapRef.current = map;

    const resizeObserver = new ResizeObserver(() => {
      map.resize();
    });
    resizeObserver.observe(container);

    const resizeTimers = [100, 400, 1000].map((ms) =>
      window.setTimeout(() => map.resize(), ms),
    );

    return () => {
      resizeTimers.forEach((id) => window.clearTimeout(id));
      resizeObserver.disconnect();
      markersRef.current.forEach((marker) => marker.remove());
      markersRef.current.clear();
      map.remove();
      mapRef.current = null;
    };
  }, []);

  useEffect(() => {
    const map = mapRef.current;
    if (!map?.isStyleLoaded()) {
      return;
    }

    syncMarkers(
      map,
      places,
      markersRef.current,
      selectedIdRef.current,
      (id) => onSelectRef.current(id),
      !selectedIdRef.current,
    );
  }, [places]);

  useEffect(() => {
    const map = mapRef.current;
    if (!map || !selectedId) {
      return;
    }

    markersRef.current.forEach((marker, id) => {
      marker.getElement().classList.toggle("is-selected", id === selectedId);
    });

    const place = places.find((item) => item.id === selectedId);
    if (!place) {
      return;
    }

    const reduceMotion =
      typeof window !== "undefined" &&
      window.matchMedia("(prefers-reduced-motion: reduce)").matches;

    map.flyTo({
      center: [place.longitude, place.latitude],
      zoom: Math.max(map.getZoom(), 12),
      pitch: 40,
      bearing: map.getBearing(),
      duration: reduceMotion ? 0 : 1000,
      essential: true,
    });
  }, [selectedId, places]);

  if (initError) {
    return (
      <div className="flex h-full min-h-[420px] items-center justify-center bg-[#e8eef2] px-6 text-center text-sm text-muted">
        {initError}
      </div>
    );
  }

  return (
    <div
      ref={containerRef}
      className="map-canvas h-full min-h-[420px] w-full bg-[#e8eef2]"
    />
  );
}
