import { User } from './user';
import { State } from './state';
import { Team } from './team';

export interface UserDetail {
  organisation: string | null;
  users: User[];
  states: State[];
  teams: Team[];
};
