import React, { useState, useMemo } from "react";
import Button from "../../components/Button";
import AdminLayout from "../../utils/AdminLayout";
import { MdEdit, MdDelete } from "react-icons/md";
import ShelterModal from "../../components/modals/ShelterModal";
import Table from "../../components/Table";
import Section from "../../components/Section";
import { GiEarthAfricaEurope } from "react-icons/gi";
import { FaHandsHelping, FaCat } from "react-icons/fa";

const Shelter = () => {
  const [openedModal, setOpenedModal] = useState(false);

  const shelterData = {
    name: "Siret #1",
    address: "Botosani",
    link: "https://google.com",
    capacity: "25",
    tags: ["disability", "pet"]
  };

  const shelterFields = [
    { key: "Name", value: shelterData.name },
    { key: "Address", value: shelterData.address },
    { key: "Google Maps", value: <a className="link" href={shelterData.link}> <GiEarthAfricaEurope /> </a> },
    { key: "Capacity", value: shelterData.capacity },
    { key: "Tags", value: 
      <div className="keyword-list">
        { shelterData.tags.includes("disability") && <FaHandsHelping /> }
        { shelterData.tags.includes("pet") && <FaCat /> } 
      </div> },
  ];

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
      accessor: "phone",
    },
    {
      Header: "Checkout Date",
      accessor: "checkoutDate",
    },
  ];

  const data = useMemo(
    () => Array(5).fill(
        {
          name: "Alexandr Lenko",
          email: "alexandr.lenko@mail.ua",
          phone: "+40 0712 345 678",
          checkoutDate: "12 March 2022",
        }
      ),
    []
  );

  return (
    <AdminLayout>
      <ShelterModal
        modalIsOpen={openedModal}
        closeModal={() => {
          setOpenedModal(false);
        }}
        shelterData={shelterData}
      />
      <div className="row-between">
        <h2>Siret #1</h2>
        <div className="row-center">
          <Button onClick={() => setOpenedModal(true)}>
            <MdEdit /> Edit
          </Button>
          <Button
            className="delete-button"
            onClick={() => alert("Are you sure?")}
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
                  <p>20</p>
                  <p>refugees</p>
                </div>
              </div>
              <div className="statistic-card">
                <div className="card-statistic">
                  <p>45 years</p>
                  <p>avg. age of refugees</p>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className="flex flex-col gap-5 w-full p-[1px]">
          <p className="section-title">Last 5 refugees</p>
          <Table data={data} columns={columns} />
        </div>
      </div>
    </AdminLayout>
  );
};

export default Shelter;
