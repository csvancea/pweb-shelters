import React from "react";
import Modal from "react-modal";
import { MdOutlineClose, MdSubject } from "react-icons/md";
import Button from "../Button";
import Input from "../Input";

const UserModal = ({ modalIsOpen, closeModal, userData }) => {
  const action = userData && "Edit user" || "Add user";
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
        <Input label="Name" placeholder="User name" defaultValue={userData && userData.name || null} />
        <Input label="Email" placeholder="User email" defaultValue={userData && userData.email || null} />
        <Input label="Phone" placeholder="User phone" defaultValue={userData && userData.phone || null} />
        <Input label="Address" placeholder="User address" defaultValue={userData && userData.address || null} />
        <Input label="Birth date" placeholder="Birth date" defaultValue={userData && userData.birthdate || null} />

        <Button type="button" onClick={closeModal}>
          Save
        </Button>
      </form>
    </Modal>
  );
};

export default UserModal;
