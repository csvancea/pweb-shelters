import React, { useMemo } from "react";
import { Link } from "react-router-dom";
import Button from "../components/Button";
import Table from "../components/Table";
import Input from "../components/Input";
import PageLayout from "../utils/PageLayout";
import { MdSearch } from "react-icons/md";
import { useNavigate } from "react-router-dom";

const Users = () => {
  const columns = useMemo(
    () => [
      {
        Header: "Name",
        accessor: "name",
      },
      {
        Header: "Current shelter",
        accessor: "shelter",
        Cell: ({ cell: { value } }) => ( <Link to={`/shelters/${value.id}`}>{value.name}</Link>)
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
        Header: "Checkout date",
        accessor: "checkout_date",
      },
    ],
    []
  );
  const data = useMemo(
    () => Array(1).fill(
      {
        name: "Alexandr Lenko",
        shelter: {
          name: "Siret #1",
          id: "0"
        },
        email: "alexandr.lenko@mail.ua",
        phone: "+40 0712 345 678",
        checkout_date: "12 March 2022",
      }),
    []
  );

  const navigate = useNavigate();
  return (
    <PageLayout>
      <div className="row-between">
        <h2>{data.length} Users</h2>
        <Input style={{width: "30rem"}} placeholder="Filter" />
        <div className="row-center">
          <Button>
              <MdSearch /> Search
          </Button>
        </div>
      </div>

      <Table
        data={data}
        columns={columns}
        onCellClick={(e, row, i, cell) => cell.column.id !== "shelter" && navigate(`/users/${i}`, { queryParams: { id: i } })}
      />
    </PageLayout>
  );
};

export default Users;
