import * as AuthApi from "../api/AuthRequests";
import { Dispatch } from "redux";
import { NavigateFunction } from "react-router-dom";
import { LoginFormData } from "../types";

export const logIn =
  (formData: LoginFormData, navigate: NavigateFunction) =>
  async (dispatch: Dispatch) => {
    dispatch({ type: "AUTH_START" });
    try {
      const { data } = await AuthApi.logIn(formData);
      const { flag, token } = data;
      if (!token || !flag) {
        dispatch({ type: "AUTH_FAIL" });
        return;
      }
      dispatch({ type: "AUTH_SUCCESS", data: data });
      navigate("/home", { replace: true });
    } catch (error) {
      console.log("ERROR: ", error);
      dispatch({ type: "AUTH_FAIL" });
    }
  };

export const logout = () => async (dispatch: Dispatch) => {
  dispatch({ type: "LOG_OUT" });
};
