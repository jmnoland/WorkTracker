import { useState, useRef, useEffect } from "react";
import jwtDecode from "jwt-decode";
import { DecodedToken, Dictionary, User, Error, FormField, ValidationRule, Form } from "./types";

export function verifyTokenExpiry(decodedToken : DecodedToken): boolean {
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
    if (typeof value !== "string") return null;
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

export function getUserMapping(users?: User[]) : Dictionary<string> {
    const userMap : Dictionary<string> = {};
    if (!users) return userMap;
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

export function useForm(initialFields: Dictionary<any>, initialValues: Dictionary<any>): Form {
    const objectKeys = Object.keys(initialValues);
    const [values, setValues] = useState(initialValues);
    const [modified, setModified] = useState([] as string[]);
    const [errors, setErrors] = useState<Dictionary<Error[]>>(objectKeys.reduce(
        (total: Dictionary<Error[]>, val: string) => {
            total[val] = [];
            return total;
        },
    {}));

    function onChange(value: any, name: string) {
        setValues({ ...values, [name]: value });
        if (!modified.includes(name)) setModified([ ...modified, name ]);
    }

    function checkRules(rules: ValidationRule<any>[], name: string): boolean {
        let allValid = true;
        const temp: Error[] = [];
        if (initialFields[name].required) {
            if (values[name] === undefined || values[name] === null || values[name] === "") {
                temp.push({ id: "R", message: initialFields[name].required });
                allValid = false;
            }
        }
        let count = 0;
        rules.forEach(rule => {
            if (!rule.validate(values[name])) {
                temp.push({ id: `V${count}`, message: rule.message });
                allValid = false;
            };
            count ++;
        });
        if (!allValid) setErrors({ ...errors, [name]: temp });
        return allValid;
    }

    function validate(): boolean {
        let valid = true;
        modified.forEach((name) => {
            const rules = initialFields[name].rules;
            if (rules) {
                if (!checkRules(rules, name)) valid = false;
            }
        });
        return valid;
    }

    function reset() {
        setValues(initialValues);
        setModified([] as string[]);
        setErrors(initialValues.map((val: any) => ({ [val]: [] })));
    }

    return {
        form: objectKeys.reduce((total: Dictionary<any>, name: string) => {
            total[name] = {
                ...initialFields[name],
                errors: errors[name],
                value: values[name],
                onChange: (val: any) => onChange(val, name),
            }
            return total;
        }, {} as Dictionary<FormField<any>>),
        modified: modified,
        validate: validate,
        reset: reset,
    };
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
