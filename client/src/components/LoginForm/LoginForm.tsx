import { useState, useEffect, useMemo } from "react";
import styles from "./LoginForm.module.scss";
import { FaUser, FaLock } from "react-icons/fa";
import { useDispatch, useSelector } from "react-redux";
import { logIn } from "../../actions/AuthActions";
import { useNavigate } from "react-router-dom";
import { LoginFormData, AuthState } from "../../types";

const LoginForm = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const initialState = useMemo<LoginFormData>(
    () => ({
      email: "",
      password: "",
    }),
    []
  );

  const [data, setData] = useState<LoginFormData>(initialState);

  const authData = useSelector(
    (state: { authReducer: AuthState }) => state.authReducer.authData
  );

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setData({ ...data, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    dispatch<any>(logIn(data, navigate));
  };

  useEffect(() => {
    if (authData && (!authData.token || !authData.flag)) {
      alert("Invalid credentials");
      setData(initialState);
    }
  }, [authData, initialState]);

  const handleForgotPassword = () => {
    console.log("Forgot password clicked");
  };

  return (
    <div className={styles.container}>
      <div className={styles.wrapper}>
        <form onSubmit={handleSubmit}>
          <h1>
            <img src="/logoUnsa.png" alt="UNSA" />
          </h1>
          <div className={styles.input_box}>
            <input
              type="text"
              placeholder="Email"
              name="email"
              value={data.email}
              onChange={handleChange}
              required
            />
            <FaUser className={styles.icon} />
          </div>
          <div className={styles.input_box}>
            <input
              type="password"
              placeholder="Password"
              name="password"
              value={data.password}
              onChange={handleChange}
              required
            />
            <FaLock className={styles.icon} />
          </div>
          <div className={styles.remenber_forgot}>
            <button
              type="button"
              className={styles.forgot_password}
              onClick={handleForgotPassword}
            >
              Forgot password?
            </button>
          </div>
          <button type="submit">Login</button>
        </form>
      </div>
    </div>
  );
};

export default LoginForm;
