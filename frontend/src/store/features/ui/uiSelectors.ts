import { RootState } from '../../rootReducer';

export const isLoginModalOpen = (state: RootState) => state.ui.isLoginModalOpen;

export const isRegisterModalOpen = (state: RootState) => state.ui.isRegisterModalOpen;
