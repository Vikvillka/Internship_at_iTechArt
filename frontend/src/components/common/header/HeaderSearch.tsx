import CloseIcon from '@mui/icons-material/Close';
import SearchIcon from '@mui/icons-material/Search';
import { Box, IconButton, InputBase } from '@mui/material';
import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { ContentMode as Mode } from '../../../models/Mode';
import { headerStyles } from './Header.styles';

const HeaderSearch: React.FC = () => {
  const navigate = useNavigate();
  const locationRouter = useLocation();
  const [keywords, setKeywords] = useState<string>('');
  const [location, setLocation] = useState<string>('');

  const hasValue = Boolean(keywords.trim() || location.trim());

  const mode = locationRouter.pathname.startsWith('/communities') ? Mode.Communities : Mode.Events;

  const handleSearch = () => {
    if (!hasValue) return;

    const params = new URLSearchParams();

    if (keywords.trim()) {
      params.set('keywords', keywords.trim());
    }

    if (location.trim()) {
      params.set('location', location.trim());
    }

    navigate(`/${mode}?${params.toString()}`);
  };

  const handleReset = () => {
    setKeywords('');
    setLocation('');
    navigate(`/${mode}`);
  };

  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') {
      if (hasValue) handleSearch();
      else handleReset();
    }
  };

  return (
    <Box sx={headerStyles.searchWrapper}>
      <InputBase
        placeholder='Search for events'
        value={keywords}
        onChange={(e) => setKeywords(e.target.value)}
        onKeyDown={handleKeyDown}
        sx={headerStyles.searchInput}
      />

      <InputBase
        placeholder='Search for city or country'
        value={location}
        onChange={(e) => setLocation(e.target.value)}
        onKeyDown={handleKeyDown}
        sx={headerStyles.locationInput}
      />

      <IconButton onClick={hasValue ? handleReset : handleSearch} sx={headerStyles.searchButton}>
        {hasValue ? <CloseIcon /> : <SearchIcon />}
      </IconButton>
    </Box>
  );
};

export default HeaderSearch;
