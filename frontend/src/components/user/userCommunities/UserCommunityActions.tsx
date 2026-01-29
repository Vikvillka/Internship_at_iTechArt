import DeleteIcon from '@mui/icons-material/Delete';
import OpenInNewIcon from '@mui/icons-material/OpenInNew';
import { Box, IconButton } from '@mui/material';
import React from 'react';
import { Link } from 'react-router-dom';
import { userCommunitiesStyles } from './UserCommunities.styles';

interface Props {
  communityId: string;
  onDelete: () => void;
}

const UserCommunityActions: React.FC<Props> = ({ communityId, onDelete }) => {
  return (
    <>
      <Link to={`/community/${communityId}`} style={{ marginLeft: 8 }}>
        <IconButton size='small'>
          <OpenInNewIcon fontSize='small' />
        </IconButton>
      </Link>
      <Box sx={userCommunitiesStyles.actionsBox}>
        <IconButton size='small' onClick={onDelete}>
          <DeleteIcon fontSize='small' />
        </IconButton>
      </Box>
    </>
  );
};

export default UserCommunityActions;
