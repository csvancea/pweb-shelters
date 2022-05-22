import React, { useState, useEffect, useCallback } from "react";
import { Link } from "react-router-dom";
import Button from "../components/Button";
import Table from "../components/Table";
import Input from "../components/Input";
import PageLayout from "../utils/PageLayout";
import { MdSearch } from "react-icons/md";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import axiosInstance from "../configs/Axios";
import { prettyDate } from "../utils/Functions";

const columns = [
  {
    Header: "Name",
    accessor: "profile.name",
  },
  {
    Header: "Email",
    accessor: "profile.email",
  },
  {
    Header: "Phone",
    accessor: "profile.phoneNumber",
  },
  {
    Header: "Current Shelter",
    accessor: "currentShelter.shelterName",
    Cell: ({ cell: { value, row } }) => ( 
      row.original.currentShelter
        ? <Link to={`/shelters/${row.original.currentShelter.shelterId}`}>{row.original.currentShelter.shelterName}</Link>
        : "-"
    )
  },
  {
    Header: "Expected Checkout",
    accessor: "currentShelter.expectedCheckOutDate",
    Cell: ({ cell: { value, row } }) => ( 
      row.original.currentShelter
        ? prettyDate(row.original.currentShelter.expectedCheckOutDate)
        : "-"
    )
  },
];

const Users = () => {
  const navigate = useNavigate();
  const [users, setUsers] = useState([]);
  const [filteredUsers, setFilteredUsers] = useState([]);
  const [searchFilter, setSearchFilter] = useState("");
  const { getAccessTokenSilently } = useAuth0();

  const getAllUsers = useCallback(async () => {
    const accessToken = await getAccessTokenSilently();
    axiosInstance
      .get(routes.profiles.getAll, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(({ data }) => setUsers(data));
  }, [getAccessTokenSilently]);

  useEffect(() => {
    getAllUsers();
  }, [getAllUsers]);

  useEffect(() => {
    if (searchFilter) {
      setFilteredUsers(
        users.filter(e => e.profile.name.toLowerCase().includes(searchFilter) || e.profile.email.toLowerCase().includes(searchFilter) || e.profile.phoneNumber.toLowerCase().includes(searchFilter))
      );
    } else {
      setFilteredUsers(users);
    }
  }, [searchFilter, users]);

  return (
    <PageLayout>
      <div className="row-between">
        <h2>{filteredUsers.length} {filteredUsers.length > 1 ? "Users" : "User"}</h2>
        <Input
          className="search-filter"
          style={{width: "30rem"}}
          placeholder="Filter"
          onChange={(e) => setSearchFilter(e.target.value?.toLowerCase())}
        />
        <div className="row-center">
          <Button
          className="search-button">
              <MdSearch /> Search
          </Button>
        </div>
      </div>

      <Table
        data={filteredUsers}
        columns={columns}
        onCellClick={(e, row, i, cell) => cell.column.id !== "currentShelter.shelterName" && navigate(`/users/${row.original.profile.id}`)}
      />
    </PageLayout>
  );
};

export default Users;
