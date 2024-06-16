import React from "react";
import styles from "../../sass/Components/Footer.module.scss";

const Footer: React.FC = () => {
  return (
    <footer className={styles.footer}>
      <div className={styles.container}>
        <span>
          <img src="/public/DEXO.png" alt="DEXO" className={styles.logo} />
        </span>
        <p className={styles.text}>
          &copy; 2024 DEXO Corp. All rights reserved.
        </p>
      </div>
    </footer>
  );
};

export default Footer;
