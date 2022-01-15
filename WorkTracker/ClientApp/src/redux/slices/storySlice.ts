import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Story, Task } from "../../types";
import {
  CreateStory,
  UpdateStory,
  DeleteStory,
  DeleteTask,
  GetStoryTasks,
  OrderUpdate,
  ChangeState,
  GetStories,
} from "../../services/story";

const initialState: {
  stories: Record<number, Story[]>,
  tasks: Record<number, Task[]>,
} = {
  stories: {},
  tasks: {}
};

const createStory = createAsyncThunk(
  "story/create",
  async (
    {
     title,
     description,
     storyPosition,
     state,
     tasks,
    }: {
      title: string,
      description: string,
      storyPosition: number,
      state: number,
      tasks: Task[]
    }) => {
    await CreateStory(
      title,
      description,
      storyPosition,
      state,
      tasks,
    );
    return {
      stories: await GetStories(state),
      stateId: state,
    };
  }
);

const updateStory = createAsyncThunk(
  "story/update",
  async (
    {
      storyId,
      listOrder,
      title,
      description,
      state,
      tasks,
    }: {
      storyId: number,
      listOrder: number,
      title: string,
      description: string,
      state: number,
      tasks: Task[]
    }) => {
    await UpdateStory(
      storyId,
      listOrder,
      title,
      description,
      state,
      tasks,
    );
    return {
      storyId,
      listOrder,
      title,
      description,
      state,
      tasks,
    };
  }
);

const deleteStory = createAsyncThunk(
  "story/delete",
  async (
    {
      storyId,
    }: {
      storyId: number
    }) => {
    await DeleteStory(storyId);
    return storyId;
  }
);

const deleteTask = createAsyncThunk(
  "story/task/delete",
  async (
    {
      storyId,
      taskId,
    }: {
      storyId: number,
      taskId: number,
    }) => {
    await DeleteTask(taskId);
    return { storyId, taskId };
  }
);

const getStoryTasks = createAsyncThunk(
  "story/tasks/get",
  async (
    {
      storyId,
    }: {
      storyId: number,
    }) => {
    return { storyId, tasks: await GetStoryTasks(storyId) };
  }
);

const getStories = createAsyncThunk(
  "story/get",
  async (
    {
      stateId
    }: {
      stateId: number
    }) => {
    return {
      stories: await GetStories(stateId),
      stateId: stateId,
    };
  }
);

const orderUpdate = createAsyncThunk(
  "story/reorder",
  async (
    {
      stateId,
      stories,
    }: {
      stateId: number,
      stories: Record<string, number>,
    }) => {
    await OrderUpdate({stateId, stories});
    return { stateId, stories };
  }
);

const changeState = createAsyncThunk(
  "story/state/change",
  async (
    {
      currentStateId,
      storyId,
      stateId,
      stories,
    }: {
      currentStateId: number,
      storyId: number,
      stateId: number,
      stories: Record<string, number>,
    }) => {
    await ChangeState(storyId, {stateId, stories});
    return { storyId, currentStateId, stateId, stories };
  }
);

function SetNewStoryState(
  {
    stateId,
    stories,
  }: { stateId: number, stories: Story[] },
  state: any,
) {
  Object.keys(state.stories).forEach((key) => {
    const stateIdKey = parseInt(key);
    if (stateId === stateIdKey) {
      state.stories[stateIdKey] = stories;
    }
  });
}

export const storySlice = createSlice({
  name: "story",
  initialState: initialState,
  reducers: {
    updateList: (state, action: PayloadAction<Record<number, Story[]>>) => {
      const stateId = parseInt(Object.keys(action.payload)[0]);
      state.stories[stateId] = action.payload[stateId];
    },
    updateListById: (
      state,
      action: PayloadAction<{ stateId: number, stories: Record<string, number>}>,
    ) => {
      const { stateId, stories } = action.payload;
      const updatedStories: Story[] = [];
      state.stories[stateId].forEach(story => {
        if (story.storyId && stories[story.storyId]) {
          const place = stories[story.storyId];
          if (updatedStories.length < place) updatedStories.push(story);
          else updatedStories.splice(place, 0, story);
        }
      });
    },
  },
  extraReducers: (builder) => {
    builder.addCase(getStories.fulfilled, (state, action) => {
      SetNewStoryState(action.payload, state);
    });
    builder.addCase(getStoryTasks.fulfilled, (state, action) => {
      const { storyId, tasks } = action.payload;
      state.tasks[storyId] = tasks;
    });
    builder.addCase(deleteTask.fulfilled, (state, action) => {
      const { storyId, taskId } = action.payload;
      state.tasks[storyId] = state.tasks[storyId]
        .filter(task => task.taskId !== taskId);
    });
    builder.addCase(orderUpdate.fulfilled, (state, action) => {
      const result = action.payload;
      updateListById(result);
    });
    builder.addCase(changeState.fulfilled, (state, action) => {
      const { storyId, currentStateId, stateId } = action.payload;
      let storyToMove: Story = {};
      for(const index in state.stories[currentStateId]) {
        const story = state.stories[currentStateId][index];
        if (story.storyId === storyId) {
          storyToMove = state.stories[currentStateId]
            .splice(parseInt(index), 1)[0];
          break;
        }
      }
      if (storyToMove.storyId) {
        state.stories[stateId].push(storyToMove);
      }
      updateListById(action.payload);
    });
    builder.addCase(createStory.fulfilled, (state, action) => {
      SetNewStoryState(action.payload, state);
    });
    builder.addCase(updateStory.fulfilled, (state, action) => {
      const payload = action.payload;
      for(const story of state.stories[payload.state]) {
        if (story.storyId === payload.storyId) {
          story.tasks = payload.tasks;
          story.title = payload.title;
          story.description = payload.description;
        }
      }
    });
    builder.addCase(deleteStory.fulfilled, (state, action) => {
      const storyId = action.payload;
      for(const stateId in Object.keys(state.stories)) {
        state.stories[stateId] = state.stories[stateId]
          .filter((val) => val.storyId !== storyId);
      }
    });
  },
});

const { updateList, updateListById } = storySlice.actions;
export {
  updateList,
  updateListById,
  getStories,
  getStoryTasks,
  orderUpdate,
  changeState,
  createStory,
  updateStory,
  deleteStory,
};
export default storySlice.reducer;
