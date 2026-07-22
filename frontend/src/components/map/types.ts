import type { Place, PlaceCategory } from "@/types/place";

export type MapPlace = Place;

export type MapCategoryFilter = PlaceCategory | "All";

/** Categories shown as distinct filter chips (Monestery folded into Monastery). */
export const MAP_FILTER_CHIPS: Array<{
  value: PlaceCategory | "All";
  label: string;
}> = [
  { value: "All", label: "Sve" },
  { value: "Monastery", label: "Manastiri" },
  { value: "Restaurant", label: "Restorani" },
  { value: "Cafe", label: "Kafići" },
  { value: "Museum", label: "Muzeji" },
  { value: "Park", label: "Parkovi" },
  { value: "CulturalSite", label: "Kultura" },
  { value: "Government", label: "Ustanove" },
  { value: "Other", label: "Ostalo" },
];

export function matchesCategoryFilter(
  placeCategory: PlaceCategory | string,
  filter: PlaceCategory | "All",
): boolean {
  if (filter === "All") {
    return true;
  }

  if (filter === "Monastery") {
    return placeCategory === "Monastery" || placeCategory === "Monestery";
  }

  return placeCategory === filter;
}

export function filterMapPlaces(
  places: MapPlace[],
  query: string,
  category: PlaceCategory | "All",
): MapPlace[] {
  const normalized = query.trim().toLowerCase();

  return places.filter((place) => {
    if (!matchesCategoryFilter(place.category, category)) {
      return false;
    }

    if (!normalized) {
      return true;
    }

    return (
      place.name.toLowerCase().includes(normalized) ||
      place.address.toLowerCase().includes(normalized) ||
      place.city.toLowerCase().includes(normalized) ||
      place.description.toLowerCase().includes(normalized)
    );
  });
}
