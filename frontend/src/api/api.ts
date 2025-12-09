import axios from 'axios';

const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL ?? 'https://localhost:5000',
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    const customError = {
      message: error.response?.data.detail || error.message || 'An unknown error occurred',
      status: error.response?.status || 500,
    };
    return Promise.reject(customError);
  },
);

export { api };
