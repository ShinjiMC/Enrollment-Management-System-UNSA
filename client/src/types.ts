export interface AuthData {
  flag: boolean;
  message: string;
  token: string;
}

export interface AuthState {
  authData: AuthData | null;
  loading: boolean;
  error: boolean;
  updateLoading: boolean;
}

export interface Action {
  type: string;
  data?: AuthData;
}

export interface LoginFormData {
  email: string;
  password: string;
}

export interface DataDecoded {
  id: string;
  username: string;
  email: string;
  role: string;
  exp: number;
  iss: string;
  aud: string;
}
