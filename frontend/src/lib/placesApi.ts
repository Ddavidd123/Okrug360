import { PagedPlacesResponse, Place, PlaceMapMarker, PlaceCategory } from "@/types/place";

const placesApiUrl =
  process.env.NEXT_PUBLIC_PLACES_API_URL ?? "http://localhost:5174";

type GetPlacesParams = {
  page?: number;
  pageSize?: number;
  city?: string;
  category?: PlaceCategory;
};

export async function getPlaces(
  params: GetPlacesParams = {}
): Promise<PagedPlacesResponse> {
  const searchParams = new URLSearchParams();

  if (params.page) {
    searchParams.set("page", params.page.toString());
  }

  if (params.pageSize) {
    searchParams.set("pageSize", params.pageSize.toString());
  }

  if (params.city) {
    searchParams.set("city", params.city);
  }

  if (params.category) {
    searchParams.set("category", params.category);
  }

  const query = searchParams.toString();
  const url = query
    ? `${placesApiUrl}/api/places?${query}`
    : `${placesApiUrl}/api/places`;

  const response = await fetch(url, {
    next: { revalidate: 60 },
  });

  if (!response.ok) {
    throw new Error("Neuspešno učitavanje mesta.");
  }

  return response.json();
}

export async function getPlaceById(id: string): Promise<Place> {
  const response = await fetch(`${placesApiUrl}/api/places/${id}`, {
    next: { revalidate: 60 },
  });

  if (!response.ok) {
    throw new Error("Mesto nije pronađeno.");
  }

  return response.json();
}

export async function getPlaceMapMarkers(
  params: Pick<GetPlacesParams, "city" | "category"> = {}
): Promise<PlaceMapMarker[]> {
  const searchParams = new URLSearchParams();

  if (params.city) {
    searchParams.set("city", params.city);
  }

  if (params.category) {
    searchParams.set("category", params.category);
  }

  const query = searchParams.toString();
  const url = query
    ? `${placesApiUrl}/api/places/map?${query}`
    : `${placesApiUrl}/api/places/map`;

  const response = await fetch(url, {
    next: { revalidate: 60 },
  });

  if (!response.ok) {
    throw new Error("Neuspešno učitavanje mape mesta.");
  }

  return response.json();
}