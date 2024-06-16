import './LoginForm.css';
import{ FaUser , FaLock} from "react-icons/fa";

const LoginForm = () => {
  return (

        <div className ='wrapper'>
            <form action="">
                <h1>
                    <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/LOGO_UNSA.png/1200px-LOGO_UNSA.png" alt="UNSA" width="300" height="100"/>
                </h1>
                <div className="input-box">
                    <input type="text" placeholder="Username" required/>
                    <FaUser className="icon" />
                </div>
                    <div className="input-box">
                        <input type="password" placeholder="Password" required/>
                        <FaLock className="icon"/>
                    </div>
                <div className="remenber-forgot">
                    <label>
                        <input type="checkbox" name="" id=""/> Remember me
                    </label>
                    <a href="#">Forgot password?</a>
                </div>
                <button type="submit">Login</button>


            </form>

        </div>
    
  );
}

export default LoginForm;