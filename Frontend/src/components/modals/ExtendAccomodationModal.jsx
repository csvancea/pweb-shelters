import React from "react";
import Modal from "react-modal";
import { MdOutlineClose } from "react-icons/md";
import Button from "../Button";
import Input from "../Input";

const ExtendAccomodationModal = ({ modalIsOpen, closeModal }) => {
  return (
    <Modal
      isOpen={modalIsOpen}
      onRequestClose={closeModal}
      contentLabel="Extend accomodation"
      className="modal"
    >
      <div className="row-between">
        <h2>Extend</h2>
        <Button onClick={closeModal} className="icon-button">
          <MdOutlineClose />
        </Button>
      </div>
      <div className="line" />
      <form>
        <Input label="Days" placeholder="Number of days of extension" />

        <Button type="button" onClick={closeModal}>
          Extend accomodation
        </Button>
      </form>
    </Modal>
  );
};

export default ExtendAccomodationModal;
