import React from 'react';

type DivProps = React.HTMLProps<HTMLDivElement>

function CreateGenericContainer(classes?: string | null) {
    const GenericContainer = React.forwardRef<HTMLDivElement, DivProps>((props, ref?) => {
        return (<div className={classes ?? undefined} {...props} ref={ref}>{props.children}</div>)
    });
    GenericContainer.displayName = `Container${classes}`;
    return GenericContainer;
}

export default CreateGenericContainer;