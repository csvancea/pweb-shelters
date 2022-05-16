import React, { useState, useEffect, useCallback } from "react";
import Button from "../components/Button";
import Table from "../components/Table";
import Input from "../components/Input";
import PageLayout from "../utils/PageLayout";
import { MdAdd, MdSearch } from "react-icons/md";
import { FaHandsHelping, FaCat } from "react-icons/fa";
import { GiEarthAfricaEurope } from "react-icons/gi";
import ShelterModal from "../components/modals/ShelterModal";
import RentModal from "../components/modals/RentModal";
import AdminOnly from "../utils/AdminOnly";
import UserOnly from "../utils/UserOnly";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import axiosInstance from "../configs/Axios";
import { prettyDate } from "../utils/Functions";

const columns = [
  {
    Header: "Name",
    accessor: "name",
  },
  {
    Header: "Address",
    accessor: "address",
    Cell: ({ cell: { value, row } }) => (
      <div className="keyword-list">
        <GiEarthAfricaEurope />
        <a className="link" href={row.original.mapsLink}>
          { value }
        </a>
      </div>
    )
  },
  {
    Header: "Capacity",
    accessor: "capacity",
    Cell: ({ cell: { value, row } }) => (
      `${row.original.numberOfUsers} / ${value}`
    )
  },
  {
    Header: "Tags",
    accessor: "pets",
    Cell: ({ cell: { row } }) => (
      <div className="keyword-list">
        { row.original.accessibility && <FaHandsHelping /> }
        { row.original.pets && <FaCat /> }
      </div>
    )
  },
  {
    Header: "Added on",
    accessor: "createdAt",
    Cell: ({ cell: { value } }) => prettyDate(value)
  },
];

const Shelters = () => {
  const navigate = useNavigate();
  const [shelters, setShelters] = useState([]);
  const [openedModal, setOpenedModal] = useState(false);
  const [openedRentModal, setOpenedRentModal] = useState(null);
  const { getAccessTokenSilently } = useAuth0();

  const getAllShelters = useCallback(async () => {
    const accessToken = await getAccessTokenSilently();
    axiosInstance
      .get(routes.shelters.getAll, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(({ data }) => setShelters(data));
  }, [getAccessTokenSilently]);

  const handleAddShelter = (form) => {
    (async () => {
      const accessToken = await getAccessTokenSilently();
      axiosInstance
        .post(routes.shelters.addShelter, form, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        })
        .then(() => getAllShelters());
    })();
  };

  const handleRent = (form) => {
    (async () => {
      const accessToken = await getAccessTokenSilently();
      axiosInstance
        .put(routes.bookings.checkIn, {...form, ShelterId: openedRentModal.id}, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        })
        .then(() => { navigate("/profile") });
    })();
  };

  useEffect(() => {
    getAllShelters();
  }, [getAllShelters]);

  return (
    <PageLayout>
      <ShelterModal
        modalIsOpen={openedModal}
        closeModal={() => {
          setOpenedModal(false);
        }}
        submitForm={handleAddShelter}
      />

      <RentModal
        modalIsOpen={openedRentModal !== null}
        closeModal={() => {
          setOpenedRentModal(null);
        }}
        submitForm={handleRent}
      />

      <div className="row-between">
        <h2>
          {shelters.length} {shelters.length > 1 ? "Shelters" : "Shelter"}
        </h2>
        <Input style={{width: "30rem"}} placeholder="Filter" />
        <div className="row-center">
          <Button>
              <MdSearch /> Search
          </Button>
          <AdminOnly>
            <Button onClick={() => setOpenedModal(true)}>
              <MdAdd /> Add Shelter
            </Button>
          </AdminOnly>
        </div>
      </div>
      <AdminOnly>
        <Table
          data={shelters}
          columns={columns}
          onCellClick={(e, row, i, cell) => cell.column.id !== "address" && navigate(`/shelters/${row.original.id}`)}
        />
      </AdminOnly>
      <UserOnly>
        <Table
          data={shelters}
          columns={columns}
          onCellClick={(e, row, i, cell) => cell.column.id !== "address" && setOpenedRentModal(row.original)}
        />
      </UserOnly>
    </PageLayout>
  );
};

export default Shelters;
