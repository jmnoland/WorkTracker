import React from 'react';

type DivProps = React.HTMLProps<HTMLDivElement>

// eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
function CreateGenericContainer(classes?: string | null) {
    const GenericContainer = React.forwardRef<HTMLDivElement, DivProps>((props, ref?) => {
        return (<div className={classes ?? undefined} {...props} ref={ref}>{props.children}</div>)
    });
    GenericContainer.displayName = `Container${classes}`;
    return GenericContainer;
}

export default CreateGenericContainer;