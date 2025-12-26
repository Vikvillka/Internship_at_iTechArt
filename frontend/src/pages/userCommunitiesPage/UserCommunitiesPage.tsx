import { Container, Typography } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import UserCommunitesHeader from '../../components/user/userCommunities/UserCommunitesHeader';
import UserCommunitiesList from '../../components/user/userCommunities/UserCommunitiesList';
import { selectCommunitiesError } from '../../store/features/communities/communitiesSelectors';
import { errorContainer, errorText, pageContainer } from '../../styles/common';

const UserCommunitiesPage: React.FC = () => {
  const error = useSelector(selectCommunitiesError);
  if (error) {
    return (
      <Container sx={errorContainer}>
        <Typography variant='h6' sx={errorText}>
          {error}
        </Typography>
      </Container>
    );
  }

  return (
    <Container sx={pageContainer}>
      <UserCommunitesHeader />
      <UserCommunitiesList />
    </Container>
  );
};

export default UserCommunitiesPage;
