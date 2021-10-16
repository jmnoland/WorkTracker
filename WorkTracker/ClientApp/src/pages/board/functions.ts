import { Story, Task, Dictionary } from "../../types";
import { DeleteTask } from "../../services/story";

export function getMaxHeight(
  container: HTMLDivElement,
  header: HTMLDivElement,
  footer: HTMLDivElement,
): string {
  const hh = header.getBoundingClientRect().height;
  const ch = container.getBoundingClientRect().height;
  const fh = footer.getBoundingClientRect().height;
  // element heights - 22 padding 20 margin
  return `${ch - hh - fh - 22 - 20}px`;
}

export function createNewTask(taskCount: number, storyId?: number | null): Task {
  return {
    taskId: taskCount + 1,
    storyId: storyId ?? 0,
    description: "",
    complete: false,
    new: true,
  };
}

export function handleTaskChange(tasks: Task[], taskId: number, value: string): Task[] {
  const temp = tasks.find((task) => task.taskId === taskId);
  const items = tasks.reduce((total, task) => {
    if (task.taskId !== taskId) total.push(task);
    else total.push({ ...temp, description: value } as Task);
    return total;
  }, [] as Task[]);
  return items;
}

export function parseTasks(tasks: Task[]) : Task[] {
  return tasks &&
  tasks.reduce((total, task) => {
    const desc = task.description && task.description.trim();
    if (desc) total.push(task);
    return total;
  }, [] as Task[]);
}

export async function removeTaskById(tasks: Task[], taskId: number): Promise<Task[]> {
  const taskToRemove = tasks.filter((task) => task.taskId === taskId)[0];
  if (taskToRemove && !taskToRemove.new) await DeleteTask(taskId);
  return [...tasks.filter((task) => task.taskId !== taskId)];
}

export function reorder(list: Story[], startIndex: number, endIndex: number): Story[] {
  const result = Array.from(list);
  const [removed] = result.splice(startIndex, 1);
  result.splice(endIndex, 0, removed);
  return result;
};

export function createPayload(stateId: string | number, storyList: Story[]): { stateId: number, stories: Dictionary<number> }{
  const temp = storyList.reduce((total, story, index) => {
    if (story.storyId !== undefined) total[story.storyId] = index;
    return total;
  }, {} as Dictionary<number>);
  return { stateId: Number(stateId), stories: temp };
}
