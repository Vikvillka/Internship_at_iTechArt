import { SubscriptionCount } from '../models/Subscription';
import { api } from './api';

export const subscriptionsApi = {
  getSubscriptionCounts: async (communityIds: string[]): Promise<SubscriptionCount[]> => {
    const response = await api.post<SubscriptionCount[]>(
      `/subscription/community/counts`,
      communityIds,
    );
    return response.data;
  },
};
