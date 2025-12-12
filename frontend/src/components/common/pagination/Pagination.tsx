import { Box, Pagination } from '@mui/material';
import { paginationStyles } from './Pagination.styles';

interface Props {
  totalPages: number;
  page: number;
  onPageChange: (page: number) => void;
}

const CustomPagination: React.FC<Props> = ({ totalPages, page, onPageChange }) => {
  return (
    <Box sx={paginationStyles.paginationContainer}>
      <Pagination
        count={totalPages}
        page={page}
        onChange={(_, value) => onPageChange(value)}
        sx={paginationStyles.pagination}
      />
    </Box>
  );
};

export default CustomPagination;
