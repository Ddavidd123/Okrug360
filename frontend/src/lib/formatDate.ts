export function formatDate(value: string | null) {
  if (!value) {
    return "";
  }

  return new Intl.DateTimeFormat("sr-Latn-RS", {
    day: "numeric",
    month: "long",
    year: "numeric",
  }).format(new Date(value));
}