import type { PlaceCategory } from "@/types/place";

export function markerColor(category: PlaceCategory | string): string {
  switch (category) {
    case "Restaurant":
    case "Cafe":
      return "#1f8a7a";
    case "Park":
      return "#2f6f5e";
    case "Monastery":
    case "Monestery":
    case "Museum":
    case "CulturalSite":
      return "#0b3d5c";
    case "Government":
      return "#486581";
    default:
      return "#627d98";
  }
}

function markerIconSvg(category: PlaceCategory | string): string {
  switch (category) {
    case "Monastery":
    case "Monestery":
      return `
        <svg class="map-marker__icon" viewBox="0 0 24 24" aria-hidden="true">
          <path d="M12 2.5v2.2" />
          <path d="M10.2 3.6h3.6" />
          <path d="M7 9.5 12 6.2 17 9.5v2.1H7z" />
          <path d="M8.2 11.6h7.6v8.2H8.2z" />
          <path d="M11.1 15.2h1.8v4.6h-1.8z" />
          <path d="M6.2 19.8h11.6" />
        </svg>
      `;
    case "Restaurant":
    case "Cafe":
      return `
        <svg class="map-marker__icon" viewBox="0 0 24 24" aria-hidden="true">
          <path d="M8 4v7.5a2 2 0 0 0 2 2H12" />
          <path d="M8 8h3.5" />
          <path d="M16 4.5v15" />
          <path d="M14.2 4.5h3.6" />
        </svg>
      `;
    case "Park":
      return `
        <svg class="map-marker__icon" viewBox="0 0 24 24" aria-hidden="true">
          <path d="M12 20.5V12" />
          <path d="M12 13.2c-2.8 0-5-2-5-4.5S9.2 4.2 12 4.2s5 2 5 4.5-2.2 4.5-5 4.5z" />
        </svg>
      `;
    case "Museum":
    case "CulturalSite":
      return `
        <svg class="map-marker__icon" viewBox="0 0 24 24" aria-hidden="true">
          <path d="M4.5 10.2 12 5.5l7.5 4.7" />
          <path d="M6.2 10.2v7.6" />
          <path d="M10 10.2v7.6" />
          <path d="M14 10.2v7.6" />
          <path d="M17.8 10.2v7.6" />
          <path d="M4.8 17.8h14.4" />
        </svg>
      `;
    default:
      return `
        <svg class="map-marker__icon" viewBox="0 0 24 24" aria-hidden="true">
          <circle cx="12" cy="12" r="3.2" />
        </svg>
      `;
  }
}

export function createMarkerElement(
  category: PlaceCategory | string,
  options: { selected?: boolean; label: string } = { label: "" },
): HTMLButtonElement {
  const color = markerColor(category);
  const button = document.createElement("button");
  button.type = "button";
  button.className = `map-marker-wrap${options.selected ? " is-selected" : ""}`;
  button.setAttribute("aria-label", options.label);
  button.innerHTML = `
    <span class="map-marker" style="--pin:${color}">
      ${markerIconSvg(category)}
    </span>
  `;
  return button;
}
