import { createSlice } from '@reduxjs/toolkit';

export interface UIState {
  isLoginModalOpen: boolean;
  isRegisterModalOpen: boolean;
}

const initialState: UIState = {
  isLoginModalOpen: false,
  isRegisterModalOpen: false,
};

export const uiSlice = createSlice({
  name: 'ui',
  initialState,
  reducers: {
    openLoginModal(state) {
      state.isLoginModalOpen = true;
    },
    closeLoginModal(state) {
      state.isLoginModalOpen = false;
    },
    openRegisterModal(state) {
      state.isRegisterModalOpen = true;
    },
    closeRegisterModal(state) {
      state.isRegisterModalOpen = false;
    },
    switchToRegisterModal(state) {
      state.isLoginModalOpen = false;
      state.isRegisterModalOpen = true;
    },
    switchToLoginModal(state) {
      state.isRegisterModalOpen = false;
      state.isLoginModalOpen = true;
    },
  },
});

export const {
  openLoginModal,
  closeLoginModal,
  openRegisterModal,
  closeRegisterModal,
  switchToRegisterModal,
  switchToLoginModal,
} = uiSlice.actions;

export default uiSlice.reducer;
