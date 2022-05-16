import React, { useState } from "react";
import Modal from "react-modal";
import { MdOutlineClose } from "react-icons/md";
import Button from "../Button";
import Input from "../Input";
import ToggleButton from "../ToggleButton";
import { FaHandsHelping, FaCat } from "react-icons/fa";
import { useForm } from "react-hook-form";

const ShelterModal = ({ modalIsOpen, closeModal, shelterData, submitForm }) => {
  const { register, handleSubmit, getValues } = useForm();
  const [accessibility, setAccessibility] = useState(shelterData && shelterData.accessibility);
  const [pets, setPets] = useState(shelterData && shelterData.pets);
  const action = shelterData && "Edit shelter" || "Add shelter";
  
  const handleClick = async () => {
    const data = getValues();
    submitForm({ ...data, accessibility: accessibility, pets: pets });
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
        <Input label="Name" placeholder="Shelter name" {...register("name")} defaultValue={shelterData && shelterData.name || null} />
        <Input label="Address" placeholder="Shelter address" {...register("address")} defaultValue={shelterData && shelterData.address || null} />
        <Input label="Google Maps Link" placeholder="Link" {...register("mapsLink")} defaultValue={shelterData && shelterData.mapsLink || null} />
        <Input label="Maximum Days For Rental" placeholder="Days" type="number" {...register("maximumDaysForRental")} defaultValue={shelterData && shelterData.maximumDaysForRental || null} />
        <div className="row-between-end gap-2">
          <Input label="Maximum Capacity" placeholder="Shelter capacity" {...register("capacity")} defaultValue={shelterData && shelterData.capacity || null} />
          <div className="keyword-list">
            <ToggleButton defaultChecked={shelterData && shelterData.accessibility || null} icon={<FaHandsHelping />} onChange={ (value) => setAccessibility(value) } />
            <ToggleButton defaultChecked={shelterData && shelterData.pets || null} icon={<FaCat />} onChange={ (value) => setPets(value) } />
          </div>
        </div>
        <Button type="button" onClick={handleSubmit(handleClick)}>
          {action}
        </Button>
      </form>
    </Modal>
  );
};

export default ShelterModal;
