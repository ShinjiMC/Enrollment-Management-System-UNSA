import React from "react";
import NavBar from "../../components/NavBar/NavBar";
import { useUser } from "../../context/UserContext";
const HomePage: React.FC = () => {
  const { user } = useUser();
  console.log("user: ", user?.token);
  return (
    <div>
      <NavBar />
      <div>HomePage</div>
      {user && (
        <div>
          <h3>Decoded Token Info:</h3>
          <div>ID: {user.id}</div>
          <div>Username: {user.username}</div>
          <div>Email: {user.email}</div>
          <div>Role: {user.role}</div>
          <div>Expiration: {user.exp}</div>
          <div>Issuer: {user.iss}</div>
          <div>Audience: {user.aud}</div>
        </div>
      )}
    </div>
  );
};

export default HomePage;
