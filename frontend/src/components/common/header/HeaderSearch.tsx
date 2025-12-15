import CloseIcon from '@mui/icons-material/Close';
import SearchIcon from '@mui/icons-material/Search';
import { Box, IconButton, InputBase } from '@mui/material';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { headerStyles } from './Header.styles';

const HeaderSearch: React.FC = () => {
  const navigate = useNavigate();

  const [keywords, setKeywords] = useState('');
  const [location, setLocation] = useState('');

  const hasValue = Boolean(keywords.trim() || location.trim());

  const handleSearch = () => {
    if (!hasValue) return;

    const params = new URLSearchParams();

    if (keywords.trim()) {
      params.append('keywords', keywords.trim());
    }

    if (location.trim()) {
      params.append('location', location.trim());
    }

    navigate(`/events?${params.toString()}`);
  };

  const handleReset = () => {
    setKeywords('');
    setLocation('');
    navigate('/');
  };

  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') {
      handleSearch();
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
