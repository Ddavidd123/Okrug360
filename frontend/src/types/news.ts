export type NewsArticle = {
  id: string;
  title: string;
  summary: string;
  content: string;
  createdAt: string;
  publishedAt: string | null;
  isPublished: boolean;
};

export type PagedNewsArticles = {
  items: NewsArticle[];
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
};
