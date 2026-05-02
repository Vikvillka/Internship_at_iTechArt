import { Alert, AlertColor, Snackbar } from '@mui/material';
import React from 'react';

interface SnackbarProps {
  open: boolean;
  message: string;
  severity?: AlertColor;
  autoHideDuration?: number;
  onClose: () => void;
}

const SnackbarComponent: React.FC<SnackbarProps> = ({
  open,
  message,
  severity = 'info',
  autoHideDuration = 3000,
  onClose,
}) => {
  return (
    <Snackbar open={open} autoHideDuration={autoHideDuration} onClose={onClose}>
      <Alert severity={severity} onClose={onClose}>
        {message}
      </Alert>
    </Snackbar>
  );
};

export default SnackbarComponent;
