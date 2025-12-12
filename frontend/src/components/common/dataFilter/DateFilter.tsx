import CalendarTodayIcon from '@mui/icons-material/CalendarMonth';
import { Box, FormControl, InputAdornment, MenuItem, Select } from '@mui/material';
import React from 'react';
import { DateOption } from '../../../models/Date';
import { dateFilterStyles } from './DateFilter.styles';

interface DateFilterProps {
  value?: DateOption;
  onChange: (value: DateOption) => void;
}

const DateFilter: React.FC<DateFilterProps> = ({ value, onChange }) => {
  return (
    <Box sx={dateFilterStyles.filterBox}>
      <FormControl fullWidth size='small'>
        <Select
          sx={dateFilterStyles.filter}
          value={value || 'any'}
          startAdornment={
            <InputAdornment position='start'>
              <CalendarTodayIcon fontSize='small' />
            </InputAdornment>
          }
          onChange={(e) => onChange(e.target.value as DateOption)}
        >
          <MenuItem value='any'>Any day</MenuItem>
          <MenuItem value='today'>Today</MenuItem>
          <MenuItem value='tomorrow'>Tomorrow</MenuItem>
          <MenuItem value='thisWeek'>This week</MenuItem>
          <MenuItem value='nextWeek'>Next week</MenuItem>
        </Select>
      </FormControl>
    </Box>
  );
};

export default DateFilter;
