import React from "react";
import { Event } from "../../models/Event";
import { Card, CardContent, Box,  CardMedia, Typography} from "@mui/material";
import PeopleIcon from '@mui/icons-material/People';
import { formatDate } from "../../helpers/formatDate";
import { trimText } from "../../helpers/trimText";

interface EventCardProps {
    event: Event;
}

const EventCard : React.FC<EventCardProps> = ({ event }) => {
    const baseUrl = process.env.REACT_APP_API_URL;
    const imageUrl = event.imagePath 
        ? `${baseUrl}/image?fileName=${event.imagePath}` 
        : `/imgs/default.png`;
    
    return (
        <Card sx ={{ 
            margin: 1,
            minHeight: '350px',
            boxShadow: 4,
            borderRadius: 3
            }}>
            <CardMedia
                component="img"
                height="190"
                image={imageUrl}
                alt={event.title}
                />
            <CardContent sx={{ pb: 2 }}>
                <Typography variant="subtitle2" color="text.secondary">
                    {formatDate(event.eventDate)}
                </Typography>
                <Typography variant="body1" sx={{ mt: 1.3, fontWeight: 'bold', fontSize: '18px', lineHeight: 1.2, height: '40px'}}>
                    {trimText(event.title, 40)}
                </Typography>
                <Typography variant="subtitle2" color="text.secondary" sx={{ mt: 1.3}}>
                   {event.communityName}
                </Typography>
                {/* I forgot to get the API data on how many participants there are. That's why it's a stub now */}
                 <Box sx={{display: 'flex', mt: 1, alignItems: 'center'}}>
                    <PeopleIcon fontSize="small" sx={{ mr: 1,}} />
                    <Typography variant="body2">
                        {event.duration || 0} attendees
                    </Typography>
                </Box>
            </CardContent>
        </Card>
    )
}

export default EventCard;