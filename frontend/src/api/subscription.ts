import { SubscriptionCount } from '../models/Subscription';
import { api } from './api';

export const subscriptionApi = {
  getSubscriptionCounts: async (communityIds: string[]): Promise<SubscriptionCount[]> => {
    const response = await api.post<SubscriptionCount[]>(
      `/subscription/community/counts`,
      communityIds,
    );
    return response.data;
  },
};
