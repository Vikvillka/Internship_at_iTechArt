import { Gender } from './Gender';

export interface User {
  id: string;
  username: string;
  email: string;
  gender: Gender;
  city: string;
  country: string;
}

export interface UserRegistration {
  username: string;
  email: string;
  password: string;
  gender: Gender;
  city: string;
  country: string;
}
