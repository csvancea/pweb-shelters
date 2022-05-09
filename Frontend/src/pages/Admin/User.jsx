import React, { useState, useMemo } from "react";
import Button from "../../components/Button";
import AdminLayout from "../../utils/AdminLayout";
import { MdEdit, MdDelete } from "react-icons/md";
import { ImExit } from "react-icons/im";
import { BsPlusLg } from "react-icons/bs"
import UserModal from "../../components/modals/UserModal";
import ExtendAccomodationModal from "../../components/modals/ExtendAccomodationModal";
import Table from "../../components/Table";
import Section from "../../components/Section";

const User = () => {
  const [openedModal, setOpenedModal] = useState(false);
  const [openedExtensionModal, setOpenedExtensionModal] = useState(false);

  const userData = {
    name: "Alexandr Lenko",
    email: "alexandr.lenko@mail.ua",
    phone: "+40 0712 345 678",
    address: "Donetsk",
    birthdate: "12 March 2022"
  };

  const userFields = [
    { key: "Name", value: userData.name },
    { key: "Email", value: userData.email },
    { key: "Phone", value: userData.phone },
    { key: "Address", value: userData.address },
    { key: "Birth date", value: userData.birthdate }
  ];

  const columns = [
    {
      Header: "Shelter",
      accessor: "shelter",
    },
    {
      Header: "Check-in",
      accessor: "checkinDate",
    },
    {
      Header: "Check-out",
      accessor: "checkoutDate",
    },
    {
      Header: "Duration",
      accessor: "duration",
    },
  ];

  const data = useMemo(
    () => Array(5).fill(
        {
          shelter: "Siret #1",
          checkinDate: "01 March 2022",
          checkoutDate: "15 March 2022",
          duration: "2 weeks",
        }
      ),
    []
  );

  return (
    <AdminLayout>
      <UserModal
        modalIsOpen={openedModal}
        closeModal={() => {
          setOpenedModal(false);
        }}
        userData={userData}
      />
      <ExtendAccomodationModal
        modalIsOpen={openedExtensionModal}
        closeModal={() => {
          setOpenedExtensionModal(false);
        }}
      />

      <div className="row-between">
        <h2>{userData.name}</h2>
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
          <Section title={"Profile Details"} fields={userFields} />
          <div className="flex flex-col gap-5">
            <p className="section-title">Current accomodation</p>
            <div className="flex gap-5">
              <div className="statistic-card">
                <div className="card-statistic">
                  <p>Siret #1</p>
                  <p>shelter</p>
                </div>
              </div>
              <div className="statistic-card">
                <div className="card-statistic">
                  <p>30 days</p>
                  <p>remaining days</p>
                </div>
              </div>
            </div>
            <Button
              className="button"
              style={{width: "auto"}}
              onClick={() => setOpenedExtensionModal(true)}
            >
              <BsPlusLg /> Extend accomodation
            </Button>
            <Button
              className="delete-button"
              style={{width: "auto"}}
              onClick={() => alert("Are you sure?")}
            >
              <ImExit /> Early checkout
            </Button>
          </div>
        </div>
        <div className="flex flex-col gap-5 w-full p-[1px]">
          <p className="section-title">Accomodation history</p>
          <Table data={data} columns={columns} />
        </div>
      </div>
    </AdminLayout>
  );
};

export default User;
