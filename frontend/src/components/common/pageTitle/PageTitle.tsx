import { Box, Typography } from '@mui/material';
import { pageTitleStyles } from './PageTitle.styles';

interface Props {
  title: string;
  rightSlot?: React.ReactNode;
}

const TitleBlock: React.FC<Props> = ({ title, rightSlot }) => {
  return (
    <Box sx={pageTitleStyles.titleBox}>
      <Typography variant='h4'>{title}</Typography>
      {rightSlot && <Box>{rightSlot}</Box>}
    </Box>
  );
};

export default TitleBlock;
