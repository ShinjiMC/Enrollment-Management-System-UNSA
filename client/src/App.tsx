// import Footer from "./components/footer/Footer";
import styles from "./sass/App.module.scss";
import AuthPage from "./pages/AuthPage/AuthPage";
import { Navigate, Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage/HomePage";
import { useSelector } from "react-redux";

function App(): JSX.Element {

  const user = useSelector((state: any) => state.authReducer.authData);

  return (
    <div className={styles.app}>
      <div className={styles.principal}>
        <Routes>
          <Route path="/" element={ 
            user ? <Navigate to="home" /> : <Navigate to="auth" />
          } />
          <Route path="/home" element={
            user ? <HomePage /> : <AuthPage />
          } />
          <Route path="/auth" element={
            user ? <HomePage /> : <AuthPage />
          } />
        </Routes>
      </div>
    </div>
  );
}

export default App;
