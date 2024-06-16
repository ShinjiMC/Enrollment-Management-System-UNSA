import styles from "../../sass/Components/LoginForm.module.scss";
import { FaUser, FaLock } from "react-icons/fa";

const LoginForm = () => {
  const handleForgotPassword = () => {
    console.log("Forgot password clicked");
  };
  return (
    <div className={styles.container}>
      <div className={styles.wrapper}>
        <form action="">
          <h1>
            <img src="/public/logoUnsa.png" alt="UNSA" />
          </h1>
          <div className={styles.input_box}>
            <input type="text" placeholder="Username" required />
            <FaUser className={styles.icon} />
          </div>
          <div className={styles.input_box}>
            <input type="password" placeholder="Password" required />
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
