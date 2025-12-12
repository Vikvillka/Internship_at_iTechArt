import EventIcon from '@mui/icons-material/BrightnessHigh';
import BrushIcon from '@mui/icons-material/Brush';
import ComputerIcon from '@mui/icons-material/Computer';
import FoodIcon from '@mui/icons-material/Fastfood';
import GamesIcon from '@mui/icons-material/Games';
import LanguageIcon from '@mui/icons-material/Language';
import MusicIcon from '@mui/icons-material/MusicNote';
import ArtIcon from '@mui/icons-material/Palette';
import PeopleIcon from '@mui/icons-material/People';
import SportsSoccerIcon from '@mui/icons-material/SportsSoccer';
import TravelExploreIcon from '@mui/icons-material/TravelExplore';
import React from 'react';

interface Category {
  label: string;
  icon: React.ElementType;
}

export const categories: Category[] = [
  { label: 'All events', icon: EventIcon },
  { label: 'Social Activities', icon: PeopleIcon },
  { label: 'Hobby', icon: BrushIcon },
  { label: 'Sports & Fitness', icon: SportsSoccerIcon },
  { label: 'Technology', icon: ComputerIcon },
  { label: 'Travel & Outdoor', icon: TravelExploreIcon },
  { label: 'Games', icon: GamesIcon },
  { label: 'Language', icon: LanguageIcon },
  { label: 'Music', icon: MusicIcon },
  { label: 'Food & Drink', icon: FoodIcon },
  { label: 'Arts', icon: ArtIcon },
];
