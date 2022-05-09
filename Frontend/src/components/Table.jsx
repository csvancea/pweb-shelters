import React from "react";
import { useTable } from "react-table/dist/react-table.development";

const Table = ({ data, columns, onRowClick, onCellClick }) => {
  // Use the state and functions returned from useTable to build your UI
  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({
      columns,
      data,
    });

  // Render the UI for your table
  return (
    <table {...getTableProps()}>
      <thead>
        {headerGroups.map((headerGroup) => (
          <tr {...headerGroup.getHeaderGroupProps()}>
            {headerGroup.headers.map((column) => (
              <th {...column.getHeaderProps()}>{column.render("Header")}</th>
            ))}
          </tr>
        ))}
      </thead>
      <tbody {...getTableBodyProps()}>
        {rows.map((row, i) => {
          prepareRow(row);
          return (
            <tr
              {...row.getRowProps()}
              className="cursor-pointer hover:bg-gray-50"
              onClick={(e) => onRowClick && onRowClick(e, row, i)}
            >
              {row.cells.map((cell) => {
                return (
                  <td 
                    {...cell.getCellProps()}
                    onClick={(e) => onCellClick && onCellClick(e, row, i, cell)}
                  >
                    {cell.render("Cell")}
                  </td>);
              })}
            </tr>
          );
        })}
      </tbody>
    </table>
  );
};

export default Table;
