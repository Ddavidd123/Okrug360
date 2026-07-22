export type PlaceCategory =
  | "Restaurant"
  | "Cafe"
  | "Monastery"
  | "Monestery"
  | "Museum"
  | "Park"
  | "CulturalSite"
  | "Government"
  | "Other";

export interface Place {
  id: string;
  name: string;
  description: string;
  category: PlaceCategory;
  address: string;
  city: string;
  latitude: number;
  longitude: number;
  imageUrl?: string | null;
  createdAt: string;
  updatedAt?: string | null;
  publishedAt?: string | null;
  isPublished: boolean;
}

export interface PlaceMapMarker {
  id: string;
  name: string;
  category: PlaceCategory;
  latitude: number;
  longitude: number;
  imageUrl?: string | null;
}

export interface PagedPlacesResponse {
  items: Place[];
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
}