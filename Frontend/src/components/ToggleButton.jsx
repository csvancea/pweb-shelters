import React, { useState, useEffect } from "react";

const ToggleButton = (props) => {
    const [toggle, setToggle] = useState(false);
    const { defaultChecked, onChange, disabled, icon, ...rest } = props;

    useEffect(() => {
        if (defaultChecked) {
            setToggle(defaultChecked)
        }
    }, [defaultChecked]);

    const triggerToggle = () => {
        if ( disabled ) {
            return;
        }

        setToggle(!toggle);

        if ( typeof onChange === 'function' ) {
            onChange(!toggle);
        }
    }
    return (
            toggle ?
                <span onClick={triggerToggle} className="keyword-list-active" {...rest}>{ icon }</span> : 
                <span onClick={triggerToggle} className="keyword-list-inactive" {...rest}>{ icon }</span>
            );
}

export default ToggleButton;