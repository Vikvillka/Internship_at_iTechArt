export function parseLocation(location: string): { city: string; country: string } {
  if (!location) {
    return { city: '', country: '' };
  }
  const parts = location.split(',').map((part) => part.trim());
  const city = parts[0] || '';
  const country = parts.length > 1 ? parts[parts.length - 1] : '';
  return { city, country };
}
