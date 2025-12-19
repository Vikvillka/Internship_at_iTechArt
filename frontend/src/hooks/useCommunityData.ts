import { useCallback } from 'react';
import { communityApi } from '../api/community';
import { subscriptionApi } from '../api/subscription';
import { categories } from '../helpers/sliderCategory/categories';
import { PagedResponse } from '../models/Common';
import { Community } from '../models/Community';

export interface CommunitiesDataResult {
  items: Community[];
  totalPages: number;
  subscriptionCounts: Record<string, number>;
}

export const useCommunitiesData = () => {
  const loadCommunities = useCallback(
    async (
      pageNumber: number,
      category: string,
      keywordsFromUrl?: string,
      locationFromUrl?: string,
    ): Promise<CommunitiesDataResult> => {
      const params = {
        keywords: keywordsFromUrl,
        location: locationFromUrl,
        category: category === categories[0].label ? undefined : category,
        page: pageNumber,
        pageSize: 20,
      };

      const data: PagedResponse<Community> = await communityApi.getCommunitiesBySearch(params);

      const communityIds = data.items.map((community) => community.id);
      const counts = await subscriptionApi.getSubscriptionCounts(communityIds);
      const subscriptionCounts: Record<string, number> = {};
      counts.forEach((count) => {
        subscriptionCounts[count.communityId] = count.count;
      });

      return {
        items: data.items,
        totalPages: Math.ceil(data.totalCount / data.pageSize),
        subscriptionCounts,
      };
    },
    [],
  );

  return { loadCommunities };
};
