export const formatDate = (dateString: string): string => {
    const date = new Date(dateString);

    if (isNaN(date.getTime())) 
        return "";

    const dayOfWeek = date.toLocaleString("en-US", { weekday: "short" }); 
    const month = date.toLocaleString("en-US", { month: "short" });       
    const day = date.getDate();                                           
    const time = date.toLocaleString("en-US", {
        hour: "numeric",
        minute: "2-digit",
        hour12: true
    });

    return `${dayOfWeek}, ${month} ${day} · ${time}`;
};