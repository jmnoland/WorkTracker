import { Task } from './task';

export interface Story {
  storyId?: number;
  stateId?: number;
  projectId?: number;
  sprintId?: number;
  title?: string;
  description?: string;
  listOrder?: number;
  createdBy?: string,
  modifiedAt?: string,
  tasks?: Task[];
};
