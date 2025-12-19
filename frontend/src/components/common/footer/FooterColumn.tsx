import { Box, Typography } from '@mui/material';
import React from 'react';
import { footerStyles } from './Footer.styles';

interface FooterColumnItem {
  text: string;
  onClick: () => void;
}

interface FooterColumnProps {
  title: string;
  items: FooterColumnItem[];
}

const FooterColumn: React.FC<FooterColumnProps> = ({ title, items }) => {
  return (
    <Box sx={footerStyles.column}>
      <Typography sx={footerStyles.columnTitle}>{title}</Typography>
      {items.map((item, index) => (
        <Typography key={index} sx={footerStyles.columnItem} onClick={item.onClick}>
          {item.text}
        </Typography>
      ))}
    </Box>
  );
};

export default FooterColumn;
