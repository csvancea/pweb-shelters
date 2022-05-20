import React, { useState, useMemo, useEffect, useCallback } from "react";
import Button from "../components/Button";
import PageLayout from "../utils/PageLayout";
import { MdEdit, MdDelete } from "react-icons/md";
import ShelterModal from "../components/modals/ShelterModal";
import Table from "../components/Table";
import Section from "../components/Section";
import { GiEarthAfricaEurope } from "react-icons/gi";
import { FaHandsHelping, FaCat } from "react-icons/fa";
import { useLocation, useNavigate } from "react-router-dom";
import axiosInstance from "../configs/Axios";
import { routes } from "../configs/Api";
import { useAuth0 } from "@auth0/auth0-react";
import { prettyDate } from "../utils/Functions";

const columns = [
  {
    Header: "Name",
    accessor: "name",
  },
  {
    Header: "Email",
    accessor: "email",
  },
  {
    Header: "Phone",
    accessor: "phoneNumber",
  },
  {
    Header: "Checkin Date",
    accessor: "checkInDate",
    Cell: ({ cell: { value } }) => prettyDate(value)
  },
  {
    Header: "Expected Checkout Date",
    accessor: "checkOutDate",
    Cell: ({ cell: { value } }) => prettyDate(value)
  }
];

const Shelter = () => {
  const { getAccessTokenSilently } = useAuth0();
  const [openedModal, setOpenedModal] = useState(false);
  const [shelterInfo, setShelterData] = useState({});
  const [metricsInfo, setMetricsData] = useState({});
  const { pathname } = useLocation();
  const navigate = useNavigate();
  const id = pathname.split("/").reverse()[0];

  const shelterFields = useMemo(
    () => [
      { key: "Name", value: shelterInfo.name },
      { key: "Address", value: shelterInfo.address },
      { key: "Google Maps", value: <a className="link" href={shelterInfo.mapsLink}> <GiEarthAfricaEurope /> </a> },
      { key: "Rental Days", value: shelterInfo.maximumDaysForRental + " days" },
      { key: "Capacity", value: `${shelterInfo.numberOfUsers} / ${shelterInfo.capacity}` },
      {
        key: "Tags",
        value:
          <div className="keyword-list">
            { shelterInfo.accessibility && <FaHandsHelping /> }
            { shelterInfo.pets && <FaCat /> }
          </div>
      }
    ],
    [shelterInfo]
  );

  const getShelter = useCallback(async () => {
    const accessToken = await getAccessTokenSilently();
    axiosInstance
      .get(routes.shelters.getShelter(id), {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(({ data }) => setShelterData(data));
  }, [getAccessTokenSilently, id]);

  const getMetricsInfo = useCallback(async () => {
    const accessToken = await getAccessTokenSilently();
    axiosInstance
      .get(routes.metrics.getShelter(id), {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(({ data }) => setMetricsData(data));
  }, [getAccessTokenSilently, id]);

  const handleUpdateShelter = (form) => {
    (async () => {
      const accessToken = await getAccessTokenSilently();
      axiosInstance
        .put(routes.shelters.updateShelter(id), form, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        })
        .then(() => getShelter());
    })();
  };

  const handleDelete = () => {
    (async () => {
      alert("Are you sure you want to delete this shelter?");
      const accessToken = await getAccessTokenSilently();
      axiosInstance
      .delete(routes.shelters.deleteShelter(id), {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(() => navigate("/shelters"));
    })();
  };

  useEffect(() => {
    getShelter();
    getMetricsInfo();
  }, [getShelter, getMetricsInfo]);

  return (
    <PageLayout>
      <ShelterModal
        modalIsOpen={openedModal}
        closeModal={() => {
          setOpenedModal(false);
        }}
        submitForm={handleUpdateShelter}
        shelterData={shelterInfo}
      />
      <div className="row-between">
        <h2>{shelterInfo.name}</h2>
        <div className="row-center">
          <Button onClick={() => setOpenedModal(true)}>
            <MdEdit /> Edit
          </Button>
          <Button
            className="delete-button"
            onClick={() => handleDelete()}
          >
            <MdDelete /> Delete
          </Button>
        </div>
      </div>
      <div className="flex flex-col gap-10">
        <div className="row-between-start">
          <Section title={"Shelter Details"} fields={shelterFields} />
          <div className="flex flex-col gap-5">
            <p className="section-title">Shelter Statistics</p>
            <div className="flex gap-5">
              <div className="statistic-card">
                <div className="card-statistic">
                  <p>{ metricsInfo.totalNumberOfRefugees }</p>
                  <p>{ metricsInfo.totalNumberOfRefugees === 1 ? "refugee" : "refugees" }</p>
                </div>
              </div>
              <div className="statistic-card">
                <div className="card-statistic">
                  <p>{ metricsInfo.totalNumberOfRefugees === 0 ? "-" : `${Math.floor(metricsInfo.averageRefugeeAge)} years` }</p>
                  <p>avg. age of refugees</p>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className="flex flex-col gap-5 w-full p-[1px]">
          <p className="section-title">Sheltered refugees</p>
          { metricsInfo?.refugeeHistory && 
            <Table
              data={metricsInfo.refugeeHistory}
              columns={columns}
              onRowClick={(e, row, i) => navigate(`/users/${row.original.id}`)}
            /> 
          }
        </div>
      </div>
    </PageLayout>
  );
};

export default Shelter;
