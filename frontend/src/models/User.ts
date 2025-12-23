import { Gender } from './Gender';

export interface User {
  id: string;
  username: string;
  email: string;
  gender: Gender;
  city: string;
  country: string;
}
