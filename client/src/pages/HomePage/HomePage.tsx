import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { jwtDecode } from "jwt-decode";
import { AuthState, DataDecoded } from "../../types";

const HomePage: React.FC = () => {
  const dispatch = useDispatch();
  const authData = useSelector(
    (state: { authReducer: AuthState }) => state.authReducer.authData
  );
  const [decodedToken, setDecodedToken] = useState<DataDecoded | null>(null);

  useEffect(() => {
    const decodeToken = async () => {
      if (authData?.token) {
        try {
          const decoded = jwtDecode<DataDecoded>(authData.token);
          const claims = Object.entries(decoded).reduce((acc, [key, value]) => {
            const segments = key.split("/");
            const lastSegment = segments[segments.length - 1];
            switch (lastSegment) {
              case "nameidentifier":
                return { ...acc, id: value };
              case "name":
                return { ...acc, username: value };
              case "emailaddress":
                return { ...acc, email: value };
              case "role":
                return { ...acc, role: value };
              case "exp":
                return { ...acc, exp: value };
              case "iss":
                return { ...acc, iss: value };
              case "aud":
                return { ...acc, aud: value };
              default:
                if (key.startsWith("http://schemas")) {
                  return { ...acc, [lastSegment]: value } as DataDecoded;
                }
                return acc;
            }
          }, {} as DataDecoded);
          setDecodedToken(claims);
        } catch (error) {
          console.error("Error decoding token:", error);
        }
      }
    };

    decodeToken();
  }, [authData]);

  const handleLogout = () => {
    dispatch({ type: "LOG_OUT" });
  };

  return (
    <div>
      <button onClick={handleLogout}>Logout</button>
      <div>HomePage</div>
      {decodedToken && (
        <div>
          <h3>Decoded Token Info:</h3>
          <div>ID: {decodedToken.id}</div>
          <div>Username: {decodedToken.username}</div>
          <div>Email: {decodedToken.email}</div>
          <div>Role: {decodedToken.role}</div>
          <div>Expiration: {decodedToken.exp}</div>
          <div>Issuer: {decodedToken.iss}</div>
          <div>Audience: {decodedToken.aud}</div>
        </div>
      )}
    </div>
  );
};

export default HomePage;
