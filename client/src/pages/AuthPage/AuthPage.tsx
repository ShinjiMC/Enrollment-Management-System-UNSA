import { useSelector } from "react-redux";
import styles from "./AuthPage.module.scss";
import LoginForm from "../../components/LoginForm/LoginForm";

const AuthPage : React.FC = () => {
  
  const loading : boolean = useSelector((state: any) => state.authReducer.loading);
  
  console.log("Loading: ", loading);
  
  return (
    <div className={styles.container}>
      <LoginForm />
    </div>
  )
}

export default AuthPage