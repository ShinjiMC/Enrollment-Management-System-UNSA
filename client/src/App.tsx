import LoginForm from "./Components/LoginForm/LoginForm";
import Footer from "./Components/footer/Footer";
import styles from "./sass/App.module.scss";

function App() {
  return (
    <div className={styles.app}>
      <div className={styles.principal}>
        <LoginForm />
      </div>
      <Footer />
    </div>
  );
}

export default App;
