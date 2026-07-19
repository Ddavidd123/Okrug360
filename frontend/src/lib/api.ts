import type { PagedNewsArticles } from "@/types/news";

const apiUrl = process.env.NEXT_PUBLIC_API_URL;

if (!apiUrl) {
  throw new Error("NEXT_PUBLIC_API_URL nije podesen.");
}

export async function getPublishedNews(
  page = 1,
  pageSize = 3,
): Promise<PagedNewsArticles> {
  const response = await fetch(
    `${apiUrl}/api/news?page=${page}&pageSize=${pageSize}`,
    {
      next: { revalidate: 30 },
    },
  );

  if (!response.ok) {
    throw new Error(`API greska: ${response.status}`);
  }

  return response.json();
}