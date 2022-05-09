import React, { useState } from "react";
import Modal from "react-modal";
import { MdOutlineClose, MdSubject } from "react-icons/md";
import Button from "../Button";
import Input from "../Input";
import Keywords from "../Keywords";
import { FaHandsHelping, FaCat } from "react-icons/fa";

const ShelterModal = ({ modalIsOpen, closeModal, shelterData }) => {
  const [keywords, setKeywords] = useState([]);
  const action = shelterData && "Edit shelter" || "Add shelter";
  return (
    <Modal
      isOpen={modalIsOpen}
      onRequestClose={closeModal}
      contentLabel={action}
      className="modal"
    >
      <div className="row-between">
        <h2>{action}</h2>
        <Button onClick={closeModal} className="icon-button">
          <MdOutlineClose />
        </Button>
      </div>
      <div className="line" />
      <form>
        <Input label="Name" placeholder="Shelter name" defaultValue={shelterData && shelterData.name || null} />
        <Input label="Address" placeholder="Shelter address" defaultValue={shelterData && shelterData.address || null} />
        <Input label="Google Maps Link" placeholder="Link" defaultValue={shelterData && shelterData.link || null} />
        {/*<Keywords
          keywords={keywords}
          setKeywords={setKeywords}
          label="Keywords"
          placeholder="Press enter to save keyword"
        />*/}
        <div className="row-between-end gap-2">
          <Input label="Capacity" placeholder="Shelter capacity" defaultValue={shelterData && shelterData.capacity || null} />
          <div className="keyword-list">
            <span key="1"><FaHandsHelping /></span>
            <span key="2"><FaCat /></span>
          </div>
        </div>
        <Button type="button" onClick={closeModal}>
          {action}
        </Button>
      </form>
    </Modal>
  );
};

export default ShelterModal;
