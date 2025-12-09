export const getEventImageUrl = (imagePath: string): string => {
  const baseUrl = process.env.REACT_APP_API_URL || 'http://localhost:5000';
  if (imagePath) {
    return `${baseUrl}/image?fileName=${imagePath}`;
  }
  return '/imgs/default.png';
};
