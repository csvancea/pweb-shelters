import React from "react";
import Modal from "react-modal";
import { MdOutlineClose } from "react-icons/md";
import Button from "../Button";
import Input from "../Input";
import { useForm } from "react-hook-form";

const RentModal = ({ modalIsOpen, closeModal, submitForm, extend }) => {
  const { register, handleSubmit, getValues } = useForm();
  const action = extend ? "Extend" : "Check in";
  const placeholder = extend ? "of accommodation" : "";

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
        <Input label="Days" type="number" min="1" placeholder={`Number of days ${placeholder}`} {...register("RentalDays")} />

        <Button type="button" onClick={handleSubmit(handleClick)}>
          {action}
        </Button>
      </form>
    </Modal>
  );
};

export default RentModal;
