import React, {forwardRef} from "react";

const Input = forwardRef((props, ref) => {
  const { label, icon, className, ...rest } = props;
  return (
    <label>
      {label && <span>{label}</span>}
      {icon}
      <input className={`input ${className}`} {...rest} ref={ref} />
    </label>
  );
});

export default Input;
