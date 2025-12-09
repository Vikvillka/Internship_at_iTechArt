import { GoogleMap, Marker, useJsApiLoader } from '@react-google-maps/api';
import React from 'react';

interface EventVenueMapProps {
  address: string;
  lat: number;
  lng: number;
  zoom?: number;
}

const containerStyle = {
  width: '100%',
  height: '300px',
  borderRadius: '20px',
};

const EventVenueMap: React.FC<EventVenueMapProps> = ({ address, lat, lng, zoom = 5 }) => {
  const { isLoaded } = useJsApiLoader({
    googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY || '',
  });
  const center = {
    lat: lat,
    lng: lng,
  };
  return isLoaded ? (
    <GoogleMap mapContainerStyle={containerStyle} center={center} zoom={zoom}>
      <Marker position={center} title={address} />
    </GoogleMap>
  ) : (
    <></>
  );
};

export default EventVenueMap;
