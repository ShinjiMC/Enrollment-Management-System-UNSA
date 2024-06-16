import * as AuthApi from "../api/AuthRequests";

export const logIn =
    (formData: any, navigate: any) => async (dispatch: any) => {
        dispatch({ type: "AUTH_START" });
        try {
            const { data } = await AuthApi.logIn(formData);
            const { flag, token } = data;
            if (!token || !flag) {
                dispatch({ type: "AUTH_FAIL" });
                return;
            }
            dispatch({ type: "AUTH_SUCCESS", data: data });
            navigate("../home", { replace: true });
        } catch (error) {
            console.log("ERROR: ",error);
            dispatch({ type: "AUTH_FAIL" });
        }
    };

export const logout = () => async (dispatch: any) => {
    dispatch({ type: "LOG_OUT" });
};