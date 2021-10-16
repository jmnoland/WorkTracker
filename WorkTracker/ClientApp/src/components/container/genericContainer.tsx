import React from 'react';

interface ElementProps {
    children?: React.ReactNode,
    key?: string | number,
    ref?: React.RefObject<HTMLDivElement> | null,
}

export default function GenericContainer(classes?: string | null): ({children, key, ref}: ElementProps) => JSX.Element {
    const tempClasses = classes ?? undefined;
    const temp = ({
        children,
        key,
        ref,
    } : ElementProps) =>
    (<div key={key} ref={ref} className={tempClasses}>{children}</div>);
    temp.displayName = "GenericContainer";
    return temp;
}
