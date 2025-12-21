import { RootState } from '../../rootReducer';

export const selectIsLoading = (state: RootState) => state.loader.isLoading;
