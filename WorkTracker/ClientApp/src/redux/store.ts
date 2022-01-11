import { configureStore, getDefaultMiddleware } from "@reduxjs/toolkit";
import { UserSlice } from "./slices";
import loggerMiddleware from "./middleware";

const store = configureStore({
  reducer: UserSlice,
  middleware: [loggerMiddleware, ...getDefaultMiddleware()],
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
