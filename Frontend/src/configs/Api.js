const base = "https://localhost:7002/api/v1/";
const twoRoute = (route, id) => id === 0 ? route : `${route}/${id}`;
const routes = {
    shelters: {
        addShelter: "Shelters/addShelter",
        updateShelter: (id) => `Shelters/updateShelter/${id}`,
        getAll: "Shelters/viewAllShelters",
        getShelter: (id) => `Shelters/viewStatusAboutShelter/${id}`,
        deleteShelter: (id) => `Shelters/deleteShelter/${id}`,
    },
    metrics: {
        getShelter: (id) => `Metrics/metricsAboutShelter/${id}`,
        getAllShelters: "Metrics/metricsAboutAllShelters",
    },
    bookings: {
        checkIn: "Bookings/checkIn",
        extend: (id) => twoRoute("Bookings/checkInExtend", id),
        getHistory: (id) => twoRoute("Bookings/viewBookingHistory", id),
        checkOut: (id) => twoRoute("Bookings/checkOut", id),
    },
    profiles: {
        getAll: "Profiles/viewAllProfiles",
        setupProfile: "Profiles/registerProfile",
        getProfile: (id) => twoRoute("Profiles/viewProfile", id),
        updateProfile: (id) => twoRoute("Profiles/updateProfile", id),
        deleteProfile: (id) => twoRoute("Profiles/deleteProfile", id),
    },
};

export { base, routes };
