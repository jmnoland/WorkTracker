export interface Task {
    taskId: number;
    storyId: number;
    description: string;
    complete: boolean;
    new?: boolean; 
};
