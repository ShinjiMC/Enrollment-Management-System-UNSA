import { Action, AuthState } from "../types";

const initialState: AuthState = {
  authData: null,
  loading: false,
  error: false,
  updateLoading: false,
};
// The authReducer function is a reducer that manages the state of the authentication
const authReducer = (
  state: AuthState = initialState,
  action: Action
): AuthState => {
  // The reducer function takes the current state and an action, and returns the new state
  switch (action.type) {
    case "AUTH_START":
      return { ...state, loading: true, error: false };
    case "AUTH_SUCCESS":
      localStorage.setItem("profile", JSON.stringify({ ...action.data }));
      return {
        ...state,
        authData: action.data || null,
        loading: false,
        error: false,
      };

    case "AUTH_FAIL":
      return { ...state, loading: false, error: true };
    case "UPDATING_START":
      return { ...state, updateLoading: true, error: false };
    case "UPDATING_SUCCESS":
      localStorage.setItem("profile", JSON.stringify({ ...action.data }));
      return {
        ...state,
        authData: action.data || null,
        updateLoading: false,
        error: false,
      };

    case "UPDATING_FAIL":
      return { ...state, updateLoading: false, error: true };

    case "LOG_OUT":
      localStorage.clear();
      return {
        ...state,
        authData: null,
        loading: false,
        error: false,
        updateLoading: false,
      };

    default:
      return state;
  }
};

export default authReducer;
