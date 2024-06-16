import { useSelector } from "react-redux";
import LoginForm from "../components/LoginForm/LoginForm";

const AuthPage : React.FC = () => {
  
  const loading : boolean = useSelector((state: any) => state.authReducer.loading);
  
  console.log("Loading: ", loading);
  
  return (
    <div>
        <LoginForm />
    </div>
  )
}

export default AuthPage