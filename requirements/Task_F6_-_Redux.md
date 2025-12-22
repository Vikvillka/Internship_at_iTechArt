### Description
Your app now has multiple pages, containers, and presenters. Time to stop passing state manually everywhere and introduce a proper global state system.

You’ll add **Redux Toolkit** + **Redux-Saga**.  
Redux Toolkit gives clear patterns and drastically reduces boilerplate.  
Redux-Saga handles async workflows cleanly, separating API logic from UI.

Your responsibilities:
- Move all API calls from containers into sagas.
- Dispatch `request` actions → sagas run → sagas call API → sagas dispatch `response` actions.
- Reducers update global state based on these responses.
- Components use selectors to read new values from the global store.
- Global loader saga tracks when async operations start and end.

This is the standard pattern for scalable React apps.

### Steps to Complete

1. Learn the concept of Redux. What are Slice, Reducer, Action, Selector and  Saga?
2. Install Redux Toolkit, React Redux, Redux-Saga.
3. Add store setup + root reducer + root saga.
4. Create a slice for each domain: models, details, loader.
5. Move **all API calls** into sagas.
6. Trigger sagas using `request*` actions.
7. Dispatch `response*` actions from sagas.
8. Update global state in reducers.
9. Use selectors in container components.
10. Presenters remain pure UI.
11. Create a global loader slice + global loader saga logic (show/hide around each API call).
12. Replace local `useState` and `useEffect` fetching with Redux-driven flows.

---
#### Recommended Libraries
- **@reduxjs/toolkit** → reducers, actions, immutable updates  
- **react-redux** → hooks (`useSelector`, `useDispatch`)  
- **redux-saga** → async workflows, isolated side effects  

Install:
```bash
npm install @reduxjs/toolkit react-redux redux-saga
```

#### Example Flow: Request → Saga → Response → Reducer → UI Update

#### 1. Slice (Reducer + Actions)

```tsx
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import type { MyModel } from "../../types";

interface State {
  items: MyModel[];
}

const initialState: State = { items: [] };

const modelsSlice = createSlice({
  name: "models",
  initialState,
  reducers: {
    getAllModels: () => {},
    handleGetAllModelsResponse: (state, action: PayloadAction<MyModel[]>) => {
      state.items = action.payload;
    },
  },
});

export const { getAllModels, handleGetAllModelsResponse } = modelsSlice.actions;
export default modelsSlice.reducer;
```

---

#### 2. Saga for Models

```tsx
import { call, put, takeLatest } from "redux-saga/effects";
import axios from "axios";
import { requestModels, responseModels } from "./modelsSlice";
import { showLoader, hideLoader } from "../loader/loaderSlice";

function* fetchModels() {
  try {
    yield put(showLoader());
    const res = yield call(axios.get, "/api/models");
    yield put(handleGetAllModelsResponse(res.data));
  } finally {
    yield put(hideLoader());
  }
}

export function* modelsSaga() {
  yield takeLatest(getAllModels.type, fetchModels);
}
```

#### 3. Loader Slice

Global state: one loader for the entire app.

```tsx
import { createSlice } from "@reduxjs/toolkit";

const loaderSlice = createSlice({
  name: "loader",
  initialState: { isLoading: false },
  reducers: {
    showLoader: state => { state.isLoading = true; },
    hideLoader: state => { state.isLoading = false; },
  },
});

export const { showLoader, hideLoader } = loaderSlice.actions;
export default loaderSlice.reducer;
```

#### 4. Root Setup

```tsx
import { all } from "redux-saga/effects";
import { modelsSaga } from "../features/models/modelsSaga";

export default function* rootSaga() {
  yield all([modelsSaga()]);
}
```

```tsx
import { configureStore } from "@reduxjs/toolkit";
import createSagaMiddleware from "redux-saga";
import modelsReducer from "../features/models/modelsSlice";
import loaderReducer from "../features/loader/loaderSlice";
import rootSaga from "./rootSaga";

const saga = createSagaMiddleware();

export const store = configureStore({
  reducer: {
    models: modelsReducer,
    loader: loaderReducer,
  },
  middleware: g => g({ thunk: false }).concat(saga),
});

saga.run(rootSaga);
```

#### 5. Selectors

```tsx
export const selectModels = (state: RootState) => state.models.items;

export const selectIsLoading = (state: RootState) => state.loader.isLoading;
```

#### 6. Component Usage (Container)

```tsx
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { requestModels } from "../store/features/models/modelsSlice";
import { selectModels } from "../store/features/models/modelsSelectors";
import { selectIsLoading } from "../store/features/loader/loaderSelectors";
import BaseModelsView from "./BaseModelsView";

const BaseModelsContainer: React.FC = () => {
  const dispatch = useDispatch();
  const items = useSelector(selectModels);
  const loading = useSelector(selectIsLoading);

  useEffect(() => {
    dispatch(requestModels());
  }, [dispatch]);

  return <BaseModelsView items={items} loading={loading} />;
};

export default BaseModelsContainer;

```

### Topics to Learn

- Redux Toolkit slicing and immutable state updates
- Redux-Saga for async workflows
- Action lifecycle: request → saga → api → response → reducer
- Using selectors for clean state access
- Architecture separation: container vs presenter using global store
- Proper global loader handling
- Removing API calls from UI components entirely

### Tips

- Never call Axios inside a component again.
- Keep reducers pure — no side effects, no async, no API.
- Always use `takeLatest` for simple data fetches to avoid outdated responses.
- Keep selectors small and typed.
- Sagas should be short: call loader, call API, dispatch response.