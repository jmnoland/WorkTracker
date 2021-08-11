import { Task } from './task';

export interface Story {
    stateId: number;
    projectId: number | null;
    sprintId: number | null;
    title: string;
    description: string;
    listOrder: number;
    tasks: Task[];
};
