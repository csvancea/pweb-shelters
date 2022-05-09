import React, { useMemo, useState } from "react";
import Button from "../components/Button";
import Table from "../components/Table";
import Input from "../components/Input";
import PageLayout from "../utils/PageLayout";
import { MdAdd, MdSearch } from "react-icons/md";
import { FaHandsHelping, FaCat } from "react-icons/fa";
import { GiEarthAfricaEurope } from "react-icons/gi";
import ShelterModal from "../components/modals/ShelterModal";
import AdminOnly from "../utils/AdminOnly";

const Shelters = () => {
  const columns = useMemo(
    () => [
      {
        Header: "Name",
        accessor: "name",
      },
      {
        Header: "Address",
        accessor: "address",
        Cell: ({ cell: { value } }) => (
          <div className="keyword-list">
            <GiEarthAfricaEurope />
            <a className="link" href={value.gmaps}>
              { value.name }
            </a>
          </div>
        )
      },
      {
        Header: "Capacity",
        accessor: "capacity",
      },
      {
        Header: "Tags",
        accessor: "tags",
        Cell: ({ cell: { value } }) => (
          <div className="keyword-list">
            { value.includes("disability") && <FaHandsHelping /> }
            { value.includes("pet") && <FaCat /> }
          </div>
        )
      },
      {
        Header: "Added on",
        accessor: "added_on",
      },
    ],
    []
  );
  const data = useMemo(
    () => Array(1).fill(
      {
        name: "Siret #1",
        address: {
          name: "Siret Customs, Botosani",
          gmaps: "https://google.com/"
        },
        capacity: "5/20",
        tags: ["disability", "pet"],
        added_on: "12 March 2022",
      }),
    []
  );

  const [openedModal, setOpenedModal] = useState(false);
  return (
    <PageLayout>
      <ShelterModal
        modalIsOpen={openedModal}
        closeModal={() => {
          setOpenedModal(false);
        }}
      />

      <div className="row-between">
        <h2>{data.length} Shelters</h2>
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

      <Table data={data} columns={columns} rowRedirect="shelters" />
    </PageLayout>
  );
};

export default Shelters;
