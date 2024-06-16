import {
  legacy_createStore as createStore,
  applyMiddleware,
  compose,
} from "redux";
import { thunk } from "redux-thunk";
import { reducers } from "../reducers";

const saveToLocalStorage = (state: any): void => {
  try {
    const serializedState = JSON.stringify(state);
    window.localStorage.setItem("store", serializedState);
  } catch (e) {
    console.log(e);
  }
};

const loadFromLocalStorage = (): any => {
  try {
    const serializedState = window.localStorage.getItem("store");
    if (serializedState === null) return undefined;
    return JSON.parse(serializedState);
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const composeEnhancers =
  (window as any).__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
const persistedState = loadFromLocalStorage();
const store = createStore(
  reducers,
  persistedState,
  composeEnhancers(applyMiddleware(thunk))
);

store.subscribe(() => saveToLocalStorage(store.getState()));

export default store;
