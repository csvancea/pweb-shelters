import React from "react";
import Modal from "react-modal";
import { MdOutlineClose } from "react-icons/md";
import Button from "../Button";
import Input from "../Input";
import { useForm } from "react-hook-form";

const UserModal = ({ modalIsOpen, closeModal, userData, submitForm }) => {
  const { register, handleSubmit, getValues } = useForm();
  const action = userData ? "Edit user" : "Add user";

  const handleClick = async () => {
    const data = getValues();
    submitForm({ ...data });
    closeModal();
  };

  return (
    <Modal
      isOpen={modalIsOpen}
      onRequestClose={closeModal}
      contentLabel={action}
      className="modal"
      ariaHideApp={false} // TODO
    >
      <div className="row-between">
        <h2>{action}</h2>
        <Button onClick={closeModal} className="icon-button">
          <MdOutlineClose />
        </Button>
      </div>
      <div className="line" />
      <form>
        <Input label="Name" placeholder="User name" {...register("name")} defaultValue={userData ? userData.name : null} />
        <Input label="Phone" placeholder="User phone" {...register("phoneNumber")} defaultValue={userData ? userData.phoneNumber : null} />
        <Input label="Address" placeholder="User address" {...register("address")} defaultValue={userData ? userData.address : null} />
        <Input label="Birth date" placeholder="Birth date" {...register("birthDate")} type="date" defaultValue={userData ? userData.birthDate?.split('T')[0] : null} />

        <Button type="button" onClick={handleSubmit(handleClick)}>
          Save
        </Button>
      </form>
    </Modal>
  );
};

export default UserModal;
