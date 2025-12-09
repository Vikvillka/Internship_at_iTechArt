export type Gender = 'Male' | 'Female' | 'Other';

export interface User {
  id: string;
  username: string;
  email: string;
  gender: Gender;
  city: string;
  country: string;
}
