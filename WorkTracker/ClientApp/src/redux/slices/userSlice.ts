import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { UserDetail, DecodedToken } from "../../types";
import { UserLogin, DemoLogin } from "../../services/auth";
import { decodeJwtToken } from "../../helper";
import { GetDetails } from "../../services/user";

const initialUserDetail: UserDetail = {
  organisation: null,
  states: [],
  teams: [],
  users: [],
};

const initialState: {
  userDetail: UserDetail,
  decodedToken: DecodedToken | null,
  user: string | null,
  permissions: string[],
  isLoggedIn: boolean,
} = {
  userDetail: initialUserDetail,
  decodedToken: null,
  user: null,
  permissions: [],
  isLoggedIn: false,
};

const loginWithEmail = createAsyncThunk(
  "user/login",
  async ({ email, password }: { email: string, password: string }) => {
    const token = await UserLogin(email, password);
    const userDetail = await GetDetails();
    return { token, userDetail };
  }
);
const loginWithDemo = createAsyncThunk(
  "user/demoLogin",
  async () => {
    const token = await DemoLogin();
    const userDetail = await GetDetails();
    return { token, userDetail };
  }
);

export const userSlice = createSlice({
  name: "user",
  initialState: initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
      state.permissions = [];
      state.isLoggedIn = false;
      state.userDetail = initialUserDetail;
      localStorage.removeItem("X-User-Token");
    },
  },
  extraReducers: (builder) => {
    builder.addCase(loginWithEmail.fulfilled, (state, action) => {
      const { token, userDetail } = action.payload;
      const decodedToken = decodeJwtToken(token);
      state.userDetail = userDetail;
      state.decodedToken = decodedToken;
      if (decodedToken) {
        state.user = decodedToken.nameid;
        state.permissions = decodedToken.role;
      }
      state.isLoggedIn = true;
    });
    builder.addCase(loginWithDemo.fulfilled, (state, action) => {
      const { token, userDetail } = action.payload;
      const decodedToken = decodeJwtToken(token);
      state.userDetail = userDetail;
      state.decodedToken = decodedToken;
      if (decodedToken) {
        state.user = decodedToken.nameid;
        state.permissions = decodedToken.role;
      }
      state.isLoggedIn = true;
    });
  },
});

const { logout } = userSlice.actions;
export { logout, loginWithEmail, loginWithDemo };
export default userSlice.reducer;
