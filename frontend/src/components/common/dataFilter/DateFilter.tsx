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
          value={value || DateOption.Any}
          startAdornment={
            <InputAdornment position='start'>
              <CalendarTodayIcon fontSize='small' />
            </InputAdornment>
          }
          onChange={(e) => onChange(e.target.value as DateOption)}
        >
          <MenuItem value={DateOption.Any}>Any day</MenuItem>
          <MenuItem value={DateOption.Today}>Today</MenuItem>
          <MenuItem value={DateOption.Tomorrow}>Tomorrow</MenuItem>
          <MenuItem value={DateOption.ThisWeek}>This week</MenuItem>
          <MenuItem value={DateOption.NextWeek}>Next week</MenuItem>
        </Select>
      </FormControl>
    </Box>
  );
};

export default DateFilter;
