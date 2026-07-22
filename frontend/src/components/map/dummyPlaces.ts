import type { Place } from "@/types/place";

/** Demo destinacije kada Places API nije dostupan. */
export const dummyPlaces: Place[] = [
  {
    id: "dummy-prohor",
    name: "Manastir Prohor Pčinjski",
    description:
      "Jedan od najznačajnijih manastira južne Srbije, u klisuri reke Pčinje. Mesto mira, istorije i pejzaža koji vredi posetiti.",
    category: "Monastery",
    address: "Klenike, Prohor Pčinjski",
    city: "Vranje",
    latitude: 42.327497,
    longitude: 21.865278,
    imageUrl: "/images/hero-pcinjski.jpg",
    createdAt: "2026-01-01T00:00:00.000Z",
    isPublished: true,
  },
  {
    id: "dummy-vranje-centar",
    name: "Centar Vranja",
    description:
      "Šetnja kroz jezgro grada — kafići, lokalni dućani i atmosfera južnjačkog grada.",
    category: "Other",
    address: "Trg Republike",
    city: "Vranje",
    latitude: 42.5514,
    longitude: 21.9003,
    imageUrl: null,
    createdAt: "2026-01-01T00:00:00.000Z",
    isPublished: true,
  },
  {
    id: "dummy-borina",
    name: "Borinski park",
    description:
      "Zeleni predah u gradu — staze, klupa i miran kutak za porodičnu šetnju.",
    category: "Park",
    address: "Borinski put",
    city: "Vranje",
    latitude: 42.558,
    longitude: 21.892,
    imageUrl: null,
    createdAt: "2026-01-01T00:00:00.000Z",
    isPublished: true,
  },
  {
    id: "dummy-muzeum",
    name: "Narodni muzej Vranje",
    description:
      "Zbirke koje pričaju priču o Pčinjskom okrugu — od arheologije do savremenog života.",
    category: "Museum",
    address: "Trg Republike 1",
    city: "Vranje",
    latitude: 42.5528,
    longitude: 21.8985,
    imageUrl: null,
    createdAt: "2026-01-01T00:00:00.000Z",
    isPublished: true,
  },
  {
    id: "dummy-kafana",
    name: "Kafana Stara Vranjanka",
    description:
      "Tradicionalna kuhinja i južnjački ambijent — idealno posle obilaska lokalnih znamenitosti.",
    category: "Restaurant",
    address: "Save Kovačevića 12",
    city: "Vranje",
    latitude: 42.5495,
    longitude: 21.9018,
    imageUrl: null,
    createdAt: "2026-01-01T00:00:00.000Z",
    isPublished: true,
  },
];
