import Footer from "./components/Footer/Footer";
import styles from "./sass/App.module.scss";
import AuthPage from "./pages/AuthPage/AuthPage";
import { Navigate, Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage/HomePage";
import { useUser } from "./context/UserContext";

function App(): JSX.Element {
  const { user } = useUser();

  return (
    <div className={styles.app}>
      <div className={styles.principal}>
        <Routes>
          <Route
            path="/"
            element={user ? <Navigate to="home" /> : <Navigate to="auth" />}
          />
          <Route path="/home" element={user ? <HomePage /> : <AuthPage />} />
          <Route path="/auth" element={!user ? <AuthPage /> : <HomePage />} />
        </Routes>
      </div>
      <Footer />
    </div>
  );
}

export default App;
