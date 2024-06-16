import axios from 'axios';

const API = axios.create({ baseURL: 'http://localhost:8001' });

export const login = ( formData: any ) => API.post('/api/auth/login', formData);
