const prettyDate = (date) => {
    const months = [
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December",
    ];

    const formattedDate = new Date(date);
    const month = formattedDate.getMonth();
    const year = formattedDate.getFullYear();
    const day = formattedDate.getDate();

    return day + " " + months[month] + " " + year;
};

const shortDate = (date) => {
    const pad = (n) => (n < 10 ? "0" : "") + n;

    const formattedDate = new Date(date);
    const month = formattedDate.getMonth();
    const day = formattedDate.getDate();

    return pad(day) + "/" + pad(month);
};

const daysBetween = (startDate, endDate) => {
    const date1 = endDate ? new Date(endDate) : new Date();
    const date2 = startDate ? new Date(startDate) : new Date();
    const diffTime = Math.abs(date1 - date2);
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

    return diffDays + (diffDays === 1 ? " day" : " days");
};

export { prettyDate, shortDate, daysBetween };
