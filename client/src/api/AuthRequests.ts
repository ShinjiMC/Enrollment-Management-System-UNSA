import axios from 'axios';

const URL = "http://localhost:8001";

const API = axios.create({ baseURL: URL });

export const logIn = ( formData: any ) => API.post('/api/auth/login', formData, {
    headers: {
        'Content-Type': 'application/json',
    },
});
