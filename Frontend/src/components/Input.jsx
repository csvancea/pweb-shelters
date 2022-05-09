import React from "react";

const Input = (props) => {
  const { label, icon, className, ...rest } = props;
  return (
    <label>
      {label && <span>{label}</span>}
      {icon}
      <input {...rest} className={`input ${className}`} />
    </label>
  );
};

export default Input;
