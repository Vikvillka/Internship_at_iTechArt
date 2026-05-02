import { createSlice } from '@reduxjs/toolkit';

export interface LoaderState {
  counter: number;
}

const initialState: LoaderState = {
  counter: 0,
};

export const loaderSlice = createSlice({
  name: 'loader',
  initialState,
  reducers: {
    incrementLoader(state) {
      state.counter += 1;
    },
    decrementLoader(state) {
      state.counter = Math.max(0, state.counter - 1);
    },
  },
});

export const { incrementLoader, decrementLoader } = loaderSlice.actions;
export const showLoader = incrementLoader;
export const hideLoader = decrementLoader;

export default loaderSlice.reducer;
