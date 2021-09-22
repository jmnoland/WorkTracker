import { useState } from "react";
import jwtDecode from "jwt-decode";
import { DecodedToken, Dictionary, User, Error, FormField, InitialFormField, ValidationRule, Form } from "./types";

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

function getInitialErrors(objectKeys: string[]) {
    const temp = objectKeys.reduce(
        (total: Dictionary<Error[]>, val: string) => {
            total[val] = [];
            return total;
        },
    {});
    return temp;
}

function getInitialModified(objectKeys: string[], fields: Dictionary<InitialFormField<unknown>>): string[] {
    const temp: string[] = [];
    objectKeys.forEach((name) => {
        if (fields[name] !== undefined) {
            if (fields[name].required !== undefined) temp.push(name);
        }
    });
    return temp;
}

export function useForm(initialFields: unknown, initialValues: unknown): Form {
    const fields = initialFields as Dictionary<InitialFormField<unknown>>;
    const objectKeys = Object.keys(initialValues as Dictionary<unknown>);
    const [values, setValues] = useState(initialValues as Dictionary<unknown>);
    const [modified, setModified] = useState(getInitialModified(objectKeys, fields));
    const [errors, setErrors] = useState<Dictionary<Error[]>>(getInitialErrors(objectKeys));

    function onChange(value: unknown, name: string) {
        setValues({ ...values, [name]: value });
        if (!modified.includes(name)) setModified([ ...modified, name ]);
    }

    function checkRules(rules: ValidationRule<unknown>[], name: string): Error[] {
        const temp: Error[] = [];
        if (fields[name].required !== undefined) {
            if (values[name] === undefined || values[name] === null || values[name] === "") {
                const reqMessage: string = fields[name].required ?? "";
                temp.push({ id: "R", message: reqMessage });
            }
        }
        let count = 0;
        rules.forEach(rule => {
            if (!rule.validate(values[name])) {
                temp.push({ id: `V${count}`, message: rule.message });
            };
            count ++;
        });
        return temp;
    }

    function validate(): boolean {
        let valid = true;
        const temp: Dictionary<Error[]> = {};
        modified.forEach((name) => {
            const rules = fields[name].rules;
            if (rules) {
                const result = checkRules(rules, name);
                if (result.length !== 0) {
                    temp[name] = result;
                    valid = false;
                }
            }
        });
        if (!valid) setErrors({ ...errors, ...temp });
        return valid;
    }

    function reset() {
        setValues(initialValues as Dictionary<unknown>);
        setModified(getInitialModified(objectKeys, fields));
        setErrors(getInitialErrors(objectKeys));
    }

    return {
        form: objectKeys.reduce((total: Dictionary<FormField<unknown>>, name: string) => {
            total[name] = {
                ...fields[name],
                errors: errors[name],
                value: values[name],
                onChange: (val: unknown) => onChange(val, name),
            }
            return total;
        }, {} as Dictionary<FormField<unknown>>),
        modified: modified,
        validate: validate,
        reset: reset,
    };
}
