import React, { useState, useMemo, useEffect, useCallback } from "react";
import Button from "../components/Button";
import PageLayout from "../utils/PageLayout";
import { MdEdit, MdDelete } from "react-icons/md";
import { ImExit } from "react-icons/im";
import { BsPlusLg } from "react-icons/bs"
import UserModal from "../components/modals/UserModal";
import RentModal from "../components/modals/RentModal";
import Table from "../components/Table";
import Section from "../components/Section";
import AdminOnly from "../utils/AdminOnly";
import UserOnly from "../utils/UserOnly";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import axiosInstance from "../configs/Axios";
import { prettyDate, daysBetween } from "../utils/Functions";
import { useLocation, useNavigate } from "react-router-dom";

const columns = [
  {
    Header: "Shelter",
    accessor: "shelterName",
  },
  {
    Header: "Check-in",
    accessor: "checkInDate",
    Cell: ({ cell: { value } }) => prettyDate(value)
  },
  {
    Header: "Check-out",
    accessor: "checkOutDate",
    Cell: ({ cell: { value, row } }) => {
      const checkOut = value != null ? value : row.original.expectedCheckOutDate;
      return prettyDate(checkOut);
    }
  },
  {
    Header: "Duration",
    accessor: "x",
    Cell: ({ cell: { value, row } }) => {
      const checkOut = row.original.checkOutDate != null ? row.original.checkOutDate : row.original.expectedCheckOutDate;
      return daysBetween(row.original.checkInDate, checkOut);
    }
  }
];

const User = ({ self }) => {
  const { user, getAccessTokenSilently } = useAuth0();
  const { pathname } = useLocation();
  const [userData, setUserData] = useState({});
  const [bookingHistory, setBookingHistory] = useState([]);
  const [openedModal, setOpenedModal] = useState(false);
  const [openedExtensionModal, setOpenedExtensionModal] = useState(false);
  const navigate = useNavigate();
  const id = self === undefined ? pathname.split("/").reverse()[0] : 0;

  const userFields = useMemo(
    () => [
      { key: "Name", value: userData.profile?.name },
      { key: "Email", value: userData.profile?.email },
      { key: "Phone", value: userData.profile?.phoneNumber },
      { key: "Address", value: userData.profile?.address },
      { key: "Birth date", value: userData.profile?.birthDate ? prettyDate(userData.profile?.birthDate) : "" }
    ],
    [userData]
  );

  const registerUser = useCallback(async () => {
    const accessToken = await getAccessTokenSilently();
    axiosInstance
      .post(routes.profiles.setupProfile, {Email: user.email, Name: user.name}, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(() => {
        setUserData({
          profile: {
            name: user.name,
            email: user.email
          }
        });
        setOpenedModal(true);
      });
  }, [user, getAccessTokenSilently]);

  const getUser = useCallback(async () => {
    const accessToken = await getAccessTokenSilently();
    axiosInstance
      .get(routes.profiles.getProfile(id), {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(({ data }) => setUserData(data))
      .catch((e) => {
        /* user profile not found. let's set it up! */
        if (e.response?.status !== 401) {
          throw e;
        }
        registerUser();
      });
  }, [getAccessTokenSilently, registerUser, id]);

  const getBookingHistory = useCallback(async () => {
    const accessToken = await getAccessTokenSilently();
    axiosInstance
      .get(routes.bookings.getHistory(id), {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(({ data }) => setBookingHistory(data));
  }, [getAccessTokenSilently, id]);

  const handleUpdateUser = (form) => {
    (async () => {
      const accessToken = await getAccessTokenSilently();
      axiosInstance
        .put(routes.profiles.updateProfile(id), form, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        })
        .then(() => getUser());
    })();
  };

  const handleDelete = () => {
    (async () => {
      const action = window.confirm("Are you sure you want to delete this user?");
      if (action) {
        const accessToken = await getAccessTokenSilently();
        axiosInstance
          .delete(routes.profiles.deleteProfile(id), {
            headers: {
              Authorization: `Bearer ${accessToken}`,
            },
          })
          .then(() => navigate("/users"));
        }
    })();
  };

  const handleExtension = (form) => {
    (async () => {
      const accessToken = await getAccessTokenSilently();
      axiosInstance
        .put(routes.bookings.extend(id), {...form, ShelterId: userData.currentShelter?.shelterId}, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        })
        .then(() => { getUser(); getBookingHistory(); });
    })();
  };

  const handleCheckOut = () => {
    (async () => {
      let action = window.confirm("Are you sure you want to checkout?");
      if (action) {
        const accessToken = await getAccessTokenSilently();
        axiosInstance
          .put(routes.bookings.checkOut(id), {ShelterId: userData.currentShelter?.shelterId}, {
            headers: {
              Authorization: `Bearer ${accessToken}`,
            },
          })
          .then(() => { getUser(); getBookingHistory(); });
        }
    })();
  };

  useEffect(() => {
    getUser();
    getBookingHistory();
  }, [getUser, getBookingHistory]);

  return (
    <PageLayout>
      <UserModal
        modalIsOpen={openedModal}
        closeModal={() => {
          setOpenedModal(false);
        }}
        submitForm={handleUpdateUser}
        userData={userData.profile}
      />
      <RentModal
        modalIsOpen={openedExtensionModal}
        closeModal={() => {
          setOpenedExtensionModal(false);
        }}
        submitForm={handleExtension}
        extend
      />

      <div className="row-between">
        <h2>{userData.profile?.name}</h2>
        <div className="row-center">
          <Button 
          className="edit-button"
          onClick={() => setOpenedModal(true)}>
            <MdEdit /> Edit
          </Button>
          {userData.currentShelter == null && id !== 0 &&
            <AdminOnly>
              <Button
                className="delete-button"
                onClick={() => handleDelete()}
              >
                <MdDelete /> Delete
              </Button>
            </AdminOnly>
          }
        </div>
      </div>
      <div className="flex flex-col gap-10">
        <div className="row-between-start">
          <Section title={"Profile Details"} fields={userFields} />
          {userData.currentShelter != null &&
          <div className="flex flex-col gap-5">
            <p className="section-title">Current Accommodation</p>
            <div className="flex gap-5">
              <div className="statistic-card">
                <div className="card-statistic">
                  <p>{userData.currentShelter.shelterName}</p>
                  <p>shelter</p>
                </div>
              </div>
              <div className="statistic-card">
                <div className="card-statistic">
                  <p>{daysBetween(new Date(), userData.currentShelter.expectedCheckOutDate)}</p>
                  <p>remaining time</p>
                </div>
              </div>
            </div>
            <Button
              className="button"
              style={{width: "auto"}}
              onClick={() => setOpenedExtensionModal(true)}
            >
              <BsPlusLg /> Extend accommodation
            </Button>
            <Button
              className="checkout-button"
              style={{width: "auto"}}
              onClick={() => handleCheckOut()}
            >
              <ImExit /> Early checkout
            </Button>
          </div> }
        </div>
        <div className="flex flex-col gap-5 w-full p-[1px]">
          <p className="section-title">Accommodation History</p>
          <AdminOnly>
            <Table
              data={bookingHistory}
              columns={columns}
              onRowClick={(e, row, i) => navigate(`/shelters/${row.original.shelterId}`)}
            />
          </AdminOnly>
          <UserOnly>
            <Table data={bookingHistory} columns={columns} />
          </UserOnly>
        </div>
      </div>
    </PageLayout>
  );
};

export default User;
