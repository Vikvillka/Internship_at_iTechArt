import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import {
  Box,
  Container,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Paper,
  Typography,
} from '@mui/material';
import React from 'react';
import PageTitle from '../../components/common/pageTitle/PageTitle';
import { Participation } from '../../models/Participation';
import { errorContainer, errorText, pageContainer } from '../../styles/common';

interface UserProfilePageProps {
  participations: Participation[];
  isLoading: boolean;
  error: string | null;
}

const UserProfilePage: React.FC<UserProfilePageProps> = ({ participations, isLoading, error }) => {
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
      <PageTitle title='Profile' />

      <Paper sx={{ p: 3, borderRadius: '16px', border: '1px solid #E0E0E0', boxShadow: 'none' }}>
        <Typography variant='h6' sx={{ mb: 2 }}>
          Joined events
        </Typography>

        {isLoading ? (
          <Typography variant='body2'>Loading joined events...</Typography>
        ) : participations.length === 0 ? (
          <Typography variant='body2'>You have not joined any events yet.</Typography>
        ) : (
          <List sx={{ p: 0 }}>
            {participations.map((participation) => (
              <ListItem
                key={participation.id}
                sx={{
                  px: 0,
                  borderBottom: '1px solid #F0F0F0',
                  '&:last-child': { borderBottom: 'none' },
                }}
              >
                <ListItemIcon sx={{ minWidth: 34 }}>
                  <CalendarTodayIcon fontSize='small' />
                </ListItemIcon>
                <ListItemText
                  primary={participation.eventName}
                  secondary={participation.isConfirmed ? 'Confirmed participation' : 'Pending'}
                />
                {participation.isConfirmed && (
                  <Box sx={{ display: 'flex', alignItems: 'center', color: '#09BA00' }}>
                    <CheckCircleOutlineIcon fontSize='small' />
                  </Box>
                )}
              </ListItem>
            ))}
          </List>
        )}
      </Paper>
    </Container>
  );
};

export default UserProfilePage;
