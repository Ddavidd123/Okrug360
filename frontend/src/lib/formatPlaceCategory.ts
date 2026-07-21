import type { PlaceCategory } from "@/types/place";

const categoryLabels: Record<string, string> = {
  Restaurant: "Restoran",
  Cafe: "Kafić",
  Monastery: "Manastir",
  Monestery: "Manastir",
  Museum: "Muzej",
  Park: "Park",
  CulturalSite: "Kulturni spomenik",
  Government: "Javna ustanova",
  Other: "Ostalo",
};

export function formatPlaceCategory(category: PlaceCategory | string): string {
  return categoryLabels[category] ?? category;
}