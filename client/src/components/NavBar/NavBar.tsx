import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { Logo, MenuOpenIcon, MenuCloseIcon } from "../icons";
import styles from "./NavBar.module.scss";
import { useUser } from "../../context/UserContext";

const NavBar: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [click, setClick] = useState(false);
  const handleClick = () => setClick(!click);
  const handleInternalNavigation = () => setClick(false);
  const handleLogout = () => {
    dispatch({ type: "LOG_OUT" });
    navigate("/");
  };
  const { user } = useUser();

  return (
    <header className={styles.header}>
      <nav className={styles.navbar}>
        <div className={styles.nav_container}>
          <div className={styles.logo_container}>
            <span className={styles.text}>DEXO</span>
            <span className={styles.icon}>
              <Logo className={styles.logo} />
            </span>
          </div>
          <ul
            className={
              click ? `${styles.nav_menu} ${styles.active}` : styles.nav_menu
            }
          >
            <li className={styles.nav_item}>
              <Link to="/" onClick={handleInternalNavigation}>
                Home
              </Link>
            </li>
            <li className={styles.nav_item}>
              <Link to="/about" onClick={handleInternalNavigation}>
                Enroll
              </Link>
            </li>
            <li className={styles.nav_item}>
              <Link to="/projects" onClick={handleInternalNavigation}>
                Career
              </Link>
            </li>
            <li className={styles.nav_item}>
              <Link to="/profile" onClick={handleInternalNavigation}>
                {user?.username}
              </Link>
            </li>
            <li className={styles.nav_item}>
              <button onClick={handleLogout} className={styles.logout_button}>
                Logout
              </button>
            </li>
          </ul>
          <button className={styles.nav_icon} onClick={handleClick}>
            {click ? (
              <span className={styles.icon}>
                <MenuCloseIcon className={styles.iconMenu} />
              </span>
            ) : (
              <span className={styles.icon}>
                <MenuOpenIcon className={styles.iconMenu} />
              </span>
            )}
          </button>
        </div>
      </nav>
    </header>
  );
};

export default NavBar;
