import { useState, useRef, useEffect } from "react";
import jwtDecode from "jwt-decode";
import { DecodedToken, Dictionary } from "./types";
import { User } from "./types/user";

export function verifyTokenExpiry(decodedToken : DecodedToken) {
    // Adding miliseconds to timestamp
    const unix = decodedToken.exp * 1000;
    if (unix > Date.now()) {
        return true;
    }
    return false;
}

export function dateToUTCUnix(date: Date): number {
    return Date.UTC(
        date.getUTCFullYear(),
        date.getUTCMonth(),
        date.getUTCDate(),
        date.getUTCHours(),
        date.getUTCMinutes(),
        date.getUTCSeconds(),
        date.getUTCMilliseconds()
    );
}

export function decodeJwtToken(token : string) : DecodedToken | null {
    if (!token) return null;
    const decodedToken: DecodedToken = jwtDecode(token);
    if (verifyTokenExpiry(decodedToken)) {
        return decodedToken;
    }
    return null;
}

export function parseDateTime(value : string) : string | null {
    if (typeof value === "string") return null;
    const date : Date = new Date(value);
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();
    const hour = date.getHours();
    const minute = date.getMinutes();
    return `${day < 10 ? `0${day}` : day}-${
        month < 10 ? `0${month}` : month
    }-${year} ${hour < 10 ? `0${hour}` : hour}:${
        minute < 10 ? `0${minute}` : minute
    }`;
}

export function getUserMapping(users : User[]) : Dictionary<string> {
    const userMap : Dictionary<string> = {};
    users.reduce((total, user) => {
        total[user.userId] = user.name;
        return total;
    }, userMap);
    return userMap;
}

export function useField(initial: any, initialVal: any) : any {
    const initialField = { ...initial, value: initialVal };
    const [field, _setField] = useState(initialField);

    const setField = (value: any, props: any) => {
        const temp = field;
        temp.value = value;
        if (temp.onChange && typeof temp.onChange === "function") {
            temp.onChange(value, props);
        }
        _setField(temp);
    };
    return { ...field, setField };
}

export function useObject(initialFields: any, initialValues: any): any {
    // if error fields are missing add them
    function getInitialValues() {
        return Object.keys(initialFields).reduce((total: any, field) => {
            const currentField = {
                ...initialFields[field],
                value: initialValues[field],
            };
            if (currentField.validation) {
                currentField.validation.errors = [];
            } else currentField.validation = { errors: [] };
            total[field] = currentField;
            return total;
        }, {});
    }
    function getInitialFields() {
        return Object.keys(initialFields).reduce((total: any, field) => {
            const currentField = { ...initialFields[field] };
            if (currentField.validation) {
                currentField.validation.errors = [];
            } else currentField.validation = { errors: [] };
            total[field] = currentField;
            return total;
        }, {});
    }

    const allInitialValuesRef = useRef(getInitialValues());

    useEffect(() => {
        allInitialValuesRef.current = getInitialValues();
    }, [initialValues]);

    const allInitialValues = allInitialValuesRef.current;

    const [values, setValues] = useState(allInitialValues);

    const [fields, setFields] = useState(getInitialFields());

    const object = {
        data: Object.keys(fields).reduce((total: any, field) => {
            const currentField = fields[field];
            currentField.value = values[field].value;
            total[field] = {
                ...currentField,
                onChange: (value: any) => {
                    if (currentField.onChange) {
                        currentField.onChange(value, object.data);
                    }
                    currentField.validation.errors = [];
                    return setValues((oldValues: any) => {
                        return {
                            ...oldValues,
                            [field]: {
                                ...oldValues[field],
                                value: value,
                            },
                        };
                    });
                },
            };
            return total;
        }, {}),
        validate() {
            let isValid = true;
            Object.keys(fields).forEach((fieldKey) => {
                const currentField = fields[fieldKey];
                if (currentField.validation && currentField.validation.rules) {
                    currentField.validation.errors = currentField.validation.rules.reduce(
                    (errors: any, rule: any, index: any) => {
                    if (!rule.validate(currentField.value, fields)) {
                        isValid = false;
                        errors.push({
                            id: `${fieldKey}-validation-fail-${index}`,
                            message: rule.message,
                        });
                    }
                        return errors;
                    }, []);
                }
            });
            if (!isValid) setValues(fields);
                return isValid;
        },
        reset() {
            setValues(() => getInitialValues());
            setFields(initialFields);
        },
    };
    return object;
}
