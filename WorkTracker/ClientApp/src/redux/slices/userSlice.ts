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

function getInitialState() {
  const initialState: {
    userDetail: UserDetail,
    decodedToken: DecodedToken | null,
    user: string | undefined | null,
    permissions: string[] | undefined,
    isLoggedIn: boolean,
  } = {
    userDetail: initialUserDetail,
    decodedToken: null,
    user: null,
    permissions: [],
    isLoggedIn: false,
  };
  const token = localStorage.getItem("X-User-Token");
  if (token === null || token === undefined) return initialState;
  const decodedToken = decodeJwtToken(token);
  initialState.decodedToken = decodedToken;
  initialState.user = decodedToken?.nameid;
  initialState.isLoggedIn = true;
  initialState.permissions = decodedToken?.role;
  return initialState;
}

const getUserDetails = createAsyncThunk(
  "user/detail",
  async () => {
    const userDetail = await GetDetails();
    return { userDetail };
  }
);
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
  initialState: getInitialState(),
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
    builder.addCase(getUserDetails.fulfilled, (state, action) => {
      const { userDetail } = action.payload;
      state.userDetail = userDetail;
    });
  },
});

const { logout } = userSlice.actions;
export { logout, loginWithEmail, loginWithDemo, getUserDetails };
export default userSlice.reducer;
