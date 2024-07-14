import React, { createContext, useContext, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { AuthState, DataDecoded } from "../types";
import { jwtDecode } from "jwt-decode";

interface UserContextProps {
  user: DataDecoded | null;
}

const UserContext = createContext<UserContextProps | undefined>(undefined);

export const UserProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const authData = useSelector(
    (state: { authReducer: AuthState }) => state.authReducer.authData
  );
  const [user, setUser] = useState<DataDecoded | null>(null);

  const contextValue = React.useMemo(() => ({ user }), [user]);

  useEffect(() => {
    if (authData?.token) {
      try {
        const token = authData.token;
        const decoded = jwtDecode<Omit<DataDecoded, "token">>(authData.token);
        const claims = Object.entries(decoded).reduce((acc, [key, value]) => {
          const segments = key.split("/");
          const lastSegment = segments[segments.length - 1];
          switch (lastSegment) {
            case "id":
              return { ...acc, id: value } as Omit<DataDecoded, "token">;
            case "username":
              return { ...acc, username: value } as Omit<DataDecoded, "token">;
            case "email":
              return { ...acc, email: value } as Omit<DataDecoded, "token">;
            case "role":
              return { ...acc, role: value } as Omit<DataDecoded, "token">;
            case "exp":
              return { ...acc, exp: value } as Omit<DataDecoded, "token">;
            case "iss":
              return { ...acc, iss: value } as Omit<DataDecoded, "token">;
            case "aud":
              return { ...acc, aud: value } as Omit<DataDecoded, "token">;
            default:
              if (key.startsWith("http://schemas")) {
                return { ...acc, [lastSegment]: value } as Omit<
                  DataDecoded,
                  "token"
                >;
              }
              return acc;
          }
        }, {} as Omit<DataDecoded, "token">);
        setUser({ ...claims, token });
      } catch (error) {
        console.error("Error decoding token:", error);
      }
    } else {
      setUser(null);
    }
  }, [authData]);

  return (
    <UserContext.Provider value={contextValue}>{children}</UserContext.Provider>
  );
};

export const useUser = () => {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error("useUser must be used within a UserProvider");
  }
  return context;
};
